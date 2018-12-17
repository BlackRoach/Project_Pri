using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Inventory_Current_Items : MonoBehaviour {

    public static Inventory_Current_Items instance = null;

    public Current_Item[] current_Items; // 인벤토리
    public Current_Item[] equip_Items;   // 장착 아이템

    public string mobile_Path;

    private JsonData item_Data;
    private JsonData save_Json;  // 인벤토리 저장
    private JsonData load_Json;  // 인벤토리 로드 

    private JsonData equip_Save_Json; // 장착 아이템 저장
    private JsonData equip_Load_Json; // 장착 아이템 로드 

    private bool is_Defulat = false;

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
        mobile_Path = Application.persistentDataPath;
        current_Items = new Current_Item[20]; // 인벤토리 아이템 
        equip_Items = new Current_Item[2]; // 장 착 아이템 
        for (int i = 0; i < current_Items.Length; i++)
        {
            current_Items[i] = new Current_Item();
        }
        for(int i = 0; i < equip_Items.Length; i++)
        {
            equip_Items[i] = new Current_Item();
        }
        
        Json_Data_Parsing();
        LOAD_NEW_DATA_JSON_Inventory();
        if (is_Defulat)
        {
            is_Defulat = false;
            Defualt_Data_Input();
            SAVE_NEW_DATA_JSON_Inventory();
        }
    }
    // json파일 가져오기
    private void Json_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/ITEM_DATA");

        item_Data = JsonMapper.ToObject(json_File.text);
    }
    // 초반에 기본 아이템 증정  
    private void Defualt_Data_Input()
    {
        current_Items[0] = new Current_Item((int)item_Data[0]["ID"],1);
        current_Items[1] = new Current_Item((int)item_Data[1]["ID"],5);     
    }
    // Json 저장 인벤토리
    public void SAVE_NEW_DATA_JSON_Inventory()
    {
        save_Json = JsonMapper.ToJson(current_Items);

        File.WriteAllText(mobile_Path + "/" + "Inventory_Item_List.json", save_Json.ToString());
    }
    // Json 로드 인벤토리
    public void LOAD_NEW_DATA_JSON_Inventory()
    {
        if (File.Exists(mobile_Path + "/" + "Inventory_Item_List.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Inventory_Item_List.json");

            load_Json = JsonMapper.ToObject(json_String);

            for(int i = 0; i < current_Items.Length; i++)
            {
                current_Items[i] = new Current_Item();
            }
            for (int i = 0; i < load_Json.Count; i++)
            {
                current_Items[i].ID = (int)load_Json[i]["ID"];
                current_Items[i].COUNT = (int)load_Json[i]["COUNT"];
            }
        }
        else
        {
            Debug.Log("file is not found!");
            is_Defulat = true;
        }
    }
    // Json 저장  장착아이템
    public void EQUIP_ITEMS_SAVE_NEW_DATA_JSON()
    {
        equip_Save_Json = JsonMapper.ToJson(equip_Items);

        File.WriteAllText(mobile_Path + "/" + "Equip_Item_List.json", equip_Save_Json.ToString());
    }
    // Json 로드 장착아이템
    public void EQUIP_ITEMS_LOAD_NEW_DATA_JSON()
    {
        if (File.Exists(mobile_Path + "/" + "Equip_Item_List.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Equip_Item_List.json");

            equip_Load_Json = JsonMapper.ToObject(json_String);

            for (int i = 0; i < equip_Items.Length; i++)
            {
                equip_Items[i] = new Current_Item();
            }
            for (int i = 0; i < equip_Load_Json.Count; i++)
            {
                equip_Items[i].ID = (int)equip_Load_Json[i]["ID"];
                equip_Items[i].COUNT = (int)equip_Load_Json[i]["COUNT"];
            }
        }
        else
        {
            Debug.Log("file is not found!");
        }
    }
    // ---------------------------------------------------------
    // 외부씬에서 인벤토리 이용하고싶을때 작동 하는 함수 ( 아이템 구매 )
    // 이 함수 만 사용하면 됩니다.. 
    // parameter 에 30001 경우 노브헬멧 30002 경우 a 포션 30003 경우 강철방패
    public void Button_Add_More_Item(int _id)
    {
        switch (_id)
        {
            case 30001:
                {
                    for(int i = 0; i < current_Items.Length; i++)
                    {
                        if(current_Items[i].ID == -1)
                        {
                            current_Items[i] = new Current_Item((int)item_Data[0]["ID"], 1);
                            break;
                        }
                    }
                }break;
            case 30002:
                {
                    bool has_Already = false; // 물약 이미 가져잇는지 아닌지 여부 판단
                    for(int i = 0; i < current_Items.Length; i++)
                    {
                        if(current_Items[i].ID == 30002)
                        {
                            has_Already = true;
                            break;
                        }
                    }
                    if (has_Already)
                    {
                        for (int i = 0; i < current_Items.Length; i++)
                        {
                            if (current_Items[i].ID == 30002)
                            {
                                current_Items[i].COUNT += 2;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < current_Items.Length; i++)
                        {
                            if (current_Items[i].ID == -1)
                            {
                                current_Items[i] = new Current_Item((int)item_Data[1]["ID"], 2);
                                break;
                            }
                        }
                    }
                }
                break;
            case 30003:
                {
                    for (int i = 0; i < current_Items.Length; i++)
                    {
                        if (current_Items[i].ID == -1)
                        {
                            current_Items[i] = new Current_Item((int)item_Data[2]["ID"], 1);
                            break;
                        }
                    }
                }
                break;
        } // end switch
        SAVE_NEW_DATA_JSON_Inventory();
    }
    // ----------------------------------------------------------
} // class











