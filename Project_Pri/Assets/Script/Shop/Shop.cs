using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

    public GameObject SlotPrefab; // 일반 아이템 슬롯
    public GameObject CashItemSlotPrefab; // 과금 아이템 슬롯
    public GameObject SpendCashSlotPrefab; // 과금 아이템 사용 슬롯
    public GameObject Shop1Panel;
    public GameObject Shop2Panel;
    public GameObject Shop3Panel;
    public GameObject Shop1ScrollRect;
    public GameObject Shop2ScrollRect;
    public GameObject Shop3ScrollRect;
    public GameObject Shop1Label;
    public GameObject Shop2Label;
    public GameObject Shop3Label;
    public Scrollbar Scrollbar1;
    public Scrollbar Scrollbar2;
    public Scrollbar Scrollbar3;

    private Item_Json_DataBase item_Database;

    private static int money = 2000; // Static 변수는 씬이 바껴도 값이 유지된다.
    private static int diamond = 0; 
    public Text moneyTxt;
    public Text diamondTxt;
    public GameObject DialoguePanelAskIfBuy;
    public GameObject DialoguePanelInvenFull;
    private int price = 0;

    private Items_Info itemInfo;

    // Script Excution Order 사용함 
    // Edit - Project Settings -Script Excution Order

    // Use this for initialization
    void Start () {
        Scrollbar1.size = 0.05f;
        Scrollbar2.size = 0.05f;

        item_Database = GameObject.Find("ItemJsonDataBase").
            GetComponent<Item_Json_DataBase>();

        for(int i=0; i<5; i++)
        {
            AddItem(30001, Shop1ScrollRect);
            AddItem(30002, Shop1ScrollRect);
            AddItem(30003, Shop1ScrollRect);
        }

        for (int i = 0; i < 5; i++)
        {
            AddItem(30003, Shop2ScrollRect);
            AddItem(30002, Shop2ScrollRect);
            AddItem(30001, Shop2ScrollRect);
        }

        AddCashItem();
        AddItemSpendingDia();
        moneyTxt.text = "보유골드 : " + money.ToString();
        diamondTxt.text = "다이아 : " + diamond.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SendValueFromSRectToSBar1()
    {
        Scrollbar1.value = Shop1ScrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition;
    }

    public void SendValueFromSBarToSRect1()
    {
        Shop1ScrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition = Scrollbar1.value;
    }

    public void SendValueFromSRectToSBar2()
    {
        Scrollbar2.value = Shop2ScrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition;
    }

    public void SendValueFromSBarToSRect2()
    {
        Shop2ScrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition = Scrollbar2.value;
    }

    private void AddItem(int _id, GameObject ShopScrollRect)
    {
        Items_Info add_Item = item_Database.Search_For_Item(_id);
        GameObject tmp = Instantiate(SlotPrefab, ShopScrollRect.transform);
        //tmp.transform.SetParent(ShopPanel.transform); // 안드로이드폰에서 슬롯이 작아지는 문제가 생김
        ShopSlot slot = tmp.GetComponent<ShopSlot>();
        //Debug.Log(add_Item.name);
        slot.nameTxt.text = add_Item.name; // 이름 입력
        slot.priceTxt.text = "가격 : " + add_Item.price.ToString(); // 가격 입력
        slot.infoTxt.text = add_Item.description; // 설명 입력
        slot.itemImage.sprite = add_Item.item_Img; // 이미지 입력
        slot.id = _id; // ID 입력
    }

    // 현금 구매 아이템 추가 (다이아 추가 아이템)
    private void AddCashItem()
    {
        GameObject tmp = Instantiate(CashItemSlotPrefab, Shop3ScrollRect.transform);
        ShopSlot slot = tmp.GetComponent<ShopSlot>();
        slot.nameTxt.text = "다이아10";
        slot.priceTxt.text = "현금 : 1000원";
        slot.infoTxt.text = "유료아이템을 구입하기 위한 다이아10개";
        slot.itemImage.color = new Color(0, 0, 0, 0);
    }

    // 다이아 소비 아이템 추가
    private void AddItemSpendingDia()
    {
        GameObject tmp = Instantiate(SpendCashSlotPrefab, Shop3ScrollRect.transform);
        ShopSlot slot = tmp.GetComponent<ShopSlot>();
        slot.nameTxt.text = "다이아 삭감";
        slot.priceTxt.text = "다이아 1개";
        slot.infoTxt.text = "다이아 1개 삭감";
        slot.itemImage.color = new Color(0, 0, 0, 0);
    }

    public void BuyItem(int _id)
    {
        itemInfo = item_Database.Search_For_Item(_id);
        int price = itemInfo.price;
        Debug.Log("DataTunnel의 아이템 개수 : " + DataTunnel.ItemInfos.Count);
        if (money < price)
        {
            Debug.Log("돈이 부족합니다.");
            return;
        }
        if(DataTunnel.ItemInfos.Count >= 3)
        {
            DialoguePanelInvenFull.SetActive(true);
            return;
        }
        this.price = price;
        DialoguePanelAskIfBuy.SetActive(true);
    }

    public void Yes()
    {
        money -= price;
        moneyTxt.text = "보유골드 : " + money.ToString();
        DialoguePanelAskIfBuy.SetActive(false);
        DataTunnel.AddItem(itemInfo); // 인벤토리에 넣기위해 장바구니에 아이템을 넣는다.
        price = 0;
    }

    public void No()
    {
        DialoguePanelAskIfBuy.SetActive(false);
    }

    public void OK()
    {
        DialoguePanelInvenFull.SetActive(false);
    }

    public void AddDiamond()
    {
        diamond += 10;
        diamondTxt.text = "다이아 : " + diamond;
    }

    public void SpendDia()
    {
        if(diamond > 0)
            diamond--;
        diamondTxt.text = "다이아 : " + diamond;

    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Shop_Result");
    }

    public void ActivateShop1()
    {
        Shop1Panel.SetActive(true);
        Shop2Panel.SetActive(false);
        Shop3Panel.SetActive(false);
        Shop1Label.GetComponent<Image>().color = Color.yellow;
        Shop2Label.GetComponent<Image>().color = Color.white;
        Shop3Label.GetComponent<Image>().color = Color.white;
    }

    public void ActivateShop2()
    {
        Shop1Panel.SetActive(false);
        Shop2Panel.SetActive(true);
        Shop3Panel.SetActive(false);
        Shop1Label.GetComponent<Image>().color = Color.white;
        Shop2Label.GetComponent<Image>().color = Color.yellow;
        Shop3Label.GetComponent<Image>().color = Color.white;
    }

    public void ActivateShop3()
    {
        Shop1Panel.SetActive(false);
        Shop2Panel.SetActive(false);
        Shop3Panel.SetActive(true);
        Shop1Label.GetComponent<Image>().color = Color.white;
        Shop2Label.GetComponent<Image>().color = Color.white;
        Shop3Label.GetComponent<Image>().color = Color.yellow;
    }



}
