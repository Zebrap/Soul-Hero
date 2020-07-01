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
        if (Input.GetKeyDown("space"))
        {
            ability.UseAbility(transform.position);
            characterAnimations.BeamSKill();
        }
    }
}
