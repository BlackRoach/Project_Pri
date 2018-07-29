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
    public Text text_Event_Count;

    public Event_State event_1;
    public Event_State event_2;

    private JsonData load_Data;

    private bool event_Key_1 = false;

    private int count;
    public int text_Around_Count;

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
        text_Event_Count.text = count.ToString();
        bg_Event.SetActive(false);
        text_Box.SetActive(false);
        Json_File_Event_Table_Read_Only();

        Defualt_All_Event_State();
    }

    private void Update()
    {
        if (count == (int)load_Data[0]["CONDITION_VALUE"] && !event_Key_1)
        {
            event_1.event_State = 1;
            event_1.trigger = true;

            bg_Event.SetActive(true);

            if (!event_Key_1)
            {
                event_Key_1 = true;
                StartCoroutine(Event_Time_Delay());
            }
        }
    }
    IEnumerator Event_Time_Delay()
    {
        yield return new WaitForSeconds(1f);
        if ((int)load_Data[0]["EVENT_CHARACTER_COUNT"] == 2)
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
        yield return new WaitForSeconds(1f);
        text_Around_Count++;
        text_Box.SetActive(true);
        
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

    private void Json_File_Event_Table_Read_Only()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Event_Manager");

        load_Data = JsonMapper.ToObject(json_File.text);
    }
    private void Defualt_All_Event_State()
    {
        event_1.complete = (int)load_Data[0]["DIALOG_COUNT"];
        event_2.complete = (int)load_Data[1]["DIALOG_COUNT"];

        event_1.event_State = 0;
        event_2.event_State = 0;

        event_1.trigger = false;
        event_2.trigger = false;

        event_1.current = 0;
        event_2.current = 0;
    }
    // 다이로그 텍스트 누를시
    public void Button_Text_Around_Count_Fuction()
    {
        if (text_Around_Count <= (int)load_Data[0]["DIALOG_COUNT"])
        {
            text_Around_Count++;
        }
    }

   
} // class











