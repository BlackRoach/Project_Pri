using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Item_Table_Json : MonoBehaviour {

    public static Item_Table_Json instance = null;

    public Item_Table_List[] item_Table;

    public string mobile_Path;

    private JsonData save_Json;
    private JsonData load_Json;

    private void Awake()
    {
        if (instance != null)
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
        item_Table = new Item_Table_List[2];

        if (Inventory_Manager.instance.is_Restart == false)
        {
            for (int i = 0; i < item_Table.Length; i++)
            {
                item_Table[i] = new Item_Table_List(-1);
            }
            Item_Table_Json_Save();
            Inventory_Manager.instance.is_Restart = true;
        }
    }

    public void Item_Table_Json_Save()
    {
        save_Json = JsonMapper.ToJson(item_Table);

        File.WriteAllText(mobile_Path + "/" + "Item_Table.json", save_Json.ToString());
    }

    public void Item_Table_Json_Load()
    {
        if (File.Exists(mobile_Path + "/" + "Item_Table.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_Table.json");

            load_Json = JsonMapper.ToObject(json_String);

            for (int i = 0; i < item_Table.Length; i++)
            {
                item_Table[i] = new Item_Table_List((int)load_Json[i]["id"]);
            }
        }
        else
        {
            Debug.Log("file is not found!");
        }
    }


} // class









