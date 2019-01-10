using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class NewInventory_Items_Data : MonoBehaviour {

    public static NewInventory_Items_Data instance = null;

    public string mobile_Path; // 모바일 저장 경로

    public List<New_Item_Data> items_Data; // json 파일에 저장,로드할 현재 아이템리스트
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
        LOAD_NEW_DATA_JSON_ITEMS_LIST();
        if (is_Begin)  // 초기 값
        {
            is_Begin = false;
            for (int i = 0; i < item_List_Data.Count; i++)
            {
                items_Data.Add(new New_Item_Data((int)item_List_Data[i]["ID"], 1));
            }
            SAVE_NEW_DATA_JSON_ITEMS_LIST();
            Initailization_Item_List_Data_From_Items_Data();
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
    public void Initailization_Item_List_Data_From_Items_Data()
    {
        item_List = new Items_List[items_Data.Count];
        for(int i = 0; i < item_List.Length; i++)
        {
            item_List[i] = new Items_List();
        }
        for(int i = 0; i < items_Data.Count; i++)
        {
            for(int j = 0; j < item_List_Data.Count; j++)
            {
                if((int)item_List_Data[j]["ID"] == items_Data[i].ID)
                {
                    item_List[i] = new Items_List((int)item_List_Data[j]["ID"], item_List_Data[j]["ITEM_NAME"].ToString(), (int)item_List_Data[j]["INVENTORY_TYPE"],
                    (int)item_List_Data[j]["ITEM_USETYPE"], (int)item_List_Data[j]["ITEM_EQUIPTYPE"], item_List_Data[j]["ITEM_ICON"].ToString(), (int)item_List_Data[j]["ITEM_PRICE"]
                    , (int)item_List_Data[j]["ITEM_PRICE_TYPE"], (int)item_List_Data[j]["ITEM_SALE_POSSIBLE_TYPE"], (int)item_List_Data[j]["ITEM_VALUETYPE_1"]
                    , (int)item_List_Data[j]["ITEM_VALUE_1"], (int)item_List_Data[j]["UPDOWN_1"], (int)item_List_Data[j]["ITEM_VALUETYPE_2"], (int)item_List_Data[j]["ITEM_VALUE_2"]
                    , (int)item_List_Data[j]["UPDOWN_2"], (int)item_List_Data[j]["ITEM_VALUETYPE_3"], (int)item_List_Data[j]["ITEM_VALUE_3"], (int)item_List_Data[j]["UPDOWN_3"]
                    , item_List_Data[j]["ITEM_DESCRIPTION_1"].ToString(), item_List_Data[j]["ITEM_DESCRIPTION_2"].ToString(), item_List_Data[j]["ITEM_SLUG"].ToString());
                    break;
                }
            }
        }
    }
    // Json 저장 인벤토리 아이템 리스트
    public void SAVE_NEW_DATA_JSON_ITEMS_LIST()
    {
        JsonData save_Json = JsonMapper.ToJson(items_Data);

        File.WriteAllText(mobile_Path + "/" + "Item_List_Data.json", save_Json.ToString());

        Initailization_Item_List_Data_From_Items_Data();
    }
    // Json 로드 인벤토리 아이템 리스트
    public void LOAD_NEW_DATA_JSON_ITEMS_LIST()
    {
        if (File.Exists(mobile_Path + "/" + "Item_List_Data.json"))
        {
            string json_String = File.ReadAllText(mobile_Path + "/" + "Item_List_Data.json");
            JsonData load_Json = JsonMapper.ToObject(json_String);
            items_Data.Clear();            
            for (int i = 0; i < load_Json.Count; i++)
            {
                items_Data.Add(new New_Item_Data((int)load_Json[i]["ID"], 1));
            }
            Initailization_Item_List_Data_From_Items_Data();
        }
        else
        {
            is_Begin = true;
            Debug.Log("file is not found!");
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
    public void Destroy_All_Items_In_Inventory_Then_ReSpawn_Setting()
    {

    }
    // 아이템 transform , image 셋팅
    IEnumerator Item_Setting_In_Inventory(GameObject _item, int _i)
    {
        _item.transform.localScale = Vector3.zero;
        _item.transform.GetComponent<New_Item>().this_Item = item_List[_i];
        _item.transform.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/New_Inventory/" + item_List[_i].ITEM_ICON.ToString());
        yield return new WaitForSeconds(0.05f);
        _item.transform.localRotation = Quaternion.identity;
        _item.transform.localPosition = Vector3.zero;
        _item.transform.localScale = new Vector3(1f, 1f, 1f);
        _item.transform.GetComponent<Image>().SetNativeSize();
    }
} // class












