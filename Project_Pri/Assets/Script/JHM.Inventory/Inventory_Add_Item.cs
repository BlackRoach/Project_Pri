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
        if (current_Index < 20)
        {
            Inventory_Add_Item_Json.instance.inventory_Item_List[current_Index].id = _id;
            current_Index++;
        }
    }





} // class










