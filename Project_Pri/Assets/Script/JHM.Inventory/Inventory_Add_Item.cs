using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Add_Item : MonoBehaviour {



    public int current_Index;


    private void Start()
    {
        current_Index = 2;
    }

    public void Add_Item_Value(int _id)
    {
        Check_For_Loop_If_Index_Is_Null();
        if (current_Index < 20)
        {
            if (_id == 30002)
            {
                for(int i = 0; i < Inventory_Add_Item_Json.instance.inventory_Item_List.Length; i++)
                {
                    if(Inventory_Add_Item_Json.instance.inventory_Item_List[i].id == 30002)
                    {
                        Inventory_Add_Item_Json.instance.inventory_Item_List[i].amount += 2;
                    }
                }
            }
            else
            {
                Inventory_Add_Item_Json.instance.inventory_Item_List[current_Index].id = _id;
                current_Index++;
            }
        }


        Inventory_Add_Item_Json.instance.SAVE_NEW_DATA_JSON_Inventory();
    }

    private void Check_For_Loop_If_Index_Is_Null()
    {
        for(int i = 0; i < Inventory_Add_Item_Json.instance.inventory_Item_List.Length; i++)
        {
            if(Inventory_Add_Item_Json.instance.inventory_Item_List[i].id == -1)
            {
                current_Index = i;
                break;
            }
        }
    }

} // class










