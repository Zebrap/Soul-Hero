using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    IDLE,
    ATTAK
}

public class PlayerMovement : MoveControl
{
    private bool moving;

    public ParticleSystem particleClick;
    public Color particleEnemyColor;
    private Color particleMoveColor;
    private ParticleSystem.ColorOverLifetimeModule particleColor;
    private float offSetParticle = 0.1f;

    private PlayerState playerState;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterAnimations = GetComponent<CharacterAnimations>();
        particleClick.Stop();
        particleMoveColor = particleClick.main.startColor.color;
        particleColor = particleClick.colorOverLifetime;
        playerState = PlayerState.IDLE;
        attack_Timer = wait_Beffore_Attack_Time;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickMove();
        }

        if (navAgent.velocity.magnitude > 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            characterAnimations.Walk(true);
        }
        else
        {
            characterAnimations.Walk(false);
        }
        if(playerState == PlayerState.ATTAK && !target.GetComponent<HealthScript>().isDead)
        {
            navAgent.destination = target.transform.position;
            if (Vector3.Distance(transform.position, target.transform.position) <= attack_Distance)
            {
                AttackSingleTarget();
            }
        }
    }


    private void ClickMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Quaternion rot = Quaternion.Euler(90, 0, 0);
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                particleColor.color = particleEnemyColor;
                target = hit.transform.gameObject;
                particleClick.transform.SetParent(target.transform);
                particleClick.transform.localPosition = new Vector3(0,offSetParticle,0);
                //   particleClick.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + offSetParticle, enemy.transform.position.z); // enemy position
                playerState = PlayerState.ATTAK;
            }
            else
            {
                particleColor.color = particleMoveColor;
                particleClick.transform.SetParent(null);
                particleClick.transform.position = new Vector3(hit.point.x, hit.point.y + offSetParticle, hit.point.z);
                playerState = PlayerState.IDLE;
                navAgent.isStopped = false;
            }
            particleClick.transform.rotation = rot;
            particleClick.Play();

            navAgent.destination = hit.point;
        }
    }

    protected override void SetState()
    {
        playerState = PlayerState.IDLE;
    }
}
