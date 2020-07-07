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

    protected override void StartPosition(Vector3 characterPosition, Vector3 forward)
    {
        pos = characterPosition + forward * fowardValue;
        transform.position = pos;
    }

    protected override void ActionOnTarget(Collider target)
    {
        target.GetComponent<HealthScript>().ApplyDamage(spellDamage);
    }
}
