using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    private GameObject target;
    private HealthScript healthTarget;
    private float healTime = 4f;
    private float healInterval = 0.2f;

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
        for (float i = 0; i < healTime/healInterval; i++) // Time/inteval = numbersOfHeal
        {
            healthTarget.HealthRegeneration(spellDamage/(int)(healTime / healInterval)); // maxHeal = (spellDamage/(healTime / healInterval))*(healTime / healInterval) = spellDamage
            yield return new WaitForSeconds(healInterval);
        }
    }
}
