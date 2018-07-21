using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour {

    public static Inventory_Controller instance = null;

    private GameObject inventory_Panel;
    private GameObject inventory_Slot_Panel;
    private GameObject character_Item_Panel;

    private Item_Json_DataBase item_DataBase;

    public GameObject slot_Prefab;
    public GameObject item_Prefab;

    public GameObject slot_Type_1;
    public GameObject slot_Type_2;
    public GameObject item_Get_On_Info_Panel;
    public GameObject item_Get_Off_Info_Panel;

    public Text item_Description;
    // 인벤토리 아이템
    public List<Items_Info> item = new List<Items_Info>();
    public List<GameObject> slot = new List<GameObject>();
    // 플레이어 착용중인 아이템 
    public List<GameObject> current_slot = new List<GameObject>();
    public List<Items_Info> current_item = new List<Items_Info>();

    public Items_Info current_Select_Item = new Items_Info();

    private int slot_Count;

    private bool is_Stackable;
    
    public int current_Index;

    
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
        // ---------------
        inventory_Panel = GameObject.Find("Inventory_Panel");
        inventory_Slot_Panel = inventory_Panel.transform.Find("Slot_Panel").gameObject;
        item_DataBase = GameObject.Find("Inventory_Json_Data").GetComponent<Item_Json_DataBase>();
        character_Item_Panel = GameObject.Find("Character_Item_Panel");
    }

    private void Start()
    {
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(false);
        current_Index = 0;
        is_Stackable = false;
        slot_Count = 20;
        // 인벤토리 슬롯 20개 만들기
        for(int i = 0; i<slot_Count; i++)
        {
            item.Add(new Items_Info());
            slot.Add(Instantiate(slot_Prefab));
            slot[i].GetComponent<Slot>().slot_Id = i;
            slot[i].transform.SetParent(inventory_Slot_Panel.transform);
        }

        StartCoroutine(Load_Item());
        // 실시간 착용 아이템 슬롯 2개 만들기
        for(int i = 0; i <  2; i++)
        {
            if (i == 0)
            {
                current_slot.Add(Instantiate(slot_Type_1));
            }
            else
            {
                current_slot.Add(Instantiate(slot_Type_2));
            }
            current_item.Add(new Items_Info());
            current_slot[i].GetComponent<Slot>().slot_Id = i;
            current_slot[i].transform.SetParent(character_Item_Panel.transform);
        }

        character_Item_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
        character_Item_Panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
    }
    // 인벤토리씬에서만 아이템 추가 기능 함수
    public void Select_Buy_Item(int _id)
    {   
        for(int i = 0; i < item.Count; i++)
        {
            if(item[i].id == -1)
            {
                current_Index = i;
                Add_Item(_id);
                break;
            }
        }
    }
    // 아이템 추가 메소드
    public void Add_Item(int _id)
    {
        Items_Info add_Item = item_DataBase.Search_For_Item(_id);
        
        if (add_Item.stackable)
        {
            if (!is_Stackable)
            {
                is_Stackable = true;
                // -----------------
                if (item[current_Index].id == -1)
                {
                    item[current_Index] = add_Item;
                    GameObject item_Obj = Instantiate(item_Prefab);
                    item_Obj.GetComponent<Item>().item_Ability = add_Item;
                    item_Obj.GetComponent<Item>().slot_Location = current_Index;
                    item_Obj.transform.SetParent(slot[current_Index].transform);
                    item_Obj.transform.localPosition = Vector2.zero;
                    item_Obj.GetComponent<Image>().sprite = add_Item.item_Img;
                    item_Obj.name = add_Item.slug;
                    current_Index++;
                }
            }
            else
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].id == 30002)
                    {
                        GameObject potion = slot[i].transform.GetChild(0).gameObject;
                        potion.GetComponent<Item>().amount += 2;
                        potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
                    }
                }
            }
        }
        else
        {
            if (item[current_Index].id == -1)
            {
                item[current_Index] = add_Item;
                GameObject item_Obj = Instantiate(item_Prefab);
                item_Obj.GetComponent<Item>().item_Ability = add_Item;
                item_Obj.GetComponent<Item>().slot_Location = current_Index;
                item_Obj.transform.SetParent(slot[current_Index].transform);
                item_Obj.transform.localPosition = Vector2.zero;
                item_Obj.GetComponent<Image>().sprite = add_Item.item_Img;
                item_Obj.name = add_Item.slug;
                current_Index++;
            }
        }
        
    }
    // 인벤토리 들어오때 Json Load
    IEnumerator Load_Item()
    {
        Inventory_Add_Item_Json.instance.LOAD_NEW_DATA_JSON_Inventory();

        yield return new WaitForSeconds(0f);
        for (int i = 0; i < item.Count; i++)
        {
            if (current_Index < 20)
            {
                if (Inventory_Add_Item_Json.instance.load_Item_Data[i].id == 30001)
                {
                    Add_Item(30001);
                }
                else if (Inventory_Add_Item_Json.instance.load_Item_Data[i].id == 30002)
                {
                    for (int j = 1; j <= Inventory_Add_Item_Json.instance.load_Item_Data[i].amount; j += 2)
                    {
                        Add_Item(30002);
                    }
                }
                else if (Inventory_Add_Item_Json.instance.load_Item_Data[i].id == 30003)
                {
                    Add_Item(30003);
                }
                else
                {
                    current_Index++;
                }
            }
        }
    }
    
    // 인벤토리 나갈때 Json Save 
    public void If_Exit_Inventory_Scene()
    {
        for(int i = 0; i < item.Count; i++)
        {
            if(item[i].id == 30002)
            {
                Inventory_Add_Item_Json.instance.inventory_Item_List[i].stackable = true;
                GameObject potion = slot[i].transform.GetChild(0).gameObject;
                Inventory_Add_Item_Json.instance.inventory_Item_List[i].amount = potion.GetComponent<Item>().amount;
            }
            else
            {
                Inventory_Add_Item_Json.instance.inventory_Item_List[i].stackable = false;
                Inventory_Add_Item_Json.instance.inventory_Item_List[i].amount = 1;
            }
            Inventory_Add_Item_Json.instance.inventory_Item_List[i].id = item[i].id;
        }

        Inventory_Add_Item_Json.instance.SAVE_NEW_DATA_JSON_Inventory();
    }
    // 아이템 창작 해제 할때 이용 되는 코드 라인
    public void Item_Get_On_Instruction_Panel(string _text)
    {
        item_Get_On_Info_Panel.SetActive(true);
        item_Get_Off_Info_Panel.SetActive(false);
        item_Description.text = _text;
    }
    public void Item_Get_Off_Instruction_Panel(string _text)
    {
        item_Description.text = _text;
    }
    public void Button_Item_Get_On_Player_Slot_Section()
    {
        current_item[Item_Information.select_Index] = current_Select_Item;
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(false);
        item[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]] = new Items_Info();
        Item_Destroy();

        Create_Player_Current_Item();
        Create_Player_Current_Item_Amount();
    }
    public void Button_Item_Get_Off_Player_Slot_Section()
    {

    }
    public void Button_Item_Instruction_Panel_Exit()
    {
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(false);
    } 

    private void Create_Player_Current_Item()
    {
        GameObject item_Obj = Instantiate(item_Prefab);
        item_Obj.GetComponent<Item>().item_Ability = current_Select_Item;
        item_Obj.GetComponent<Item>().slot_Location = current_Index;
        item_Obj.transform.SetParent(current_slot[Item_Information.select_Index].transform);
        item_Obj.transform.localPosition = Vector2.zero;
        item_Obj.GetComponent<Image>().sprite = current_Select_Item.item_Img;
        item_Obj.name = current_Select_Item.slug;

        Destroy(item_Obj.transform.GetChild(0).gameObject);
    }
    private void Create_Player_Current_Item_Amount()
    {
        if(current_item[Item_Information.select_Index].id == 30001 || current_item[Item_Information.select_Index].id == 30003)
        {
            if(Item_Information.select_Index == 0)
            {
                character_Item_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = 1.ToString();
            } else if(Item_Information.select_Index == 1)
            {
                character_Item_Panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 1.ToString();
            }
        }
        else if(current_item[Item_Information.select_Index].id == 30002)
        {
            if(Item_Information.select_Index == 0)
            {
                character_Item_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = 10.ToString();
            } else if (Item_Information.select_Index == 1)
            {
                character_Item_Panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 10.ToString();
            }
        }
    }
    private void Item_Destroy()
    {
        if (current_item[Item_Information.select_Index].id == 30001 || current_item[Item_Information.select_Index].id == 30003)
        {
            Destroy(slot[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]].transform.GetChild(0)
                .gameObject);
        } else if(current_item[Item_Information.select_Index].id == 30002)
        {
            GameObject potion = slot[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]].transform.GetChild(0).gameObject;

            if (potion.GetComponent<Item>().amount <= 0)
            {
                Destroy(slot[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]].transform.GetChild(0)
                .gameObject);
            }
            else
            {
                potion.GetComponent<Item>().amount -= 1;
                potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
            }
        }
    }
    // -----------------------------------
} // class










