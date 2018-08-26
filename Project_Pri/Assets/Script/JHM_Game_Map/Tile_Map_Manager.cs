using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Tile_Map_Manager : MonoBehaviour {

    public static Tile_Map_Manager instance = null;

    public GameObject tile_Map_3;
    public GameObject tile_Map_4;
    public GameObject tile_Map_5;

    public GameObject prefab_Player;
    public Text text_Count;
    public Transform Tile_Map_Parent;


    public int count;


    private TextAsset json_File;
    private JsonData load_Data;

    public GameObject target_Map_3;
    public GameObject target_Map_4;
    public GameObject target_Map_5;
    public int current_Map;

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
        current_Map = 0;
        count = 1;
        text_Count.text = count.ToString();
        Tile_Map_Parent = GameObject.Find("Grid").transform;
        Defualt_Json_Data();

        prefab_Player.SetActive(false);
    }
    
    public void Add_One()
    {
        count++;
        text_Count.text = count.ToString();
    }
    public void Sub_One()
    {
        count--;
        text_Count.text = count.ToString();
    }

    private void Defualt_Json_Data()
    {
        json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Tile_Map_List");

        load_Data = JsonMapper.ToObject(json_File.text);
    }

    public void Button_Pressed_Tile_Map_Result()
    {
        if(current_Map == 3)
        {
            Destroy(target_Map_3.gameObject);
        } else if(current_Map == 4)
        {
            Destroy(target_Map_4.gameObject);
        }
        else if(current_Map == 5)
        {
            Destroy(target_Map_5.gameObject);
        }

        string tile_Name = load_Data[count - 1]["TILEMAP_NAME"].ToString();
        if (count == 3 && tile_Name == load_Data[count - 1]["TILEMAP_NAME"].ToString())
        {
            target_Map_3 = Instantiate(tile_Map_3);
            target_Map_3.transform.SetParent(Tile_Map_Parent);
            target_Map_3.transform.localPosition = Vector3.zero;
            current_Map = count;

            prefab_Player.SetActive(true);
            prefab_Player.transform.position = new Vector3(1, 3, 0);
            prefab_Player.transform.rotation = Quaternion.identity;
        }
        if (count == 4 && tile_Name == load_Data[count - 1]["TILEMAP_NAME"].ToString())
        {
            target_Map_4 = Instantiate(tile_Map_4);
            target_Map_4.transform.SetParent(Tile_Map_Parent);
            target_Map_4.transform.localPosition = Vector3.zero;
            current_Map = count;
        }
        if(count == 5 && tile_Name == load_Data[count - 1]["TILEMAP_NAME"].ToString())
        {
            target_Map_5 = Instantiate(tile_Map_5);
            target_Map_5.transform.SetParent(Tile_Map_Parent);
            target_Map_5.transform.localPosition = Vector3.zero;
            current_Map = count;

            prefab_Player.SetActive(true);
            prefab_Player.transform.position = new Vector3(1, -7, 0);
            prefab_Player.transform.rotation = Quaternion.identity;
        }

        

    }

} // class








