using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        BaseSword,
        DarkSword,
        HealthPotion,
        ManaPotion,
        HeavyBlade
    }

    public ItemType itemType;
    public int amount;
    private int power;
    private string description ="";

    public Item(ItemType type, int amount)
    {
        this.itemType = type;
        this.amount = amount;
        SetPower();
    }

    public Item(ItemType type)
    {
        this.itemType = type;
        this.amount = 1;
        SetPower();
    }

    public Sprite GetSprite()
    {
        string path = "items/Sprite/" + itemType;
        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.BaseSword:
            case ItemType.DarkSword:
            case ItemType.HeavyBlade:
                return false;
            case ItemType.ManaPotion:
            case ItemType.HealthPotion:
                return true;
        }
    }


    public PlayerEquipment.EquipSlot GetEquipSlot()
    {
        switch (itemType)
        {
            default:
            case ItemType.ManaPotion:
            case ItemType.HealthPotion:
                return PlayerEquipment.EquipSlot.UseItem;
            case ItemType.DarkSword:
            case ItemType.BaseSword:
            case ItemType.HeavyBlade:
                return PlayerEquipment.EquipSlot.Weapon;
        }
    }

    public void SetPower()
    {
        switch (itemType)
        {
            default:
            case ItemType.ManaPotion:
                power = 20;
                description = "Mana Potion" + Environment.NewLine+"Recovers " + power+ " mana";
                break;
            case ItemType.HealthPotion:
                power = 20;
                description = "Health Potion" + Environment.NewLine + "Recovers " + power + " life";
                break;
            case ItemType.DarkSword:
                power = 40;
                description = "Dark Sword"+ Environment.NewLine;
                WeaponDescription();
                break;
            case ItemType.BaseSword:
                power = 10;
                description = "Base Sword" + Environment.NewLine;
                WeaponDescription();
                break;
            case ItemType.HeavyBlade:
                power = 70;
                description = "Heavy Blade" + Environment.NewLine;
                WeaponDescription();
                break;
        }
    }

    public int GetPower()
    {
        return power;
    }

    private void WeaponDescription()
    {
        description += "Attack power: " + power;
    }

    public string GetDescription()
    {
        return description;
    }
}
