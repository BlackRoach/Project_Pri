using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class Play_Talk_Event : MonoBehaviour {


    public static Play_Talk_Event instance = null;

    public GameObject talk_Event_Parent;
    public GameObject bg_Img;
    public GameObject dialog_Box;
    public GameObject select_Box;
    // 케릭터 이미지 그리고 애니메이션 
    public GameObject left_Character;
    public GameObject middle_Character;
    public GameObject right_Character;
    // --------------------
    public Schedule_Each_Count event_List = new Schedule_Each_Count();  // 대화 이벤트 횟수

    public Current_Talk_Event current_Event = new Current_Talk_Event(); // 대화이벤트 실행매니저

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
        bg_Img.SetActive(false);
        left_Character.SetActive(false);
        middle_Character.SetActive(false);
        right_Character.SetActive(false);

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
                        if((int)play_Count_Data[i]["TRIGGER_NUM"] == 1)
                        {
                            current_Event.event_Name = event_List_Data[0]["EVENT_NAME"].ToString();
                            current_Event.end_Event_Count = (int)event_List_Data[0]["EVENT_COUNT"];
                            current_Event.current_Event_Count++;
                            current_Event.event_Fuction = event_List_Data[0]["EVENT_1"].ToString();
                            current_Event.input_Value = event_List_Data[0]["EVENT_IN_1"].ToString();

                            Event_Run_Manager();
                            break;
                        }
                    }
                }
            }
        }
    }
    // 대사 오토 타이핑
    IEnumerator Auto_Typing_Dialog_Text(string str_Text)
    {
        dialog_Box.transform.GetChild(0).GetComponent<Text>().text = " ";
        string _text = str_Text;
        foreach (char input_Char in _text.ToCharArray())
        {
            dialog_Box.transform.GetChild(0).GetComponent<Text>().text += input_Char;
            yield return new WaitForSeconds(0.05f);
        }
        dialog_Box.transform.GetChild(3).gameObject.SetActive(true);
    }

    // 이벤트 함수 모음
    public void BG_MAKE()
    {
        talk_Event_Parent.SetActive(true);
        bg_Img.SetActive(true);
        bg_Img.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        // --------------------
        current_Event.current_Event_Count++;
        current_Event.event_Fuction = "CHA_MAKE_L";
        current_Event.input_Value = "marienne";
        Event_Run_Manager();
    }
    public void BG_OUT()
    {
        bg_Img.GetComponent<Image>().sprite = null;
        bg_Img.SetActive(false);
        talk_Event_Parent.SetActive(false);
        // --------------------
        current_Event.current_Event_Count++;
        current_Event.event_Fuction = "0";
        current_Event.input_Value = "0";
    }
    public void Char_Make_Left()
    {
        left_Character.SetActive(true);
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Fade_In");
        left_Character.GetComponent<Animation>().Play();
        // --------------------
        current_Event.current_Event_Count++;
        current_Event.event_Fuction = "DIALOG_MAKE";
        current_Event.input_Value = "70001";
        Event_Run_Manager();
    }
    public void Char_Make_Right()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Fade_In");
        right_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_Middle()
    {
        middle_Character.GetComponent<Animation>().clip = middle_Character.GetComponent<Animation>().GetClip("Fade_In");
        middle_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_LM()
    {
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Fade_In");
        left_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_RM()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Fade_In");
        right_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_MDM()
    {
        middle_Character.GetComponent<Animation>().clip = middle_Character.GetComponent<Animation>().GetClip("Middle_Down_Fade_In");
        middle_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_LDM()
    {
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Down_Fade_In");
        left_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_RDM()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Down_Fade_In");
        right_Character.GetComponent<Animation>().Play();
    }
    public void Char_Move_To_Left()
    {
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Move_Char");
        left_Character.GetComponent<Animation>().Play();
    }
    public void Char_Move_To_Right()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Move_Char");
        right_Character.GetComponent<Animation>().Play();
    }
    public void Char_Out()
    {
        left_Character.SetActive(true);
        right_Character.SetActive(true);
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Fade_In");
        right_Character.GetComponent<Animation>().Play();
    }
    public void Char_Out_Left()
    {
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Move_Fade_Out");
        left_Character.GetComponent<Animation>().Play();
    }
    public void Char_Out_Right()
    {
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Move_Fade_Out");
        right_Character.GetComponent<Animation>().Play();
    }
    public void DIALOG_MAKE()
    {
        dialog_Box.SetActive(true);
        dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
        if (current_Event.input_Value == dialog_List_Data[0]["ID"].ToString())
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[0]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[0]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[0]["DIALOG_FACE"].ToString());
        }
        // --------------------------
        current_Event.current_Event_Count++;
        current_Event.event_Fuction = "DIALOG_MAKE";
        current_Event.input_Value = "70002";
    }
    public void SELECT_MAKE()
    {
        select_Box.SetActive(true);
    }
    public void Character_Fade_Out()
    {
        left_Character.SetActive(true);
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Fade_out");
        left_Character.GetComponent<Animation>().Play();
        // -----------------------------
        current_Event.current_Event_Count++;
        current_Event.event_Fuction = "BG_OUT";
        current_Event.input_Value = "school_img";
        StartCoroutine(Left_Character_Fade_Out_Done());
    }
    // 케릭터 fade out 끝나고 난후 false 작업
    IEnumerator Left_Character_Fade_Out_Done()
    {
        yield return new WaitForSeconds(1.2f);
        left_Character.SetActive(false);
        Event_Run_Manager();
    }
    // ---------------------------
    public void Button_Dialog_Text_Next_Or_Exit()
    {
        if (current_Event.current_Event_Count == 4)
        {
            dialog_Box.SetActive(true);
            dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
            if (current_Event.input_Value == dialog_List_Data[1]["ID"].ToString())
            {
                StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[1]["DIALOG_TEXT"].ToString()));
            }
            // ------------------------
            current_Event.current_Event_Count++;
            current_Event.event_Fuction = "CHA_OUT";
            current_Event.input_Value = "marienne";
        }
        else
        {
            dialog_Box.SetActive(false);
            Event_Run_Manager();
        }
    }
    // end 함수 모음
    // 각종 이벤트 실행 매니저
    private void Event_Run_Manager()
    {
        if(current_Event.event_Name == "EVENT_1")
        {
            if (current_Event.current_Event_Count <= current_Event.end_Event_Count)
            {
                switch (current_Event.event_Fuction)
                {
                    case "BG_MAKE":
                        {
                            BG_MAKE();
                        }break;
                    case "CHA_MAKE_L":
                        {
                            Char_Make_Left();
                        }break;
                    case "DIALOG_MAKE":
                        {
                            DIALOG_MAKE();
                        }break;
                    case "CHA_OUT":
                        {
                            Character_Fade_Out();
                        }
                        break;
                    case "BG_OUT":
                        {
                            BG_OUT();
                        }
                        break;
                }
            }
        }
    }




} // class








