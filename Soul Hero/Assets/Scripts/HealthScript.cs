using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int healthMax = 100;
    public int health;
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
        health = healthMax;
        characterAnimations = GetComponent<CharacterAnimations>();
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
        }
        if (healthFill != null)
        {
            healthFill.fillAmount = health / 100f;
            healthFill.color = gradient.Evaluate(health / 100f);
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
            healthFill.fillAmount = health / 100f;
            healthFill.color = gradient.Evaluate(health / 100f);
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
                GiveExp();
                GetComponent<EnemyControler>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = false;
                healthUI.SetActive(false);
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

    public void HealthRegeneration(int regeneration)
    {
        health = Mathf.Clamp(health + healthRegen, 0, healthMax);
        healthFill.fillAmount = health / 100f;
        healthFill.color = gradient.Evaluate(health / 100f);
        if (healthText != null)
        {
            healthText.text = health + " / " + healthMax;
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
    }
}
