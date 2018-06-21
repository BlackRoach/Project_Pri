using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class Json_Controller : MonoBehaviour {

    public static Json_Controller instance = null;
    
    public Text[] text_Condition;
    
    private JsonData load_Data;

    //private string json_File_1;

    private TextAsset json_File_1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Condition_Panel에 있는 능력치 초기화
    public void Defualt_Json_Data()
    {
        
        // json_File_1 = File.ReadAllText(Application.dataPath + "/JHM.Assets/Resources/JsonFile01.json");
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
    // ---------------------
    // Condition_Panel에 있는 능력치 조절 control
    public void Power_Plus_Pressed()
    {
        int i = Game_Controller.instance.Add_One(int.Parse(text_Condition[0].text.ToString()));
        text_Condition[0].text = i.ToString();
    }
    public void Power_Minus_Pressed()
    {
        int i = Game_Controller.instance.Sub_One(int.Parse(text_Condition[0].text.ToString()));
        text_Condition[0].text = i.ToString();
    }
    public void Con_Plus_Pressed()
    {
        int i = Game_Controller.instance.Add_One(int.Parse(text_Condition[1].text.ToString()));
        text_Condition[1].text = i.ToString();
    }
    public void Con_Minus_Pressed()
    {
        int i = Game_Controller.instance.Sub_One(int.Parse(text_Condition[1].text.ToString()));
        text_Condition[1].text = i.ToString();
    }
    public void HP_Plus_Pressed()
    {
        int i = Game_Controller.instance.Add_One(int.Parse(text_Condition[2].text.ToString()));
        text_Condition[2].text = i.ToString();
    }
    public void HP_Minus_Pressed()
    {
        int i = Game_Controller.instance.Sub_One(int.Parse(text_Condition[2].text.ToString()));
        text_Condition[2].text = i.ToString();
    }
    // ----------------------------------


} // class









