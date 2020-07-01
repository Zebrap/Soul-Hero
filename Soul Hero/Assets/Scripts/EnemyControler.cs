using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnamyState
{
    CHASE,
    ATTAK
}

public class EnemyControler : MoveControl
{
    private EnamyState enamy_State;
    private NavMeshObstacle obstacle;

    void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
        obstacle = GetComponent<NavMeshObstacle>();
    }

    void Start()
    {
        enamy_State = EnamyState.CHASE;

        attack_Timer = wait_Beffore_Attack_Time;
    }

    void Update()
    {
        if (target.GetComponent<HealthScript>().isDead)
        {
            characterAnimations.Walk(false);
            return;
        }

        if(enamy_State == EnamyState.CHASE )
        {
            ChasePlayer();
        }

        if (enamy_State == EnamyState.ATTAK)
        {
            FreezOnAttack();
            AttackSingleTarget();
        }
    }

    private void FreezOnAttack()
    {
        navAgent.enabled = false;
    }


    void ChasePlayer()
    {
        navAgent.avoidancePriority = (int)Vector3.Distance(transform.position, target.transform.position);
        
        navAgent.SetDestination(target.transform.position);
        for (int i = 0; i < navAgent.path.corners.Length - 1; i++)
            Debug.DrawLine(navAgent.path.corners[i], navAgent.path.corners[i + 1], Color.red);
        if (navAgent.velocity.sqrMagnitude == 0)
        {
            characterAnimations.Walk(false);
        }
        else
        {
            characterAnimations.Walk(true);
        }

        if(Vector3.Distance(transform.position, target.transform.position)<= attack_Distance)
        {
            enamy_State = EnamyState.ATTAK;
            obstacle.enabled = true;
        }
        else
        {
            obstacle.enabled = false;
        }
    }

    protected override void SetState()
    {
        enamy_State = EnamyState.CHASE;
        navAgent.enabled = true;
    }
}
