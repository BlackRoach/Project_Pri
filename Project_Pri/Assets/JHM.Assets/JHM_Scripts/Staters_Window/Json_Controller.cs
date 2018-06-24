using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class Json_Game_Data
{
    public int power;
    public int con;
    public int hp;

    public Json_Game_Data(int _power, int _con, int _hp)
    {
        power = _power;
        con = _con;
        hp = _hp;
    }
}



public class Json_Controller : MonoBehaviour {

    public static Json_Controller instance = null;
    
    public Text[] text_Condition;
    
    private JsonData load_Data;

    //private string json_File_1;

    private TextAsset json_File_1;

    private List<Json_Game_Data> save_Data_01 = new List<Json_Game_Data>();
    private List<Json_Game_Data> save_Data_02 = new List<Json_Game_Data>();
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
        json_File_1 = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Defualt_Ability");

        load_Data = JsonMapper.ToObject(json_File_1.text);

        Defualt_Json_Parsing_Data(load_Data);
    }
    private void Defualt_Json_Parsing_Data(JsonData data)
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
    

    public void New_Json_Data_Saving_Button_Pressed()
    {
        int temp = int.Parse(Game_Controller.instance.text_Save_Location_Number.text.ToString());
        switch (temp)
        {
            case 1:
                {
                    Json_Data_Save_Area_Number_1();
                } break;
            case 2:
                {
                    Json_Data_Save_Area_Number_2();
                }
                break;
        }       
    }
    private void Json_Data_Save_Area_Number_1()
    {
        save_Data_01.Add(new Json_Game_Data(int.Parse(text_Condition[0].text.ToString()),
            int.Parse(text_Condition[1].text.ToString()),
            int.Parse(text_Condition[2].text.ToString())));

        JsonData to_Json = JsonMapper.ToJson(save_Data_01);

        File.WriteAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_01.json", to_Json.ToString());

        Debug.Log(to_Json.ToString());
    }
    private void Json_Data_Save_Area_Number_2()
    {
        save_Data_02.Add(new Json_Game_Data(int.Parse(text_Condition[0].text.ToString()),
            int.Parse(text_Condition[1].text.ToString()),
            int.Parse(text_Condition[2].text.ToString())));

        JsonData to_Json = JsonMapper.ToJson(save_Data_02);

        File.WriteAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_02.json", to_Json.ToString());

        Debug.Log(to_Json.ToString());
    }
} // class









