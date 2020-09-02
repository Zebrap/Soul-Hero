using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    public float dealDamageIntervalTime = 0.2f;
    private float timerDamage = 0f;
    public int spellDamage = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == Tags.PLAYER_TAG)
        {
            HealthScript playerhealth = other.GetComponent<HealthScript>();
            timerDamage += Time.deltaTime;
            if(timerDamage > dealDamageIntervalTime)
            {
                timerDamage = 0f;
                playerhealth.ApplyDamage(spellDamage);
            }
        }

    }
}
