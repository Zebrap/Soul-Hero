﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    protected float wait_Beffore_Attack_Time = 1f;
    protected float attack_Timer;

    public int attack_damage = 10;

    protected void AttackSingleTarget()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >=
   attack_Distance + attack_Distance_OffSet)
        {
            navAgent.isStopped = false;
            SetState();
       //     playerState = PlayerState.IDLE;
        }
        else
        {
            RotateToTarget();
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            characterAnimations.Walk(false);
        }

        attack_Timer += Time.deltaTime;
        if (attack_Timer > wait_Beffore_Attack_Time)
        {
            target.GetComponent<HealthScript>().ApplyDamage(attack_damage);
            characterAnimations.Attack3();
            attack_Timer = 0f;
        }
    }
    
    protected virtual void SetState()
    {

    }

    protected void RotateToTarget()
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        float singleStep = roationDegreesPerSecond * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    
}