using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;



public class NewInventory_Manager : MonoBehaviour {

    public GameObject rena_Attire_Status_Panel; // 의상 패널
    public GameObject party_Status_Panel; // 모험 패널

    private GameObject rena_Character_Image_Panel; // 레나 케릭터 이미지 패널
    private GameObject rena_Attire_Status_Text; // 레나 의상 스테이터스 텍스트 관련 패널
    private GameObject party_Status_Text; // 파티 스테이터스 텍스트 관련 패널
    private GameObject party_Face_Icon_Member; // 파티 맴버 Face Icon parent

    private bool is_Changed; // 레나 의상스테이터스 보여줄때 on / off 해주는 변수

    // 디폴트 json_data
    private JsonData rena_Part_Data;
    private void Awake()
    {
        rena_Character_Image_Panel = rena_Attire_Status_Panel.transform.GetChild(0).gameObject;
        rena_Attire_Status_Text = rena_Attire_Status_Panel.transform.GetChild(3).gameObject;
        party_Status_Text = party_Status_Panel.transform.GetChild(4).gameObject;
        party_Face_Icon_Member = party_Status_Panel.transform.GetChild(6).gameObject;
    }
    private void Start()
    {
        Json_Data_Parsing();
        is_Changed = false;  
        rena_Attire_Status_Panel.SetActive(true);
        party_Status_Panel.SetActive(false);
        Setting_Rena_Attire_Status_Text_Input();
        Setting_Party_Status_Text_Input();
        Party_Face_Icon_Member_Image_Input();
        Rena_Attire_Setting();
    }
    // defualt_Json_Data_Parsing
    private void Json_Data_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/RENA_PARTS");

