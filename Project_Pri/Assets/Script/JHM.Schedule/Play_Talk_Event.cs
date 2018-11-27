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
    public Schedule_Each_Count event_List;  // 대화 이벤트 횟수
    public Talk_Event_Trigger_Manager[] trigger_List; // 대화 이벤트 트리거
    public Current_Talk_Event current_Event; // 대화이벤트 실행매니저

    public bool is_Trigger; // 스케쥴실행때 1이면 기존 일정 정지 0이면 진행 , 도와주는 boolean

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
    event_List = new Schedule_Each_Count();  // 대화 이벤트 횟수
    trigger_List = new Talk_Event_Trigger_Manager[10]; // 대화 이벤트 트리거
    current_Event = new Current_Talk_Event(); // 대화이벤트 실행매니저
    is_Trigger = false;
    }
    private void Start()
    {
        for(int i = 0; i < trigger_List.Length; i++)
        {
            trigger_List[i] = new Talk_Event_Trigger_Manager();
        }
        for(int i = 0; i < trigger_List.Length; i++)
        {
            trigger_List[i].Trigger_Num = i + 1;
            trigger_List[i].Command_Event = ("EVENT_" + (i + 1).ToString());
        }

        talk_Event_Parent.SetActive(false);
        bg_Img.SetActive(false);
        left_Character.SetActive(false);
        middle_Character.SetActive(false);
        right_Character.SetActive(false);
        dialog_Box.SetActive(false);
        select_Box.SetActive(false);
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
                    if((int)play_Count_Data[i]["TRIGGER_NOT"] == 1)
                    {
                        is_Trigger = true; // 기존 스케쥴 정지
                        // 트리거 작동
                        if ((int)play_Count_Data[i]["TRIGGER_NUM"] == 1)
                        {
                            current_Event.event_Name = event_List_Data[0]["EVENT_NAME"].ToString();
                            current_Event.end_Event_Count = (int)event_List_Data[0]["EVENT_COUNT"];
                            current_Event.current_Event_Count++;
                            current_Event.event_Fuction = event_List_Data[0]["EVENT_1"].ToString();
                            current_Event.input_Value = event_List_Data[0]["EVENT_IN_1"].ToString();

                            trigger_List[0].Play_Count += 2;
                        }
                        if((int)play_Count_Data[i]["TRIGGER_NUM"] == 2)
                        {
                            current_Event.event_Name = event_List_Data[1]["EVENT_NAME"].ToString();
                            current_Event.end_Event_Count = (int)event_List_Data[1]["EVENT_COUNT"];
                            current_Event.current_Event_Count++;
                            current_Event.event_Fuction = event_List_Data[1]["EVENT_1"].ToString();
                            current_Event.input_Value = event_List_Data[1]["EVENT_IN_1"].ToString();

                            trigger_List[1].Play_Count += 2;
                        }
                        if ((int)play_Count_Data[i]["TRIGGER_NUM"] == 3)
                        {
                            current_Event.event_Name = event_List_Data[2]["EVENT_NAME"].ToString();
                            current_Event.end_Event_Count = (int)event_List_Data[2]["EVENT_COUNT"];
                            current_Event.current_Event_Count++;
                            current_Event.event_Fuction = event_List_Data[2]["EVENT_1"].ToString();
                            current_Event.input_Value = event_List_Data[2]["EVENT_IN_1"].ToString();

                            trigger_List[2].Play_Count += 2;
                        }
                        if ((int)play_Count_Data[i]["TRIGGER_NUM"] == 4)
                        {
                            current_Event.event_Name = event_List_Data[3]["EVENT_NAME"].ToString();
                            current_Event.end_Event_Count = (int)event_List_Data[3]["EVENT_COUNT"];
                            current_Event.current_Event_Count++;
                            current_Event.event_Fuction = event_List_Data[3]["EVENT_1"].ToString();
                            current_Event.input_Value = event_List_Data[3]["EVENT_IN_1"].ToString();

                            trigger_List[3].Play_Count += 2;
                        }
                        Event_Run_Manager();
                        break;
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
        Talk_Event_Next_Input();
    }
    public void BG_OUT()
    {
        bg_Img.GetComponent<Image>().sprite = null;
        bg_Img.SetActive(false);
        talk_Event_Parent.SetActive(false);
        // --------------------
        if(current_Event.event_Name == "EVENT_1") trigger_List[0].Play_Count++;
        if (current_Event.event_Name == "EVENT_2") trigger_List[1].Play_Count++;
        if (current_Event.event_Name == "EVENT_4") trigger_List[3].Play_Count++;
        if (current_Event.event_Name == "EVENT_5") trigger_List[4].Play_Count++;
        if (current_Event.event_Name == "EVENT_6") trigger_List[5].Play_Count++;
        if (current_Event.event_Name == "EVENT_7") trigger_List[6].Play_Count++;

        current_Event = new Current_Talk_Event(); // 대화실행 매니저 데이터 초기화
        ScheduleManager.instance.Schedule_Loop_Start_Again();
    }
    public void Char_Make_Left()
    {
        left_Character.SetActive(true);
        left_Character.transform.localPosition = new Vector3(-400f, 0f, 0f);
        left_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        left_Character.GetComponent<Image>().SetNativeSize();
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Fade_In");
        left_Character.GetComponent<Animation>().Play();
        // --------------------
        Talk_Event_Next_Input();
    }
    public void Char_Make_Right()
    {
        right_Character.SetActive(true);
        right_Character.transform.localPosition = new Vector3(400f, 0f, 0f);
        right_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        right_Character.GetComponent<Image>().SetNativeSize();
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Fade_In");
        right_Character.GetComponent<Animation>().Play();
        // ---------------------
        Talk_Event_Next_Input();
    }
    public void Char_Make_Middle()
    {
        middle_Character.GetComponent<Animation>().clip = middle_Character.GetComponent<Animation>().GetClip("Fade_In");
        middle_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_LM()
    {
        left_Character.SetActive(true);
        left_Character.transform.localPosition = new Vector3(-400f, 0f, 0f);
        left_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        left_Character.GetComponent<Image>().SetNativeSize();
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Fade_In");
        left_Character.GetComponent<Animation>().Play();
        // --------------------
        Talk_Event_Next_Input();
    }
    public void Char_Make_RM()
    {
        right_Character.SetActive(true);
        right_Character.transform.localPosition = new Vector3(400f, 0f, 0f);
        right_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        right_Character.GetComponent<Image>().SetNativeSize();
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Fade_In");
        right_Character.GetComponent<Animation>().Play();
        // ---------------------
        Talk_Event_Next_Input();
    }
    public void Char_Make_MDM()
    {
        middle_Character.GetComponent<Animation>().clip = middle_Character.GetComponent<Animation>().GetClip("Middle_Down_Fade_In");
        middle_Character.GetComponent<Animation>().Play();
    }
    public void Char_Make_LDM()
    {
        left_Character.SetActive(true);
        left_Character.transform.localPosition = new Vector3(-400f, 0f, 0f);
        left_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        left_Character.GetComponent<Image>().SetNativeSize();
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Down_Fade_In");
        left_Character.GetComponent<Animation>().Play();
        // --------------------
        Talk_Event_Next_Input();
    }
    public void Char_Make_RDM()
    {
        right_Character.SetActive(true);
        right_Character.transform.localPosition = new Vector3(400f, 0f, 0f);
        right_Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + current_Event.input_Value);
        right_Character.GetComponent<Image>().SetNativeSize();
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Down_Fade_In");
        right_Character.GetComponent<Animation>().Play();
        // --------------------
        Talk_Event_Next_Input();
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
    public void Char_Out_Move_Left()
    {
        left_Character.transform.localPosition = new Vector3(-400f, 0f, 0f);
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Left_Move_Fade_Out");
        left_Character.GetComponent<Animation>().Play();

        StartCoroutine(Left_Char_FO_LM());
    }
    IEnumerator Left_Char_FO_LM()
    {
        yield return new WaitForSeconds(1.2f);
        left_Character.SetActive(false);
        Talk_Event_Next_Input();
    }
    public void Char_Out_Move_Right()
    {
        right_Character.transform.localPosition = new Vector3(400f, 0f, 0f);
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Right_Move_Fade_Out");
        right_Character.GetComponent<Animation>().Play();
        StartCoroutine(Right_Char_FO_RM());
    }
    IEnumerator Right_Char_FO_RM()
    {
        yield return new WaitForSeconds(1.2f);
        right_Character.SetActive(false);
        Talk_Event_Next_Input();
    }
    public void DIALOG_MAKE()
    {
        dialog_Box.SetActive(true);
        dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
        if (current_Event.input_Value == "70001")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[0]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[0]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[0]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70003")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[2]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[2]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[2]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70006")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[5]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[5]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[5]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70008")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[7]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[7]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[7]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70009")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[8]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[8]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[8]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70010")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[9]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[9]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[9]["DIALOG_FACE"].ToString());
        }
        if (current_Event.input_Value == "70011")
        {
            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[10]["DIALOG_TEXT"].ToString()));
            dialog_Box.transform.GetChild(1).GetComponent<Text>().text = dialog_List_Data[10]["DIALOG_NAME"].ToString();
            dialog_Box.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/" + dialog_List_Data[10]["DIALOG_FACE"].ToString());
        }
    }
    public void Left_Character_Fade_Out()
    {
        left_Character.transform.localPosition = new Vector3(-400f, 0f, 0f);
        left_Character.GetComponent<Animation>().clip = left_Character.GetComponent<Animation>().GetClip("Fade_out");
        left_Character.GetComponent<Animation>().Play();
        // -----------------------------
        StartCoroutine(Left_Character_Fade_Out_Done());
    }
    // 케릭터 fade out 끝나고 난후 false 작업
    IEnumerator Left_Character_Fade_Out_Done()
    {
        yield return new WaitForSeconds(1.2f);
        left_Character.SetActive(false);
        Talk_Event_Next_Input();
    }
    public void Right_Character_Fade_Out()
    {
        right_Character.transform.localPosition = new Vector3(400f, 0f, 0f);
        right_Character.GetComponent<Animation>().clip = right_Character.GetComponent<Animation>().GetClip("Fade_out");
        right_Character.GetComponent<Animation>().Play();
        // -----------------------------
        StartCoroutine(Right_Character_Fade_Out_Done());
    }
    // 케릭터 fade out 끝나고 난후 false 작업
    IEnumerator Right_Character_Fade_Out_Done()
    {
        yield return new WaitForSeconds(1.2f);
        right_Character.SetActive(false);
        Talk_Event_Next_Input();
    }
    // ---------------------------
    // 대화 풍선 클릭시 다음행동 ( 다음 텍스트나 , 나가기)
    public void Button_Dialog_Text_Next_Or_Exit()
    {        
        if (current_Event.current_Event_Count == 3 && current_Event.event_Name == "EVENT_1")
        {
            current_Event.current_Event_Count++;
            current_Event.event_Fuction = "DM";
            current_Event.input_Value = "70002";
            // ------------------------
            dialog_Box.SetActive(true);
            dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
            if (current_Event.input_Value == "70002")
            {
                StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[1]["DIALOG_TEXT"].ToString()));
            }
        }else if (current_Event.event_Name == "EVENT_2" && current_Event.current_Event_Count <= 5)
        {
            switch (current_Event.current_Event_Count)
            {
                case 4:
                    {
                        current_Event.current_Event_Count++;
                        current_Event.event_Fuction = "DM";
                        current_Event.input_Value = "70004";
                        // ------------------------
                        dialog_Box.SetActive(true);
                        dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
                        if (current_Event.input_Value == "70004")
                        {
                            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[3]["DIALOG_TEXT"].ToString()));
                        }
                    } break;
                case 5:
                    {
                        current_Event.current_Event_Count++;
                        current_Event.event_Fuction = "DM";
                        current_Event.input_Value = "70005";
                        // ------------------------
                        dialog_Box.SetActive(true);
                        dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
                        if (current_Event.input_Value == "70005")
                        {
                            StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[4]["DIALOG_TEXT"].ToString()));
                        }
                    } break;
            }
        } else if(current_Event.event_Name == "EVENT_3" && current_Event.current_Event_Count <= 4)
        {
            if(current_Event.current_Event_Count == 4)
            {
                current_Event.current_Event_Count++;
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70007";
                // ------------------------
                dialog_Box.SetActive(true);
                dialog_Box.transform.GetChild(3).gameObject.SetActive(false);
                if (current_Event.input_Value == "70007")
                {
                    StartCoroutine(Auto_Typing_Dialog_Text(dialog_List_Data[6]["DIALOG_TEXT"].ToString()));
                }
            }
        }
        else
        {
            dialog_Box.SetActive(false);
            Talk_Event_Next_Input();
        }
    }
    // SELECT_MAKE 함수
    public void SELECT_MAKE()
    {
        select_Box.SetActive(true);
    }
    public void Button_Select_Make_Pressed(int count)
    {
        select_Box.SetActive(false);
        Select_Event(count);
    }
    // Select 버튼 누를시 발동하는 이벤트 
    private void Select_Event(int count)
    {
        current_Event = new Current_Talk_Event();
        switch (count)
        {
            case 1:
                {
                    current_Event.event_Name = event_List_Data[4]["EVENT_NAME"].ToString();
                    current_Event.end_Event_Count = (int)event_List_Data[4]["EVENT_COUNT"];
                    current_Event.current_Event_Count++;
                    current_Event.event_Fuction = event_List_Data[4]["EVENT_1"].ToString();
                    current_Event.input_Value = event_List_Data[4]["EVENT_IN_1"].ToString();
                    trigger_List[4].Play_Count += 2;
                }
                break;
            case 2:
                {
                    current_Event.event_Name = event_List_Data[5]["EVENT_NAME"].ToString();
                    current_Event.end_Event_Count = (int)event_List_Data[5]["EVENT_COUNT"];
                    current_Event.current_Event_Count++;
                    current_Event.event_Fuction = event_List_Data[5]["EVENT_1"].ToString();
                    current_Event.input_Value = event_List_Data[5]["EVENT_IN_1"].ToString();
                    trigger_List[5].Play_Count += 2;
                }
                break;
            case 3:
                {
                    current_Event.event_Name = event_List_Data[6]["EVENT_NAME"].ToString();
                    current_Event.end_Event_Count = (int)event_List_Data[6]["EVENT_COUNT"];
                    current_Event.current_Event_Count++;
                    current_Event.event_Fuction = event_List_Data[6]["EVENT_1"].ToString();
                    current_Event.input_Value = event_List_Data[6]["EVENT_IN_1"].ToString();
                    trigger_List[6].Play_Count += 2;
                }
                break;
        }
        if (current_Event.event_Name == "EVENT_3") trigger_List[2].Play_Count++;
        Event_Run_Manager();
    }
    // end 함수 모음
    // --------------------------------
    private void Talk_Event_Next_Input()
    {
        current_Event.current_Event_Count++;
        if (current_Event.event_Name == "EVENT_1")
        {
            if(current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "CM_L";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 3)
            {
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70001";
            }
            if (current_Event.current_Event_Count == 5)
            {
                current_Event.event_Fuction = "CO_L";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 6)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "school_img";
            }
        } // end event_1
        if (current_Event.event_Name == "EVENT_2")
        {
            if(current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "CM_L";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 3)
            {
                current_Event.event_Fuction = "CM_R";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 4)
            {
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70003";
            }
            if (current_Event.current_Event_Count == 7)
            {
                current_Event.event_Fuction = "CO_ML";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 8)
            {
                current_Event.event_Fuction = "CO_MR";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 9)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "market_img";
            }
        } // end event_2
        if (current_Event.event_Name == "EVENT_3")
        {
            if (current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "CM_LM";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 3)
            {
                current_Event.event_Fuction = "CM_RM";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 4)
            {
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70006";
            }
            if (current_Event.current_Event_Count == 6)
            {
                current_Event.event_Fuction = "CO_L";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 7)
            {
                current_Event.event_Fuction = "CO_R";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 8)
            {
                current_Event.event_Fuction = "SELECT_MAKE";
                current_Event.input_Value = "78001";
            }
        } // end event_3
        if (current_Event.event_Name == "EVENT_4")
        {
            if (current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "CM_LDM";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 3)
            {
                current_Event.event_Fuction = "CM_RDM";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 4)
            {
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70008";
            }
            if (current_Event.current_Event_Count == 5)
            {
                current_Event.event_Fuction = "CO_L";
                current_Event.input_Value = "rena_ex";
            }
            if (current_Event.current_Event_Count == 6)
            {
                current_Event.event_Fuction = "CO_R";
                current_Event.input_Value = "marienne";
            }
            if (current_Event.current_Event_Count == 7)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "grassland_img";
            }
        } // end event_4
        if (current_Event.event_Name == "EVENT_5")
        {
            if (current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "grassland_img";
            }
        } // end event_5
        if (current_Event.event_Name == "EVENT_6")
        {
            if (current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "grassland_img";
            }
        } // end event_6
        if (current_Event.event_Name == "EVENT_7")
        {
            if (current_Event.current_Event_Count == 2)
            {
                current_Event.event_Fuction = "DM";
                current_Event.input_Value = "70011";
            }
            if (current_Event.current_Event_Count == 3)
            {
                current_Event.event_Fuction = "BO";
                current_Event.input_Value = "grassland_img";
            }
        } // end event_7
        Event_Run_Manager();
    }
    // 각종 이벤트 실행 매니저
    private void Event_Run_Manager()
    {
        if (current_Event.current_Event_Count <= current_Event.end_Event_Count)
        {
            switch (current_Event.event_Fuction)
            {
                case "BM":
                    {
                        BG_MAKE();  // 배경화면 IN
                    }
                    break;
                case "CM_L":
                    {
                        Char_Make_Left(); // 왼쪽케릭터 제자리 FADE_IN
                    }break;
                case "CM_R":
                    {
                        Char_Make_Right(); // 오른쪽케릭터 제자지 FADE_IN
                    }
                    break;
                case "DM":
                    {
                        DIALOG_MAKE(); // 대화 풍선 IN
                    }break;
                case "CO_L":
                    {
                        Left_Character_Fade_Out(); // 왼쪽 케릭터 제자리 FADE_OUT
                    }
                    break;
                case "CO_R":
                    {
                        Right_Character_Fade_Out(); // 오른쪽 케릭터 제자리 FADE_OUT
                    }
                    break;
                case "CO_ML":
                    {
                        Char_Out_Move_Left(); // 왼쪽 케릭터 왼쪽으로 이동하면서 FADE_OUT
                    }
                    break;
                case "CO_MR":
                    {
                        Char_Out_Move_Right(); // 오른쪽 케릭터 오른쪽으로 이동하면서 FADE_OUT
                    }
                    break;
                case "CM_LM":
                    {
                        Char_Make_LM(); // 왼쪽 케릭터 왼쪽 밖에서 오른쪽 방향 이동하면서 FADE IN
                    }
                    break;
                case "CM_RM":
                    {
                        Char_Make_RM(); // 오른쪽 케릭터 오른쪽 밖에서 왼쪽 방향 이동하면서 FADE IN
                    }
                    break;
                case "CM_LDM":
                    {
                        Char_Make_LDM(); // 왼쪽 케릭터 왼쪽 아래에서 부터 올라와서 FADE IN
                    }
                    break;
                case "CM_RDM":
                    {
                        Char_Make_RDM(); // 오른쪽 케릭터 오른쪽 아래에서 부터 올라와서 FADE IN
                    }
                    break;
                case "SELECT_MAKE":
                    {
                        SELECT_MAKE(); // 선택지 생성
                    }
                    break;
                case "BO":
                    {
                        BG_OUT(); // 배경화면 OUT
                    }
                    break;
            }
        }        
    }
    // -------------------------------
} // class








