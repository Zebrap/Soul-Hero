using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBossControl : EnemyControler
{
    private float intervalSpell = 2f;
    private float timerSpell = 1.5f;
    public bool castSpell = false;

    void Update()
    {
        if (target.GetComponent<HealthScript>().isDead)
        {
            characterAnimations.Walk(false);
            return;
        }

        if (enamy_State == EnamyState.CHASE)
        {
            ChasePlayer();
        }

        if (enamy_State == EnamyState.ATTAK)
        {
            //   characterAnimations.
            RotateToTarget();
            if (timerSpell > intervalSpell)
            {
                // cast spell
                timerSpell = 0;
                RotateToTarget();
                castSpell = true;
                characterAnimations.SetTrigger(AnimationsTags.CAST);
            }
            else if(!castSpell)
            {
                timerSpell += Time.deltaTime;
                AttackSingleTarget();
            }
        }
    }
}
