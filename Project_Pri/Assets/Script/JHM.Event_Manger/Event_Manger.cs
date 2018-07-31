using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Event_Manger : MonoBehaviour {

    public static Event_Manger instance = null;

    public GameObject bg_Event;
    public GameObject character_Rio;
    public GameObject text_Box;
    public Transform char_Spawn_Pos_1;
    public Transform char_Spawn_Pos_2;
    public Transform text_Box_Pos_1;
    public Transform text_Box_Pos_2;
    public Text text_Event_Count;
    public Text text_Dialog;

    public Event_State event_1;
    public Event_State event_2;

    private JsonData event_Data;
    private JsonData dialog_Data;

    private bool event_Key_1 = false;
    private bool event_Key_2 = false;

    private int count;
    public int text_Around_Count;

    public bool is_Typing;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        count = 0;
        text_Around_Count = 0;
        is_Typing = false;
        text_Event_Count.text = count.ToString();
        bg_Event.SetActive(false);
        text_Box.SetActive(false);
        Json_File_Event_Table_Read_Only();

        Defualt_All_Event_State();
    }

    private void Update()
    {
        if (count == (int)event_Data[0]["CONDITION_VALUE"] && event_1.event_State != 2)
        {
            event_1.event_State = 1;
            event_1.trigger = true;

            bg_Event.SetActive(true);

            if (!event_Key_1)
            {
                event_Key_1 = true;
                StartCoroutine(Event_One_Time_Delay());
            }
        }
        if(count == (int)event_Data[1]["CONDITION_VALUE"] && event_2.event_State != 2)
        {
            event_2.event_State = 1;
            event_2.trigger = true;

            bg_Event.SetActive(true);

            if (!event_Key_2)
            {
                event_Key_2 = true;
                StartCoroutine(Event_Two_Time_Delay());
            }

        }
    }
    // Event_1
    IEnumerator Event_One_Time_Delay()
    {
        yield return new WaitForSeconds(1f);
        if ((int)event_Data[0]["EVENT_CHARACTER_COUNT"] == 2)
        {
            GameObject rio_1 = Instantiate(character_Rio);
            rio_1.transform.SetParent(char_Spawn_Pos_1.transform);
            rio_1.transform.localPosition = Vector3.zero;
            rio_1.transform.localRotation = Quaternion.identity;

            // ---------------------

            GameObject rio_2 = Instantiate(character_Rio);
            rio_2.transform.SetParent(char_Spawn_Pos_2.transform);
            rio_2.transform.localPosition = Vector3.zero;
            rio_2.transform.localRotation = Quaternion.identity;
        }
        yield return new WaitForSeconds(2.5f);
        text_Around_Count++;
        text_Box.SetActive(true);
        StartCoroutine(Auto_Typing());
        if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 1)
        {
            text_Box.transform.SetParent(text_Box_Pos_1.transform);
            text_Box.transform.localPosition = Vector3.zero;
            text_Box.transform.localRotation = Quaternion.identity;
        }
        if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 2)
        {
            text_Box.transform.SetParent(text_Box_Pos_2.transform);
            text_Box.transform.localPosition = Vector3.zero;
            text_Box.transform.localRotation = Quaternion.identity;
        }
    }
    // Event_2
    IEnumerator Event_Two_Time_Delay()
    {
        yield return new WaitForSeconds(1f);
        if ((int)event_Data[1]["EVENT_CHARACTER_COUNT"] == 1)
        {
            GameObject rio_1 = Instantiate(character_Rio);
            rio_1.transform.SetParent(char_Spawn_Pos_1.transform);
            rio_1.transform.localPosition = Vector3.zero;
            rio_1.transform.localRotation = Quaternion.identity;
        }
        yield return new WaitForSeconds(2.5f);
        text_Around_Count++;
        text_Box.SetActive(true);
        StartCoroutine(Auto_Typing());
        if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 1)
        {
            text_Box.transform.SetParent(text_Box_Pos_1.transform);
            text_Box.transform.localPosition = Vector3.zero;
            text_Box.transform.localRotation = Quaternion.identity;
        }
        if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 2)
        {
            text_Box.transform.SetParent(text_Box_Pos_2.transform);
            text_Box.transform.localPosition = Vector3.zero;
            text_Box.transform.localRotation = Quaternion.identity;
        }
    }
    public void Add_One()
    {
        count++;
        text_Event_Count.text = count.ToString();
    }
    public void Sub_One()
    {
        count--;
        text_Event_Count.text = count.ToString();
    }
    // Json_Resource Files 불러오기
    private void Json_File_Event_Table_Read_Only()
    {
        TextAsset event_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Event_Manager");

        event_Data = JsonMapper.ToObject(event_List.text);

        // -----------------------------------

        TextAsset dialog_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Dialog_Manager");

        dialog_Data = JsonMapper.ToObject(dialog_List.text);
    }
    // event_Data 초기화
    private void Defualt_All_Event_State()
    {
        event_1.complete = (int)event_Data[0]["DIALOG_COUNT"];
        event_2.complete = (int)event_Data[1]["DIALOG_COUNT"];

        event_1.event_State = 0;
        event_2.event_State = 0;

        event_1.trigger = false;
        event_2.trigger = false;

        event_1.current = 0;
        event_2.current = 0;
    }
    // 다이로그 텍스트 누를시(Button)
    public void Button_Text_Around_Count_Fuction()
    {
        if (!is_Typing)
        {
            if (event_1.trigger)
            {
                if (text_Around_Count <= (int)event_Data[0]["DIALOG_COUNT"])
                {
                    text_Around_Count++;
                    event_1.current++;
                    if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 1)
                    {
                        text_Box.transform.SetParent(text_Box_Pos_1.transform);
                        text_Box.transform.localPosition = Vector3.zero;
                        text_Box.transform.localRotation = Quaternion.identity;
                    }
                    if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 2)
                    {
                        text_Box.transform.SetParent(text_Box_Pos_2.transform);
                        text_Box.transform.localPosition = Vector3.zero;
                        text_Box.transform.localRotation = Quaternion.identity;
                    }
                    if (text_Around_Count == 2)
                    {
                        StartCoroutine(Auto_Typing());
                    }
                    if (text_Around_Count == 3)
                    {
                        StartCoroutine(Auto_Typing());
                    }
                    if (text_Around_Count == 4)
                    {
                        text_Box.SetActive(false);
                        if (event_1.current == event_1.complete)
                        {
                            bg_Event.SetActive(false);
                            Destroy(char_Spawn_Pos_1.transform.GetChild(0).transform.gameObject);
                            Destroy(char_Spawn_Pos_2.transform.GetChild(0).transform.gameObject);
                        }
                        event_1.trigger = false;
                        event_1.event_State = 2;
                        text_Around_Count = 0;
                    }
                }
            }
            if (event_2.trigger)
            {
                if (text_Around_Count <= (int)event_Data[1]["DIALOG_COUNT"])
                {
                    text_Around_Count++;
                    event_2.current++;
                    if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 1)
                    {
                        text_Box.transform.SetParent(text_Box_Pos_1.transform);
                        text_Box.transform.localPosition = Vector3.zero;
                        text_Box.transform.localRotation = Quaternion.identity;
                    }
                    if ((int)dialog_Data[text_Around_Count - 1]["DIALOG_POSITION"] == 2)
                    {
                        text_Box.transform.SetParent(text_Box_Pos_2.transform);
                        text_Box.transform.localPosition = Vector3.zero;
                        text_Box.transform.localRotation = Quaternion.identity;
                    }
                    if (text_Around_Count == 2)
                    {
                        StartCoroutine(Auto_Typing());
                    }
                    if (text_Around_Count == 3)
                    {
                        text_Box.SetActive(false);
                        if (event_2.current == event_2.complete)
                        {
                            bg_Event.SetActive(false);
                            Destroy(char_Spawn_Pos_1.transform.GetChild(0).transform.gameObject);
                        }
                        event_2.trigger = false;
                        event_2.event_State = 2;
                        text_Around_Count = 0;
                    }
                }
            }
        }
    }
    // Text_Auto 타이핑
    IEnumerator Auto_Typing()
    {
        text_Dialog.text = " ";
        yield return new WaitForSeconds(0f);
        if ((int)dialog_Data[0]["DIALOG_NUMBER"] == text_Around_Count)
        {
            foreach (char text in dialog_Data[0]["DIALOG_TEXT"].ToString().ToCharArray())
            {
                is_Typing = true;

                text_Dialog.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        if ((int)dialog_Data[1]["DIALOG_NUMBER"] == text_Around_Count)
        {
            foreach (char text in dialog_Data[1]["DIALOG_TEXT"].ToString().ToCharArray())
            {
                is_Typing = true;

                text_Dialog.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        if ((int)dialog_Data[2]["DIALOG_NUMBER"] == text_Around_Count)
        {
            foreach (char text in dialog_Data[2]["DIALOG_TEXT"].ToString().ToCharArray())
            {
                is_Typing = true;

                text_Dialog.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        is_Typing = false;
    }
} // class











