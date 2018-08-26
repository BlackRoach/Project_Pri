using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class UI_Event_Manager : MonoBehaviour {

    public GameObject event_Entry_Name_1;
    public GameObject event_Entry_Name_2;

    [SerializeField]
    private int random_Number;

    private bool is_Available;

    private JsonData event_Member_Data;

    private void Start()
    {
        TextAsset event_List = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/EVENT_MEMBER");

        event_Member_Data = JsonMapper.ToObject(event_List.text);

        random_Number = 0;

        is_Available = true;
    }
    private void Update()
    {
        Event_Entry_Festibal_List_Input();
    }


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
        for(int i = 0; i < event_Member_Data.Count; i++)
        {
            if ((int)event_Member_Data[i]["FESTIVAL_ENTRY"] == 1)
            {
                
            }
            if ((int)event_Member_Data[i]["FESTIVAL_ENTRY"] == 2)
            {

            }
            if ((int)event_Member_Data[i]["FESTIVAL_ENTRY"] == 3)
            {

            }
        }
    }



} // class






