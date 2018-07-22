using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Information : MonoBehaviour {

    private Item item_Select;
    // previous_slot_index 인덱스 안에 어떤 Number 가 있는지 previous_slot_index 의 인덱스 위치 도와주는 integer
    public static int select_Index;
    // item 이 위치를 파악해주는 boolean  인벤토리에 있는지 장착 아이템인지
    public bool set_Pos;

   
    private void Awake()
    {
        item_Select = transform.GetComponent<Item>();

        set_Pos = false;
    }

    public void When_Click_Item_Released()
    {
        if (!set_Pos)
        {
            Inventory_Controller.instance.Item_Get_On_Instruction_Panel(item_Select.item_Ability.description);
        }
        else
        {
            Inventory_Controller.instance.Item_Get_Off_Instruction_Panel(item_Select.item_Ability.description);
        }
        if (!set_Pos)
        {
            Inventory_Controller.instance.current_Select_Item = item_Select.item_Ability;
            StartCoroutine(Find_Select_Item_Parent_Position());
        }
        else
        {
            select_Index = transform.parent.GetComponent<Slot>().slot_Id;
            
        }

    }

    IEnumerator Find_Select_Item_Parent_Position()
    {
        yield return new WaitForSeconds(0.4f);
        
        select_Index = Inventory_Controller.instance.current_Select_Item.equip_Type - 1;

        Item_Care_Manager.instance.previous_Slot_Index[select_Index] = transform.parent.GetComponent<Slot>().slot_Id;
        
    }






} // class







