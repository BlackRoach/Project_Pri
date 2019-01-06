using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Item : MonoBehaviour {

    public Items_List this_Item;

    public void Button_Event_Trigger_Point_Up()
    {
        NewInventory_Manager.instance.Item_Status_Info_Turn_On(this_Item);
        NewInventory_Manager.instance.slot_Num = this.gameObject.transform.parent.gameObject;
    }
} // class










