using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    public bool isDead = false;
    public bool isPlayer;
    private CharacterAnimations characterAnimations;

    [SerializeField]
    private Image health_UI;


    private void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health_UI != null)
        {
            health_UI.fillAmount = health / 100f;
        }
        if(health <= 0)
        {
            characterAnimations.Die();
            isDead = true;

            if (isPlayer)
            {
                GetComponent<PlayerMovement>().enabled = false;

                GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyControler>().enabled = false;
            }
            else
            {
                GetComponent<EnemyControler>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}
