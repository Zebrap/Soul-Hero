﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float maxMana;
    public int base_attack_damage;
    private int attack_damage_with_weapon;

    HealthScript healthScript;
    ManaScript manaScript;
    PlayerMovement playerMovement;
    PlayerEquipment playerEquipment;

    private void Awake()
    {
        healthScript = GetComponent<HealthScript>();
        manaScript = GetComponent<ManaScript>();
        playerMovement = GetComponent<PlayerMovement>();
        playerEquipment = GetComponent<PlayerEquipment>();

    }

    private void Start()
    {


        healthScript.healthMax = maxHealth;
        manaScript.manaMax = maxMana;
        playerEquipment.OnWeaponChange += WeaponChange;
        playerEquipment.OnUseHeal += UseHeal;
        playerEquipment.OnUseMana += UseMana;
        Calculate_Damage();
    }

    private void WeaponChange(object sender, System.EventArgs e)
    {
        Calculate_Damage();
    }

    private void UseHeal(object sender, ParamterEventArgs e)
    {
        healthScript.HealthRegeneration(e.item.GetPower());
    }

    private void UseMana(object sender, ParamterEventArgs e)
    {
        manaScript.ManaRegeneration(e.item.GetPower());
    }

    private void Calculate_Damage()
    {
        if (playerEquipment.GetWeaponItem() != null)
        {
            attack_damage_with_weapon = base_attack_damage + playerEquipment.GetWeaponItem().GetPower();
        }
        else
        {
            attack_damage_with_weapon = base_attack_damage;
        }
        playerMovement.attack_damage = attack_damage_with_weapon;
    }
}
