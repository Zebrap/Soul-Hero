using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControler : MonoBehaviour
{
    // TODO prefb spell? List spells?
    public Abilitys ability; 

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ability.UseAbility(transform.position);
        }
    }
}
