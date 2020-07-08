using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    private GameObject target;
    private HealthScript healthTarget;
    private int numberOfHeals = 4;

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
        for (int i = 0; i < numberOfHeals; i++)
        {
            healthTarget.HealthRegeneration(spellDamage/numberOfHeals);
            yield return new WaitForSeconds(1f);
        }
    }
}
