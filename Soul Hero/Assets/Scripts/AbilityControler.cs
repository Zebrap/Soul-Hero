using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityControler : MonoBehaviour
{
    public List<Ability> abilities;
    protected CharacterAnimations characterAnimations;
    private PlayerMovement player;

    void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(player.canMove)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActiveAbility(0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                ActiveAbility(1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ActiveAbility(2);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ActiveAbility(3);
            }
        }
    }

    IEnumerator CastTime(float timeCast)
    {
        yield return new WaitForSeconds(timeCast);
        player.NavMeshAgent_is_Stop(false);
    }

    private void ActiveAbility(int id)
    {
        if (abilities.Count > id)
        {
            if (!abilities[id].gameObject.activeSelf)
            {
                player.NavMeshAgent_is_Stop(true);
                abilities[0].UseAbility(transform.position, transform.forward);
                characterAnimations.UseAbility(abilities[0].abilityEnum);
                float timeCast = abilities[id].TimeCast();
                StartCoroutine(CastTime(timeCast));
            }
        }
        else
        {
            print("not learn yet "+id);
        }
    }
}
