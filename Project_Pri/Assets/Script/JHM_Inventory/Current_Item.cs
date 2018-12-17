using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Current_Item {

    private int id;

    private int count;

	public Current_Item(int _id, int _count)
    {
        this.id = _id;
        this.count = _count;
    }
    public Current_Item()
    {
        this.id = -1;
        this.count = 0;
    }


    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }
    public int COUNT
    {
        get { return this.count; }
        set { this.count = value; }
    }
} // class












