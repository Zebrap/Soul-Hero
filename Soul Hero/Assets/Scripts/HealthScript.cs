using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int healthMax = 100;
    public int health = 100;
    public bool isDead = false;
    private bool isPlayer;
    private CharacterAnimations characterAnimations;

    [SerializeField]
    private Image health_UI;
    [SerializeField]
    private Text healthText;


    private void Awake()
    {
        characterAnimations = GetComponent<CharacterAnimations>();
        health = healthMax;
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (this.GetComponent<PlayerMovement>())
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (health_UI != null)
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
