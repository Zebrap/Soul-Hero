using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnITemListChange;

    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
  /*      AddItem(new Item(Item.ItemType.BaseSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));
        AddItem(new Item(Item.ItemType.DarkSword, 1));*/
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item invItem in itemList)
            {
                if(invItem.itemType == item.itemType)
                {
                    invItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnITemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item invItem in itemList)
            {
                if (invItem.itemType == item.itemType)
                {
                    invItem.amount -= item.amount;
                    itemInInventory = invItem;
                }
            }
            if (itemInInventory!=null && itemInInventory.amount<=0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnITemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
