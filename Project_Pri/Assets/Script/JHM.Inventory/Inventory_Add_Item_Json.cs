using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class Inventory_Add_Item_Json : MonoBehaviour {

    public static Inventory_Add_Item_Json instance = null;
    
    public Items_List[] inventory_Item_List;

    public Items_List[] load_Item_Data;

    private string mobile_Path;
    
    private JsonData save_Json;
    private JsonData load_Json;

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
        load_Item_Data = new Items_List[20];
        inventory_Item_List = new Items_List[20];

        mobile_Path = Application.persistentDataPath;

        if (Inventory_Manager.instance.is_Defualt == false)
        {
            Inventory_Manager.instance.is_Defualt = true;
            Defualt_Inventory_Value();
        }
        else
        {
            LOAD_NEW_DATA_JSON_Inventory();

            for (int i = 0; i < load_Item_Data.Length; i++)
            {
                inventory_Item_List[i] = new Items_List(load_Item_Data[i].id,load_Item_Data[i].amount,
                    load_Item_Data[i].stackable);
            }
        }
        
    }
     // Json 저장
    public void SAVE_NEW_DATA_JSON_Inventory()
    { 
        save_Json = JsonMapper.ToJson(inventory_Item_List);

        File.WriteAllText(mobile_Path + "/" + "Inventory_Item_List.json", save_Json.ToString());
    }

    // Json 로드
    public void LOAD_NEW_DATA_JSON_Inventory()
    {
        if (File.Exists(mobile_Path + "/" + "Inventory_Item_List.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Inventory_Item_List.json");

            load_Json = JsonMapper.ToObject(json_String);

            for (int i = 0; i < load_Item_Data.Length; i++)
            {
                load_Item_Data[i] = new Items_List((int)load_Json[i]["id"], (int)load_Json[i]["amount"],
                    (bool)load_Json[i]["stackable"]);
            }
        }
        else
        {
            Debug.Log("file is not found!");
        }        
    }
    // 인벤토리 초기템
    public void Defualt_Inventory_Value()
    {
        for (int i = 0; i < 20; i++)
        {
            inventory_Item_List[i] = new Items_List();
            inventory_Item_List[i].stackable = false;
            inventory_Item_List[i].amount = 1;
        }
        // 초기 값 아이템
        inventory_Item_List[0].id = 30001;
        inventory_Item_List[1].id = 30002;
        inventory_Item_List[1].stackable = true;
        inventory_Item_List[1].amount += 4;

        SAVE_NEW_DATA_JSON_Inventory();
    }

    
    
    
} // class









