using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    // TODO prefb spell? List spells?
    public Abilitys ability;
    protected CharacterAnimations characterAnimations;
    private PlayerMovement player;

    void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && player.canMove)
        {
            ability.UseAbility(transform.position, transform.forward);
            characterAnimations.BeamSKill();
        }
    }
}
