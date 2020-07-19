﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour, IHealthScript
{
    public event EventHandler DieEvent;

    public float healthMax { get; set; } = 100f;
    public int health { get; set; }
    public bool isDead = false;
    private bool isPlayer;
    private CharacterAnimations characterAnimations;
#pragma warning disable 0649
    [SerializeField]
    private GameObject healthUI;
#pragma warning disable 0649
    [SerializeField]
    private Image healthFill;
#pragma warning disable 0649
    [SerializeField]
    private Text healthText;

    public Gradient gradient;

    private float waitTimeToDisable = 3f;

    public int healthRegen = 0;
    private float time = 0.0f;
    public float period = 1f;

    public int experience = 3;
    private Experience playerExperience;

    private void Awake()
    {
        health = (int)healthMax;
        characterAnimations = GetComponent<CharacterAnimations>();
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = (health / healthMax);
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
    }

    public void Start()
    {
        if (this.GetComponent<PlayerMovement>())
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
            playerExperience = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<Experience>();
        }
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = health / healthMax;
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
        if (health <= 0)
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
                GiveExp();
                GetComponent<EnemyControler>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = false;
                healthUI.SetActive(false);
                DieEvent?.Invoke(this, EventArgs.Empty);
                StartCoroutine(DisableEnemy());
            }
        }
    }

    IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(waitTimeToDisable);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (healthRegen > 0)
        {
            if (time < period)
            {
                time += Time.deltaTime;
            }
            else
            {
                HealthRegeneration(healthRegen);
                time = 0;
            }
        }
    }

    public void HealthRegeneration(int regeneration)
    {
        if (health > 0)
        {
            health = Mathf.Clamp(health + regeneration, 0, (int)healthMax);
            healthFill.fillAmount = health / healthMax;
            healthFill.color = gradient.Evaluate(health / healthMax);
            if (healthText != null)
            {
                healthText.text = health + " / " + healthMax;
            }
        }
    }

    private void GiveExp()
    {
        playerExperience.GetExp(experience);
    }

    public void AddMaxHealth(int value)
    {
        healthMax += value;
        health += value;
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = health / healthMax;
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
    }

    public void reviveEnemy()
    {
        health = (int)healthMax;
        GetComponent<EnemyControler>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = false;
        healthUI.SetActive(true);
        isDead = false;

        if (healthFill != null)
        {
            healthFill.fillAmount = (health / healthMax);
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
    }
}
