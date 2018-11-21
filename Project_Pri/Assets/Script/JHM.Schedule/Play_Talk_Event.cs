using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class Play_Talk_Event : MonoBehaviour {


    public static Play_Talk_Event instance = null;

    public GameObject talk_Event_Parent;
    public Image bg_Img;
    // 케릭터 이미지 그리고 애니메이션 
    public GameObject left_Character;
    public GameObject middle_Character;
    public GameObject right_Character;
    // --------------------
    public Schedule_Each_Count event_List = new Schedule_Each_Count();  // 대화 이벤트 횟수

    public Current_Talk_Event current_Event = new Current_Talk_Event();

    private JsonData play_Count_Data;
    private JsonData trigger_List_Data;
    private JsonData event_List_Data;
    private JsonData dialog_List_Data;
    private JsonData select_List_Data;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        talk_Event_Parent.SetActive(false);
        Json_Data_All_Parsing();
    }
    private void Json_Data_All_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/PLAY_COUNT_DATA");
        play_Count_Data = JsonMapper.ToObject(json_File_1.text);
        TextAsset json_File_2 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/TRIGGER_LIST");
        trigger_List_Data = JsonMapper.ToObject(json_File_2.text);
        TextAsset json_File_3 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/EVENT_LIST_1");
        event_List_Data = JsonMapper.ToObject(json_File_3.text);
        TextAsset json_File_4 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/DIALOG_LIST_1");
        dialog_List_Data = JsonMapper.ToObject(json_File_4.text);
        TextAsset json_File_5 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/SELECT_LIST");
        select_List_Data = JsonMapper.ToObject(json_File_5.text);
    }
    public void Decited_Schedule_Manager(string title)
    {
        string str = " ";
        int count = 0;
        switch (title)
        {
            case "마법학교":
                {
                    if (event_List.MAGIC_P >= 0 && event_List.MAGIC_P < 50)
                    {
                        event_List.MAGIC_P += 1;
                    }
                    str = "MAGIC_P";
                    count = event_List.MAGIC_P;
                }
                break;
            case "무술도장":
                {
                    if (event_List.MILITARY_P >= 0 && event_List.MILITARY_P < 50)
                    {
                        event_List.MILITARY_P += 1;
                    }
                    str = "MILITARY_P";
                    count = event_List.MILITARY_P;
                }
                break;
            case "요리학교":
                {
                    if (event_List.COOKING_P >= 0 && event_List.COOKING_P < 50)
                    {
                        event_List.COOKING_P += 1;
                    }
                    str = "COOKING_P";
                    count = event_List.COOKING_P;
                }
                break;
            case "예절학교":
                {
                    if (event_List.MANNERS_P >= 0 && event_List.MANNERS_P < 50)
                    {
                        event_List.MANNERS_P += 1;
                    }
                    str = "MANNERS_P";
                    count = event_List.MANNERS_P;
                }
                break;
            case "학교":
                {
                    if (event_List.SCHOOL_P >= 0 && event_List.SCHOOL_P < 50)
                    {
                        event_List.SCHOOL_P += 1;
                    }
                    str = "SCHOOL_P";
                    count = event_List.SCHOOL_P;
                }
                break;
            case "음악학교":
                {
                    if (event_List.MUSIC_P >= 0 && event_List.MUSIC_P < 50)
                    {
                        event_List.MUSIC_P += 1;
                    }
                    str = "MUSIC_P";
                    count = event_List.MUSIC_P;
                }
                break;
            case "무용학교":
                {
                    if (event_List.DANCING_P >= 0 && event_List.DANCING_P < 50)
                    {
                        event_List.DANCING_P += 1;
                    }
                    str = "DANCING_P";
                    count = event_List.DANCING_P;
                }
                break;
            case "그림학교":
                {
                    if (event_List.PICTURE_P >= 0 && event_List.PICTURE_P < 50)
                    {
                        event_List.PICTURE_P += 1;
                    }
                    str = "PICTURE_P";
                    count = event_List.PICTURE_P;
                }
                break;
            case "수도원":
                {
                    if (event_List.MONASTERY_P >= 0 && event_List.MONASTERY_P < 50)
                    {
                        event_List.MONASTERY_P += 1;
                    }
                    str = "MONASTERY_P";
                    count = event_List.MONASTERY_P;
                }
                break;
            case "사냥꾼":
                {
                    if (event_List.HUNTER_P >= 0 && event_List.HUNTER_P < 50)
                    {
                        event_List.HUNTER_P += 1;
                    }
                    str = "HUNTER_P";
                    count = event_List.HUNTER_P;
                }
                break;
            case "술집":
                {
                    if (event_List.PUB_P >= 0 && event_List.PUB_P < 50)
                    {
                        event_List.PUB_P += 1;
                    }
                    str = "PUB_P";
                    count = event_List.PUB_P;
                }
                break;
            case "가정교사":
                {
                    if (event_List.TUTOR_P >= 0 && event_List.TUTOR_P < 50)
                    {
                        event_List.TUTOR_P += 1;
                    }
                    str = "TUTOR_P";
                    count = event_List.TUTOR_P;
                }
                break;
            case "음유시인":
                {
                    if (event_List.BARD_P >= 0 && event_List.BARD_P < 50)
                    {
                        event_List.BARD_P += 1;
                    }
                    str = "BARD_P";
                    count = event_List.BARD_P;
                }
                break;
            case "농장":
                {
                    if (event_List.FARM_P >= 0 && event_List.FARM_P < 50)
                    {
                        event_List.FARM_P += 1;
                    }
                    str = "FARM_P";
                    count = event_List.FARM_P;
                }
                break;
            case "성당":
                {
                    if (event_List.CATHEDRAL_P >= 0 && event_List.CATHEDRAL_P < 50)
                    {
                        event_List.CATHEDRAL_P += 1;
                    }
                    str = "CATHEDRAL_P";
                    count = event_List.CATHEDRAL_P;
                }
                break;
            case "묘지기":
                {
                    if (event_List.GRAVEYARD_P >= 0 && event_List.GRAVEYARD_P < 50)
                    {
                        event_List.GRAVEYARD_P += 1;
                    }
                    str = "GRAVEYARD_P";
                    count = event_List.GRAVEYARD_P;
                }
                break;
            case "시장":
                {
                    if (event_List.MARKET_P >= 0 && event_List.MARKET_P < 50)
                    {
                        event_List.MARKET_P += 1;
                    }
                    str = "MARKET_P";
                    count = event_List.MARKET_P;
                }
                break;
            case "광산":
                {
                    if (event_List.MINE_P >= 0 && event_List.MINE_P < 50)
                    {
                        event_List.MINE_P += 1;
                    }
                    str = "MINE_P";
                    count = event_List.MINE_P;
                }
                break;
            case "메이드":
                {
                    if (event_List.MAID_P >= 0 && event_List.MAID_P < 50)
                    {
                        event_List.MAID_P += 1;
                    }
                    str = "MAID_P";
                    count = event_List.MAID_P;
                }
                break;
            case "집안일":
                {
                    if (event_List.HOUSEWORK_P >= 0 && event_List.HOUSEWORK_P < 50)
                    {
                        event_List.HOUSEWORK_P += 1;
                    }
                    str = "HOUSEWORK_P";
                    count = event_List.HOUSEWORK_P;
                }
                break;
            case "극장":
                {
                    if (event_List.THEATER_P >= 0 && event_List.THEATER_P < 50)
                    {
                        event_List.THEATER_P += 1;
                    }
                    str = "THEATER_P";
                    count = event_List.THEATER_P;
                }
                break;
            case "자유행동":
                {
                    if (event_List.FREE_P >= 0 && event_List.FREE_P < 50)
                    {
                        event_List.FREE_P += 1;
                    }
                    str = "FREE_P";
                    count = event_List.FREE_P;
                }
                break;
        } // end switch 

        Searching_Play_Count_Data(str, count);
    } // end Decited_Schedule_Manager

    // json 파일에 스케쥴 이벤트 실행횟수 찾아 트리거 동작
    // select place and count 일치 검색
    private void Searching_Play_Count_Data(string str,int count)
    {
        for(int i =0; i < play_Count_Data.Count; i++)
        {
            if(str == play_Count_Data[i]["EVENT_PLACE"].ToString())
            {
                if(count == (int)play_Count_Data[i]["COMMAND_COUNT"])
                {
                    if((int)play_Count_Data[i]["TRIGGER_NOT"] != 0)
                    {
                        // 트리거 작동
                        // 테스트 코드 
                        talk_Event_Parent.SetActive(true);
                        current_Event.input_Value = "school_img";
                        BG_MAKE();
                        Char_Make_Left();
                        Char_Make_Right();
                        // ---------
                    }
                }
            }
        }
    }



    // 이벤트 함수 모음
    public void BG_MAKE()
    {
        bg_Img.sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
    }
    public void Char_Make_Left()
    {
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Fade_In");
        left_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_Right()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Fade_In");
        right_Character.GetComponent<Animation>().Play();
    }

    // end 함수 모음
} // class








