using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour , IBeginDragHandler,IDragHandler,IEndDragHandler{


    public Transform target_Parent;

    public bool is_Return; // 드래그 드랍 실패시 데이터 원위치 아이템 원위치

    private Slot slot_Num;

    public void OnBeginDrag(PointerEventData eventData)
    {
        is_Return = true;
        // 아이템 드래그 할때 실행
        InventoryManager.instance.equip_Type_Info_Panel.SetActive(false);
        slot_Num = this.transform.parent.GetComponentInParent<Slot>();
        InventoryManager.instance.selected_Item = InventoryManager.instance.items[slot_Num.slot_Num];
        InventoryManager.instance.items[slot_Num.slot_Num] = new Item_Data();
        // ------------------------------------
        target_Parent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.localPosition = new Vector3(eventData.position.x - 630f, eventData.position.y - 360f,
            this.transform.localPosition.z); 
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryManager.instance.equip_Type_Info_Panel.SetActive(false);
        this.transform.SetParent(target_Parent);
        this.transform.localPosition = Vector3.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // ---------------------------------
        if (is_Return)
        {
            InventoryManager.instance.items[slot_Num.slot_Num] = InventoryManager.instance.selected_Item;
        }
        // 마지막엔 항시 json 파일 저장
        for(int i = 0; i < InventoryManager.instance.items.Length; i++)
        {
            Inventory_Current_Items.instance.current_Items[i].ID = InventoryManager.instance.items[i].ID;
            Inventory_Current_Items.instance.current_Items[i].COUNT = InventoryManager.instance.items[i].ITEM_COUNT;
        }
        Inventory_Current_Items.instance.SAVE_NEW_DATA_JSON_Inventory();
    }

    // 아이템 장착 인포 실행 함수
    public void Event_Trigger_Pointer_Up()
    {
        if(this.gameObject.GetComponentInParent<Slot>() != null)
        {
            InventoryManager.instance.selected_Index = this.gameObject.GetComponentInParent<Slot>().slot_Num;
        }
        InventoryManager.instance.equip_Type_Info_Panel.SetActive(true);
        InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(0).
            GetComponent<Text>().text = InventoryManager.instance.items[InventoryManager.instance.selected_Index].ITEM_DESCRIPTION;
        if(InventoryManager.instance.items[InventoryManager.instance.selected_Index].ID == 30002)
        {
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(1).transform.GetChild(0).
                GetComponent<Text>().text = "사 용 ";
        }
        else
        {
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(1).transform.GetChild(0).
                GetComponent<Text>().text = "장 착 ";
        }
        if(this.transform.parent.name == "Equip_Item_Slot_1" || this.transform.parent.name == "Equip_Item_Slot_2")
        {
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(1).gameObject.SetActive(false);
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(2).gameObject.SetActive(true);

            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(0).
            GetComponent<Text>().text = InventoryManager.instance.equip_Items[InventoryManager.instance.selected_Index]
            .ITEM_DESCRIPTION;
        }
        else
        {
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(1).gameObject.SetActive(true);
            InventoryManager.instance.equip_Type_Info_Panel.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
} // class













