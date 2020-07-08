using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhityBombAbility : Ability
{

    protected override void MoveAblity()
    {
        timer += Time.deltaTime / timeStartCollider;
        transform.position = Vector3.Lerp(pos, offSetPosition + pos, timer);
    }

    protected override void StartPosition(GameObject parent)
    {
        pos = parent.transform.position + parent.transform.forward * fowardValue;
        transform.position = pos;
    }

    protected override void ActionOnTarget(Collider target)
    {
        target.GetComponent<HealthScript>().ApplyDamage(spellDamage);
    }

    protected override void EffectOnTarget()
    {
    }
}
