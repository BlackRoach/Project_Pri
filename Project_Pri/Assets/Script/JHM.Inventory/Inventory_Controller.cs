using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour {


    private GameObject inventory_Panel;
    private GameObject inventory_Slot_Panel;

    private Item_Json_DataBase item_DataBase;

    public GameObject slot_Prefab;
    public GameObject item_Prefab;

    public List<Items_Info> item = new List<Items_Info>();
    public List<GameObject> slot = new List<GameObject>();

    private int slot_Count;

    private void Awake()
    {
        inventory_Panel = GameObject.Find("Inventory_Panel");
        inventory_Slot_Panel = inventory_Panel.transform.Find("Slot_Panel").gameObject;
        item_DataBase = GameObject.Find("Inventory_Json_Data").GetComponent<Item_Json_DataBase>();
    }

    private void Start()
    {
        slot_Count = 20;
        
        for(int i = 0; i<slot_Count; i++)
        {
            item.Add(new Items_Info());
            slot.Add(Instantiate(slot_Prefab));
            slot[i].transform.SetParent(inventory_Slot_Panel.transform);
        }
    }

    public void Add_Item(int _id)
    {
        Items_Info add_Item = item_DataBase.Search_For_Item(_id);

        for(int i = 0; i < item.Count; i++)
        {
            if(item[i].id == -1)
            {
                item[i] = add_Item;
                GameObject item_Obj = Instantiate(item_Prefab);
                item_Obj.transform.SetParent(slot[i].transform);
                item_Obj.transform.localPosition = Vector2.zero;
                item_Obj.GetComponent<Image>().sprite = add_Item.item_Img;
                item_Obj.name = add_Item.slug;
                break;
            }
        }

    }
} // class










