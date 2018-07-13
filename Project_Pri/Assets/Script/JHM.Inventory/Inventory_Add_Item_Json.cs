using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class Inventory_Add_Item_Json : MonoBehaviour {

    public static Inventory_Add_Item_Json instance = null;
    
    public List<Items_List> inventory_Item_List;

    public string mobile_Path;

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
        mobile_Path = Application.persistentDataPath;

        for (int i = 0; i < 20; i++)
        {
            inventory_Item_List.Add(new Items_List());
        }       
    }

    public void SAVE_NEW_DATA_JSON_Inventory()
    { 
        save_Json = JsonMapper.ToJson(inventory_Item_List);

        File.WriteAllText(mobile_Path + "/" + "Inventory_Item_List.json", save_Json.ToString());
    }

    public void LOAD_NEW_DATA_JSON_Inventory()
    {
        List<Items_List> new_Item_Data = new List<Items_List>();

        for(int i = 0; i < 20; i++)
        {
            new_Item_Data.Add(new Items_List());
        }

        if (File.Exists(mobile_Path + "/" + "Inventory_Item_List.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Inventory_Item_List.json");

            load_Json = JsonMapper.ToObject(json_String);
            
            for(int i = 0; i < new_Item_Data.Count; i++)
            {
                new_Item_Data[i] = new Items_List((int)load_Json[i]["id"],(int)load_Json[i]["amount"],
                    (bool)load_Json[i]["stackable"]);
            }                      
        }
        else
        {
            Debug.Log("file is not found!");
        }
    }





} // class









