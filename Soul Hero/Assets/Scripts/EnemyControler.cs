using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnamyState
{
    CHASE,
    ATTAK
}

public class EnemyControler : MonoBehaviour
{
    private CharacterAnimations enemy_animation;
    private NavMeshAgent navAgent;

    private Transform playerTarget;

    public float move_Speed = 3.5f;
    public float attack_Distance = 1f;
    public float chase_Player_After_Attack_Distance = 1f;

    private float wait_Beffore_Attack_Time = 1f;
    private float attack_Timer;


    private EnamyState enamy_State;

    void Awake()
    {
        enemy_animation = GetComponent<CharacterAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start()
    {
        enamy_State = EnamyState.CHASE;

        attack_Timer = wait_Beffore_Attack_Time;
    }

    void Update()
    {
        if(enamy_State == EnamyState.CHASE)
        {
            ChasePlayer();
        }

        if (enamy_State == EnamyState.ATTAK)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = move_Speed;
        if(navAgent.velocity.sqrMagnitude == 0)
        {
            enemy_animation.Walk(false);
        }
        else
        {
            enemy_animation.Walk(true);
        }

        if(Vector3.Distance(transform.position, playerTarget.position)<= attack_Distance)
        {
            enamy_State = EnamyState.ATTAK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        enemy_animation.Walk(false);

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Beffore_Attack_Time)
        {
            enemy_animation.Attack3();
            attack_Timer = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) >=
            attack_Distance + chase_Player_After_Attack_Distance)
        {
            navAgent.isStopped = false;
            enamy_State = EnamyState.CHASE;
        }
    }

}
