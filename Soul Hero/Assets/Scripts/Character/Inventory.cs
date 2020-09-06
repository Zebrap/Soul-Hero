using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnITemListChange;

    private List<Item> itemList;
    private Action<Item> useItemAction;
    public InventorySlot[] itemArrayInventory;

    public Inventory(Action<Item> useItemAction, int inventorySlots)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
        itemArrayInventory = new InventorySlot[inventorySlots];
        for (int i = 0; i < inventorySlots; i++)
        {
            itemArrayInventory[i] = new InventorySlot(i);
        }
        AddItem(new Item(Item.ItemType.HealthPotion,10));
        AddItem(new Item(Item.ItemType.ManaPotion,10));
     //   AddItem(new Item(Item.ItemType.BaseSword));
     //   AddItem(new Item(Item.ItemType.DarkSword));
    }

    public bool AddItem(Item item)
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
                if (GetEmptyInventorySlot() != null)
                {
                    itemList.Add(item);
                    GetEmptyInventorySlot().SetItem(item);
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            if (GetEmptyInventorySlot() != null)
            {
                itemList.Add(item);
                GetEmptyInventorySlot().SetItem(item);
            }
            else
            {
                return false;
            }
        }
        OnITemListChange?.Invoke(this, EventArgs.Empty);
        return true;
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
                GetInventorySlotWithItem(itemInInventory).RemoveItem();
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            GetInventorySlotWithItem(item).RemoveItem();
            itemList.Remove(item);
        }
        OnITemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem_SaveStack(Item item)
    {
        if (itemList.Contains(item))
        {
            GetInventorySlotWithItem(item).RemoveItem();
            itemList.Remove(item);
            OnITemListChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddItem(Item item, InventorySlot inventorySlot)
    {
        RemoveItem_SaveStack(item);

        itemList.Add(item);
        inventorySlot.SetItem(item);

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

    public InventorySlot[] GetInventorySlotArray()
    {
        return itemArrayInventory;
    }

    public InventorySlot GetEmptyInventorySlot()
    {
        foreach (InventorySlot inventorySlot in itemArrayInventory)
        {
            if (inventorySlot.IsEmpty())
            {
                return inventorySlot;
            }
        }
        Debug.Log("Cannot find an empty InventorySlot!");
        return null;
    }

    public InventorySlot GetInventorySlotWithItem(Item item)
    {
        foreach (InventorySlot inventorySlot in itemArrayInventory)
        {
            if (inventorySlot.GetItem() == item)
            {
                return inventorySlot;
            }
        }
        Debug.Log("Cannot find Item " + item + " in a InventorySlot!");
        return null;
    }

    public class InventorySlot
    {

        private int index;
        private Item item;

        public InventorySlot(int index)
        {
            this.index = index;
        }

        public Item GetItem()
        {
            return item;
        }

        public void SetItem(Item item)
        {
            this.item = item;
        }

        public void RemoveItem()
        {
            item = null;
        }

        public bool IsEmpty()
        {
            return item == null;
        }

    }
}
