using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool moving;

    private CharacterAnimations characterAnimations;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimations = GetComponent<CharacterAnimations>();
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
