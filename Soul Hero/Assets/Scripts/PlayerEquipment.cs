using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public event EventHandler OnEquipmentChanged;

    public enum EquipSlot
    {
        Weapon,
        UseItem,
    }


    private Item weaponItem;
    private Item useItem1;

    private void Awake()
    {

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
        this.weaponItem = weaponItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetUseItem1(Item useItem)
    {
        this.useItem1 = useItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }


    public void TryEquipItem(EquipSlot equipSlot, Item item)
    {
        if (equipSlot == item.GetEquipSlot())
        {
            switch (equipSlot)
            {
                default:
                case EquipSlot.Weapon: SetWeaponItem(item); break;
                case EquipSlot.UseItem: SetUseItem1(item); break;
            }
        }
    }
}
