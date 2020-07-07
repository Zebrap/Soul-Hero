﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        BaseSword,
        DarkSword
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
        switch (itemType)
        {
            default:
            case ItemType.BaseSword: return ItemAssets.Instance.baseSword;
            case ItemType.DarkSword: return ItemAssets.Instance.darkSword;
        }
    }
}
