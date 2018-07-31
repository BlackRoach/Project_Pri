﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Items_Info
{

    public int id;

    public string name;

    public string description;

    public int value_Type;

    public int value;

    public int price;

    public int use_Type;

    public int equip_Type;

    public string slug;

    // -----------------------------
    public Sprite item_Img;
    public bool stackable;
    // 아이템 맞는 스킬 푸시
    public Items_Info(int _id, string _name,string _description, int _value_Type, int _value, int _price, int _use_Type,
        int _equip_Type, string _slug)
    {
        id = _id;
        name = _name;
        description = _description;
        value_Type = _value_Type;
        value = _value;
        price = _price;
        use_Type = _use_Type;
        equip_Type = _equip_Type;
        slug = _slug;
    }
    // 아이템 없을때 id는 -1 나머지 공백 
    public Items_Info()
    {
        id = -1;
    }
    public Items_Info(int _id,string _name)
    {
        id = _id;
        name = _name;
    }
} // class
