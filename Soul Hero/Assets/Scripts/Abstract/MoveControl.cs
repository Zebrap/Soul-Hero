using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum CharacterState
{
    CHASE,
    ATTAK
}

abstract public class MoveControl : MonoBehaviour
{
    public float roationDegreesPerSecond = 180f;
    protected GameObject target;
    protected NavMeshAgent navAgent;
    protected CharacterAnimations characterAnimations;

    public float attack_Distance = 1f;
    public float attack_Distance_OffSet = 1f;

    [SerializeField]
    protected float wait_Beffore_Attack_Time;
    protected float attack_Timer;

    public int attack_damage = 10;
    public int bonus_attack_damage = 0;

    [HideInInspector]
    public bool canMove;
    private Transform attackedEnemy;
    private bool dealAttackDamage;

    protected void AttackSingleTarget()
    {
        /*     if (Vector3.Distance(transform.position, target.transform.position) >=
                                 attack_Distance + attack_Distance_OffSet)
             {
                 SetState();
                 if (navAgent.enabled)
                 {
                     navAgent.isStopped = false;
                 }
             }
             else
             {
                 RotateToTarget();
                 if (navAgent.enabled)
                 {
                     navAgent.velocity = Vector3.zero;
                     navAgent.isStopped = true;
                 }
                 characterAnimations.Walk(false);
             }*/
        RotateToTarget();
        if (navAgent.enabled)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
        }
        characterAnimations.Walk(false);

        attack_Timer += Time.deltaTime;
        if (attack_Timer > wait_Beffore_Attack_Time)
        {
            attack_Timer = 0f;
            if (Vector3.Distance(transform.position, target.transform.position) >=
                            attack_Distance + attack_Distance_OffSet)           //check distanace if want to attack
            {
                SetState();
                if (navAgent.enabled)
                {
                    navAgent.isStopped = false;
                }
                return;
            }
            dealAttackDamage = true;
            attackedEnemy = target.transform;
            characterAnimations.Attack3(wait_Beffore_Attack_Time);
        }
    }

    public void dealDamage()
    {
        if (dealAttackDamage)
        {
            dealAttackDamage = false;
            attackedEnemy.GetComponent<HealthScript>().ApplyDamage(attack_damage + bonus_attack_damage);
        }
    }

    protected abstract void SetState();

    protected void RotateToTarget()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float singleStep = roationDegreesPerSecond * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void NavMeshAgent_is_Stop(bool stop)
    {
        if(stop == true)
        {
            navAgent.velocity = Vector3.zero;
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        navAgent.isStopped = stop;
    }

    public void Set_wait_Beffore_Attack_Time(float value)
    {
        if (wait_Beffore_Attack_Time == value) return;
        wait_Beffore_Attack_Time = value;
        characterAnimations.SetAttackSpeed(wait_Beffore_Attack_Time);
    }
}
