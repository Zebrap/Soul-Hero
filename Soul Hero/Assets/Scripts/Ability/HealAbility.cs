using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    private GameObject target;
    private HealthScript healthTarget;
    public float healInterval = 0.2f;

    protected override void EffectOnTarget()
    {
        StartCoroutine(HealOverTime());
    }

    protected override void MoveAblity()
    {
        // transform.position = target.transform.position;
    }

    protected override void StartPosition(GameObject parent)
    {
        this.transform.SetParent(parent.transform);
        transform.position = parent.transform.position;
        target = parent;
        healthTarget = target.GetComponent<HealthScript>();
    }

    IEnumerator HealOverTime()
    {
        for (float i = 0; i < effect_duration / healInterval; i++) // Time/inteval = numbersOfHeal
        {
            healthTarget.HealthRegeneration(spellDamage/(int)(effect_duration / healInterval)); // maxHeal = (spellDamage/(healTime / healInterval))*(healTime / healInterval) = spellDamage
            yield return new WaitForSeconds(healInterval);
        }
    }
}
