using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    #pragma warning disable 0649
    [SerializeField]
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup CanvasGroup;
    private Item item;
    private Vector2 startPose;
    private Transform myParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPose = rectTransform.anchoredPosition;
        transform.SetParent(canvas.transform);
        CanvasGroup.alpha = 0.8f;
        CanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup.alpha = 1f;
        CanvasGroup.blocksRaycasts = true;
        CompareParent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      //  Debug.Log("Down");
    }

    public void OnDrop(PointerEventData eventData)
    {
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
    public Item GetItem()
    {
        return item;
    }

    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

    public void SetMyParent(Transform myparent)
    {
        myParent = myparent;
    }

    private void CompareParent()
    {
        if(transform.parent == canvas.transform)
        {
            transform.SetParent(myParent);
            rectTransform.anchoredPosition = startPose;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show item stats
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide item stats
    }
}
