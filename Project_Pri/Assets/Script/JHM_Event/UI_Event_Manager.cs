using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class UI_Event_Manager : MonoBehaviour
{

    public GameObject event_Entry_Name_1;
    public GameObject event_Entry_Name_2;

    public GameObject event_Character_Ability_1;
    public GameObject event_Character_Ability_2;

    public GameObject event_Result_1;
    public GameObject event_Result_2;
    [SerializeField]
    private Text text_Winner_Name;
    [SerializeField]
    private Text text_Year;
    [SerializeField]
    private int random_Number;

    private bool is_Available;

    private JsonData event_Member_Data;
    [SerializeField]
    private int current_Year;
    [SerializeField]
    private int[] see_Result = new int[8];

    private int[] time_Line = new int[8];
    [SerializeField]
    private bool[] find_Winner = new bool[8];

    private bool is_Done;
    private void Start()
    {
        TextAsset event_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/EVENT_MEMBER");

        event_Member_Data = JsonMapper.ToObject(event_List.text);

        random_Number = 0;

        is_Available = true;

        current_Year = 0;

        is_Done = false;
        Reset_All_The_Data();
    }
    private void Update()
    {
        // 이벤트 참가자 나타내기
        Event_Entry_Festibal_List_Input();
        // 우승자 결과 나타내기
        if (find_Winner[0] == true && find_Winner[1] == true && find_Winner[2] == true && find_Winner[3] == true &&
            find_Winner[4] == true && find_Winner[5] == true && find_Winner[6] == true && find_Winner[7] == true)
        {
            if (!is_Done)
            {
                is_Done = true;
                int winner = 0;
                for (int i = 0; i < see_Result.Length; i++)
                {
                    if (winner <= see_Result[i])
                    {
                        winner = see_Result[i];
                    }
                }
                for (int i = 0; i < see_Result.Length; i++)
                {
                    if (see_Result[i] == winner)
                    {
                        if (i >= 0 && i < 4)
                        {
                            text_Winner_Name.text = event_Entry_Name_1.transform.GetChild(i).GetComponent<Text>().text.ToString();
                        }
                        if (i >= 4 && i < 8)
                        {
                            text_Winner_Name.text = event_Entry_Name_2.transform.GetChild(i - 4).GetComponent<Text>().text.ToString();
                        }
                    }
                }
                Reset_All_The_Data();
            }
        }
    }
    // 초기화
    private void Reset_All_The_Data()
    {
        for (int i = 0; i < time_Line.Length; i++)
        {
            time_Line[i] = 0;
        }
        for (int i = 0; i < find_Winner.Length; i++)
        {
            find_Winner[i] = false;
        }
    }
    public void Button_Year_Count_Plus_One()
    {
        current_Year++;
        text_Year.text = current_Year.ToString();
    }
    public void Button_Year_Count_Minus_One()
    {
        current_Year--;
        text_Year.text = current_Year.ToString();
    }

    // 1 ~ 3 랜덤값산출
    public void Button_Pressed_Ramdom_Number()
    {
        is_Available = true;
        random_Number = Random.Range(1, 4);
    }

    private void Event_Entry_Festibal_List_Input()
    {
        if (is_Available)
        {
            switch (random_Number)
            {
                case 1:
                    {
                        Event_Entry_List_Push();
                    }
                    break;
                case 2:
                    {
                        Event_Entry_List_Push();
                    }
                    break;
                case 3:
                    {
                        Event_Entry_List_Push();
                    }
                    break;
            }
            is_Available = false;
        }
    }
    private void Event_Entry_List_Push()
    {
        int name_Index_1 = 0;
        int name_Index_2 = 0;
        int ability_Index_1 = 0;
        int ability_Index_2 = 0;
        for (int i = 0; i < event_Member_Data.Count; i++)
        {
            if ((int)event_Member_Data[i]["FESTIVAL_ENTRY"] == random_Number)
            {
                if ((int)event_Member_Data[i]["NUMBER"] <= 8)
                {
                    if ((int)event_Member_Data[i]["NUMBER"] >= 1 && (int)event_Member_Data[i]["NUMBER"] <= 4)
                    {
                        event_Entry_Name_1.transform.GetChild(name_Index_1).GetComponent<Text>().text
                            = event_Member_Data[i]["NAME_"].ToString();

                        event_Character_Ability_1.transform.GetChild(ability_Index_1).GetComponent<Text>().text
                            = event_Member_Data[i]["STATE"].ToString();
                        ability_Index_1++;
                        event_Character_Ability_1.transform.GetChild(ability_Index_1).GetComponent<Text>().text
                            = event_Member_Data[i]["BASIC_PLUS"].ToString();
                        ability_Index_1++;
                        event_Character_Ability_1.transform.GetChild(ability_Index_1).GetComponent<Text>().text
                            = event_Member_Data[i]["PLUS"].ToString();
                        ability_Index_1++;
                        name_Index_1++;
                    }
                    if ((int)event_Member_Data[i]["NUMBER"] >= 5 && (int)event_Member_Data[i]["NUMBER"] <= 8)
                    {
                        event_Entry_Name_2.transform.GetChild(name_Index_2).GetComponent<Text>().text
                            = event_Member_Data[i]["NAME_"].ToString();

                        event_Character_Ability_2.transform.GetChild(ability_Index_2).GetComponent<Text>().text
                            = event_Member_Data[i]["STATE"].ToString();
                        ability_Index_2++;
                        event_Character_Ability_2.transform.GetChild(ability_Index_2).GetComponent<Text>().text
                            = event_Member_Data[i]["BASIC_PLUS"].ToString();
                        ability_Index_2++;
                        event_Character_Ability_2.transform.GetChild(ability_Index_2).GetComponent<Text>().text
                            = event_Member_Data[i]["PLUS"].ToString();
                        ability_Index_2++;
                        name_Index_2++;
                    }
                }
            }
        }
    }
    // 점수 산출 과정 
    public void Event_Calculater()
    {
        is_Done = false;
        int[] state = new int[8];
        for (int i = 0; i < 8; i++)
        {
            state[i] = (int)event_Member_Data[i]["STATE"];

        }

        for (int i = 0; i < 8; i++)
        {
            see_Result[i] = (Random.Range(1, state[i] + 1)) + 10 * (1 + current_Year);
        }

        current_Year++;

        text_Year.text = current_Year.ToString();

        StartCoroutine(Event_Character_Number_1_Time_Line());
        StartCoroutine(Event_Character_Number_2_Time_Line());
        StartCoroutine(Event_Character_Number_3_Time_Line());
        StartCoroutine(Event_Character_Number_4_Time_Line());
        StartCoroutine(Event_Character_Number_5_Time_Line());
        StartCoroutine(Event_Character_Number_6_Time_Line());
        StartCoroutine(Event_Character_Number_7_Time_Line());
        StartCoroutine(Event_Character_Number_8_Time_Line());
    }
    // 각점수 타임 라인
    IEnumerator Event_Character_Number_1_Time_Line()
    {
        time_Line[0]++;
        event_Result_1.transform.GetChild(0).GetComponent<Text>().text = time_Line[0].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[0] > time_Line[0])
        {
            StartCoroutine(Event_Character_Number_1_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_1_Time_Line());
            find_Winner[0] = true;
        }
    }
    IEnumerator Event_Character_Number_2_Time_Line()
    {
        time_Line[1]++;
        event_Result_1.transform.GetChild(1).GetComponent<Text>().text = time_Line[1].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[1] > time_Line[1])
        {
            StartCoroutine(Event_Character_Number_2_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_2_Time_Line());
            find_Winner[1] = true;
        }
    }
    IEnumerator Event_Character_Number_3_Time_Line()
    {
        time_Line[2]++;
        event_Result_1.transform.GetChild(2).GetComponent<Text>().text = time_Line[2].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[2] > time_Line[2])
        {
            StartCoroutine(Event_Character_Number_3_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_3_Time_Line());
            find_Winner[2] = true;
        }
    }
    IEnumerator Event_Character_Number_4_Time_Line()
    {
        time_Line[3]++;
        event_Result_1.transform.GetChild(3).GetComponent<Text>().text = time_Line[3].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[3] > time_Line[3])
        {
            StartCoroutine(Event_Character_Number_4_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_4_Time_Line());
            find_Winner[3] = true;
        }
    }
    IEnumerator Event_Character_Number_5_Time_Line()
    {
        time_Line[4]++;
        event_Result_2.transform.GetChild(0).GetComponent<Text>().text = time_Line[4].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[4] > time_Line[4])
        {
            StartCoroutine(Event_Character_Number_5_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_5_Time_Line());
            find_Winner[4] = true;
        }
    }
    IEnumerator Event_Character_Number_6_Time_Line()
    {
        time_Line[5]++;
        event_Result_2.transform.GetChild(1).GetComponent<Text>().text = time_Line[5].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[5] > time_Line[5])
        {
            StartCoroutine(Event_Character_Number_6_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_6_Time_Line());
            find_Winner[5] = true;
        }
    }
    IEnumerator Event_Character_Number_7_Time_Line()
    {
        time_Line[6]++;
        event_Result_2.transform.GetChild(2).GetComponent<Text>().text = time_Line[6].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[6] > time_Line[6])
        {
            StartCoroutine(Event_Character_Number_7_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_7_Time_Line());
            find_Winner[6] = true;
        }
    }
    IEnumerator Event_Character_Number_8_Time_Line()
    {
        time_Line[7]++;
        event_Result_2.transform.GetChild(3).GetComponent<Text>().text = time_Line[7].ToString();
        yield return new WaitForSeconds(0.02f);
        if (see_Result[7] > time_Line[7])
        {
            StartCoroutine(Event_Character_Number_8_Time_Line());
        }
        else
        {
            StopCoroutine(Event_Character_Number_8_Time_Line());
            find_Winner[7] = true;
        }
    }
    // ---------------------
} // class





