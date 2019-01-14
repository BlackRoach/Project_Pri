using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class New_Item_Data  {
    private int id;
    private int amount;

    public New_Item_Data(int _id,int _amount)
    {
        this.id = _id;
        this.amount = _amount;
    }
    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }
    public int AMOUNT
    {
        get { return this.amount; }
        set { this.amount = value; }
    }

} // class










