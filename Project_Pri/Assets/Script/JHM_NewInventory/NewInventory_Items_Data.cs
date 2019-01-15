using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class NewInventory_Items_Data : MonoBehaviour {

    public static NewInventory_Items_Data instance = null;

    public string mobile_Path; // 모바일 저장 경로

    public List<New_Item_Data> items_Data_1; // json 파일에 저장,로드할 아이템리스트 Save_Type 1
    public List<New_Item_Data> items_Data_2; // json 파일에 저장,로드할 아이템리스트 Save_Type 2
    public List<New_Item_Data> items_Data_3; // json 파일에 저장,로드할 아이템리스트 Save_Type 3
    public Items_List[] item_List;  // 현재 인벤토리 아이템들의 데이터
    public GameObject item_Prefab; // 인벤토리에 들어갈 아이템 프리펩
    // 디폴트 json_data
    private JsonData item_List_Data;
    private bool is_Begin; // 게임 처음 실행 할때만 적용
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
        is_Begin = false;
        mobile_Path = Application.persistentDataPath;
        Json_Data_Parsing();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 1) {
            LOAD_NEW_DATA_JSON_ITEMS_LIST_1();
            if (is_Begin)  // 초기 값
            {
                is_Begin = false;
                for (int i = 0; i < item_List_Data.Count; i++)
                {
                    items_Data_1.Add(new New_Item_Data((int)item_List_Data[i]["ID"], 1));
                }
                SAVE_NEW_DATA_JSON_ITEMS_LIST_1();
            }  else
            {
                SAVE_NEW_DATA_JSON_ITEMS_LIST_1();
            }
        } else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 2)
        {
            LOAD_NEW_DATA_JSON_ITEMS_LIST_2();
            if (is_Begin)  // 초기 값
            {
                is_Begin = false;
                for (int i = 0; i < item_List_Data.Count; i++)
                {
                    items_Data_2.Add(new New_Item_Data((int)item_List_Data[i]["ID"], 1));
                }
                SAVE_NEW_DATA_JSON_ITEMS_LIST_2();
            } else
            {
                SAVE_NEW_DATA_JSON_ITEMS_LIST_2();
            }
        }else if (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE == 3)
        {
            LOAD_NEW_DATA_JSON_ITEMS_LIST_3();
            if (is_Begin)  // 초기 값
            {
                is_Begin = false;
                for (int i = 0; i < item_List_Data.Count; i++)
                {
                    items_Data_3.Add(new New_Item_Data((int)item_List_Data[i]["ID"], 1));
                }
                SAVE_NEW_DATA_JSON_ITEMS_LIST_3();
            }else
            {
                SAVE_NEW_DATA_JSON_ITEMS_LIST_3();
            }
        }
    }
    private void Start()
    {
        Spawning_Inventory_Items();
    }
    // defualt json 리소스 파일 푸싱
    private void Json_Data_Parsing()
    {
        TextAsset json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/New_Inventory_Data/ITEM_LIST_2");

        item_List_Data = JsonMapper.ToObject(json_File_1.text);
    }
    public void Initailization_Item_List_Data_From_Items_Data_1()
    {
        item_List = new Items_List[items_Data_1.Count];
        for(int i = 0; i < item_List.Length; i++)
        {
            item_List[i] = new Items_List();
        }
        for(int i = 0; i < items_Data_1.Count; i++)
        {
            for(int j = 0; j < item_List_Data.Count; j++)
            {
                if((int)item_List_Data[j]["ID"] == items_Data_1[i].ID)
                {
                    Inventory_Item_Data_Input(i, j);
                    break;
                }
            }
        }
    }
    public void Initailization_Item_List_Data_From_Items_Data_2()
    {
        item_List = new Items_List[items_Data_2.Count];
        for (int i = 0; i < item_List.Length; i++)
        {
            item_List[i] = new Items_List();
        }
        for (int i = 0; i < items_Data_2.Count; i++)
        {
            for (int j = 0; j < item_List_Data.Count; j++)
            {
                if ((int)item_List_Data[j]["ID"] == items_Data_2[i].ID)
                {
                    Inventory_Item_Data_Input(i, j);
                    break;
                }
            }
        }
    }
    public void Initailization_Item_List_Data_From_Items_Data_3()
    {
        item_List = new Items_List[items_Data_3.Count];
        for (int i = 0; i < item_List.Length; i++)
        {
            item_List[i] = new Items_List();
        }
        for (int i = 0; i < items_Data_3.Count; i++)
        {
            for (int j = 0; j < item_List_Data.Count; j++)
            {
                if ((int)item_List_Data[j]["ID"] == items_Data_3[i].ID)
                {
                    Inventory_Item_Data_Input(i,j);
                    break;
                }
            }
        }
    }
    private void Inventory_Item_Data_Input(int i,int j)
    {
        item_List[i] = new Items_List((int)item_List_Data[j]["ID"], item_List_Data[j]["ITEM_NAME"].ToString(), (int)item_List_Data[j]["INVENTORY_TYPE"],
                   (int)item_List_Data[j]["ITEM_USETYPE"], (int)item_List_Data[j]["ITEM_EQUIPTYPE"], item_List_Data[j]["ITEM_ICON"].ToString(), (int)item_List_Data[j]["ITEM_PRICE"]
                   , (int)item_List_Data[j]["ITEM_PRICE_TYPE"], (int)item_List_Data[j]["ITEM_SALE_POSSIBLE_TYPE"], (int)item_List_Data[j]["ITEM_VALUETYPE_1"]
                   , (int)item_List_Data[j]["ITEM_VALUE_1"], (int)item_List_Data[j]["UPDOWN_1"], (int)item_List_Data[j]["ITEM_VALUETYPE_2"], (int)item_List_Data[j]["ITEM_VALUE_2"]
                   , (int)item_List_Data[j]["UPDOWN_2"], (int)item_List_Data[j]["ITEM_VALUETYPE_3"], (int)item_List_Data[j]["ITEM_VALUE_3"], (int)item_List_Data[j]["UPDOWN_3"]
                   , item_List_Data[j]["ITEM_DESCRIPTION_1"].ToString(), item_List_Data[j]["ITEM_DESCRIPTION_2"].ToString(), item_List_Data[j]["ITEM_SLUG"].ToString());
    }
    // Json 저장 인벤토리 아이템 리스트 Save_Type 1
    public void SAVE_NEW_DATA_JSON_ITEMS_LIST_1()
    {
        JsonData save_Json = JsonMapper.ToJson(items_Data_1);
        File.WriteAllText(mobile_Path + "/" + "Item_List_Data_1.json", save_Json.ToString());
        Initailization_Item_List_Data_From_Items_Data_1();
    }
    // Json 저장 인벤토리 아이템 리스트 Save_Type 2
    public void SAVE_NEW_DATA_JSON_ITEMS_LIST_2()
    {
        JsonData save_Json = JsonMapper.ToJson(items_Data_2);
        File.WriteAllText(mobile_Path + "/" + "Item_List_Data_2.json", save_Json.ToString());
        Initailization_Item_List_Data_From_Items_Data_2();
    }
    // Json 저장 인벤토리 아이템 리스트 Save_Type 3
    public void SAVE_NEW_DATA_JSON_ITEMS_LIST_3()
    {
        JsonData save_Json = JsonMapper.ToJson(items_Data_3);
        File.WriteAllText(mobile_Path + "/" + "Item_List_Data_3.json", save_Json.ToString());
        Initailization_Item_List_Data_From_Items_Data_3();
    }
    // Json 로드 인벤토리 아이템 리스트 Save_Type 1
    public void LOAD_NEW_DATA_JSON_ITEMS_LIST_1()
    {
        if (File.Exists(mobile_Path + "/" + "Item_List_Data_1.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_List_Data_1.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            items_Data_1.Clear();            
            for (int i = 0; i < load_Json.Count; i++)
            {
                items_Data_1.Add(new New_Item_Data((int)load_Json[i]["ID"], 1));
            }
            Initailization_Item_List_Data_From_Items_Data_1();
        }
        else
        {
            is_Begin = true;
        }
    }
    // Json 로드 인벤토리 아이템 리스트 Save_Type 2
    public void LOAD_NEW_DATA_JSON_ITEMS_LIST_2()
    {
        if (File.Exists(mobile_Path + "/" + "Item_List_Data_2.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_List_Data_2.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            items_Data_2.Clear();
            for (int i = 0; i < load_Json.Count; i++)
            {
                items_Data_2.Add(new New_Item_Data((int)load_Json[i]["ID"], 1));
            }
            Initailization_Item_List_Data_From_Items_Data_2();
        }
        else
        {
            is_Begin = true;
        }
    }
    // Json 로드 인벤토리 아이템 리스트 Save_Type 3
    public void LOAD_NEW_DATA_JSON_ITEMS_LIST_3()
    {
        if (File.Exists(mobile_Path + "/" + "Item_List_Data_3.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_List_Data_3.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            items_Data_3.Clear();
            for (int i = 0; i < load_Json.Count; i++)
            {
                items_Data_3.Add(new New_Item_Data((int)load_Json[i]["ID"], 1));
            }
            Initailization_Item_List_Data_From_Items_Data_3();
        }
        else
        {
            is_Begin = true;
        }
    }
    // 아이템 셋팅 인벤토리 (NewInventory_Scene에서만 적용)
    // 인벤토리 씬에 들어올때 바로 아이템을 인벤토리에 분산 배치 ( 의상, 퀘스트, 잡화)
    private void Spawning_Inventory_Items()
    {
        int j = 0; 
        int k = 0;
        int t = 0;
        int index_1 = 0; // 인벤토리 (의상)
        int index_2 = 0; // 인벤토리 (퀘스트)
        int index_3 = 0; // 인벤토리 (잡화)
        // -------------------
        for (int i =0; i < item_List.Length; i++)
        {
            // 의상 인벤토리
            if(item_List[i].INVENTORY_TYPE == 1)
            {
                GameObject item = Instantiate(item_Prefab);
                if(j < 20)  // 페이지 1
                {
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(0).transform.GetChild(index_1).transform;
                }else if(j >= 20 && j < 40) // 페이지 2
                {
                    index_1 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(1).transform.GetChild(index_1).transform;
                }else if (j >= 40 && j < 60) // 페이지 3
                {
                    index_1 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(2).transform.GetChild(index_1).transform;
                }else if (j >= 60 && j < 80) // 페이지 4
                {
                    index_1 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(3).transform.GetChild(index_1).transform;
                }else if (j >= 80 && j < 100) // 페이지 5
                {
                    index_1 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(4).transform.GetChild(index_1).transform;
                }
                StartCoroutine(Item_Setting_In_Inventory(item, i));
                j++;
                index_1++;

            } // 퀘스트 인벤토리
            else if (item_List[i].INVENTORY_TYPE == 2)
            {
                GameObject item = Instantiate(item_Prefab);
                if (k < 20)  // 페이지 1
                {
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(0).transform.GetChild(index_2).transform;
                }
                else if (k >= 20 && k < 40) // 페이지 2
                {
                    index_2 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(1).transform.GetChild(index_2).transform;
                }
                else if (k >= 40 && k < 60) // 페이지 3
                {
                    index_2 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(2).transform.GetChild(index_2).transform;
                }
                else if (k >= 60 && k < 80) // 페이지 4
                {
                    index_2 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(3).transform.GetChild(index_2).transform;
                }
                else if (k >= 80 && k < 100) // 페이지 5
                {
                    index_2 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(4).transform.GetChild(index_2).transform;
                }
                StartCoroutine(Item_Setting_In_Inventory(item, i));
                k++;
                index_2++;
            } // 잡화 인벤토리
            else if (item_List[i].INVENTORY_TYPE == 3)
            {
                GameObject item = Instantiate(item_Prefab);
                if (t < 20)  // 페이지 1
                {
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(0).transform.GetChild(index_3).transform;
                }
                else if (t >= 20 && t < 40) // 페이지 2
                {
                    index_3 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(1).transform.GetChild(index_3).transform;
                }
                else if (t >= 40 && t < 60) // 페이지 3
                {
                    index_3 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(2).transform.GetChild(index_3).transform;
                }
                else if (t >= 60 && t < 80) // 페이지 4
                {
                    index_3 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(3).transform.GetChild(index_3).transform;
                }
                else if (t >= 80 && t < 100) // 페이지 5
                {
                    index_3 = 0;
                    item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(4).transform.GetChild(index_3).transform;
                }
                StartCoroutine(Item_Setting_In_Inventory(item, i));
                t++;
                index_3++;
            }
        }
    }
    // 새로운 아이템 착용시 이미 착용 아이템은 다시 인벤토리로..
    public void Previous_Item_Put_Back_To_Inventory()
    {
        switch (item_List[item_List.Length - 1].INVENTORY_TYPE)
        {
            case 1:
                {
                    Previous_Item_Put_To_The_Inventory_Type_1();
                }
                break;
            case 2:
                {
                    Previous_Item_Put_To_The_Inventory_Type_2();
                }
                break;
            case 3:
                {
                    Previous_Item_Put_To_The_Inventory_Type_3();
                }
                break;
        }
    }
    // 착용전 이전 아이템은 의상 인벤토리로 스폰
    private void Previous_Item_Put_To_The_Inventory_Type_1()
    {
        bool is_Done = false;
        int index = 0;
        for (int i = 0; i < 100; i++) //  인벤토리 의상 
        {
            if (!is_Done)
            {
                if (i < 20)
                {
                    if (NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(0).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 1
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_1(0, index);
                        break;
                    }
                    index++;
                    if (i == 19)
                    {
                        index = 0;
                    }
                } else if (i >= 20 && i < 40)
                {
                    if (NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(1).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 2
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_1(1, index);
                        break;
                    }
                    index++;
                    if (i == 39)
                    {
                        index = 0;
                    }
                } else if (i >= 40 && i < 60)
                {
                    if (NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(2).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 3
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_1(2, index);
                        break;
                    }
                    index++;
                    if (i == 59)
                    {
                        index = 0;
                    }
                }else if (i >= 60 && i < 80)
                {
                    if (NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(3).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 4
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_1(3, index);
                        break;
                    }
                    index++;
                    if (i == 79)
                    {
                        index = 0;
                    }
                }else if (i >= 80 && i < 100)
                {
                    if (NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(4).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 5
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_1(4,index);
                        break;
                    }
                    index++;
                }
            }
        }
    }
    private void Spawn_Item__In_Inventory_Type_1(int page, int i)
    {
        GameObject item = Instantiate(item_Prefab);
        item.transform.parent = NewInventory_Manager.instance.inventory_Type_1.transform.GetChild(page).transform.GetChild(i).transform;
        StartCoroutine(Item_Setting_In_Inventory(item, item_List.Length - 1));
    }
    // 착용전 이전 아이템은 퀘스트 인벤토리로 스폰
    private void Previous_Item_Put_To_The_Inventory_Type_2()
    {
        bool is_Done = false;
        int index = 0;
        for (int i = 0; i < 100; i++) //  인벤토리 의상 
        {
            if (!is_Done)
            {
                if (i < 20)
                {
                    if (NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(0).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 1
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_2(0, index);
                        break;
                    }
                    index++;
                    if (i == 19)
                    {
                        index = 0;
                    }
                } else if (i >= 20 && i < 40)
                {
                    if (NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(1).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 2
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_2(1, index);
                        break;
                    }
                    index++;
                    if (i == 39)
                    {
                        index = 0;
                    }
                } else if (i >= 40 && i < 60)
                {
                    if (NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(2).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 3
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_2(2, index);
                        break;
                    }
                    index++;
                    if (i == 59)
                    {
                        index = 0;
                    }
                }else if (i >= 60 && i < 80)
                {
                    if (NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(3).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 4
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_2(3, index);
                        break;
                    }
                    index++;
                    if (i == 79)
                    {
                        index = 0;
                    }
                } else if (i >= 80 && i < 100)
                {
                    if (NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(4).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 5
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_2(4,index);
                        break;
                    }
                    index++;
                }
            }
        }
    }
    private void Spawn_Item__In_Inventory_Type_2(int page, int i)
    {
        GameObject item = Instantiate(item_Prefab);
        item.transform.parent = NewInventory_Manager.instance.inventory_Type_2.transform.GetChild(page).transform.GetChild(i).transform;
        StartCoroutine(Item_Setting_In_Inventory(item, item_List.Length - 1));
    }
    // 착용전 이전 아이템은 잡화 인벤토리로 스폰
    private void Previous_Item_Put_To_The_Inventory_Type_3()
    {
        bool is_Done = false;
        int index = 0;
        for (int i = 0; i < 100; i++) //  인벤토리 잡화
        {
            if (!is_Done)
            {
                if (i < 20)
                {
                    if (NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(0).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 1
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_3(0, index);
                        break;
                    }
                    index++;
                    if (i == 19)
                    {
                        index = 0;
                    }
                } else if (i >= 20 && i < 40)
                {
                    if (NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(1).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 2
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_3(1, index);
                        break;
                    }
                    index++;
                    if (i == 39)
                    {
                        index = 0;
                    }
                } else if (i >= 40 && i < 60)
                {
                    if (NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(2).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 3
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_3(2, index);
                        break;
                    }
                    index++;
                    if (i == 59)
                    {
                        index = 0;
                    }
                }  else if (i >= 60 && i < 80)
                {
                    if (NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(3).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 4
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_3(3,index);
                        break;
                    }
                    index++;
                    if (i == 79)
                    {
                        index = 0;
                    }
                } else if (i >= 80 && i < 100)
                {
                    if (NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(4).transform.GetChild(index).transform.childCount != 1)
                    {   // 페이지 5
                        is_Done = true;
                        Spawn_Item__In_Inventory_Type_3(4, index);
                        break;
                    }
                    index++;
                }
            }
        }
    }
    private void Spawn_Item__In_Inventory_Type_3(int page,int i)
    {
        GameObject item = Instantiate(item_Prefab);
        item.transform.parent = NewInventory_Manager.instance.inventory_Type_3.transform.GetChild(page).transform.GetChild(i).transform;
        StartCoroutine(Item_Setting_In_Inventory(item, item_List.Length - 1));
    }
    // 아이템 transform , image 셋팅
    IEnumerator Item_Setting_In_Inventory(GameObject _item, int _i)
    {
        _item.transform.localScale = Vector3.zero;
        _item.transform.GetComponent<New_Item>().this_Item = item_List[_i];
        _item.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/New_Inventory/" + item_List[_i].ITEM_ICON.ToString());
        yield return new WaitForSeconds(0.05f);
        _item.transform.localRotation = Quaternion.identity;
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localScale = new Vector3(1f, 1f, 1f);
        _item.transform.GetComponent<Image>().SetNativeSize();
    }
} // class












