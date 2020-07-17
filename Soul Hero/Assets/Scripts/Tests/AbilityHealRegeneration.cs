using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

namespace Tests
{
    public class AbilityHealRegeneration
    {
        [Test]
        public void AbilityHealRegenerationSimplePasses()
        {
            GameObject player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("TestPlayer"));
            HealthScript healthScript;
            HealAbility healAbility = MonoBehaviour.Instantiate(Resources.Load<HealAbility>("HealAbilityTest")); ;
            healthScript = player.transform.GetComponent<HealthScript>();

            healthScript.healthMax = 100f;
            healthScript.health = 20;

            healAbility.spellDamage = 40;
            healAbility.UseAbility(player);
            float time = healAbility.effect_duration / healAbility.healInterval;
            wait(time, healthScript);
        }

        IEnumerator wait(float v, HealthScript healthScript)
        {
            yield return new WaitForSeconds(v);
            Assert.AreEqual(60, healthScript.health);
        }
    }
}
