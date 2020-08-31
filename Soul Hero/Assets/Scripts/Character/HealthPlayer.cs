using UnityEngine;

public class HealthPlayer : HealthScript
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject panelGameOver;

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
            Dead();
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

    private void Dead()
    {
        panelGameOver.SetActive(true);
        characterAnimations.Die();
        isDead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<AbilityControler>().enabled = false;
        GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyControler>().enabled = false;
    }

    public override void HealthText(int health, int healthMax)
    {
        healthText.text = health + " / " + healthMax;
    }
}
