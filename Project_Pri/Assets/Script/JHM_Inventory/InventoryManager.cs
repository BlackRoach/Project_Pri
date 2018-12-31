using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance = null;

    public GameObject item_Prefab;
    public GameObject slot_Panel;
    public GameObject equip_Type_Info_Panel;
    public GameObject equip_Slot_Panel;
    public GameObject inventory_Isfull_Panel;

    public Item_Data[] items;     // 인벤토리 아이템 
    public Item_Data[] equip_Items; // 장착 아이템 
    public GameObject[] slot;
    public Item_Data selected_Item;

    public int selected_Index; // 아이템 창작시 선택되는 인덱스

    private JsonData item_Data;
         
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
        inventory_Isfull_Panel.SetActive(false);
        equip_Type_Info_Panel.SetActive(false);
        selected_Index = 0;
        slot = new GameObject[slot_Panel.transform.childCount];
        items = new Item_Data[slot_Panel.transform.childCount];
        equip_Items = new Item_Data[2];
        for (int i =0; i< slot_Panel.transform.childCount; i++)
        {
            slot[i] = slot_Panel.transform.GetChild(i).gameObject;
            slot[i].GetComponent<Slot>().slot_Num = i;
            items[i] = new Item_Data();
        }
        for(int i = 0; i < equip_Items.Length; i++)
        {
            equip_Items[i] = new Item_Data();
            equip_Slot_Panel.transform.GetChild(i).GetComponent<Slot>().slot_Num = i;
        }
        Inventory_Current_Items.instance.LOAD_NEW_DATA_JSON_Inventory();
        Inventory_Current_Items.instance.EQUIP_ITEMS_LOAD_NEW_DATA_JSON();
        Json_Data_Parsing();
        StartCoroutine(Load());
    }
    // 현재 아이템 로드
    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.05f);
        // 인벤토리
        for (int i = 0; i < items.Length; i++)
        {
            if ((int)Inventory_Current_Items.instance.current_Items[i].ID != -1)
            {
                Items_Data_Input((int)Inventory_Current_Items.instance.current_Items[i].ID, i);
                items[i].ITEM_COUNT = (int)Inventory_Current_Items.instance.current_Items[i].COUNT;                
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ID != -1)
            {
                Item_Into_The_Slot_Setting(i, items[i].ITEM_COUNT);
            }
        }
        // 장착 아이템
        for (int i = 0; i < equip_Items.Length; i++)
        {
            if ((int)Inventory_Current_Items.instance.equip_Items[i].ID != -1)
            {
                Equip_Items_Data_Input((int)Inventory_Current_Items.instance.equip_Items[i].ID, i);
                equip_Items[i].ITEM_COUNT = (int)Inventory_Current_Items.instance.equip_Items[i].COUNT;
            }
        }
        for (int i = 0; i < equip_Items.Length; i++)
        {
            if (equip_Items[i].ID != -1)
            {
                Equip_Item_Into_The_Slot_Setting(i, equip_Items[i].ITEM_COUNT);
            }
        }
    }
    // json파일 가져오기
    private void Json_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/ITEM_DATA");

        item_Data = JsonMapper.ToObject(json_File.text);
    }
    // 아이템 추가 함수
    public void Add_Item(int _id)
    {
        bool is_Stack = false; // 만약 stack 아이템이 이미 있을경우
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].ID == 30002 && _id == 30002)
            {
                is_Stack = true;
                break;
            }
        }
        // --------------------------------
        for(int i = 0; i < items.Length; i++)
        {
            if (is_Stack)
            {
                if (items[i].ID == 30002 && _id == 30002) // 만약 a 포션이 잇으면 갯수만 올리기
                {
                    items[i].ITEM_COUNT += 2;
                    slot[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = items[i].ITEM_COUNT.ToString();
                    // JSON 저장 물약 갯수
                    Inventory_Current_Items.instance.current_Items[i].ID = items[i].ID;
                    Inventory_Current_Items.instance.current_Items[i].COUNT = items[i].ITEM_COUNT;
                    Inventory_Current_Items.instance.SAVE_NEW_DATA_JSON_Inventory();
                    // -----------------
                    break;
                }
            }
            else
            {
                if (items[i].ID == -1) // 추가 아이템
                {
                    switch (_id)
                    {
                        case 30001: // 노브헬멧 추가하기
                            {
                                Items_Data_Input(_id, i);
                                items[i].ITEM_COUNT = 1;
                            }
                            break;
                        case 30002: // A 포션  추가하기
                            {
                                Items_Data_Input(_id, i);
                                items[i].ITEM_COUNT = 2;
                            }
                            break;
                        case 30003: // 강철방패 추가하기
                            {
                                Items_Data_Input(_id, i);
                                items[i].ITEM_COUNT = 1;
                            }
                            break;
                    } // end switch
                    if (_id != -1)
                    {
                        Item_Into_The_Slot_Setting(i, items[i].ITEM_COUNT); // 아이템 슬롯에 배치
                                                                            // JSON 저장  ( 외부씬이동할때 인벤토리 데이터들은 저장하기 )
                        for (int j = 0; j < items.Length; j++)
                        {
                            Inventory_Current_Items.instance.current_Items[j].ID = items[j].ID;
                            Inventory_Current_Items.instance.current_Items[j].COUNT = items[j].ITEM_COUNT;
                        }
                        Inventory_Current_Items.instance.SAVE_NEW_DATA_JSON_Inventory();
                        // ---------------------
                    }
                    break;
                }
            }
        }     
    }
    // 아이템 슬롯에 배치 인벤토리
    private void Item_Into_The_Slot_Setting(int _index,int _count)
    {
        GameObject item_Obj = Instantiate(item_Prefab);
        item_Obj.transform.SetParent(slot[_index].transform);
        item_Obj.transform.localPosition = Vector3.zero;
        item_Obj.transform.localScale = new Vector3(1f,1f,1f);
        item_Obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + items[_index].ITEM_SLUG);
        item_Obj.transform.GetChild(0).GetComponent<Text>().text = _count.ToString();
    }
    // 아이템 슬롯에 배치 장착 아이템
    private void Equip_Item_Into_The_Slot_Setting(int _index, int _count)
    {
        GameObject item_Obj = Instantiate(item_Prefab);     
        if(equip_Items[_index].ID == 30001 || equip_Items[_index].ID == 30002)
        {
            item_Obj.transform.SetParent(equip_Slot_Panel.transform.GetChild(0).gameObject.transform);
            equip_Slot_Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _count.ToString();
        }
        if(equip_Items[_index].ID == 30003)
        {
            item_Obj.transform.SetParent(equip_Slot_Panel.transform.GetChild(1).gameObject.transform);
            equip_Slot_Panel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = _count.ToString();
        }
        item_Obj.transform.localPosition = Vector3.zero;
        item_Obj.transform.localScale = new Vector3(1f, 1f, 1f);
        item_Obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + equip_Items[_index].ITEM_SLUG);
        item_Obj.transform.GetChild(0).gameObject.SetActive(false);
    }
    // 아이템 데이터 Input 인벤토리
    private void Items_Data_Input(int _id,int _index)
    {
        if(_id == 30001)
        {
            items[_index] = new Item_Data((int)item_Data[0]["ID"], item_Data[0]["ITEM_NAME"].ToString()
                                , item_Data[0]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[0]["ITEM_VALUETYPE"], (int)item_Data[0]["ITEM_VALUE"]
                                , (int)item_Data[0]["ITEM_PRICE"], (int)item_Data[0]["ITEM_USETYPE"], (int)item_Data[0]["ITEM_EQUIPTYPE"]
                                , item_Data[0]["ITEM_SLUG"].ToString());
        }
        if (_id == 30002)
        {
            items[_index] = new Item_Data((int)item_Data[1]["ID"], item_Data[1]["ITEM_NAME"].ToString()
                                , item_Data[1]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[1]["ITEM_VALUETYPE"], (int)item_Data[1]["ITEM_VALUE"]
                                , (int)item_Data[1]["ITEM_PRICE"], (int)item_Data[1]["ITEM_USETYPE"], (int)item_Data[1]["ITEM_EQUIPTYPE"]
                                , item_Data[1]["ITEM_SLUG"].ToString());
        }
        if (_id == 30003)
        {
            items[_index] = new Item_Data((int)item_Data[2]["ID"], item_Data[2]["ITEM_NAME"].ToString()
                                , item_Data[2]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[2]["ITEM_VALUETYPE"], (int)item_Data[2]["ITEM_VALUE"]
                                , (int)item_Data[2]["ITEM_PRICE"], (int)item_Data[2]["ITEM_USETYPE"], (int)item_Data[2]["ITEM_EQUIPTYPE"]
                                , item_Data[2]["ITEM_SLUG"].ToString());
        }
    }
    // 아이템 데이터 Input 장착 아이템
    private void Equip_Items_Data_Input(int _id, int _index)
    {
        if (_id == 30001)
        {
            equip_Items[_index] = new Item_Data((int)item_Data[0]["ID"], item_Data[0]["ITEM_NAME"].ToString()
                                , item_Data[0]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[0]["ITEM_VALUETYPE"], (int)item_Data[0]["ITEM_VALUE"]
                                , (int)item_Data[0]["ITEM_PRICE"], (int)item_Data[0]["ITEM_USETYPE"], (int)item_Data[0]["ITEM_EQUIPTYPE"]
                                , item_Data[0]["ITEM_SLUG"].ToString());
        }
        if (_id == 30002)
        {
            equip_Items[_index] = new Item_Data((int)item_Data[1]["ID"], item_Data[1]["ITEM_NAME"].ToString()
                                , item_Data[1]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[1]["ITEM_VALUETYPE"], (int)item_Data[1]["ITEM_VALUE"]
                                , (int)item_Data[1]["ITEM_PRICE"], (int)item_Data[1]["ITEM_USETYPE"], (int)item_Data[1]["ITEM_EQUIPTYPE"]
                                , item_Data[1]["ITEM_SLUG"].ToString());
        }
        if (_id == 30003)
        {
            equip_Items[_index] = new Item_Data((int)item_Data[2]["ID"], item_Data[2]["ITEM_NAME"].ToString()
                                , item_Data[2]["ITEM_DESCRIPTION"].ToString(), (int)item_Data[2]["ITEM_VALUETYPE"], (int)item_Data[2]["ITEM_VALUE"]
                                , (int)item_Data[2]["ITEM_PRICE"], (int)item_Data[2]["ITEM_USETYPE"], (int)item_Data[2]["ITEM_EQUIPTYPE"]
                                , item_Data[2]["ITEM_SLUG"].ToString());
        }
    }
    // 아이템 장착시 실행되는 함수
    public void Button_Equip_Type_Item_Pressed_On()
    {
        // 아이템 창작시 장착슬롯에 아이템잇는지 확인여부
        if(items[selected_Index].ID == 30001 || items[selected_Index].ID == 30002)
        {
            if(equip_Items[0].ID != -1)
            {
                Equip_Items_Checkfor_Slot_Before_Setting(0);
            }
        }
        if(items[selected_Index].ID == 30003)
        {
            if (equip_Items[1].ID != -1)
            {
                Equip_Items_Checkfor_Slot_Before_Setting(1);
            }
        }
        // 창작 및 아이템 스킬 푸싱 
        StartCoroutine(Equip_Item_Pushing());
    }
    IEnumerator Equip_Item_Pushing()
    {
        yield return new WaitForSeconds(0.05f);
        if (items[selected_Index].ID == 30001 || items[selected_Index].ID == 30002)
        {
            equip_Items[0] = items[selected_Index];
            slot[selected_Index].transform.GetChild(0).transform.
                SetParent(equip_Slot_Panel.transform.GetChild(0).gameObject.transform);
            equip_Slot_Panel.transform.GetChild(0).transform.GetChild(1).transform.localPosition = Vector3.zero;
            equip_Slot_Panel.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            equip_Slot_Panel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text =
                equip_Items[0].ITEM_COUNT.ToString();
        }
        if (items[selected_Index].ID == 30003)
        {
            equip_Items[1] = items[selected_Index];
            slot[selected_Index].transform.GetChild(0).transform.
                SetParent(equip_Slot_Panel.transform.GetChild(1).gameObject.transform);
            equip_Slot_Panel.transform.GetChild(1).transform.GetChild(1).transform.localPosition = Vector3.zero;
            equip_Slot_Panel.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            equip_Slot_Panel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text =
                equip_Items[1].ITEM_COUNT.ToString();
        }
        items[selected_Index] = new Item_Data();
        Json_Data_Saving();
        equip_Type_Info_Panel.SetActive(false);
    }
    // 아이템 해제시 실행되는 함수
    public void Button_Equip_Type_Item_Pressed_Off()
    {
        bool is_Empty = false;
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].ID == -1)
            {
                is_Empty = true;
            }
        }
        if (is_Empty)
        {
            Destroy(equip_Slot_Panel.transform.GetChild(selected_Index).transform.GetChild(1).gameObject);
            equip_Slot_Panel.transform.GetChild(selected_Index).transform.GetChild(0).GetComponent<Text>().text
                = 0.ToString();
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = equip_Items[selected_Index];
                    Item_Into_The_Slot_Setting(i, items[i].ITEM_COUNT);
                    break;
                }
            }
            equip_Items[selected_Index] = new Item_Data();
            Json_Data_Saving();
            equip_Type_Info_Panel.SetActive(false);
        }
        else
        {
            inventory_Isfull_Panel.SetActive(true);
        }
    }
    public void Button_Close_Inventory_IsFull_Panel()
    {
        inventory_Isfull_Panel.SetActive(false);
    }
    // 아이템 장착 인포 나가기 
    public void Button_Equip_Type_Item_Info_Exit()
    {
        equip_Type_Info_Panel.SetActive(false);
    }
    // 아이템 장착전 장착슬롯에 아이템 잇는지 여부 확인 (있으면 먼저 인벤토리에 전 장착아이템 푸싱)
    private void Equip_Items_Checkfor_Slot_Before_Setting(int _index)
    {
        Destroy(equip_Slot_Panel.transform.GetChild(_index).transform.GetChild(1).gameObject);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ID == -1)
            {
                items[i] = equip_Items[_index];
                Item_Into_The_Slot_Setting(i, items[i].ITEM_COUNT);
                break;
            }
        }
        equip_Items[_index] = new Item_Data();
    }
    // json 파일에 모든 데이터 저장
    private void Json_Data_Saving()
    {
        for (int j = 0; j < items.Length; j++)
        {
            Inventory_Current_Items.instance.current_Items[j].ID = items[j].ID;
            Inventory_Current_Items.instance.current_Items[j].COUNT = items[j].ITEM_COUNT;
        }
        for (int i = 0; i < equip_Items.Length; i++)
        {
            Inventory_Current_Items.instance.equip_Items[i].ID = equip_Items[i].ID;
            Inventory_Current_Items.instance.equip_Items[i].COUNT = equip_Items[i].ITEM_COUNT;
        }
        Inventory_Current_Items.instance.SAVE_NEW_DATA_JSON_Inventory();
        Inventory_Current_Items.instance.EQUIP_ITEMS_SAVE_NEW_DATA_JSON();
    }
    
} // class










