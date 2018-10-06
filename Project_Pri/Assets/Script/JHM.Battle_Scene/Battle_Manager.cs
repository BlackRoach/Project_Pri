using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class Battle_Manager : MonoBehaviour {

    public static Battle_Manager instance = null;
    // 케릭터 위치와 이미지 오브젝트
    public GameObject main_Character_Pos;
    public GameObject target_Character_Pos; 
    // 케릭터 능력치 value
    public GameObject main_Character_Value;
    public GameObject target_Character_Value;

    public Text text_Announcement;
    public Text text_current_Year_Count;
    public Text text_Winner;
    public Text text_Sparring_Char_Score;
    public Text text_Main_Char_Score;

    public bool battle_Done_1, battle_Done_2, is_Done;

    private JsonData battle_Data;

    [SerializeField]
    private int gambling;
    [SerializeField]
    private int mainChar_Muscularstrength; // 주인공 근력값
    [SerializeField]
    private int current_Year; // 진행 연도
    [SerializeField]
    private int sparring_Char_Score;
    [SerializeField]
    private int main_Char_Score;

    public bool battle_Start; // 케릭터 움직임 시작

    [SerializeField]
    private int score_main, score_target; // 최종 스코어
    [SerializeField]
    private bool event_Over , _off;
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
        event_Over = _off = false;
        text_Announcement.text = " ";
        sparring_Char_Score = main_Char_Score = 0;
        text_Winner.text = " ";
        text_Sparring_Char_Score.text = sparring_Char_Score.ToString();
        text_Main_Char_Score.text = main_Char_Score.ToString();
        text_current_Year_Count.text = 0.ToString();
        gambling = 0;
        current_Year = 0;
        Defualt_Json_Battle_Data_Parsing();
        mainChar_Muscularstrength = PlayerPrefs.GetInt("s_muscular_strength");
        // 케릭터 이미지 빈공간  실행시
        main_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = null;
        target_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = null;
        main_Character_Pos.SetActive(false);
        target_Character_Pos.SetActive(false);

        battle_Start = false;
        battle_Done_1 = battle_Done_2 = is_Done = false;
        score_main = score_target = 0;

    }
    private void Defualt_Json_Battle_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/SPARRING_DATA");

        battle_Data = JsonMapper.ToObject(json_File.text);
    }
    private void Update()
    {
        if (battle_Done_1 && battle_Done_2)
        {
            if (!is_Done)
            {
                is_Done = true;
            }
        }

        Reset_All_The_Data_Or_Event_Finished();

        Finished_Event_Sparring();
    }

    // 진행 연도 plus,minus
    public void Button_Current_Year_Add_One()
    {
        current_Year++;
        text_current_Year_Count.text = current_Year.ToString();
    }
    public void Button_Current_Year_Sub_One()
    {
        current_Year--;
        text_current_Year_Count.text = current_Year.ToString();
    }
    // ------------------
    // 대련자 선정
    public void Button_Start_Character_Selection()
    {
        if (!battle_Start)
        {
            int temp = 0;
            temp = Random.Range(1, 4);
            gambling = temp;
            Set_Sparring_Character();
            Set_Main_Character();

            // -------------------------------

            target_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" +
                battle_Data[gambling - 1]["CHARACTER_SD"]);

            target_Character_Pos.transform.GetChild(0).transform.localScale = new Vector3(-1f, 1f, 1f);

            main_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" +
                "character_sd_eileen");

            main_Character_Pos.SetActive(true);
            target_Character_Pos.SetActive(true);
        }
    }

    private void Set_Sparring_Character()
    {
        target_Character_Value.transform.GetChild(0).GetComponent<Text>().text = battle_Data[gambling - 1]["NAME"].ToString();
        target_Character_Value.transform.GetChild(1).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE"].ToString();
        target_Character_Value.transform.GetChild(2).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_BASIC"].ToString();
        target_Character_Value.transform.GetChild(3).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_PLUS"].ToString();
    }
    private void Set_Main_Character()
    {
        main_Character_Value.transform.GetChild(1).GetComponent<Text>().text = mainChar_Muscularstrength.ToString();
        main_Character_Value.transform.GetChild(2).GetComponent<Text>().text = 10.ToString();
        main_Character_Value.transform.GetChild(3).GetComponent<Text>().text = 10.ToString();
    }
    // -------------------
    // 대련 개시
    public void Button_Sparring_Is_Start()
    {
        text_Announcement.text = battle_Data[gambling - 1]["START_TEXT"].ToString(); // 멘트
        battle_Start = true;
        Sparring_Calculate_Result(); // result 결과
    }

    private void Sparring_Calculate_Result()
    {
        sparring_Char_Score = Random.Range(1, (int)battle_Data[gambling - 1]["STATE"] + 1) + (int)battle_Data[gambling - 1]["STATE_BASIC"]
            + ((int)battle_Data[gambling - 1]["STATE_PLUS"] * current_Year);
        main_Char_Score = Random.Range(1, mainChar_Muscularstrength + 1) + 10 + (10 * current_Year);
    }
    // 결과 점수 
    public void Round_sparring_Battle_Score()
    {
        target_Character_Value.transform.GetChild(4).GetComponent<Text>().text = sparring_Char_Score.ToString();
        main_Character_Value.transform.GetChild(4).GetComponent<Text>().text = main_Char_Score.ToString();
        battle_Done_1 = true;
        battle_Done_2 = true;
    }
    public void Setting_Score()
    {
        if(main_Char_Score >= sparring_Char_Score)
        {
            score_main++;
            text_Main_Char_Score.text = score_main.ToString();
            text_Announcement.text = battle_Data[gambling - 1]["WIN_TEXT"].ToString();
        }
        else
        {
            score_target++;
            text_Sparring_Char_Score.text = score_target.ToString();
            text_Announcement.text = battle_Data[gambling - 1]["LOSE_TEXT"].ToString();
        }
    }
    // -----------------------------------
    // 초기화
    public void Reset_All_The_Data_Or_Event_Finished()
    {
        if(score_main == 2 || score_target == 2)
        {
            if(score_main == 2)
            {
                text_Announcement.text = " 우 승 자는 주 인 공 ";
                text_Winner.text = " 주 인 공 ";
            }
            if (score_target == 2)
            {
                text_Announcement.text = " 우 승 자는 "+ battle_Data[gambling - 1]["NAME"].ToString(); 
                text_Winner.text = battle_Data[gambling - 1]["NAME"].ToString();
            }
            event_Over = true;
            return;
        }
        if(!battle_Start && battle_Done_1 && battle_Done_2 && is_Done)
        {
            sparring_Char_Score = main_Char_Score = 0;
            target_Character_Value.transform.GetChild(4).GetComponent<Text>().text = sparring_Char_Score.ToString();
            main_Character_Value.transform.GetChild(4).GetComponent<Text>().text = main_Char_Score.ToString();
            battle_Done_1 = battle_Done_2 = is_Done = false;

            // ---------------------------------

            Button_Sparring_Is_Start();
        }
    }
    

    private void Finished_Event_Sparring()
    {
        if (event_Over && !battle_Start)
        {
            if (!_off)
            {
                text_Announcement.text = " ";
                text_current_Year_Count.text = 0.ToString();
                sparring_Char_Score = main_Char_Score = 0;
                gambling = current_Year = 0;
                battle_Done_1 = battle_Done_2 = is_Done =  false;
                _off = true;
                target_Character_Value.transform.GetChild(4).GetComponent<Text>().text = sparring_Char_Score.ToString();
                main_Character_Value.transform.GetChild(4).GetComponent<Text>().text = main_Char_Score.ToString();
                main_Character_Pos.SetActive(false);
                target_Character_Pos.SetActive(false);
                score_main = score_target = 0;
                text_Main_Char_Score.text = score_main.ToString();
                text_Sparring_Char_Score.text = score_target.ToString();
                StartCoroutine(End_Of_Sparring_Event());
            }
        }
    }
    IEnumerator End_Of_Sparring_Event()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Talk_Scene");
    }
} // class












