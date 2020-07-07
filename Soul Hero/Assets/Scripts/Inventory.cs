using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnITemListChange;

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
  /*      AddItem(new Item(Item.ItemType.BaseSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));*/
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnITemListChange?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
