using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private Transform pfInventoryItem;

    private Transform itemContainer;
    private EquipmentSlot weaponSlot;
    private EquipmentSlot useSlot1;

    private PlayerEquipment playerEquipment;

    private void Awake()
    {
        itemContainer = transform.Find("ItemSlotContainer");
        weaponSlot = transform.Find("weaponSlot").GetComponent<EquipmentSlot>();
        useSlot1 = transform.Find("useSlot1").GetComponent<EquipmentSlot>();

        weaponSlot.OnItemDropped += WeaponSlot_OnItemDropped;
        useSlot1.OnItemDropped += UseSlot1_OnItemDropped;
    }

    private void Start()
    {
        playerEquipment = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<PlayerEquipment>();
        UpdateVisual();
        playerEquipment.OnEquipmentChanged += CharacterEquipment_OnEquipmentChanged;
    }

    private void UseSlot1_OnItemDropped(object sender, EquipmentSlot.OnItemDroppedEventArgs e)
    {
        playerEquipment.TryEquipItem(PlayerEquipment.EquipSlot.UseItem, e.item);
    }

    private void WeaponSlot_OnItemDropped(object sender, EquipmentSlot.OnItemDroppedEventArgs e)
    {
        playerEquipment.TryEquipItem(PlayerEquipment.EquipSlot.Weapon, e.item);
    }

    public void SetPlayerEquipment(PlayerEquipment playerEq)
    {
        playerEquipment = playerEq;

        playerEquipment.OnEquipmentChanged += CharacterEquipment_OnEquipmentChanged;
    }
    private void CharacterEquipment_OnEquipmentChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Item weaponItem = playerEquipment.GetWeaponItem();
        if(weaponItem != null)
        {
            Transform uiItemTransform = Instantiate(pfInventoryItem, weaponSlot.transform.Find("SkillBG"));
            uiItemTransform.gameObject.SetActive(true);
            Image image = uiItemTransform.GetComponent<Image>();
            image.sprite = weaponItem.GetSprite();
        }
        Item useItem = playerEquipment.GetUseItem1();
        if (useItem != null)
        {
            Transform uiItemTransform = Instantiate(pfInventoryItem, useSlot1.transform.Find("SkillBG"));
            uiItemTransform.gameObject.SetActive(true);
            Image image = uiItemTransform.GetComponent<Image>();
            image.sprite = useItem.GetSprite();
        }
    }
}
