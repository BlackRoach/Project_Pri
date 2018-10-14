using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList  {

    private int id; 
    private int phaseMaxCount; // 상점 전체 페이지수
    private int shopPhase; // 상점 페이지 숫자
    private string shopName; // 상점 이름
    private string shopCharacter; // 상점 캐릭터 이미지 파일명
    private string characterNameKr; // 상점 캐릭터 한국어 이름
    private int shopTalkID; // 상점 캐릭터 대사 ID
    private int phaseInCount; // 상점 페이지당 아이템 개수

    private List<ShopItem> shopItemList; // 상점 페이지당 아이템 리스트

    public ShopList(int id, int phaseMaxCount, int shopPhase,
        string shopName, string shopCharacter, string characterNameKr,
        int shopTalkID, int phaseInCount, List<ShopItem> shopItemList)
    {
        this.id = id;
        this.phaseMaxCount = phaseMaxCount;
        this.shopPhase = shopPhase;
        this.shopName = shopName;
        this.shopCharacter = shopCharacter;
        this.characterNameKr = characterNameKr;
        this.shopTalkID = shopTalkID;
        this.phaseInCount = phaseInCount;

        this.shopItemList = new List<ShopItem>();

        for(int i = 0; i < shopItemList.Count; i++)
        {
            this.shopItemList.Add(shopItemList[i]);
        }
    }

    public int ID
    {
        get { return id; }
    }
    public int PHASE_MAX_COUNT
    {
        get { return phaseMaxCount; }
    }
    public int SHOP_PHASE
    {
        get { return shopPhase; }
    }
    public string SHOP_CHARACTER // NPC 이미지 파일이름
    {
        get { return shopCharacter; }
    }
    public string CHARACTER_NAME_KR
    {
        get { return characterNameKr; }
    }
    public int SHOP_TALK_ID
    {
        get { return shopTalkID; }
    }
    public List<ShopItem> SHOP_ITEM_LIST
    {
        get { return shopItemList; }
    }
}
