using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class Play_Talk_Event : MonoBehaviour {


    public static Play_Talk_Event instance = null;

    public Schedule_Each_Count event_List = new Schedule_Each_Count();  // 대화 이벤트 횟수

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
} // class








