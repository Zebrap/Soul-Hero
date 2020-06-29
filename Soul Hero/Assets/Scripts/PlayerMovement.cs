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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimations = GetComponent<CharacterAnimations>();
        particleClick.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                // if hit.collider.tag == enemy
                agent.destination = hit.point;
                Quaternion rot = Quaternion.Euler(90, 0, 0);
                particleClick.transform.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
                particleClick.transform.rotation = rot;
                particleClick.Play();
            }
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
}
