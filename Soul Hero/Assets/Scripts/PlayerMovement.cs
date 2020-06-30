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
    private GameObject enemy;
    private PlayerState playerState;

    private float wait_Beffore_Attack_Time = 1f;
    private float attack_Timer;

    public float attack_damage = 10f;

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
        if(playerState == PlayerState.ATTAK && !enemy.GetComponent<HealthScript>().isDead)
        {
            agent.destination = enemy.transform.position;
            if (Vector3.Distance(transform.position, enemy.transform.position) <= attack_Distance)
            {
                AttackSingleEnemy();
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
                enemy = hit.transform.gameObject;
                particleClick.transform.SetParent(enemy.transform);
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
            enemy.GetComponent<HealthScript>().ApplyDamage(attack_damage);
            characterAnimations.Attack3();
            attack_Timer = 0f;
        }

        if (Vector3.Distance(transform.position, enemy.transform.position) >=
           attack_Distance + attack_Distance_OffSet)
        {
            agent.isStopped = false;
            playerState = PlayerState.IDLE;
        }
    }
}
