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

    private Rigidbody rigidbody;

    void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        navAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
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
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(target.transform.position);
        if(navAgent.velocity.sqrMagnitude == 0)
        {
            characterAnimations.Walk(false);
        }
        else
        {
            characterAnimations.Walk(true);
        }

        if(Vector3.Distance(transform.position, base.target.transform.position)<= attack_Distance)
        {
            enamy_State = EnamyState.ATTAK;
        }
    }

    protected override void SetState()
    {
        enamy_State = EnamyState.CHASE;
    }
}
