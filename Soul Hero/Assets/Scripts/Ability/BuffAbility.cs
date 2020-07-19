using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : Ability
{
    private PlayerMovement playerAttack;
    private int spellPowerBuff; 

    protected override void EffectOnTarget()
    {
        playerAttack.bonus_attack_damage += spellDamage;
        spellPowerBuff = spellDamage;
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
        playerAttack.bonus_attack_damage -= spellPowerBuff;
    }
}
