using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem {

    private int id;
    private string icon; // 아이템 이미지
    private string name; // 아이템 이름
    private int price; // 아이템 가격
    private string effect; // 아이템 효과 설명글

    public ShopItem(int id, string icon, string name, int price, string effect)
    {
        this.id = id;
        this.icon = icon;
        this.name = name;
        this.price = price;
        this.effect = effect;
    }

    public int ID
    {
        get { return id; }
    }
    public string ICON
    {
        get { return icon; }
    }
    public string NAME
    {
        get { return name; }
    }
    public int PRICE
    {
        get { return price; }
    }
    public string EFFECT
    {
        get { return effect; }
    }
}
