using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool moving;
    public ParticleSystem particleClick;
    private CharacterAnimations characterAnimations;
    public Color particleEnemyColor;
    private Color particleMoveColor;
    private ParticleSystem.ColorOverLifetimeModule particleColor;
    private float offSetParticle = 0.1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimations = GetComponent<CharacterAnimations>();
        particleClick.Stop();
        particleMoveColor = particleClick.main.startColor.color;
        particleColor = particleClick.colorOverLifetime;
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
                Vector3 enemyPosition = hit.transform.gameObject.transform.position;
                particleClick.transform.position = new Vector3(enemyPosition.x, enemyPosition.y + offSetParticle, enemyPosition.z); // enemy position
            }
            else
            {
                particleColor.color = particleMoveColor;
                particleClick.transform.position = new Vector3(hit.point.x, hit.point.y + offSetParticle, hit.point.z);
            }
            particleClick.transform.rotation = rot;
            particleClick.Play();

            agent.destination = hit.point;
        }
    }
}
