using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform itemSlot;

    private void Awake()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlot = transform.Find("ItemSlot");
    }

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<PlayerMovement>().inventory;
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
        // TODO Change to grid
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 70f;
        foreach(Item item in inventory.GetItemList())
        {
            RectTransform itemSlotReactTransform = Instantiate(itemSlot, ItemSlotContainer).GetComponent<RectTransform>();
            itemSlotReactTransform.gameObject.SetActive(true);
            itemSlotReactTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

            itemSlotReactTransform.GetComponent<ClickManager>().leftClick.AddListener(() =>
            {
                print("Left click");
            });
            itemSlotReactTransform.GetComponent<ClickManager>().rightClick.AddListener(() =>
            {
                print("right click");
            });
            itemSlotReactTransform.GetComponent<ClickManager>().middleClick.AddListener(() =>
            {
                print("middle click");
            });
            /*   itemSlotReactTransform.GetComponent<Button>().onClick.AddListener(() => {
                   inventory.UseItem(item);
                 //  inventory.RemoveItem(item);
               });*/

            Image image = itemSlotReactTransform.Find("BackGround").GetComponent<Image>();
            image.sprite = item.GetSprite();
            Text textAmount = itemSlotReactTransform.Find("Text").GetComponent<Text>();
            if(item.amount > 1)
            {
                textAmount.text = item.amount.ToString();
            }
            else
            {
                textAmount.text = "";
            }
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
