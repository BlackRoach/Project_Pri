using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class ShopManager : MonoBehaviour {

    enum KIND_OF_MESSAGEBOX
    {
        ASK_BUY, NOT_ENOUGH_MONEY, INVENTORY_FULL,
    }
    public static ShopManager instance;

    private List<ShopList> shopList; // 상점 정보 목록
    private List<ShopTalkList> shopTalkList; // NPC 대사 목록
    public List<ShopItemSlot> ItemSlots; // 6개의 아이템 슬롯
    public List<GameObject> tabs; // 5개의 탭

    public Inventory_Add_Item inventoryAddItem;
    public Sprite activeTap; // 활성화된 탭 그림
    public Sprite inactiveTap; // 비활성화된 탭 그림
    public Image npcImage; // 상점 NPC 초상화
    public Text npcName; // 상점 NPC 이름표
    public Text npcMessage; // 상점 NPC 대화창
    public Text pageText; // 상점 페이지 번호 텍스트
    public Text goldText;
    public GameObject DialogPanel; // 대화상자 패널
    public GameObject NotEnoughMoney; // 소지금액부족 대화상자
    public GameObject AskBuyItem; // 구매 여부 대화상자
    public GameObject InventoryFull; // 인벤토리 가득참 대화상자
    public Image buyItemImage;
    public Text buyItemInfo;
    public Text buyItemEffect;
    public Text buyItemPrice;

    public int gold;
    private int curActiveTap; // 현재 활성화된 탭 번호
    private int shopID;
    private int openedPage; // 펼쳐진 페이지
    private ShopList activeShop; // 활성화 되어있는 상점정보
    private ShopItem selectedItem; // 선택된 아이템

    

    private void Awake()
    {
        instance = this;

        SHOP_LIST_LOAD();
        SHOP_TALK_LIST_LOAD();
        curActiveTap = 0;
        shopID = 501;
        openedPage = 1;
        gold = 1100;
        goldText.text = gold.ToString();
        selectedItem = null;
 
        ChangeShop(shopID);
        ChangeTab(0);
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
        
        return int.Parse(json.ToString());

        
    }

    // 탭을 선택하면 여러가지가 바뀐다.
    public void ChangeTab(int tabNumber)
    {
        ChangeTabProperties("#685247FF", inactiveTap); // 이전탭 비활성화
        curActiveTap = tabNumber;
        ChangeTabProperties("#E4C496FF", activeTap); // 현재탭 활성화

        NPC_Change();
    }

    // 탭 모양을 바꾼다.
    private void ChangeTabProperties(string hexColor, Sprite image)
    {
        Color tmpColor;

        tabs[curActiveTap].GetComponent<Image>().sprite = image;
        tabs[curActiveTap].GetComponent<Image>().SetNativeSize();
        ColorUtility.TryParseHtmlString(hexColor, out tmpColor); // Hex Color 바꾸기
        tabs[curActiveTap].transform.GetComponentInChildren<Text>().color = tmpColor;
    }

    // 상점 NPC 를 바꾼다.
    private void NPC_Change()
    {
        string characterImage = activeShop.SHOP_CHARACTER;
        npcImage.sprite = Resources.Load<Sprite>("KKT_Resources/Shop/" + characterImage);
        npcName.text = activeShop.CHARACTER_NAME_KR;

        ChangeMessage(0);
    }

    // 아이템 배치
    private void DeployItem()
    {
        int itemNumber = 0; // 찾은 아이템 숫자

        // 선택된 탭에 해당하는 상점에 접근한다. ID와 Phase로 구분한다.
        for (int i = 0; i < shopList.Count; i++)
        {
            if (shopList[i].ID == shopID && shopList[i].SHOP_PHASE == openedPage)
            {
                activeShop = shopList[i];
                break;
            }
        }

        // 상점에서 아이템 정보를 받아서 슬롯에 넣어준다.
        List<ShopItem> tmpItem = activeShop.SHOP_ITEM_LIST;
        for (int i = 0; i < tmpItem.Count; i++)
        {
            ItemSlots[i].ChangeValues(
                tmpItem[i].ID,
                tmpItem[i].NAME,
                tmpItem[i].PRICE.ToString(),
                tmpItem[i].EFFECT,
                tmpItem[i].ICON
            );
            itemNumber++;
        }

        for (int i = itemNumber; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].ChangeValues(-1, "", "", "", "");
        }
    }

    // 상점 변경
    public void ChangeShop(int shopID)
    {
        this.shopID = shopID; // 상점 ID
        openedPage = 1; // 상점 페이지 (SHOP_PHASE)
        
        DeployItem();
        ChangePageNumber();
    }

    // 화면상에 표시되는 상점 페이지 숫자를 바꾼다.
    private void ChangePageNumber()
    {
        pageText.text = openedPage + " / " + activeShop.PHASE_MAX_COUNT;
    }

    // 상점아이템 목록을 앞페이지로 넘기기
    public void PageChangeFront()
    {
        if (openedPage == 1) return;

        openedPage--;
        ChangePageNumber();
        DeployItem();
    }

    // 상점아이템 목록을 뒤페이지로 넘기기
    public void PageChangeBehind()
    {
        if (openedPage == activeShop.PHASE_MAX_COUNT) return;

        openedPage++;
        ChangePageNumber();
        DeployItem();
    }

    public void SelectItem(int id)
    {
        if (id == -1) return;

        
        for(int i = 0; i < activeShop.SHOP_ITEM_LIST.Count; i++)
        {
            if(activeShop.SHOP_ITEM_LIST[i].ID == id)
            {
                selectedItem = activeShop.SHOP_ITEM_LIST[i];
                break;
            }
        }

        if(gold < selectedItem.PRICE)
        {
            ChangeMessage(3);
            OpenDialogueBox(KIND_OF_MESSAGEBOX.NOT_ENOUGH_MONEY);
            return;
        }
        else if(inventoryAddItem.current_Index >= 20)
        {
            OpenDialogueBox(KIND_OF_MESSAGEBOX.INVENTORY_FULL);
            return;
        }
        
        ChangeMessage(Random.Range(1,3)); // 1 ~ 2 랜덤
        OpenDialogueBox(KIND_OF_MESSAGEBOX.ASK_BUY);

    }

    private void ChangeMessage(int talkNum)
    {
        for (int i = 0; i < shopTalkList.Count; i++)
        {
            if (activeShop.SHOP_TALK_ID == shopTalkList[i].ID)
            {
                npcMessage.text = shopTalkList[i].TALK_LIST[talkNum];
                break;
            }
        }
    }

    private void OpenDialogueBox(KIND_OF_MESSAGEBOX message)
    {
        DialogPanel.SetActive(true);

        switch(message)
        {
            case KIND_OF_MESSAGEBOX.ASK_BUY:
                buyItemImage.sprite = Resources.Load<Sprite>("KKT_Resources/Shop/"
                    + selectedItem.ICON);
                buyItemInfo.text = Item_Json_DataBase.
                    instance.Search_For_Item(selectedItem.ID).description;
                buyItemEffect.text = selectedItem.EFFECT;
                buyItemPrice.text = selectedItem.PRICE.ToString();
                AskBuyItem.SetActive(true);
                break;
            case KIND_OF_MESSAGEBOX.INVENTORY_FULL:
                InventoryFull.SetActive(true);
                break;
            case KIND_OF_MESSAGEBOX.NOT_ENOUGH_MONEY:
                NotEnoughMoney.SetActive(true);
                break;
        }

    }

    public void CloseDialogueBox()
    {
        DialogPanel.SetActive(false);
        NotEnoughMoney.SetActive(false);
        AskBuyItem.SetActive(false);
        InventoryFull.SetActive(false);
    }

    // 구매 버튼 클릭
    public void BuyItem()
    {
        gold -= selectedItem.PRICE;
        goldText.text = gold.ToString();
        inventoryAddItem.Add_Item_Value(selectedItem.ID);
        CloseDialogueBox();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }

    // <수정사항>
    // SHOP_LIST Excel 파일 
    // - SHOP_CHARACTER 부분에 확장자 .png 없애야 함.
}
