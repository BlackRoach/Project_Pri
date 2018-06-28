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
    public Json_Game_Data() { }
}

public class Json_Controller : MonoBehaviour
{

    public static Json_Controller instance = null;

    public Text[] text_Condition;

    private JsonData load_Data;

    private TextAsset json_File_1;

    private Json_Game_Data save_Data_01 = new Json_Game_Data();
    private Json_Game_Data save_Data_02 = new Json_Game_Data();

    public string mobile_Path;
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
    private void Start()
    {
        mobile_Path = Application.persistentDataPath;

        Debug.Log(mobile_Path);
    }
    // Condition_Panel에 있는 능력치 초기화
    public void Defualt_Json_Data()
    {       
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
        Game_Controller.instance.Clicked_Button_Save_Description_Panel_Close();
    }
    private void Json_Data_Save_Area_Number_1()
    {
        save_Data_01 = new Json_Game_Data(int.Parse(text_Condition[0].text.ToString()),
            int.Parse(text_Condition[1].text.ToString()),
            int.Parse(text_Condition[2].text.ToString()));

        string save_Json = JsonUtility.ToJson(save_Data_01);

        File.WriteAllText(mobile_Path + "/" + "save_01.json", save_Json);
       
    }
    private void Json_Data_Save_Area_Number_2()
    {
        save_Data_02 = new Json_Game_Data(int.Parse(text_Condition[0].text.ToString()),
            int.Parse(text_Condition[1].text.ToString()),
            int.Parse(text_Condition[2].text.ToString()));

        string save_Json = JsonUtility.ToJson(save_Data_02);

        File.WriteAllText(mobile_Path + "/" + "save_02.json", save_Json);

    }
    // ---------------------------
    // DATA 불러오기
    public void Json_Data_Load_Area_Number_1()
    {
        if (File.Exists(mobile_Path + "/" + "save_01.json"))
        {
            string load_Json = File.ReadAllText(mobile_Path + "/" + "save_01.json");

            Json_Game_Data load_Data = new Json_Game_Data();
            JsonUtility.FromJsonOverwrite(load_Json, load_Data);

            int temp = (load_Data.power + load_Data.con + load_Data.hp) * 2 + 11000;
            if (load_Data.save_Code == temp)
            {
                Game_Controller.instance.Clicked_Button_To_Condition_Panel_From_Load();
                Load_Json_Parsing_Data(load_Data);
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
        if (File.Exists(mobile_Path + "/" + "save_02.json"))
        {
            string load_Json = File.ReadAllText(mobile_Path + "/" + "save_02.json");

            Json_Game_Data load_Data = new Json_Game_Data();
            JsonUtility.FromJsonOverwrite(load_Json, load_Data);

            int temp = (load_Data.power + load_Data.con + load_Data.hp) * 2 + 11000;
            if (load_Data.save_Code == temp)
            {
                Game_Controller.instance.Clicked_Button_To_Condition_Panel_From_Load();
                Load_Json_Parsing_Data(load_Data);
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
    private void Load_Json_Parsing_Data(Json_Game_Data data)
    {
        text_Condition[0].text = data.power.ToString();
        text_Condition[1].text = data.con.ToString();
        text_Condition[2].text = data.hp.ToString();
    }

    // ------------------------------
} // class





