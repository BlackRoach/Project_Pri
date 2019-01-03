using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Items_List  {
    private int id;
    private string item_Name;
    private int inventory_Type;
    private int item_UseType;
    private int item_EquipType;
    private string item_Icon;
    private int item_Price;
    private int item_Price_Type;
    private int item_Sale_Possible_Type;
    private int item_ValueType_1;
    private int item_Value_1;
    private int upDown_1;
    private int item_ValueType_2;
    private int item_Value_2;
    private int upDown_2;
    private int item_ValueType_3;
    private int item_Value_3;
    private int upDown_3;
    private string item_Description_1;
    private string item_Description_2;
    private string item_Slug;               

    public Items_List(int _id,string _item_Name,int _inventory_Type,int _item_UseType,int _item_EquipType,string _item_Icon,int _item_Price,int _item_Price_Type
        ,int _item_Sale_Possible_Type,int _item_ValueType_1,int _item_Value_1,int _upDown_1,int _item_ValueType_2,int _item_Value_2,int _upDown_2,int _item_ValueType_3
        ,int _item_Value_3,int _upDown_3,string _item_Description_1,string _item_Description_2,string _item_Slug)
    {
        this.id = _id;
        this.item_Name = _item_Name;
        this.inventory_Type = _inventory_Type;
        this.item_UseType = _item_UseType;
        this.item_EquipType = _item_EquipType;
        this.item_Icon = _item_Icon;
        this.item_Price = _item_Price;
        this.item_Price_Type = _item_Price_Type;
        this.item_Sale_Possible_Type = _item_Sale_Possible_Type;
        this.item_ValueType_1 = _item_ValueType_1;
        this.item_Value_1 = _item_Value_1;
        this.upDown_1 = _upDown_1;
        this.item_ValueType_2 = _item_ValueType_2;
        this.item_Value_2 = _item_Value_2;
        this.upDown_2 = _upDown_2;
        this.item_ValueType_3 = _item_ValueType_3;
        this.item_Value_3 = _item_Value_3;
        this.upDown_3 = _upDown_3;
        this.item_Description_1 = _item_Description_1;
        this.item_Description_2 = _item_Description_2;
        this.item_Slug = _item_Slug;
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
    public int INVENTORY_TYPE
    {
        get { return this.inventory_Type; }
        set { this.inventory_Type = value; }
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
    public string ITEM_ICON
    {
        get { return this.item_Icon; }
        set { this.item_Icon = value; }
    }
    public int ITEM_PRICE
    {
        get { return this.item_Price; }
        set { this.item_Price = value; }
    }
    public int ITEM_PRICE_TYPE
    {
        get { return this.item_Price_Type; }
        set { this.item_Price_Type = value; }
    }
    public int ITEM_SALE_POSSIBLE_TYPE
    {
        get { return this.item_Sale_Possible_Type; }
        set { this.item_Sale_Possible_Type = value; }
    }
    public int ITEM_VALUETYPE_1
    {
        get { return this.item_ValueType_1; }
        set { this.item_ValueType_1 = value; }
    }
    public int ITEM_VALUE_1
    {
        get { return this.item_Value_1; }
        set { this.item_Value_1 = value; }
    }
    public int UPDOWN_1
    {
        get { return this.upDown_1; }
        set { this.upDown_1 = value; }
    }
    public int ITEM_VALUETYPE_2
    {
        get { return this.item_ValueType_2; }
        set { this.item_ValueType_2 = value; }
    }
    public int ITEM_VALUE_2
    {
        get { return this.item_Value_2; }
        set { this.item_Value_2 = value; }
    }
    public int UPDOWN_2
    {
        get { return this.upDown_2; }
        set { this.upDown_2 = value; }
    }
    public int ITEM_VALUETYPE_3
    {
        get { return this.item_ValueType_3; }
        set { this.item_ValueType_3 = value; }
    }
    public int ITEM_VALUE_3
    {
        get { return this.item_Value_3; }
        set { this.item_Value_3 = value; }
    }
    public int UPDOWN_3
    {
        get { return this.upDown_3; }
        set { this.upDown_3 = value; }
    }
    public string ITEM_DESCRIPTION_1
    {
        get { return this.item_Description_1; }
        set { this.item_Description_1 = value; }
    }
    public string ITEM_DESCRIPTION_2
    {
        get { return this.item_Description_2; }
        set { this.item_Description_2 = value; }
    }
    public string ITEM_SLUG
    {
        get { return this.item_Slug; }
        set { this.item_Slug = value; }
    }
} // class



