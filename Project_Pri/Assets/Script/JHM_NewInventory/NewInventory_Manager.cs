﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
public class NewInventory_Manager : MonoBehaviour
{
    public static NewInventory_Manager instance = null;
    public GameObject rena_Attire_Status_Panel; // 의상 패널
    public GameObject party_Status_Panel; // 모험 패널
    public GameObject inventory_Panel; // 인벤토리 패널
    public GameObject item_Status_Panel; // 아이템 상세설명 패널
    public GameObject inventory_Type_1, inventory_Type_2, inventory_Type_3; // 인벤토리 의상 , 퀘스트 , 잡화 3개 구성
    public GameObject slot_Num; // 인벤토리안에 소모품이나 아이템을 다쓴경우 삭제하기 위해 만든 변수
    private GameObject rena_Character_Image_Panel; // 레나 케릭터 이미지 패널
    private GameObject rena_Attire_Status_Text; // 레나 의상 스테이터스 텍스트 관련 패널
    private GameObject party_Status_Text; // 파티 스테이터스 텍스트 관련 패널
    private GameObject party_Face_Icon_Member; // 파티 맴버 Face Icon parent
    private GameObject party_character_Skills; // 파티 맴버 skills parent
    private GameObject text_Skills_Panel; // 각 케릭터의 스킬 Description
    private int Current_Page_1, Current_Page_2, Current_Page_3; // 페이지 현재 넘버
    // 디폴트 json_data
    private JsonData rena_Part_Data;
    private JsonData party_List_Data;
    private JsonData skills_Data;
    private Items_List selected_Item; // 인벤토리에서 선택한 아이템 
    private int[] rena_Item_Value = new int[12]; // 아이템 착용시 레나 스테이터스 장착 value
    private int[] party_Member_Item_Value = new int[7]; // 아이템 착용시 파티 스테이터스 장착 value
    private int selected_Party_Member; // 선택한 파티 맴버 케릭터
    private int attire_Id, mood, old;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        rena_Character_Image_Panel = rena_Attire_Status_Panel.transform.GetChild(0).gameObject;
        rena_Attire_Status_Text = rena_Attire_Status_Panel.transform.GetChild(3).gameObject;
        party_Status_Text = party_Status_Panel.transform.GetChild(3).gameObject;
        party_Face_Icon_Member = party_Status_Panel.transform.GetChild(6).gameObject;
        party_character_Skills = party_Status_Panel.transform.GetChild(5).gameObject;
        text_Skills_Panel = party_Status_Panel.transform.GetChild(7).gameObject;
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_Items_Data.instance.Selected_Save_Type_Include_Defualt_Data();
    }
    private void Start()
    {
        selected_Party_Member = 1;
        for (int i = 0; i < rena_Item_Value.Length; i++)
        {
            rena_Item_Value[i] = 0;
        }
        for (int i = 0; i < party_Member_Item_Value.Length; i++)
        {
            party_Member_Item_Value[i] = 0;
        }
        selected_Item = new Items_List();
        Json_Data_Parsing();
        rena_Attire_Status_Panel.SetActive(true);
        inventory_Panel.SetActive(true);
        item_Status_Panel.SetActive(false);
        party_Status_Panel.SetActive(false);
        text_Skills_Panel.SetActive(false);
        Setting_Rena_Attire_Status_Text_Input();
        Setting_Party_Status_Text_Input();
        Party_Face_Icon_Member_Image_Input();
        Rena_Attire_Setting();
        Party_Member_Selected_Character_Skills_Tree();
        Rena_Attire_Equip_Item();
        Current_Page_1 = Current_Page_2 = Current_Page_3 = 1;
        inventory_Type_1.transform.GetChild(5).GetComponent<Text>().text = Current_Page_1.ToString() + " / 5";
        inventory_Type_2.transform.GetChild(5).GetComponent<Text>().text = Current_Page_2.ToString() + " / 5";
        inventory_Type_3.transform.GetChild(5).GetComponent<Text>().text = Current_Page_3.ToString() + " / 5";
        Button_Pressed_Inventory_Type_1();
    }
    // defualt_Json_Data_Parsing
    private void Json_Data_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/RENA_PARTS");
        rena_Part_Data = JsonMapper.ToObject(json_File_1.text);
        TextAsset json_File_2 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/PARTY_LIST");
        party_List_Data = JsonMapper.ToObject(json_File_2.text);
        TextAsset json_File_3 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/SKILL_LIST");
        skills_Data = JsonMapper.ToObject(json_File_3.text);
    }
    // 레나 의상 스테이터스 on / off 용 버튼
    public void Button_Pressed_Showing_Rena_Status()
    {
        if (!rena_Attire_Status_Text.activeInHierarchy)
        {
            rena_Attire_Status_Text.SetActive(true);
        }
        else
        {
            rena_Attire_Status_Text.SetActive(false);
        }
    }
    // 파티 맴버 Image_Icon 푸싱
    private void Party_Face_Icon_Member_Image_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                        party_Face_Icon_Member.transform.GetChild(i).GetChild(0).GetComponent<Image>().SetNativeSize();
                    }
                }
                break;
            case 2:
                {
                    for (int i = 4; i < 8; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i - 4).GetChild(0).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                        party_Face_Icon_Member.transform.GetChild(i - 4).GetChild(0).GetComponent<Image>().SetNativeSize();
                    }
                }
                break;
            case 3:
                {
                    for (int i = 8; i < 12; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i - 8).GetChild(0).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                        party_Face_Icon_Member.transform.GetChild(i - 8).GetChild(0).GetComponent<Image>().SetNativeSize();
                    }
                }
                break;
        }
    }
    // 파티 맴버 창에서 케릭터를 선택할 경우 모든 정보는 선택한 케릭터정보로 바뀜 (스테이터스창 , 스킬창 , 착용아이템 )
    public void Button_Selected_Party_Member_First()
    {
        selected_Party_Member = 1;
        Party_Member_Selected_Character_Skills_Tree();
        Setting_Party_Status_Text_Input();
    }
    public void Button_Selected_Party_Member_Second()
    {
        selected_Party_Member = 2;
        Party_Member_Selected_Character_Skills_Tree();
        Setting_Party_Status_Text_Input();
    }
    public void Button_Selected_Party_Member_Thirth()
    {
        selected_Party_Member = 3;
        Party_Member_Selected_Character_Skills_Tree();
        Setting_Party_Status_Text_Input();
    }
    public void Button_Selected_Party_Member_Four()
    {
        selected_Party_Member = 4;
        Party_Member_Selected_Character_Skills_Tree();
        Setting_Party_Status_Text_Input();
    }
    // 파티 맴버의 선택한 케릭터의 소유하고있는 스킬트리 and SD 케릭터 모델
    private void Party_Member_Selected_Character_Skills_Tree()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Selected_Party_Member_Skills_Setting(selected_Party_Member - 1);
                }
                break;
            case 2:
                {
                    Selected_Party_Member_Skills_Setting(selected_Party_Member + 3);
                }
                break;
            case 3:
                {
                    Selected_Party_Member_Skills_Setting(selected_Party_Member + 7);
                }
                break;
        }
    }
    // 파티맴버 스킬 and SD 케릭터 모델 이미지 푸싱
    private void Selected_Party_Member_Skills_Setting(int i)
    {
        // 스킬
        party_character_Skills.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "skill_icon_" +
                        NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK1);
        party_character_Skills.transform.GetChild(0).GetChild(0).gameObject.name = NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK1.ToString();
        party_character_Skills.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
        party_character_Skills.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40f);
        party_character_Skills.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "skill_icon_" +
                        NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK2);
        party_character_Skills.transform.GetChild(1).GetChild(0).gameObject.name = NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK2.ToString();
        party_character_Skills.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
        party_character_Skills.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40f);
        party_character_Skills.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "skill_icon_" +
                        NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK3);
        party_character_Skills.transform.GetChild(2).GetChild(0).gameObject.name = NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK3.ToString();
        party_character_Skills.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40f);
        party_character_Skills.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40f);
        // sd 케릭터 모델
        party_Status_Panel.transform.GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].SD_CHARACTER_MODEL);
        // Equip Items 장착아이템 무기 
        if ((int)NewInventory_JsonData.instance.party_Status[i].WEAPON_ID != 0)
        {
            if (party_Status_Panel.transform.GetChild(1).childCount == 2)
            {
                Destroy(party_Status_Panel.transform.GetChild(1).transform.GetChild(1).gameObject);
            }
            GameObject weapon = Instantiate(NewInventory_Items_Data.instance.item_Prefab);
            weapon.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "item_icon_" +
                            NewInventory_JsonData.instance.party_Status[i].WEAPON_ID);
            weapon.transform.parent = party_Status_Panel.transform.GetChild(1).transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = new Vector3(1f, 1f, 1f);
            weapon.transform.GetComponent<Image>().SetNativeSize();
        }
        else
        {
            if (party_Status_Panel.transform.GetChild(1).childCount == 2)
            {
                Destroy(party_Status_Panel.transform.GetChild(1).transform.GetChild(1).gameObject);
            }
        }
        // Equip Items 장착아이템 아머
        if ((int)NewInventory_JsonData.instance.party_Status[i].ARMOR_ID != 0)
        {
            if (party_Status_Panel.transform.GetChild(2).childCount == 2)
            {
                Destroy(party_Status_Panel.transform.GetChild(2).transform.GetChild(1).gameObject);
            }
            GameObject armor = Instantiate(NewInventory_Items_Data.instance.item_Prefab);
            armor.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "item_icon_" +
                            NewInventory_JsonData.instance.party_Status[i].ARMOR_ID);
            armor.transform.parent = party_Status_Panel.transform.GetChild(2).transform;
            armor.transform.localPosition = Vector3.zero;
            armor.transform.localScale = new Vector3(1f, 1f, 1f);
            armor.transform.GetComponent<Image>().SetNativeSize();
        }
        else
        {
            if (party_Status_Panel.transform.GetChild(2).childCount == 2)
            {
                Destroy(party_Status_Panel.transform.GetChild(2).transform.GetChild(1).gameObject);
            }
        }
        // 해고 버튼 true or false
        if (NewInventory_JsonData.instance.party_Status[i].DISMISSIBILITY_TYPE == 1)
        {
            party_Status_Panel.transform.GetChild(4).transform.gameObject.SetActive(false);
        }
        else if (NewInventory_JsonData.instance.party_Status[i].DISMISSIBILITY_TYPE == 0)
        {
            party_Status_Panel.transform.GetChild(4).transform.gameObject.SetActive(true);
        }
    }
    // 레나 착용 의상 로드
    private void Rena_Attire_Equip_Item()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Rena_Setting_Clothe(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1);
                }
                break;
            case 2:
                {
                    Rena_Setting_Clothe(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1);
                }
                break;
            case 3:
                {
                    Rena_Setting_Clothe(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1);
                }
                break;
        }
    }
    private void Rena_Setting_Clothe(int index)
    {
        rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                                    Resources.Load<Sprite>("JHM.Img/New_Inventory/" + "item_icon_"
                                    + NewInventory_JsonData.instance.rena_Attire_Status[index].ATTIRE_ID.ToString());
        rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().SetNativeSize();
    }
    // 레나 의상 스테이터스 값 푸싱
    private void Setting_Rena_Attire_Status_Text_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Rena_Attire_Status_Text(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
            case 2:
                {
                    Rena_Attire_Status_Text(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
            case 3:
                {
                    Rena_Attire_Status_Text(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
        }
    }
    private void Rena_Attire_Status_Text(int i)
    {
        rena_Item_Value[0] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_MUSCULAR_STRENGTH;
        rena_Item_Value[1] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_MAGIC_POWER;
        rena_Item_Value[2] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_STAMINA;
        rena_Item_Value[3] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_INTELLECT;
        rena_Item_Value[4] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_CHARM;
        rena_Item_Value[5] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_SENSE;
        rena_Item_Value[6] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_PRIDE;
        rena_Item_Value[7] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_ARTISTIC;
        rena_Item_Value[8] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_ELEGANCE;
        rena_Item_Value[9] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_MORALITY;
        rena_Item_Value[10] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_RELIABILITY;
        rena_Item_Value[11] = NewInventory_JsonData.instance.rena_Attire_Status[i - 1].EQUIP_STRESS;
        //--------------------------------------------------------------------
        rena_Attire_Status_Text.transform.GetChild(0).GetComponent<Text>().text = "근력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MUSCULAR_STRENGTH.ToString() + " + " + "(" + rena_Item_Value[0] + ")";
        rena_Attire_Status_Text.transform.GetChild(1).GetComponent<Text>().text = "마법력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MAGIC_POWER.ToString() + " + " + "(" + rena_Item_Value[1] + ")";
        rena_Attire_Status_Text.transform.GetChild(2).GetComponent<Text>().text = "체력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].STAMINA.ToString() + " + " + "(" + rena_Item_Value[2] + ")";
        rena_Attire_Status_Text.transform.GetChild(3).GetComponent<Text>().text = "지력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].INTELLECT.ToString() + " + " + "(" + rena_Item_Value[3] + ")";
        rena_Attire_Status_Text.transform.GetChild(4).GetComponent<Text>().text = "매력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].CHARM.ToString() + " + " + "(" + rena_Item_Value[4] + ")";
        rena_Attire_Status_Text.transform.GetChild(5).GetComponent<Text>().text = "센스: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].SENSE.ToString() + " + " + "(" + rena_Item_Value[5] + ")";
        rena_Attire_Status_Text.transform.GetChild(6).GetComponent<Text>().text = "자존감: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].PRIDE.ToString() + " + " + "(" + rena_Item_Value[6] + ")";
        rena_Attire_Status_Text.transform.GetChild(7).GetComponent<Text>().text = "예술성: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].ARTISTIC.ToString() + " + " + "(" + rena_Item_Value[7] + ")";
        rena_Attire_Status_Text.transform.GetChild(8).GetComponent<Text>().text = "기품: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].ELEGANCE.ToString() + " + " + "(" + rena_Item_Value[8] + ")";
        rena_Attire_Status_Text.transform.GetChild(9).GetComponent<Text>().text = "도덕: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MORALITY.ToString() + " + " + "(" + rena_Item_Value[9] + ")";
        rena_Attire_Status_Text.transform.GetChild(10).GetComponent<Text>().text = "신뢰: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].RELIABILITY.ToString() + " + " + "(" + rena_Item_Value[10] + ")";
        rena_Attire_Status_Text.transform.GetChild(11).GetComponent<Text>().text = "스트레스: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].STRESS.ToString() + " + " + "(" + rena_Item_Value[11] + ")";
    }
    // 파티 스테이터스 값 푸싱
    private void Setting_Party_Status_Text_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Party_Status_Text(selected_Party_Member - 1);
                }
                break;
            case 2:
                {
                    Party_Status_Text(selected_Party_Member + 3);
                }
                break;
            case 3:
                {
                    Party_Status_Text(selected_Party_Member + 7);
                }
                break;
        }
    }
    private void Party_Status_Text(int i)
    {
        party_Member_Item_Value[0] = NewInventory_JsonData.instance.party_Status[i].EQUIP_ATK;
        party_Member_Item_Value[1] = NewInventory_JsonData.instance.party_Status[i].EQUIP_DEF;
        party_Member_Item_Value[2] = NewInventory_JsonData.instance.party_Status[i].EQUIP_MAG;
        party_Member_Item_Value[3] = NewInventory_JsonData.instance.party_Status[i].EQUIP_REP;
        party_Member_Item_Value[4] = NewInventory_JsonData.instance.party_Status[i].EQUIP_SP;
        party_Member_Item_Value[5] = NewInventory_JsonData.instance.party_Status[i].EQUIP_SP2;
        party_Member_Item_Value[6] = NewInventory_JsonData.instance.party_Status[i].EQUIP_HP_MAX;
        // -------------------------------------------------------------------------------
        party_Status_Text.transform.GetChild(0).GetComponent<Text>().text = "Name: " +
            NewInventory_JsonData.instance.party_Status[i].PARTY_NAME.ToString();
        party_Status_Text.transform.GetChild(1).GetComponent<Text>().text = "Grade: " +
            NewInventory_JsonData.instance.party_Status[i].PARTY_GRADE.ToString();
        party_Status_Text.transform.GetChild(2).GetComponent<Text>().text = "FAME: " +
            NewInventory_JsonData.instance.party_Status[i].FAME.ToString();
        party_Status_Text.transform.GetChild(3).GetComponent<Text>().text = "ATK: " +
            NewInventory_JsonData.instance.party_Status[i].ATK.ToString() + " + " + "(" + party_Member_Item_Value[0] + ")";
        party_Status_Text.transform.GetChild(4).GetComponent<Text>().text = "DEF: " +
            NewInventory_JsonData.instance.party_Status[i].DEF.ToString() + " + " + "(" + party_Member_Item_Value[1] + ")";
        party_Status_Text.transform.GetChild(5).GetComponent<Text>().text = "MAG: " +
            NewInventory_JsonData.instance.party_Status[i].MAG.ToString() + " + " + "(" + party_Member_Item_Value[2] + ")";
        party_Status_Text.transform.GetChild(6).GetComponent<Text>().text = "REP: " +
            NewInventory_JsonData.instance.party_Status[i].REP.ToString() + " + " + "(" + party_Member_Item_Value[3] + ")";
        party_Status_Text.transform.GetChild(7).GetComponent<Text>().text = "SP: " +
            NewInventory_JsonData.instance.party_Status[i].SP.ToString() + " + " + "(" + party_Member_Item_Value[4] + ")";
        party_Status_Text.transform.GetChild(8).GetComponent<Text>().text = "SP2: " +
           NewInventory_JsonData.instance.party_Status[i].SP2.ToString() + " + " + "(" + party_Member_Item_Value[5] + ")";
        party_Status_Text.transform.GetChild(9).GetComponent<Text>().text = "HP: " +
           NewInventory_JsonData.instance.party_Status[i].HP.ToString() + "/" +
           NewInventory_JsonData.instance.party_Status[i].HP_MAX.ToString() + " + " + "(" + party_Member_Item_Value[6] + ")";
    }
    // 조건에 맞는 레나의 옷차림 세팅
    private void Rena_Attire_Setting()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        int _stress = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].STRESS;
        if (_stress >= 0 && _stress <= 30)
        {
            NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 1;
            Rena_Attire_Parts_Setting();
        }
        else if (_stress >= 31 && _stress <= 60)
        {
            NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 2;
            Rena_Attire_Parts_Setting();
        }
        else if (_stress >= 61 && _stress <= 200)
        {
            NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 3;
            Rena_Attire_Parts_Setting();
        }
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Rena_Attire_Status();
    }
    // 조건에 맞는 레나의 파츠 세팅
    private void Rena_Attire_Parts_Setting()
    {
        attire_Id = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].ATTIRE_ID;
        mood = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD;
        old = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].OLD;
        for (int i = 0; i < rena_Part_Data.Count; i++)
        {
            if (old == (int)rena_Part_Data[i]["OLD"] && mood == (int)rena_Part_Data[i]["MOOD"] && attire_Id == (int)rena_Part_Data[i]["ATTIRE_ID"])
            {
                // 바디 파츠
                rena_Character_Image_Panel.transform.GetChild(0).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["BODY_PARTS"].ToString());
                rena_Character_Image_Panel.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                // 의상 파츠
                rena_Character_Image_Panel.transform.GetChild(1).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["ATTIRE_PARTS"].ToString());
                rena_Character_Image_Panel.transform.GetChild(1).GetComponent<Image>().SetNativeSize();
                // 얼굴 파츠 1
                rena_Character_Image_Panel.transform.GetChild(2).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["MOOD_FACE_PARTS_1"].ToString());
                rena_Character_Image_Panel.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
                // 얼굴 파츠 2
                rena_Character_Image_Panel.transform.GetChild(3).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["MOOD_FACE_PARTS_2"].ToString());
                rena_Character_Image_Panel.transform.GetChild(3).GetComponent<Image>().SetNativeSize();
                Button_Pressed_Rena_Attire_Status(); // 레나 fade in 하면서 등장
                break;
            }
        }
    }
    // 레나 눈깜박임 함수
    IEnumerator Rena_Character_Image_Eye_Running()
    {
        yield return new WaitForSeconds(2f);
        rena_Character_Image_Panel.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        rena_Character_Image_Panel.transform.GetChild(3).gameObject.SetActive(false);
        StartCoroutine(Rena_Character_Image_Eye_Running());
    }
    IEnumerator Rena_Eye_Start()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Rena_Character_Image_Eye_Running()); // 레나 눈깜박임
    }
    // 레나 의상 패널  ON
    public void Button_Pressed_Rena_Attire_Status()
    {
        rena_Attire_Status_Panel.SetActive(true);
        party_Status_Panel.SetActive(false);
        // --------------------------------------
        // 레나 케릭터 fade in 하면서 등장
        StopAllCoroutines(); // 레나 눈깜박임 동작 멈춤 //  한번더 주는이유는 레나 fade in 모션이 이버튼 클릭햇을때 작동하기위해서 입니다
        rena_Character_Image_Panel.SetActive(false);
        rena_Character_Image_Panel.SetActive(true);
        StartCoroutine(Rena_Eye_Start()); // 레나 눈깜박임 동작 시작
    }
    // 레나 모험 패널  ON
    public void Button_Pressed_Party_Status()
    {
        // 의상 인벤토리 아이템 클릭햇을경우 모험페이지로 이동할때 사용 버튼 false 
        if (selected_Item.INVENTORY_TYPE == 1)
        {
            item_Status_Panel.transform.GetChild(2).gameObject.SetActive(false);
        }
        // --------------------------------------
        rena_Attire_Status_Panel.SetActive(false);
        party_Status_Panel.SetActive(true);
        // --------------------------------------
        rena_Character_Image_Panel.SetActive(false);
        StopAllCoroutines(); // 레나 눈깜박임 동작 멈춤
    }
    // 인벤토리 타입 1 (의상)
    public void Button_Pressed_Inventory_Type_1()
    {
        inventory_Panel.transform.GetChild(0).gameObject.SetActive(true);
        inventory_Panel.transform.GetChild(1).gameObject.SetActive(false);
        inventory_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    // 인벤토리 타입 2 (퀘스트)
    public void Button_Pressed_Inventory_Type_2()
    {
        inventory_Panel.transform.GetChild(0).gameObject.SetActive(false);
        inventory_Panel.transform.GetChild(1).gameObject.SetActive(true);
        inventory_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    // 인벤토리 타입 3 (잡화)
    public void Button_Pressed_Inventory_Type_3()
    {
        inventory_Panel.transform.GetChild(0).gameObject.SetActive(false);
        inventory_Panel.transform.GetChild(1).gameObject.SetActive(false);
        inventory_Panel.transform.GetChild(2).gameObject.SetActive(true);
    }
    // 인벤토리 페이지 1 ( 의 상 )
    public void Button_Inventory_Type_1_LeftArrow()
    {
        if (Current_Page_1 > 1 && Current_Page_1 <= 5)
        {
            Current_Page_1--;
            Inventory_Type_1_Setting(Current_Page_1);
        }
    }
    public void Button_Inventory_Type_1_RightArrow()
    {
        if (Current_Page_1 >= 1 && Current_Page_1 < 5)
        {
            Current_Page_1++;
            Inventory_Type_1_Setting(Current_Page_1);
        }
    }
    private void Inventory_Type_1_Setting(int page_Num)
    {
        inventory_Type_1.transform.GetChild(5).GetComponent<Text>().text = page_Num.ToString() + " / 5";
        for (int i = 0; i < 5; i++)
        {
            if (i == page_Num - 1)
            {
                inventory_Type_1.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                inventory_Type_1.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    // ---------------------------------------
    // 인벤토리 페이지 2 ( 퀘 스 트 )
    public void Button_Inventory_Type_2_LeftArrow()
    {
        if (Current_Page_2 > 1 && Current_Page_2 <= 5)
        {
            Current_Page_2--;
            Inventory_Type_2_Setting(Current_Page_2);
        }
    }
    public void Button_Inventory_Type_2_RightArrow()
    {
        if (Current_Page_2 >= 1 && Current_Page_2 < 5)
        {
            Current_Page_2++;
            Inventory_Type_2_Setting(Current_Page_2);
        }
    }
    private void Inventory_Type_2_Setting(int page_Num)
    {
        inventory_Type_2.transform.GetChild(5).GetComponent<Text>().text = page_Num.ToString() + " / 5";
        for (int i = 0; i < 5; i++)
        {
            if (i == page_Num - 1)
            {
                inventory_Type_2.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                inventory_Type_2.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    // ---------------------------------------
    // 인벤토리 페이지 3 ( 잡 화 )
    public void Button_Inventory_Type_3_LeftArrow()
    {
        if (Current_Page_3 > 1 && Current_Page_3 <= 5)
        {
            Current_Page_3--;
            Inventory_Type_3_Setting(Current_Page_3);
        }
    }
    public void Button_Inventory_Type_3_RightArrow()
    {
        if (Current_Page_3 >= 1 && Current_Page_3 < 5)
        {
            Current_Page_3++;
            Inventory_Type_3_Setting(Current_Page_3);
        }
    }
    private void Inventory_Type_3_Setting(int page_Num)
    {
        inventory_Type_3.transform.GetChild(5).GetComponent<Text>().text = page_Num.ToString() + " / 5";
        for (int i = 0; i < 5; i++)
        {
            if (i == page_Num - 1)
            {
                inventory_Type_3.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                inventory_Type_3.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    // ---------------------------------------
    // 인벤토리 아이템 상세 설명 창
    public void Item_Status_Info_Turn_On(Items_List select_Item)
    {
        item_Status_Panel.SetActive(true);
        // 만약 _item_UseType 이 1 일경우 사용버튼 true  2일 경우 false
        if (select_Item.ITEM_USETYPE == 1)
        {
            if (party_Status_Panel.gameObject.activeInHierarchy && inventory_Panel.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                item_Status_Panel.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                item_Status_Panel.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else if (select_Item.ITEM_USETYPE == 2)
        {
            item_Status_Panel.transform.GetChild(2).gameObject.SetActive(false);
        }
        // 이미지 
        item_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + select_Item.ITEM_ICON.ToString());
        item_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().SetNativeSize();
        // ------------------------------------
        // 설명 창 텍스트 
        item_Status_Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "[이름]: " + select_Item.ITEM_NAME.ToString();
        item_Status_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "[설명]: " + select_Item.ITEM_DESCRIPTION_1.ToString();
        item_Status_Panel.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "[효과]" + "\n" + select_Item.ITEM_DESCRIPTION_2.ToString();
        item_Status_Panel.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "[가격]" + "\n" + select_Item.ITEM_PRICE.ToString()
            + " 골드";
        // ------------------------------------
        selected_Item = select_Item;  // 아이템 사용버튼 누를시 어떤아이템인지 확인 item_value
    }
    // 아이템 사용 버튼 누를시 실행되는 함수 
    public void Button_Pressed_Item_EquipType_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        Inventory_Item_Data_Loading();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Item_EquipType_Value(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
            case 2:
                {
                    Item_EquipType_Value(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
            case 3:
                {
                    Item_EquipType_Value(NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE);
                }
                break;
        }
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Rena_Attire_Status();
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Party_Status();
        Inventory_Item_Data_Saving();
        Party_Face_Icon_Member_Image_Input();
        item_Status_Panel.SetActive(false);
        // 레나 복장 교체하면 작동
        if (attire_Id != NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].ATTIRE_ID
            || mood != NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD
            || old != NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].OLD)
        {
            Rena_Attire_Setting(); // 레나 의상 체인지와 fade in 하면서 등장
        }
    }
    // 아이템 사용시 의상 스테이터스 값  UP , DOWN
    private void Item_EquipType_Value(int save_Type)
    {
        switch (selected_Item.ITEM_EQUIPTYPE)
        {
            case 1: // 의상실 의상슬롯장착 ( 좋은옷 )
                {
                    //  이전 아이템
                    if (NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID != 0)
                    {
                        rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = null;
                        if (NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID == 30005)
                        {
                            NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].MUSCULAR_STRENGTH -= rena_Item_Value[0];
                            NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STAMINA -= rena_Item_Value[2];
                            NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].EQUIP_MUSCULAR_STRENGTH -= rena_Item_Value[0];
                            NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].EQUIP_STAMINA -= rena_Item_Value[2];
                        }
                        if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
                        {
                            NewInventory_Items_Data.instance.items_Data_1.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID, 1));
                        }
                        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
                        {
                            NewInventory_Items_Data.instance.items_Data_2.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID, 1));
                        }
                        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
                        {
                            NewInventory_Items_Data.instance.items_Data_3.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID, 1));
                        }
                        Inventory_Item_Data_Saving();
                        NewInventory_Items_Data.instance.Previous_Item_Put_Back_To_Inventory();
                    }
                    //  착용할 아이템
                    if (selected_Item.ID == 30005) // 좋은옷 착용
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 1) // 근력
                        {
                            if (selected_Item.UPDOWN_1 == 1)
                            { // 값을 더하라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].MUSCULAR_STRENGTH += selected_Item.ITEM_VALUE_1;
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].EQUIP_MUSCULAR_STRENGTH = selected_Item.ITEM_VALUE_1;
                                rena_Item_Value[0] = selected_Item.ITEM_VALUE_1;
                            }
                        }
                        if (selected_Item.ITEM_VALUETYPE_2 == 3) // 체력
                        {
                            if (selected_Item.UPDOWN_2 == 1)
                            { // 값을 더하라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STAMINA += selected_Item.ITEM_VALUE_2;
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].EQUIP_STAMINA = selected_Item.ITEM_VALUE_2;
                                rena_Item_Value[2] = selected_Item.ITEM_VALUE_2;
                            }
                        }
                        for (int i = 0; i < NewInventory_Items_Data.instance.item_List.Length; i++)
                        {
                            if (selected_Item.ID == 30005) // 좋은옷 
                            {
                                rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                                        Resources.Load<Sprite>("JHM.Img/New_Inventory/" + selected_Item.ITEM_ICON.ToString());
                                rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().SetNativeSize();
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID = selected_Item.ID;
                            }
                        }
                    }
                    else if (selected_Item.ID == 30004)  // 평상복 
                    {
                        for (int i = 0; i < NewInventory_Items_Data.instance.item_List.Length; i++)
                        {
                            if (selected_Item.ID == 30004) // 평상복 창작시
                            {
                                rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite =
                                        Resources.Load<Sprite>("JHM.Img/New_Inventory/" + selected_Item.ITEM_ICON.ToString());
                                rena_Attire_Status_Panel.transform.GetChild(1).GetChild(0).GetComponent<Image>().SetNativeSize();
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].ATTIRE_ID = selected_Item.ID;
                            }
                        }
                    }
                    Rena_Attire_Status_Text(save_Type); // 레나 스테이터스 텍스트
                    Destory_Selected_Item_Data();
                }
                break;
            case 2:  // 의상실 소모품
                {
                    if (selected_Item.ID == 30007) // 시집
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 6) // 센스
                        {
                            if (selected_Item.UPDOWN_1 == 1)
                            { // 값을 더하라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].SENSE += selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    else if (selected_Item.ID == 30014) // 빨간 책
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 12) // 스트레스
                        {
                            if (selected_Item.UPDOWN_1 == 2)
                            { // 값을 뻬라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STRESS -= selected_Item.ITEM_VALUE_1;
                            }
                            if (Limited_Less_Than_Zero(NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STRESS))
                            {
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STRESS = 0;
                            }
                        }
                    }
                    else if (selected_Item.ID == 30008) // 떡국
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 22) // OLD
                        {
                            if (selected_Item.UPDOWN_1 == 1)
                            { // 값을 더하라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].OLD += selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    else if (selected_Item.ID == 30009) // 시간의 비약
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 22) // OLD
                        {
                            if (selected_Item.UPDOWN_1 == 2)
                            { // 값을 뻬라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].OLD -= selected_Item.ITEM_VALUE_1;
                            }
                            if (Limited_Less_Than_Zero(NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].OLD))
                            {
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].OLD = 0;
                            }
                        }
                    }
                    else if (selected_Item.ID == 30013) // 상사의 사진
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 12) // OLD
                        {
                            if (selected_Item.UPDOWN_1 == 1)
                            { // 값을 더하라는 의미
                                NewInventory_JsonData.instance.rena_Attire_Status[save_Type - 1].STRESS += selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    Rena_Attire_Status_Text(save_Type); // 레나 스테이터스 텍스트
                    Destory_Selected_Item_Data();
                }
                break;
            case 3:  // 모험실 무기슬롯장착
                {
                    // 강철검
                    int _index = 0;
                    if (save_Type == 1)
                    {
                        _index = 0;
                    }
                    else if (save_Type == 2)
                    {
                        _index = 4;
                    }
                    else if (save_Type == 3)
                    {
                        _index = 8;
                    }
                    // 이전 아이템
                    if (NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].WEAPON_ID != 0)
                    {   // 착용전 무기 슬롯에 있는 아이템은 인벤토리로~~!  
                        if (party_Status_Panel.transform.GetChild(1).transform.childCount == 2)
                        {
                            Destroy(party_Status_Panel.transform.GetChild(1).transform.GetChild(1).gameObject);
                            NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ATK -=
                                 NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].EQUIP_ATK;
                            if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
                            {
                                NewInventory_Items_Data.instance.items_Data_1.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].WEAPON_ID, 1));
                            }
                            else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
                            {
                                NewInventory_Items_Data.instance.items_Data_2.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].WEAPON_ID, 1));
                            }
                            else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
                            {
                                NewInventory_Items_Data.instance.items_Data_3.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].WEAPON_ID, 1));
                            }
                            Inventory_Item_Data_Saving();
                            NewInventory_Items_Data.instance.Previous_Item_Put_Back_To_Inventory();
                        }
                    }
                    // 착용 아이템
                    if (selected_Item.ITEM_VALUETYPE_1 == 14)
                    {
                        if (selected_Item.UPDOWN_1 == 1)
                        { // 값을 더하라는 의미
                            NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ATK += selected_Item.ITEM_VALUE_1;
                            NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].WEAPON_ID = selected_Item.ID;
                            NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].EQUIP_ATK = selected_Item.ITEM_VALUE_1;
                        }
                    }
                    Party_Status_Text((_index + selected_Party_Member) - 1); // 파티 스테이터스 텍스트
                    Selected_Party_Member_Skills_Setting((_index + selected_Party_Member) - 1);
                    Destory_Selected_Item_Data();
                }
                break;
            case 4:  // 모험실 방어구 슬롯 장착
                {
                    int _index = 0;
                    if (save_Type == 1)
                    {
                        _index = 0;
                    }
                    else if (save_Type == 2)
                    {
                        _index = 4;
                    }
                    else if (save_Type == 3)
                    {
                        _index = 8;
                    }
                    // 이전 아이템
                    if (NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID != 0)
                    {    // 착용전 무기 슬롯에 있는 아이템은 인벤토리로~~!  
                        if (party_Status_Panel.transform.GetChild(2).transform.childCount == 2)
                        {
                            Destroy(party_Status_Panel.transform.GetChild(2).transform.GetChild(1).gameObject);
                            NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].DEF -=
                                      NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].EQUIP_DEF;
                            if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
                            {
                                NewInventory_Items_Data.instance.items_Data_1.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID, 1));
                            }
                            else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
                            {
                                NewInventory_Items_Data.instance.items_Data_2.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID, 1));
                            }
                            else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
                            {
                                NewInventory_Items_Data.instance.items_Data_3.Add(
                                new New_Item_Data(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID, 1));
                            }
                            Inventory_Item_Data_Saving();
                            NewInventory_Items_Data.instance.Previous_Item_Put_Back_To_Inventory();
                        }
                    }
                    // 착용 아이템
                    if (selected_Item.ID == 30001) // 노브 헬멧
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 15) // def
                        {
                            if (selected_Item.UPDOWN_1 == 1) // up
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].DEF += selected_Item.ITEM_VALUE_1;
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID = selected_Item.ID;
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].EQUIP_DEF = selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    if (selected_Item.ID == 30003) // 강철 방패
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 15) // def
                        {
                            if (selected_Item.UPDOWN_1 == 1) // hp
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].DEF += selected_Item.ITEM_VALUE_1;
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].ARMOR_ID = selected_Item.ID;
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].EQUIP_DEF = selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    Party_Status_Text((_index + selected_Party_Member) - 1); // 파티 스테이터스 텍스트
                    Selected_Party_Member_Skills_Setting((_index + selected_Party_Member) - 1);
                    Destory_Selected_Item_Data();
                }
                break;
            case 5: // 모험실 소모품
                {
                    int _index = 0;
                    if (save_Type == 1)
                    {
                        _index = 0;
                    } else if (save_Type == 2)
                    {
                        _index = 4;
                    }else if (save_Type == 3)
                    {
                        _index = 8;
                    }
                    if (selected_Item.ID == 30011) // 독약
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 20) // hp
                        {
                            if (selected_Item.UPDOWN_1 == 2) // value down
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].HP -= selected_Item.ITEM_VALUE_1;
                            }
                            if (Limited_Less_Than_Zero(NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].HP))
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].HP = 0;
                            }
                        }
                    }else if (selected_Item.ID == 30002) // HP 회복 물약
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 20) // HP
                        {
                            if (selected_Item.UPDOWN_1 == 1) // value up
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].HP += selected_Item.ITEM_VALUE_1;
                            }
                        }
                    } else if (selected_Item.ID == 30010) // HP 영구 상승 물약
                    {
                        if (selected_Item.ITEM_VALUETYPE_1 == 21) // HP_MAX
                        {
                            if (selected_Item.UPDOWN_1 == 1) // value up
                            {
                                NewInventory_JsonData.instance.party_Status[(_index + selected_Party_Member) - 1].HP_MAX += selected_Item.ITEM_VALUE_1;
                            }
                        }
                    }
                    Party_Status_Text((_index + selected_Party_Member) - 1); // 파티 스테이터스 텍스트
                    Selected_Party_Member_Skills_Setting((_index + selected_Party_Member) - 1); 
                    if (selected_Item.AMOUNT == 1)
                    {
                        Destory_Selected_Item_Data();
                    } else
                    {
                        Selected_Inventory_Item_Decrease_Amount(selected_Item.ID);
                    }
                }
                break;
            case 6:
                {
                    if (selected_Item.ID == 30015)  // 파티원 파트리샤 추가
                    {
                        for (int i = 0; i < NewInventory_JsonData.instance.party_Status.Length; i++)
                        {
                            if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE
                                == NewInventory_JsonData.instance.party_Status[i].SAVE_NUM)
                            {
                                if (NewInventory_JsonData.instance.party_Status[i].PARTY_ID == 0)
                                {
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_ID = (int)party_List_Data[2]["ID"];
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON = party_List_Data[2]["PARTY_FACE_ICON"].ToString();
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_NAME = party_List_Data[2]["PARTY_NAME"].ToString();
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_GRADE = party_List_Data[2]["PARTY_GRADE"].ToString();
                                    NewInventory_JsonData.instance.party_Status[i].FAME = (int)party_List_Data[2]["PARTY_FAME"];
                                    NewInventory_JsonData.instance.party_Status[i].ATK = (int)party_List_Data[2]["PARTY_PHY_ATK"];
                                    NewInventory_JsonData.instance.party_Status[i].DEF = (int)party_List_Data[2]["PARTY_DEF"];
                                    NewInventory_JsonData.instance.party_Status[i].MAG = (int)party_List_Data[2]["PARTY_MAG_ATK"];
                                    NewInventory_JsonData.instance.party_Status[i].REP = (int)party_List_Data[2]["PARTY_MAG_DEF"];
                                    NewInventory_JsonData.instance.party_Status[i].SP = (int)party_List_Data[2]["PARTY_SP"];
                                    NewInventory_JsonData.instance.party_Status[i].SP2 = (int)party_List_Data[2]["PARTY_SP2"];
                                    NewInventory_JsonData.instance.party_Status[i].HP = (int)party_List_Data[2]["PARTY_HP"];
                                    NewInventory_JsonData.instance.party_Status[i].HP_MAX = (int)party_List_Data[2]["PARTY_HP"];
                                    NewInventory_JsonData.instance.party_Status[i].SD_CHARACTER_MODEL = party_List_Data[2]["PARTY_SD_MODEL"].ToString();
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK_NUM = (int)party_List_Data[2]["PARTY_ATTACK_NUM"];
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK1 = (int)party_List_Data[2]["PARTY_ATTACK1"];
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK2 = (int)party_List_Data[2]["PARTY_ATTACK2"];
                                    NewInventory_JsonData.instance.party_Status[i].PARTY_ATTACK3 = (int)party_List_Data[2]["PARTY_ATTACK3"];
                                    break;
                                }
                            }
                        }
                    }
                    Destory_Selected_Item_Data();
                }
                break;
        }
    }
    // 만약 스테이터스 value 가 0이하일경우
    private bool Limited_Less_Than_Zero(int _value)
    {
        return (_value < 0) ? true : false;
    }
    // 인벤토리 아이템 Save
    private void Inventory_Item_Data_Saving()
    {
        if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
        {
            NewInventory_Items_Data.instance.SAVE_NEW_DATA_JSON_ITEMS_LIST_1();
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
        {
            NewInventory_Items_Data.instance.SAVE_NEW_DATA_JSON_ITEMS_LIST_2();
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
        {
            NewInventory_Items_Data.instance.SAVE_NEW_DATA_JSON_ITEMS_LIST_3();
        }
    }
    // 인벤토리 아이템 Load
    private void Inventory_Item_Data_Loading()
    {
        if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
        {
            NewInventory_Items_Data.instance.LOAD_NEW_DATA_JSON_ITEMS_LIST_1();
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
        {
            NewInventory_Items_Data.instance.LOAD_NEW_DATA_JSON_ITEMS_LIST_2();
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
        {
            NewInventory_Items_Data.instance.LOAD_NEW_DATA_JSON_ITEMS_LIST_3();
        }
    }
    // 아이템 사용버튼 클릭완료시 데이터 추가 하고 인벤토리에있는아이템 삭제
    private void Destory_Selected_Item_Data()
    {
        Destroy(slot_Num.transform.GetChild(0).gameObject);
        if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1)
        {
            int index = 0;
            for (int i = 0; i < NewInventory_Items_Data.instance.items_Data_1.Count; i++)
            {
                if (selected_Item.ID == NewInventory_Items_Data.instance.items_Data_1[i].ID)
                {
                    index = i;
                    break;
                }
            }
         //   NewInventory_Items_Data.instance.items_Data_1.RemoveAt(index);
            NewInventory_Items_Data.instance.items_Data_1.Remove(NewInventory_Items_Data.instance.items_Data_1[index]);
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
        {
            int index = 0;
            for (int i = 0; i < NewInventory_Items_Data.instance.items_Data_2.Count; i++)
            {
                if (selected_Item.ID == NewInventory_Items_Data.instance.items_Data_2[i].ID)
                {
                    index = i;
                    break;
                }
            }
         //   NewInventory_Items_Data.instance.items_Data_2.RemoveAt(index);
            NewInventory_Items_Data.instance.items_Data_2.Remove(NewInventory_Items_Data.instance.items_Data_2[index]);
        }
        else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
        {
            int index = 0;
            for (int i = 0; i < NewInventory_Items_Data.instance.items_Data_3.Count; i++)
            {
                if (selected_Item.ID == NewInventory_Items_Data.instance.items_Data_3[i].ID)
                {
                    index = i;
                    break;
                }
            }
        //    NewInventory_Items_Data.instance.items_Data_3.RemoveAt(index);
            NewInventory_Items_Data.instance.items_Data_3.Remove(NewInventory_Items_Data.instance.items_Data_3[index]);
        }
    }
    // 인벤토리에 아이템 사용시 amount가 1보다 클경우 1 감소
    private void Selected_Inventory_Item_Decrease_Amount(int id)
    {
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    for(int i = 0; i < NewInventory_Items_Data.instance.items_Data_1.Count; i++)
                    {
                        if(id == NewInventory_Items_Data.instance.items_Data_1[i].ID)
                        {
                            NewInventory_Items_Data.instance.items_Data_1[i].AMOUNT--;
                            slot_Num.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text =
                            NewInventory_Items_Data.instance.items_Data_1[i].AMOUNT.ToString();
                            slot_Num.transform.GetChild(0).GetComponent<New_Item>().this_Item.AMOUNT =
                                 NewInventory_Items_Data.instance.items_Data_1[i].AMOUNT;
                            break;
                        }
                    }
                }break;
            case 2:
                {
                    for (int i = 0; i < NewInventory_Items_Data.instance.items_Data_2.Count; i++)
                    {
                        if (id == NewInventory_Items_Data.instance.items_Data_2[i].ID)
                        {
                            NewInventory_Items_Data.instance.items_Data_2[i].AMOUNT--;
                            slot_Num.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text =
                            NewInventory_Items_Data.instance.items_Data_2[i].AMOUNT.ToString();
                            slot_Num.transform.GetChild(0).GetComponent<New_Item>().this_Item.AMOUNT =
                               NewInventory_Items_Data.instance.items_Data_2[i].AMOUNT;
                            break;
                        }
                    }
                }break;
            case 3:
                {
                    for (int i = 0; i < NewInventory_Items_Data.instance.items_Data_3.Count; i++)
                    {
                        if (id == NewInventory_Items_Data.instance.items_Data_3[i].ID)
                        {
                            NewInventory_Items_Data.instance.items_Data_3[i].AMOUNT--;
                            slot_Num.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text =
                            NewInventory_Items_Data.instance.items_Data_3[i].AMOUNT.ToString();
                            slot_Num.transform.GetChild(0).GetComponent<New_Item>().this_Item.AMOUNT =
                               NewInventory_Items_Data.instance.items_Data_3[i].AMOUNT;
                            break;
                        }
                    }
                } break;
        }
    }
    // 해고 버튼 누를시 선택한 케릭터 데이터 삭제
    public void Button_Selected_Character_Fire()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Selected_Character_Delete(selected_Party_Member - 1);
                }
                break;
            case 2:
                {
                    Selected_Character_Delete(selected_Party_Member + 3);
                }
                break;
            case 3:
                {
                    Selected_Character_Delete(selected_Party_Member + 7);
                }
                break;
        }
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Party_Status();
        Setting_Party_Status_Text_Input();
        Party_Member_Selected_Character_Skills_Tree();
        Party_Face_Icon_Member_Image_Input();
    }
    // 케릭터 삭제
    private void Selected_Character_Delete(int i)
    {
        int save_Num = NewInventory_JsonData.instance.party_Status[i].SAVE_NUM;
        int party_Num = NewInventory_JsonData.instance.party_Status[i].PARTY_NUM;
        NewInventory_JsonData.instance.party_Status[i] = new Party_Status(save_Num, party_Num);
    }
    // 스킬 버튼 클릭시 SKILL DESCRIPTION 
    public void Event_Skill_Description_1_Pointer_Down()
    {
        text_Skills_Panel.SetActive(true);
        Post_Selected_Skill_Text(party_character_Skills.transform.GetChild(0).GetChild(0).gameObject.name);
    }
    public void Event_Skill_Description_2_Pointer_Down()
    {
        text_Skills_Panel.SetActive(true);
        Post_Selected_Skill_Text(party_character_Skills.transform.GetChild(1).GetChild(0).gameObject.name);
    }
    public void Event_Skill_Description_3_Pointer_Down()
    {
        text_Skills_Panel.SetActive(true);
        Post_Selected_Skill_Text(party_character_Skills.transform.GetChild(2).GetChild(0).gameObject.name);
    }
    public void Event_Skill_Description_Pointer_Up()
    {
        text_Skills_Panel.SetActive(false);
    }
    private void Post_Selected_Skill_Text(string _skill_Id)
    {
        int skil_Id = int.Parse(_skill_Id);
        text_Skills_Panel.transform.GetChild(0).GetComponent<Text>().text = " ";
        for (int i = 0; i < skills_Data.Count; i++)
        {
            int skill_Icon = (int)skills_Data[i]["ID"];
            if (skill_Icon == skil_Id)
            {
                text_Skills_Panel.transform.GetChild(0).GetComponent<Text>().text = skills_Data[i]["SKILL_DESCRIPTION"].ToString();
                break;
            }
        }
    }
    // 나가기 버튼 (메인씬)
    public void Button_Pressed_Load_To_MainScene()
    {
        SceneManager.LoadScene("Main");
    }
} // class
















