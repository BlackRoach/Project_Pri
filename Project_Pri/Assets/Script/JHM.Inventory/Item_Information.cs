using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Information : MonoBehaviour {

    private Item item_Select;
    // previous_slot_index 인덱스 안에 어떤 Number 가 있는지 previous_slot_index 의 인덱스 위치 도와주는 integer
    public static int select_Index;

    private void Awake()
    {
        item_Select = transform.GetComponent<Item>();
    }

    public void When_Click_Item_Released()
    {
        Inventory_Controller.instance.Item_Get_On_Instruction_Panel(item_Select.item_Ability.description);
        Inventory_Controller.instance.current_Select_Item = item_Select.item_Ability;
        StartCoroutine(Find_Select_Item_Parent_Position());

    }

    IEnumerator Find_Select_Item_Parent_Position()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 2; i++)
        {
            if (Inventory_Controller.instance.current_item[i].id == -1)
            {
                Item_Care_Manager.instance.previous_Slot_Index[i] = transform.parent.GetComponent<Slot>().slot_Id;
                select_Index = i;
                break;
            }
        }
    }






} // class







