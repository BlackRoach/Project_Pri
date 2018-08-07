using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CGSlot : MonoBehaviour, IPointerDownHandler
{
    public bool active = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        if(active)
        {
            Sprite image = GetComponent<Image>().sprite;
            FindObjectOfType<CGGallearyManager>().ShowFullSizeCG(image);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
