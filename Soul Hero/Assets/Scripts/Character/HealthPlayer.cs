using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthPlayer : HealthScript
{

    private void Start()
    {

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

    public override void ApplyDamage(int damage)
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
        if (health <= 0 && !isDead)
        {
            characterAnimations.Die();
            isDead = true;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<AbilityControler>().enabled = false;
            GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyControler>().enabled = false;

        }
    }

    public override void AddMaxHealth(int value)
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

}
