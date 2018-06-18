using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class Json_Controller : MonoBehaviour {

    public GameObject origin_Panel;
    public GameObject condition_Panel;

    public Text[] text_Condition;
    
    private JsonData load_Data;

    //private string json_File_1;

    private TextAsset json_File_1;
    private void Start()
    {
        origin_Panel.SetActive(true);
        condition_Panel.SetActive(false);
    }

    public void Load_Json_Files()
    {
        origin_Panel.SetActive(false);
        condition_Panel.SetActive(true);

        // --------------------------

        // json_File_1 = File.ReadAllText(Application.dataPath + "/JHM.Assets/Resources/JsonFile01.json");\
        json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/JsonFile01");

        load_Data = JsonMapper.ToObject(json_File_1.text);

        Json_Parsing_Data(load_Data);
    }
    
    private void Json_Parsing_Data(JsonData data)
    {
        text_Condition[0].text = data[0]["START_STR"].ToString();
        text_Condition[1].text = data[0]["START_CON"].ToString(); 
        text_Condition[2].text = data[0]["START_HP"].ToString();
    }

    
} // class









