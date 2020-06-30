using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    IDLE,
    ATTAK
}

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterAnimations characterAnimations;
    private bool moving;

    public ParticleSystem particleClick;
    public Color particleEnemyColor;
    private Color particleMoveColor;
    private ParticleSystem.ColorOverLifetimeModule particleColor;
    private float offSetParticle = 0.1f;

    public float attack_Distance = 1f;
    public float attack_Distance_OffSet = 1f;
    private Vector3 enemyPosition;
    private PlayerState playerState;

    private float wait_Beffore_Attack_Time = 1f;
    private float attack_Timer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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

        if (agent.velocity.magnitude > 0)
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

        if (playerState == PlayerState.ATTAK && Vector3.Distance(transform.position, enemyPosition) <= attack_Distance)
        {
            AttackSingleEnemy();
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
                enemyPosition = hit.transform.gameObject.transform.position;
                particleClick.transform.position = new Vector3(enemyPosition.x, enemyPosition.y + offSetParticle, enemyPosition.z); // enemy position
                playerState = PlayerState.ATTAK;
            }
            else
            {
                particleColor.color = particleMoveColor;
                particleClick.transform.position = new Vector3(hit.point.x, hit.point.y + offSetParticle, hit.point.z);
                playerState = PlayerState.IDLE;
                agent.isStopped = false;
            }
            particleClick.transform.rotation = rot;
            particleClick.Play();

            agent.destination = hit.point;
        }
    }

    private void AttackSingleEnemy()
    {

        agent.velocity = Vector3.zero;
        agent.isStopped = true;

        characterAnimations.Walk(false);
        attack_Timer += Time.deltaTime;
        if (attack_Timer > wait_Beffore_Attack_Time)
        {
            characterAnimations.Attack3();
            attack_Timer = 0f;
        }

        if (Vector3.Distance(transform.position, enemyPosition) >=
           attack_Distance + attack_Distance_OffSet)
        {
            agent.isStopped = false;
        }
    }
}
