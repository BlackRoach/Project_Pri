using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;

public class ShopManager : MonoBehaviour {

    private List<ShopList> shopList;
    private List<ShopTalkList> shopTalkList;
    public List<ShopItemSlot> ItemSlots; // 6개의 아이템 슬롯
    public List<GameObject> tabs; // 5개의 탭

    public Sprite activeTap; // 활성화된 탭 그림
    public Sprite inactiveTap; // 비활성화된 탭 그림
    private int curActiveTap; // 현재 활성화된 탭 번호
    private void Awake()
    {
        SHOP_LIST_LOAD();
        SHOP_TALK_LIST_LOAD();
        curActiveTap = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
    private void SHOP_LIST_LOAD()
    {
        TextAsset file = Resources.Load<TextAsset>("JsonDB/SHOP_LIST");
        JsonData shopData = JsonMapper.ToObject(file.text);

        //Debug.Log("CHARACTER_NAME_KR : " + shopData[0]["CHARACTER_NAME_KR"]);

        shopList = new List<ShopList>();
        List<ShopItem> tmpItem = new List<ShopItem>();

        for(int i = 0; i < shopData.Count; i++)
        {
            int itemCount = JsonDataToInt(shopData[i]["PHASE_IN_COUNT"]); // 아이템개수
            //Debug.Log(itemCount);
            for (int j = 1; j <= itemCount; j++)
            {
                ShopItem tmp = new ShopItem(
                    JsonDataToInt(shopData[i]["ITEM_" + j + "_ID"]),
                    shopData[i]["ITEM_" + j + "_ICON"].ToString(),
                    shopData[i]["ITEM_" + j + "_NAME"].ToString(),
                    JsonDataToInt(shopData[i]["ITEM_" + j + "_PRICE"]),
                    shopData[i]["ITEM_" + j + "_EFFECT"].ToString()
                );
                tmpItem.Add(tmp);
            }

            ShopList tmpShop = new ShopList(
                JsonDataToInt(shopData[i]["ID"]),
                JsonDataToInt(shopData[i]["PHASE_MAX_COUNT"]),
                JsonDataToInt(shopData[i]["SHOP_PHASE"]),
                shopData[i]["SHOP_NAME"].ToString(),
                shopData[i]["SHOP_CHARACTER"].ToString(),
                shopData[i]["CHARACTER_NAME_KR"].ToString(),
                JsonDataToInt(shopData[i]["SHOP_TALK_ID"]),
                JsonDataToInt(shopData[i]["PHASE_IN_COUNT"]),
                tmpItem
            );

            shopList.Add(tmpShop);
            tmpItem.Clear();
        }

        //Debug.Log(shopList.Count);
        //Debug.Log(shopList[0].SHOP_ITEM_LIST.Count);

    }

    private void SHOP_TALK_LIST_LOAD()
    {
        TextAsset file = Resources.Load<TextAsset>("JsonDB/SHOP_TALK_LIST");
        JsonData talkData = JsonMapper.ToObject(file.text);

        //Debug.Log("TALK_1 : " + talkData[0]["TALK_1"]);

        shopTalkList = new List<ShopTalkList>();
        List<string> tmpMessages = new List<string>();

        for(int i = 0; i < talkData.Count; i++)
        {
            for(int j=1; j <=4; j++)
            {
                string message = talkData[i]["TALK_" + j.ToString()].ToString();
                tmpMessages.Add(message);
            }

            ShopTalkList tmpTalk = new ShopTalkList(
                JsonDataToInt(talkData[i]["ID"]),
                talkData[i]["TALK_GROUP_NAME"].ToString(),
                tmpMessages
            );

            shopTalkList.Add(tmpTalk);
            tmpMessages.Clear();
        }

        //Debug.Log(shopTalkList.Count);
        //Debug.Log(shopTalkList[0].TALK_LIST.Count);
    }

    private int JsonDataToInt(JsonData json)
    {
        return Int32.Parse(json.ToString());
    }

    // 탭을 선택하면 탭모양이 바뀐다.
    public void ChangeTab(int tabNumber)
    {
        ChangeTabProperties("#685247FF", inactiveTap); // 비활성화
        curActiveTap = tabNumber;
        ChangeTabProperties("#E4C496FF", activeTap); // 활성화
    }

    private void ChangeTabProperties(string hexColor, Sprite image)
    {
        Color tmpColor;

        tabs[curActiveTap].GetComponent<Image>().sprite = image;
        tabs[curActiveTap].GetComponent<Image>().SetNativeSize();
        ColorUtility.TryParseHtmlString(hexColor, out tmpColor); // Hex Color 바꾸기
        tabs[curActiveTap].transform.GetComponentInChildren<Text>().color = tmpColor;
    }

    // 아이템 배치
    public void DeployItems(int shopID)
    {
        int id = shopID; // 상점 ID
        int phase = 1; // 상점 페이지 (SHOP_PHASE)
        int itemNumber = 0; // 찾은 아이템 숫자

        // 선택된 탭에 해당하는 상점에 접근한다. ID와 Phase로 구분한다.
        ShopList tmpShop = null;
        for(int i = 0; i < shopList.Count; i++)
        {
            if(shopList[i].ID == id && shopList[i].SHOP_PHASE == phase)
            {
                tmpShop = shopList[i];
                break;
            }
        }

        // 상점에서 아이템 정보를 받아서 슬롯에 넣어준다.
        List<ShopItem> tmpItem = tmpShop.SHOP_ITEM_LIST;
        for(int i = 0; i < tmpItem.Count; i++)
        {
            ItemSlots[i].ChangeValues(
                tmpItem[i].NAME,
                tmpItem[i].PRICE.ToString(),
                tmpItem[i].EFFECT,
                tmpItem[i].ICON
            );
            itemNumber++;
        }

        for(int i = itemNumber; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].ChangeValues(
                "", "", "", "");
        }

    }
    // SHOP_LIST Excel 파일의 
    // - ITEM_?_PRICE 부분 맨뒤에 G 없애야 함.
    // - ITEM_?_ICON 부분 맨뒤에 확장자 .png 없애야 함.
}
