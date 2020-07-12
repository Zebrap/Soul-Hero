using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private Action<Item> onDropAction;

    public void SetOnDropAction(Action<Item> onDropAction)
    {
        this.onDropAction = onDropAction;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<DragDrop>().GetItem();
        Debug.Log(item.itemType);
        if(eventData.pointerDrag != null)
        {
            onDropAction(item);
        }
    }
}
