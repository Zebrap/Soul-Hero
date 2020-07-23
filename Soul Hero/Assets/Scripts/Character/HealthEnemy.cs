﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthEnemy : HealthScript
{
    public event EventHandler DieEvent;
    public int experience = 3;
    private Experience playerExperience;
    private float waitTimeToDisable = 3f;

    private void Start()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = (health / healthMax);
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
        playerExperience = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<Experience>();
    }

    public override void ApplyDamage(int damage)
    {
        health -= damage;
        if (healthFill != null)
        {
            healthFill.fillAmount = health / healthMax;
            healthFill.color = gradient.Evaluate(health / healthMax);
        }

        if (healthText != null)
        {
            healthText.gameObject.SetActive(true);
            healthText.text = damage.ToString();

            StopCoroutine("ResetShowDamage");
            StartCoroutine("ResetShowDamage");
        }
        if (health <= 0 && !isDead)
        {
            characterAnimations.Die();
            isDead = true;

            GiveExp();
            GetComponent<EnemyControler>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            healthFill.gameObject.SetActive(false);
            DieEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DisableEnemy());
        }
    }

    private void GiveExp()
    {
        playerExperience.GetExp(experience);
    }

    IEnumerator DisableEnemy()
    {
        yield return new WaitForSeconds(waitTimeToDisable);
        healthText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    IEnumerator ResetShowDamage()
    {
        yield return new WaitForSeconds(1.5f);
        healthText.gameObject.SetActive(false);
        yield return null;
    }

    public void reviveEnemy()
    {
        health = (int)healthMax;
        GetComponent<EnemyControler>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = false;
     //   healthUI.SetActive(true);
        healthFill.gameObject.SetActive(true);
        isDead = false;

        if (healthFill != null)
        {
            healthFill.fillAmount = (health / healthMax);
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
    }

    public override void AddMaxHealth(int value)
    {
        healthMax += value;
        health += value;
        if (healthFill != null)
        {
            healthFill.fillAmount = health / healthMax;
            healthFill.color = gradient.Evaluate(health / healthMax);
        }
    }
}
