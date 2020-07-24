using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class HealthScript : MonoBehaviour
{
    public float healthMax = 100f;
    public int health;
    public bool isDead = false;
    protected CharacterAnimations characterAnimations;
#pragma warning disable 0649
    [SerializeField]
    protected GameObject healthUI;
#pragma warning disable 0649
    [SerializeField]
    protected Image healthFill;
#pragma warning disable 0649
    [SerializeField]
    protected Text healthText;

    public Gradient gradient;

    public int healthRegen = 0;
    private float time = 0.0f;
    public float period = 1f;


    private void Awake()
    {
        health = (int)healthMax;
        characterAnimations = GetComponent<CharacterAnimations>();
    }

    public abstract void ApplyDamage(int damage);

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
    public abstract void AddMaxHealth(int value);
}
