using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour {

    public static Inventory_Controller instance = null;

    private GameObject inventory_Panel;
    private GameObject inventory_Slot_Panel;

    private Item_Json_DataBase item_DataBase;

    public GameObject slot_Prefab;
    public GameObject item_Prefab;

    public List<Items_Info> item = new List<Items_Info>();
    public List<GameObject> slot = new List<GameObject>();

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
    }

    private void Start()
    {
        current_Index = 0;
        is_Stackable = false;
        slot_Count = 20;
        
        for(int i = 0; i<slot_Count; i++)
        {
            item.Add(new Items_Info());
            slot.Add(Instantiate(slot_Prefab));
            slot[i].GetComponent<Slot>().slot_Id = i;
            slot[i].transform.SetParent(inventory_Slot_Panel.transform);
        }

        StartCoroutine(Load_Item());
        
    }
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
} // class










