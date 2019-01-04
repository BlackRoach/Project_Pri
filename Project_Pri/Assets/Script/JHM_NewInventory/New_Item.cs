using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Item : MonoBehaviour {

    public Items_List this_Item;

    public void Button_Event_Trigger_Point_Up()
    {
        NewInventory_Manager.instance.Item_Status_Info_Turn_On(this_Item.ITEM_NAME,this_Item.ITEM_USETYPE,this_Item.ITEM_ICON,this_Item.ITEM_PRICE
            ,this_Item.ITEM_PRICE_TYPE,this_Item.ITEM_DESCRIPTION_1,this_Item.ITEM_DESCRIPTION_2);
    }
} // class










