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
    public GameObject error_Information_Panel;

    public Text item_Description_01;
    public Text item_Description_02;
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

    public int item_Table_Id;
 
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
        error_Information_Panel.SetActive(false);
        item_Table_Id = 0;
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
       
        // 실시간 착용 아이템 슬롯 2개 만들기
        for (int i = 0; i < 2; i++)
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
        StartCoroutine(Load_Item());
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
        Item_Table_Json.instance.Item_Table_Json_Load();
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
        // Item Table
        for(int i = 0; i < Item_Table_Json.instance.item_Table.Length; i++)
        {
            if (Item_Table_Json.instance.item_Table[i].id != -1)
            {          
                switch (Item_Table_Json.instance.item_Table[i].id)
                {
                    case 30001:
                        {
                            current_item[i] = item_DataBase.Search_For_Item(30001);
                        }break;
                    case 30002:
                        {
                            current_item[i] = item_DataBase.Search_For_Item(30002);
                        }
                        break;
                    case 30003:
                        {
                            current_item[i] = item_DataBase.Search_For_Item(30003);
                        }
                        break;
                }
            }
        }
        for(int i = 0; i < current_item.Count; i++)
        {
            if(current_item[i].id != -1)
            {
                GameObject item_Obj = Instantiate(item_Prefab);
                item_Obj.transform.SetParent(current_slot[i].transform);
                item_Obj.transform.localPosition = Vector2.zero;
                item_Obj.GetComponent<Item>().item_Ability = current_item[i];
                item_Obj.GetComponent<Item>().slot_Location = i;
                item_Obj.GetComponent<Image>().sprite = current_item[i].item_Img;
                item_Obj.name = current_item[i].slug;
                item_Obj.GetComponent<Item_Information>().set_Pos = true;
                Destroy(item_Obj.transform.GetChild(0).gameObject);
                
                if(current_item[i].id == 30001 || current_item[i].id == 30003)
                {
                    character_Item_Panel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = 1.ToString();
                } else if(current_item[i].id == 30002)
                {
                    character_Item_Panel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = 10.ToString();
                }
            }
            else
            {
                character_Item_Panel.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
            }
        }
        // ------------------

        for(int i = 0; i < item.Count; i++)
        {
            if (item[i].id == 30002)
            {
                GameObject potion = slot[i].transform.GetChild(0).gameObject;
                if (potion.GetComponent<Item>().amount != Inventory_Add_Item_Json.instance.load_Item_Data[i].amount)
                {
                    potion.GetComponent<Item>().amount++;
                    potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
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
        for(int i = 0; i < current_item.Count; i++)
        {
            Item_Table_Json.instance.item_Table[i].id = current_item[i].id;
        }
        Inventory_Add_Item_Json.instance.SAVE_NEW_DATA_JSON_Inventory();
        Item_Table_Json.instance.Item_Table_Json_Save();
    }
    // 아이템 창작 해제 할때 이용 되는 코드 라인
    public void Item_Get_On_Instruction_Panel(string _text)
    {
        item_Get_On_Info_Panel.SetActive(true);
        item_Get_Off_Info_Panel.SetActive(false);
        if(current_Select_Item.use_Type == 1)
        {
            item_Get_On_Info_Panel.transform.GetChild(1).gameObject.SetActive(true);
            item_Get_On_Info_Panel.transform.GetChild(2).gameObject.SetActive(false);
        } else if(current_Select_Item.use_Type == 2)
        {
            item_Get_On_Info_Panel.transform.GetChild(1).gameObject.SetActive(false);
            item_Get_On_Info_Panel.transform.GetChild(2).gameObject.SetActive(true);
        }

        item_Description_01.text = _text;
    }
    public void Item_Get_Off_Instruction_Panel(string _text)
    {
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(true);
        item_Description_02.text = _text;
    }
    public void Button_Item_Get_On_Player_Slot_Section()
    {
        int count = 0;
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id != -1)
            {
                count++;
            }
        }
        if (current_item[Item_Information.select_Index].id != -1)
        {
            item_Table_Id = current_item[Item_Information.select_Index].id;
        }
        else
        {
            item_Table_Id = 0;
        }
        if (count != item.Count || item_Table_Id == 0
            || item[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]].id == 30001 &&
            item_Table_Id == 30002)
        {
            
            current_item[Item_Information.select_Index] = current_Select_Item;
            if (current_item[Item_Information.select_Index].id == 30001 || current_item[Item_Information.select_Index].id == 30003)
            {
                item[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]] = new Items_Info();
            }
            else if (current_item[Item_Information.select_Index].id == 30002)
            {
                if (slot[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]].transform.GetChild(0).gameObject
                    .GetComponent<Item>().amount <= 0)
                {
                    item[Item_Care_Manager.instance.previous_Slot_Index[Item_Information.select_Index]] = new Items_Info();
                }
            }
            Item_Destroy();
            if (item_Table_Id != 0)
            {
                if (item_Table_Id == 30002)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        if (item[i].id == 30002)
                        {
                            GameObject potion = slot[i].transform.GetChild(0).gameObject;
                            potion.GetComponent<Item>().amount++;
                            potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
                        }
                    }
                }
                else
                {
                    Select_Buy_Item(item_Table_Id);
                }
            }
            Create_Player_Current_Item();
            Create_Player_Current_Item_Amount();
        }
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(false);
    }
    
    public void Button_Item_Get_Off_Player_Slot_Section()
    {
        
        Items_Info check_Item = current_slot[Item_Information.select_Index].transform.GetChild(2).transform.GetComponent<
            Item>().item_Ability;
        item_Get_On_Info_Panel.SetActive(false);
        item_Get_Off_Info_Panel.SetActive(false);
        int temp = 0;
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == -1)
            {
                Destroy(current_slot[Item_Information.select_Index].transform.GetChild(2).gameObject);
                if (current_item[Item_Information.select_Index].id != 30002)
                {
                    Select_Buy_Item(current_item[Item_Information.select_Index].id);
                }
                else
                {
                    for (int j = 0; j < item.Count; j++)
                    {
                        if (item[j].id == 30002)
                        {
                            GameObject potion = slot[j].transform.GetChild(0).gameObject;
                            potion.GetComponent<Item>().amount += 1;
                            potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
                        }
                    }
                }
                current_item[Item_Information.select_Index] = new Items_Info();
                if (Item_Information.select_Index == 0)
                {
                    character_Item_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                }
                else
                {
                    character_Item_Panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                }
                break;
            }
            else
            {
                temp++;
            }
        }
        if(temp == item.Count)
        {
            bool ctrl = false;
            int count = 0;
            if (check_Item.id == 30002)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].id == 30002)
                    {
                        GameObject potion = slot[i].transform.GetChild(0).gameObject;
                        potion.GetComponent<Item>().amount += 1;
                        potion.transform.GetChild(0).GetComponent<Text>().text = potion.GetComponent<Item>().amount.ToString();
                        Destroy(current_slot[Item_Information.select_Index].transform.GetChild(2).gameObject);
                        current_item[Item_Information.select_Index] = new Items_Info();
                        if (Item_Information.select_Index == 0)
                        {
                            character_Item_Panel.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                        }
                        else
                        {
                            character_Item_Panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = 0.ToString();
                        }
                        ctrl = true;
                        break;
                    }
                    else
                    {
                        count++;
                    }
                }
                if(count == item.Count)
                {
                    error_Information_Panel.SetActive(true);
                    ctrl = true;
                } 
            }
            if (!ctrl)
            {
                error_Information_Panel.SetActive(true);
            }
        }
    }
    public void Exit_Error_Information_Panel()
    {
        error_Information_Panel.SetActive(false);
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
        item_Obj.GetComponent<Item>().slot_Location = Item_Information.select_Index;
        item_Obj.transform.SetParent(current_slot[Item_Information.select_Index].transform);
        item_Obj.transform.localPosition = Vector2.zero;
        item_Obj.GetComponent<Image>().sprite = current_Select_Item.item_Img;
        item_Obj.name = current_Select_Item.slug;
        item_Obj.GetComponent<Item_Information>().set_Pos = true;
        
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

            if (potion.GetComponent<Item>().amount <= 1)
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
    // --------------------------------------------------
} // class










