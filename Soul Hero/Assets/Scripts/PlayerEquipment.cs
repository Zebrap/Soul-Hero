using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public event EventHandler OnEquipmentChanged;
    [HideInInspector]
    public Inventory inventory;

    public enum EquipSlot
    {
        Weapon,
        UseItem,
    }


    private Item weaponItem;
    private Item useItem1;

    private void Awake()
    {

        inventory = new Inventory(UseItem, 20);
    }

    public Item GetWeaponItem()
    {
        return weaponItem;
    }

    public Item GetUseItem1()
    {
        return useItem1;
    }

    private void SetWeaponItem(Item weaponItem)
    {
        Item item = GetWeaponItem();
        if (item != null){
            inventory.AddItem(item);
        }
        this.weaponItem = weaponItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetUseItem1(Item useItem)
    {
        Item item = GetUseItem1();
        if (item != null)
        {
            inventory.AddItem(item);
        }
        this.useItem1 = useItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UnequipWeapon()
    {
        this.weaponItem = null;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TryEquipItem(EquipSlot equipSlot, Item item)
    {
        if (equipSlot == item.GetEquipSlot())
        {
            switch (equipSlot)
            {
                default:
                case EquipSlot.Weapon:
                    SetWeaponItem(item);
                    inventory.RemoveItem_SaveStack(item);
                    break;
                case EquipSlot.UseItem:
                    SetUseItem1(item);
                    inventory.RemoveItem_SaveStack(item);
                    break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (inventory.AddItem(itemWorld.GetItem()))
            {
                itemWorld.DestroySelf();
            }
        }
    }


    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                inventory.RemoveItem(new Item(Item.ItemType.HealthPotion, 1));
                break;
        }
    }

}

