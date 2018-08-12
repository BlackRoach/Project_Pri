using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class Tile_Map_Manager : MonoBehaviour {

    public GameObject tile_Map_3;
    public GameObject tile_Map_4;

    public Text text_Count;

    [SerializeField]
    private int count;


    private TextAsset json_File;
    private JsonData load_Data;
    private void Start()
    {
        tile_Map_3.SetActive(false);
        tile_Map_4.SetActive(false);

        count = 1;
        text_Count.text = count.ToString();

        Defualt_Json_Data();
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
        string tile_Name = load_Data[count - 1]["TILEMAP_NAME"].ToString();
        if (count == 3 && tile_Name == load_Data[count - 1]["TILEMAP_NAME"].ToString())
        {
            tile_Map_3.SetActive(true);
            tile_Map_4.SetActive(false);
        }
        if (count == 4 && tile_Name == load_Data[count - 1]["TILEMAP_NAME"].ToString())
        {
            tile_Map_3.SetActive(false);
            tile_Map_4.SetActive(true);
        }
    }


} // class








