using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class NewInventory_Items_Data : MonoBehaviour {

    public static NewInventory_Items_Data instance;

    public string mobile_Path; // 모바일 저장 경로

    public List<Items_List> item_List;  // 현재 인벤토리 아이템 리스트

    // 디폴트 json_data
    private JsonData item_List_Data;

    private void Awake()
    {
        mobile_Path = Application.persistentDataPath;

        Json_Data_Parsing();
    }
    // defualt json 리소스 파일 푸싱
    private void Json_Data_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/ITEM_LIST_2");

        item_List_Data = JsonMapper.ToObject(json_File_1.text);
    }
    // Json 저장 인벤토리 아이템 리스트
    public void SAVE_NEW_DATA_JSON_ITEMS_LIST()
    {
        JsonData save_Json = JsonMapper.ToJson(item_List);

        File.WriteAllText(mobile_Path + "/" + "Item_List_Data.json", save_Json.ToString());
    }
    // Json 로드 인벤토리 아이템 리스트
    public void LOAD_NEW_DATA_JSON_ITEMS_LIST()
    {
        if (File.Exists(mobile_Path + "/" + "Item_List_Data.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_List_Data.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);            
        }
        else
        {
            Debug.Log("file is not found!");
        }
    }


} // class












