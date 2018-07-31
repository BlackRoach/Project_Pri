using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items_List {

    public int id;

    public int amount;

    public bool stackable;

    
    public Items_List(int _id,int _amount,bool _stackable)
    {
        id = _id;
        amount = _amount;
        stackable = _stackable;
    }

    public Items_List()
    {
        id = -1;
    }


} // class






