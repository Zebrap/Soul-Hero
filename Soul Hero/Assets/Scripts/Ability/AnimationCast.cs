using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCast : MonoBehaviour
{
    private WizardBossControl player;

    public void Awake()
    {
        if (player == null)
        {
            player = transform.parent.gameObject.transform.parent.gameObject.GetComponent<WizardBossControl>();
        }
    }
    public GameObject aoeSpellPrefab;

    public void CastSpell()
    {
        Instantiate(aoeSpellPrefab, player.GetTargetPos(), Quaternion.Euler(-90,0,0));
    }

    public void EndCast()
    {
        player.castSpell = false;
    }
}
