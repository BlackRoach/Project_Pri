using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Slot : MonoBehaviour , IDropHandler,IPointerEnterHandler,IPointerExitHandler{

    public bool can_Drop;

    public int slot_Num;

    private void Start()
    {       
        if(this.gameObject.transform.parent.name == "Slot_Panel")
        {
            can_Drop = true;
        }
        else
        {
            can_Drop = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (can_Drop)
        {
            Item target = eventData.pointerDrag.GetComponent<Item>();
            if (target != null)
            {
                target.is_Return = false;
                target.target_Parent = this.transform;
                // --------------------------------------
                InventoryManager.instance.items[this.slot_Num] = InventoryManager.instance.selected_Item;
            }
        }
        
    }

} // class






