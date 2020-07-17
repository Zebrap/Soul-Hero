using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : Ability
{
    private PlayerMovement playerAttack;

    protected override void EffectOnTarget()
    {
        playerAttack.attack_damage += spellDamage;
        StartCoroutine(EndBuff());
    }

    protected override void MoveAblity()
    {

    }

    protected override void StartPosition(GameObject parent)
    {
        this.transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
        playerAttack = parent.GetComponent<PlayerMovement>();
    }

    IEnumerator EndBuff()
    {
        yield return new WaitForSeconds(effect_duration);
        playerAttack.attack_damage -= spellDamage;
    }
}