        rena_Part_Data = JsonMapper.ToObject(json_File_1.text);
    }
    // 레나 의상 스테이터스 on / off 용 버튼
    public void Button_Pressed_Showing_Rena_Status()
    {
        is_Changed = !is_Changed;
        if (is_Changed)
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
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    for(int i = 0; i < 4; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                    }
                }break;
            case 2:
                {
                    for (int i = 4; i < 8; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i - 4).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                    }
                }
                break;
            case 3:
                {
                    for (int i = 8; i < 12; i++)
                    {
                        party_Face_Icon_Member.transform.GetChild(i - 8).GetComponent<Image>().sprite =
                           Resources.Load<Sprite>("JHM.Img/New_Inventory/" + NewInventory_JsonData.instance.party_Status[i].PARTY_FACE_ICON);
                    }
                }
                break;
        }
    }
    // 레나 의상 스테이터스 값 푸싱
    private void Setting_Rena_Attire_Status_Text_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
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
        rena_Attire_Status_Text.transform.GetChild(0).GetComponent<Text>().text = "근력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MUSCULAR_STRENGTH.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(1).GetComponent<Text>().text = "마법력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MAGIC_POWER.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(2).GetComponent<Text>().text = "체력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].STAMINA.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(3).GetComponent<Text>().text = "지력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].INTELLECT.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(4).GetComponent<Text>().text = "매력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].CHARM.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(5).GetComponent<Text>().text = "센스: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].SENSE.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(6).GetComponent<Text>().text = "자존감: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].PRIDE.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(7).GetComponent<Text>().text = "예술성: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i - 1].ARTISTIC.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(8).GetComponent<Text>().text = "기품: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].ELEGANCE.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(9).GetComponent<Text>().text = "도덕: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].MORALITY.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(10).GetComponent<Text>().text = "신뢰: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].RELIABILITY.ToString() + " + " + "(0)";
        rena_Attire_Status_Text.transform.GetChild(11).GetComponent<Text>().text = "스트레스: " +
           NewInventory_JsonData.instance.rena_Attire_Status[i - 1].STRESS.ToString() + " + " + "(0)";
    }
    // 파티 스테이터스 값 푸싱
    private void Setting_Party_Status_Text_Input()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Party_Status_Text(0);
                }
                break;
            case 2:
                {
                    Party_Status_Text(4);
                }
                break;
            case 3:
                {
                    Party_Status_Text(8);
                }
                break;
        }
    }
    private void Party_Status_Text(int i)
    {
        party_Status_Text.transform.GetChild(0).GetComponent<Text>().text = "이름: " +
            NewInventory_JsonData.instance.party_Status[i].PARTY_NAME.ToString();
        party_Status_Text.transform.GetChild(1).GetComponent<Text>().text = "Grade: " +
            NewInventory_JsonData.instance.party_Status[i].PARTY_GRADE.ToString();
        party_Status_Text.transform.GetChild(2).GetComponent<Text>().text = "FAME: " +
            NewInventory_JsonData.instance.party_Status[i].FAME.ToString();
        party_Status_Text.transform.GetChild(3).GetComponent<Text>().text = "ATK: " +
            NewInventory_JsonData.instance.party_Status[i].ATK.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(4).GetComponent<Text>().text = "DEF: " +
            NewInventory_JsonData.instance.party_Status[i].DEF.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(5).GetComponent<Text>().text = "MAG: " +
            NewInventory_JsonData.instance.party_Status[i].MAG.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(6).GetComponent<Text>().text = "REP: " +
            NewInventory_JsonData.instance.party_Status[i].REP.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(7).GetComponent<Text>().text = "SP: " +
            NewInventory_JsonData.instance.party_Status[i].SP.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(8).GetComponent<Text>().text = "SP2: " +
           NewInventory_JsonData.instance.party_Status[i].SP2.ToString() + " + " + "(0)";
        party_Status_Text.transform.GetChild(9).GetComponent<Text>().text = "HP: " +
           NewInventory_JsonData.instance.party_Status[i].HP.ToString() + "/" +
           NewInventory_JsonData.instance.party_Status[i].HP_MAX.ToString() + " + " + "(0)";
    }
    // 조건에 맞는 레나의 옷차림 세팅
    private void Rena_Attire_Setting()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    int _stress = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].STRESS;
                    if (_stress >= 0 && _stress <= 30)
                    {
                        NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 1;
                        Rena_Attire_Parts_Setting();
                    } else if(_stress >= 31 && _stress <= 60)
                    {
                        NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 2;
                        Rena_Attire_Parts_Setting();
                    } else if(_stress >= 61 && _stress <= 200)
                    {
                        NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD = 3;
                        Rena_Attire_Parts_Setting();
                    }                   
                }break;
            case 2:
                {
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
                }
                break;
            case 3:
                {
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
                }
                break;
        }
        NewInventory_Json_Data_Saving_ALL();
    }
    // 조건에 맞는 레나의 파츠 세팅
    private void Rena_Attire_Parts_Setting()
    {
        int attire_Id = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].ATTIRE_ID;
        int mood = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].MOOD;
        int old = NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].OLD;
        for (int i = 0; i < rena_Part_Data.Count; i++)
        {
            if (old == (int)rena_Part_Data[i]["OLD"])
            {
                if (mood == (int)rena_Part_Data[i]["MOOD"])
                {
                    if (attire_Id == (int)rena_Part_Data[i]["ATTRIE_ID"])
                    {
                        // 바디 파츠
                        rena_Character_Image_Panel.transform.GetChild(0).GetComponent<Image>().sprite =
                            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["BODY_PARTS"].ToString());
                        // 의상 파츠
                        rena_Character_Image_Panel.transform.GetChild(1).GetComponent<Image>().sprite =
                            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["ATTRIE_PARTS"].ToString());
                        // 얼굴 파츠 1
                        rena_Character_Image_Panel.transform.GetChild(2).GetComponent<Image>().sprite =
                            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["MOOD_FACE_PARTS_1"].ToString());
                        // 얼굴 파츠 2
                        rena_Character_Image_Panel.transform.GetChild(3).GetComponent<Image>().sprite =
                            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + rena_Part_Data[i]["MOOD_FACE_PARTS_2"].ToString());

                        StartCoroutine(Rena_Eye_Start()); // 눈깜박임
                        break;
                    }                    
                }
            }
        }
    }
    IEnumerator Rena_Character_Image_Eye_Running()
    {
        yield return new WaitForSeconds(2f);
        rena_Character_Image_Panel.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        rena_Character_Image_Panel.transform.GetChild(3).gameObject.SetActive(false);
        StartCoroutine(Rena_Character_Image_Eye_Running());
    }
    // 변경사항 있을경우 All Json Files data에 저장(덮어 씨우기)
    private void NewInventory_Json_Data_Saving_ALL()
    {
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Party_Status();
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Rena_Attire_Status();
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Save_Type_Option();
    }
    // 레나 의상 패널  ON
    public void Button_Pressed_Rena_Attire_Status()
    {
        rena_Attire_Status_Panel.SetActive(true);
        party_Status_Panel.SetActive(false);
        // --------------------------------------
        rena_Character_Image_Panel.SetActive(true);
        StartCoroutine(Rena_Eye_Start());
    }
    // 레나 모험 패널  ON
    public void Button_Pressed_Party_Status()
    {
        rena_Attire_Status_Panel.SetActive(false);
        party_Status_Panel.SetActive(true);
        // --------------------------------------
        rena_Character_Image_Panel.SetActive(false);
        StopAllCoroutines(); // 레나 눈깜박임 동작 멈춤
    }
    IEnumerator Rena_Eye_Start()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Rena_Character_Image_Eye_Running()); // 레나 눈깜박임
    }
    // 나가기 버튼 (메인씬)
    public void Button_Pressed_Load_To_MainScene()
    {
        SceneManager.LoadScene("Main");
    }
} // class
















