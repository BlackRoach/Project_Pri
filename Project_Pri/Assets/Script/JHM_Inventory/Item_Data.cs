using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Data  {

    public int id;
    public string item_Name;
    public string item_Description;
    public int item_ValueType;
    public int item_Value;
    public int item_Price;
    public int item_UseType;
    public int item_EquipType;
    public string item_Slug;
    // ---------------------------------
    public int item_Count;
    
    public Item_Data(int _id, string _item_Name, string _item_Description, int _item_ValueType, int _Item_Value, int _item_Price,
        int _item_UseType, int _item_EquipType, string _item_Slug)
    {
        this.id = _id;
        this.item_Name = _item_Name;
        this.item_Description = _item_Description;
        this.item_ValueType = _item_ValueType;
        this.item_Value = _Item_Value;
        this.item_Price = _item_Price;
        this.item_UseType = _item_UseType;
        this.item_EquipType = _item_EquipType;
        this.item_Slug = _item_Slug;
    }
    public Item_Data()
    {
        this.id = -1;
        this.item_Count = 0;
    }
    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }
    public string ITEM_NAME
    {
        get { return this.item_Name; }
        set { this.item_Name = value; }
    }
    public string ITEM_DESCRIPTION
    {
        get { return this.item_Description; }
        set { this.item_Description = value; }
    }
    public int ITEM_VALUETYPE
    {
        get { return this.item_ValueType; }
        set { this.item_ValueType = value; }
    }
    public int ITEM_VALUE
    {
        get { return this.item_Value; }
        set { this.item_Value = value; }
    }
    public int ITEM_PRICE
    {
        get { return this.item_Price; }
        set { this.item_Price = value; }
    }
    public int ITEM_USETYPE
    {
        get { return this.item_UseType; }
        set { this.item_UseType = value; }
    }
    public int ITEM_EQUIPTYPE
    {
        get { return this.item_EquipType; }
        set { this.item_EquipType = value; }
    }
    public string ITEM_SLUG
    {
        get { return this.item_Slug; }
        set { this.item_Slug = value; }
    }
    // ---------------------------
    public int ITEM_COUNT
    {
        get { return this.item_Count; }
        set { this.item_Count = value; }
    }
} // class













