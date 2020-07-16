using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public event EventHandler OnEquipmentChanged;
    public event EventHandler OnWeaponChange;
    public event EventHandler OnUseHeal;
    public event EventHandler OnUseMana;
    [HideInInspector]
    public Inventory inventory;

    public Transform WeaponPlayerSlot;
    private WeaponStats weaponStats;

    public enum EquipSlot
    {
        Weapon,
        UseItem,
    }


    private Item weaponItem;
    private Item useItem1;
    private Item useItem2;

    private void Awake()
    {

        inventory = new Inventory(UseItem, 20);
        SetWeaponItem(new Item(Item.ItemType.BaseSword));
    }

    public Item GetWeaponItem()
    {
        return weaponItem;
    }

    public Item GetUseItem1()
    {
        return useItem1;
    }
    public Item GetUseItem2()
    {
        return useItem2;
    }

    private void SetWeaponItem(Item weaponItem)
    {
        Item item = GetWeaponItem();
        if (item != null){
            inventory.AddItem(item);
        }
        this.weaponItem = weaponItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
        VisualChangeWeaponPlayer();
    }

    private void VisualChangeWeaponPlayer()
    {
        foreach (Transform child in WeaponPlayerSlot)
        {
            GameObject.Destroy(child.gameObject);
        }
        string path = "items/" + this.weaponItem.itemType;
        GameObject instance = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        weaponStats = instance.GetComponent<WeaponStats>();
        Vector3 pos = instance.transform.position;
        Quaternion rot = instance.transform.rotation;
        instance.transform.SetParent(WeaponPlayerSlot);
        instance.transform.localPosition = pos;
        instance.transform.localRotation = rot;
        OnWeaponChange?.Invoke(this, EventArgs.Empty);
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

    private void SetUseItem2(Item useItem)
    {
        Item item = GetUseItem2();
        if (item != null)
        {
            inventory.AddItem(item);
        }
        this.useItem2 = useItem;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetUseItem(Item useItem, int slot)
    {
        switch (slot)
        {
            case 0:
                SetUseItem1(useItem);
                break;
            case 1:
                SetUseItem2(useItem);
                break;
        }
    }


    public WeaponStats GetWeaponStats()
    {
        return weaponStats;
    }

    private void UnequipWeapon()
    {
        this.weaponItem = null;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UnequipSlot1()
    {
        this.useItem1 = null;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }
    private void UnequipSlot2()
    {
        this.useItem2 = null;
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TryEquipItem(EquipSlot equipSlot, Item item)
    {
        if (equipSlot == item.GetEquipSlot())
        {
            switch (equipSlot)
            {
                case EquipSlot.Weapon:
                    SetWeaponItem(item);
                    inventory.RemoveItem_SaveStack(item);
                    break;
                case EquipSlot.UseItem:
                    SetUseItem1(item);
                    inventory.RemoveItem_SaveStack(item);
                    break;
                default:
                    break;
            }
        }
    }

    public void TryEquipItem(EquipSlot equipSlot, Item item, int slot)
    {
        if (equipSlot == item.GetEquipSlot())
        {
            switch (equipSlot)
            {
                case EquipSlot.Weapon:
                    SetWeaponItem(item);
                    inventory.RemoveItem_SaveStack(item);
                    break;
                case EquipSlot.UseItem:
                    SetUseItem(item, slot);
                    inventory.RemoveItem_SaveStack(item);
                    break;
                default:
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GetUseItem1() != null)
        {
            UseItemFromSlot(GetUseItem1(), 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && GetUseItem2() != null)
        {
            UseItemFromSlot(GetUseItem2(), 1);
        }
    }

    private void UseItemFromSlot(Item item, int slot)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                OnUseHeal?.Invoke(this, EventArgs.Empty);
                break;
            case Item.ItemType.ManaPotion:
                OnUseMana?.Invoke(this, EventArgs.Empty);
                break;
        }
        RemoveItem(item, slot);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item, int slot)
    {
        item.amount -= 1;
        if (item.amount <= 0)
        {
            switch (slot)
            {
                case 0:
                    this.useItem1 = null;
                    break;
                case 1:
                    this.useItem2 = null;
                    break;
            }
        }
    }
}

