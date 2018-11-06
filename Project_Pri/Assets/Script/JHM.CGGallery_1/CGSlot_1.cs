using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CGSlot_1 : MonoBehaviour , IPointerDownHandler
{
    public bool active = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
        {
            Sprite image = GetComponent<Image>().sprite;
            FindObjectOfType<VacanceCG_Manager>().ShowFullSizeCG(image);
        }
    }
} // class

