using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Transform uiDescription;
    public string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiDescription!=null)
        {
            uiDescription.Find(UiTags.TEXT).GetComponent<Text>().text = description;
            uiDescription.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (uiDescription != null)
        {
            uiDescription.gameObject.SetActive(false);
        }
    }

    public void SetUI(Transform ui)
    {
        uiDescription = ui;
    }
}
