using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {

    public Text infoTxt;
    public Text nameTxt;
    public Text priceTxt;
    public Image itemImage;

    public int id;
   
    private Shop shop;
    private IAPManager iapManager;

    // Prefab을 Instantiate 하고 shop 클래스를 연결해준다.
    public void Start()
    {
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        iapManager = GameObject.Find("IAPManager").GetComponent<IAPManager>();
    }

    public void BuyItem()
    {
        shop.BuyItem(id);
    }

    public void BuyCashItem()
    {
        iapManager.BuyConsumable();
    }

    public void SpendDia()
    {
        shop.SpendDia();
    }
}

// 시작시에 과금 아이템 슬롯을 생성한다.

// 과금 아이템 슬롯을 클릭한다.
// 다이아가 10 증가한다.

