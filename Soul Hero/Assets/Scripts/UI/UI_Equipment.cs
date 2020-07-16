using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Equipment : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private Transform pfInventoryItem;
#pragma warning disable 0649
    [SerializeField] private Canvas canvas;

    private Transform itemContainer;
    private EquipmentSlot weaponSlot;
    private EquipmentSlot useSlot1;
    private EquipmentSlot useSlot2;

    private PlayerEquipment playerEquipment;

    private void Awake()
    {
        itemContainer = transform.Find(UiTags.ITEM_SLOT_CONTAINER);
        weaponSlot = transform.Find("weaponSlot").GetComponent<EquipmentSlot>();
        useSlot1 = transform.Find("useSlot1").GetComponent<EquipmentSlot>();
        useSlot2 = transform.Find("useSlot2").GetComponent<EquipmentSlot>();

        weaponSlot.OnItemDropped += WeaponSlot_OnItemDropped;
        useSlot1.OnItemDropped += UseSlot1_OnItemDropped;
        useSlot2.OnItemDropped += UseSlot2_OnItemDropped;
    }

    private void Start()
    {
        playerEquipment = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<PlayerEquipment>();
        UpdateVisual();
        playerEquipment.OnEquipmentChanged += CharacterEquipment_OnEquipmentChanged;
    }
    
    private void UseSlot2_OnItemDropped(object sender, EquipmentSlot.OnItemDroppedEventArgs e)
    {
        playerEquipment.TryEquipItem(PlayerEquipment.EquipSlot.UseItem, e.item, 1);
    }

    private void UseSlot1_OnItemDropped(object sender, EquipmentSlot.OnItemDroppedEventArgs e)
    {
        playerEquipment.TryEquipItem(PlayerEquipment.EquipSlot.UseItem, e.item, 0);
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
            Transform uiItemTransform = Instantiate(pfInventoryItem, weaponSlot.transform.Find(UiTags.BACKGROUND));
            uiItemTransform.gameObject.SetActive(true);
            Image image = uiItemTransform.GetComponent<Image>();
            image.sprite = weaponItem.GetSprite();
            DragDrop dragDrop = uiItemTransform.GetComponent<DragDrop>();
            dragDrop.SetItem(weaponItem);
            dragDrop.SetCanvas(canvas);
            dragDrop.SetMyParent(weaponSlot.transform.Find(UiTags.BACKGROUND).transform);
        }
        Item useItem1 = playerEquipment.GetUseItem1();
        if (useItem1 != null)
        {
            Transform uiItemTransform = Instantiate(pfInventoryItem, useSlot1.transform.Find(UiTags.BACKGROUND));
            uiItemTransform.gameObject.SetActive(true);
            Image image = uiItemTransform.GetComponent<Image>();
            image.sprite = useItem1.GetSprite();
            DragDrop dragDrop = uiItemTransform.GetComponent<DragDrop>();
            dragDrop.SetItem(useItem1);
            dragDrop.SetCanvas(canvas);
            dragDrop.SetMyParent(useSlot1.transform.Find(UiTags.BACKGROUND).transform);
            Text textAmount = useSlot1.transform.Find(UiTags.TEXT).GetComponent<Text>();
            if (useItem1.amount > 1)
            {
                textAmount.text = useItem1.amount.ToString();
            }
            else
            {
                textAmount.text = "";
            }
        }
        else
        {
            foreach (Transform child in useSlot1.transform.Find(UiTags.BACKGROUND).transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        Item useItem2 = playerEquipment.GetUseItem2();
        if (useItem2 != null)
        {
            Transform uiItemTransform = Instantiate(pfInventoryItem, useSlot2.transform.Find(UiTags.BACKGROUND));
            uiItemTransform.gameObject.SetActive(true);
            Image image = uiItemTransform.GetComponent<Image>();
            image.sprite = useItem2.GetSprite();
            DragDrop dragDrop = uiItemTransform.GetComponent<DragDrop>();
            dragDrop.SetItem(useItem2);
            dragDrop.SetCanvas(canvas);
            dragDrop.SetMyParent(useSlot2.transform.Find(UiTags.BACKGROUND).transform);
            Text textAmount = useSlot2.transform.Find(UiTags.TEXT).GetComponent<Text>();
            if (useItem2.amount > 1)
            {
                textAmount.text = useItem2.amount.ToString();
            }
            else
            {
                textAmount.text = "";
            }
        }
        else
        {
            foreach (Transform child in useSlot2.transform.Find(UiTags.BACKGROUND).transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
