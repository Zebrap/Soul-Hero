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
        HealthPotion
    }

    public ItemType itemType;
    public int amount;

    public Item(ItemType type, int amount)
    {
        this.itemType = type;
        this.amount = amount;
    }

    public Item(ItemType type)
    {
        this.itemType = type;
        this.amount = 1;
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
                return false;
            case ItemType.HealthPotion:
                return true;
        }
    }


    public PlayerEquipment.EquipSlot GetEquipSlot()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion:
                return PlayerEquipment.EquipSlot.UseItem;
            case ItemType.DarkSword:
            case ItemType.BaseSword:
                return PlayerEquipment.EquipSlot.Weapon;
        }
    }
}
