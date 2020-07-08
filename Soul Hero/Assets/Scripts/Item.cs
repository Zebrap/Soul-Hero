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

    public Sprite GetSprite()
    {
        string path = "items/Sprite/" + itemType;
        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    /*    switch (itemType)
        {
            default:
            case ItemType.BaseSword: return ItemAssets.Instance.baseSword;
            case ItemType.DarkSword: return ItemAssets.Instance.darkSword;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotion;
        }*/
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
}
