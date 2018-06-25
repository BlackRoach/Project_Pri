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

    public int save_Code;

    public Json_Game_Data(int _power, int _con, int _hp)
    {
        power = _power;
        con = _con;
        hp = _hp;

        save_Code = (power + con + hp) * 2 + 11000;
    }
}

public class Json_Controller : MonoBehaviour
{

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

        Debug.Log(Application.persistentDataPath);
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
    // DATA 저장 
    public void New_Json_Data_Saving_Button_Pressed()
    {
        int temp = int.Parse(Game_Controller.instance.text_Save_Location_Number.text.ToString());
        switch (temp)
        {
            case 1:
                {
                    Json_Data_Save_Area_Number_1();
                }
                break;
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

        JsonData save_Json = JsonMapper.ToJson(save_Data_01);

        File.WriteAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_01.json", save_Json.ToString());

        Debug.Log(save_Json.ToString());
    }
    private void Json_Data_Save_Area_Number_2()
    {
        save_Data_02.Add(new Json_Game_Data(int.Parse(text_Condition[0].text.ToString()),
            int.Parse(text_Condition[1].text.ToString()),
            int.Parse(text_Condition[2].text.ToString())));

        JsonData save_Json = JsonMapper.ToJson(save_Data_02);

        File.WriteAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_02.json", save_Json.ToString());

        Debug.Log(save_Json.ToString());
    }
    // ---------------------------
    // DATA 불러오기
    public void Json_Data_Load_Area_Number_1()
    {
        if (File.Exists(Application.dataPath + "/Resources/JHM.Resources.Json/save_01.json"))
        {
            string load_Json = File.ReadAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_01.json");

            JsonData load_One = JsonMapper.ToObject(load_Json);

            int temp = ((int.Parse(load_One[0]["power"]
                .ToString()) + int.Parse(load_One[0]["con"].ToString()) + int.Parse(load_One[0]["hp"].ToString())) * 2)
                + 11000;
            if (int.Parse(load_One[0]["save_Code"].ToString()) == temp)
            {
                Game_Controller.instance.Clicked_Button_To_Condition_Panel_From_Load();
                Load_Json_Parsing_Data(load_One);
            }
            else
            {
                Game_Controller.instance.Clicked_Button_Erorr_Panel();
            }
        }
        else
        {
            Debug.Log("json data(save_1) 파일 찾지 못함!");
            Game_Controller.instance.Clicked_Button_Erorr_Panel();
        }

    }
    public void Json_Data_Load_Area_Number_2()
    {
        if (File.Exists(Application.dataPath + "/Resources/JHM.Resources.Json/save_02.json"))
        {
            string load_Json = File.ReadAllText(Application.dataPath + "/Resources/JHM.Resources.Json/save_02.json");

            JsonData load_Two = JsonMapper.ToObject(load_Json);

            int temp = ((int.Parse(load_Two[0]["power"]
                .ToString()) + int.Parse(load_Two[0]["con"].ToString()) + int.Parse(load_Two[0]["hp"].ToString())) * 2)
                + 11000;
            if (int.Parse(load_Two[0]["save_Code"].ToString()) == temp)
            {
                Game_Controller.instance.Clicked_Button_To_Condition_Panel_From_Load();
                Load_Json_Parsing_Data(load_Two);
            }
            else
            {
                Game_Controller.instance.Clicked_Button_Erorr_Panel();
            }
        }
        else
        {
            Debug.Log("json data(save_2) 파일 찾지 못함!");
            Game_Controller.instance.Clicked_Button_Erorr_Panel();
        }
    }
    private void Load_Json_Parsing_Data(JsonData data)
    {
        text_Condition[0].text = data[0]["power"].ToString();
        text_Condition[1].text = data[0]["con"].ToString();
        text_Condition[2].text = data[0]["hp"].ToString();
    }

    // ------------------------------
} // class





