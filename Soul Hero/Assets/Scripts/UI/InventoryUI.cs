﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private Transform pfInventoryItem;

    #pragma warning disable 0649
    [SerializeField]
    private Canvas canvas;
    #pragma warning disable 0649
    [SerializeField]
    private Transform descriptionInventoryUI;

    private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform itemSlot;

    private void Awake()
    {
        ItemSlotContainer = transform.Find(UiTags.ITEM_SLOT_CONTAINER);
        itemSlot = transform.Find(UiTags.ITEM_SLOT);
    }

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<PlayerEquipment>().inventory;
        inventory.OnITemListChange += InventoryOnItemListChange;
        RefreshInventoryItems();

    }

    private void InventoryOnItemListChange(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in ItemSlotContainer)
        {
            if (child == itemSlot) continue;
            Destroy(child.gameObject);
        }
        foreach(Inventory.InventorySlot slot in inventory.itemArrayInventory)
        {
            Item item = slot.GetItem();
            RectTransform itemSlotReactTransform = Instantiate(itemSlot, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotReactTransform.gameObject.SetActive(true);

            if (!slot.IsEmpty())
            {
                Transform uiItem = Instantiate(pfInventoryItem, itemSlotReactTransform.Find(UiTags.BACKGROUND).transform);
                
                //    itemSlotReactTransform.GetComponent<DragDrop>().SetItem(item);

                Image image = uiItem.GetComponent<Image>();
                image.sprite = item.GetSprite();
                DragDrop dragDrop = uiItem.GetComponent<DragDrop>();
                dragDrop.SetItem(item);
                dragDrop.SetDescriptionUI(descriptionInventoryUI);
                dragDrop.SetCanvas(canvas);
                dragDrop.SetMyParent(itemSlotReactTransform.Find(UiTags.BACKGROUND).transform);
                Text textAmount = itemSlotReactTransform.Find(UiTags.TEXT).GetComponent<Text>();
                if (item.amount > 1)
                {
                    textAmount.text = item.amount.ToString();
                }
                else
                {
                    textAmount.text = "";
                }
            }
            else
            {
                Text textAmount = itemSlotReactTransform.Find(UiTags.TEXT).GetComponent<Text>();
                textAmount.text = "";
            }
            Inventory.InventorySlot tmpInventorySlot = slot;

            ItemSlot UIitemSlot = itemSlotReactTransform.GetComponent<ItemSlot>();
            UIitemSlot.SetOnDropAction((Item draggItem) =>
            {
                Item draggedItem = draggItem;
                inventory.AddItem(draggedItem, tmpInventorySlot);
            });
        }
    }

    void OnDisable()
    {
        descriptionInventoryUI.gameObject.SetActive(false);
    }
}
