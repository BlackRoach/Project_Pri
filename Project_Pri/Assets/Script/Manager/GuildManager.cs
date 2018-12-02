using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
public class GuildManager : MonoBehaviour {

    
    private InGamemanager ingameManager;
    private JsonFileWriter jsonFileWriter;

    private JsonData loadPartyData;
    private JsonData loadData;
    [SerializeField] private GameObject FrontWindow;
    [SerializeField] private GameObject PartyWindow;
    [SerializeField] private GameObject InfoWindow;
    [SerializeField] private Text name;
    [SerializeField] private Text front;
    [SerializeField] private Text info_name;
    [SerializeField] private Text info_text;
    [SerializeField] private Text info_info;
    [SerializeField] private Image[] party_face = new Image[6];
    [SerializeField] private Image party_standing;
    private string guild_id;
    private string guild_front_name;
    private string guild_front_character;
    private string[] guild_front_text = new string[7];
    private string guild_party_num;
    private string[] guild_party_id;
    private string[] guild_party_face;

    private string party_id;
    private string party_name;
    private string party_grade;
    private string party_type_a;
    private string party_type_b;
    private string party_phy_atk;
    private string party_mag_atk;
    private string party_def;
    private string party_mag_def;
    private string party_hp;
    private string party_mp;
    private string party_sp;
    private string party_sp2;
    private string party_fame;
    private string party_story;
    private string party_price;
    private string party_guild_text;
    private string party_stand;

    // Use this for initialization
    void Start () {
        ingameManager = InGamemanager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        //if (PlayerPrefs.GetInt("NewGame") == 1)
        //{
        //    jsonFile = Resources.Load<TextAsset>("JsonDB/GUILD_TABLE") as TextAsset;
        //    loadData = JsonMapper.ToObject(jsonFile.text);
        //    jsonFile = Resources.Load<TextAsset>("JsonDB/PARTY_TABLE") as TextAsset;
        //    loadPartyData = JsonMapper.ToObject(jsonFile.text);
        //}    //
        //else
        //{
        //    jsonFile = Resources.Load<TextAsset>("JsonDB/GUILD_TABLE") as TextAsset;
        //    loadData = JsonMapper.ToObject(jsonFile.text);
        //    jsonFile = Resources.Load<TextAsset>("JsonDB/PARTY_TABLE") as TextAsset;
        //    loadPartyData = JsonMapper.ToObject(jsonFile.text);
        //}
        loadData = jsonFileWriter.SerializeData("GUILD_TABLE");
        loadPartyData = jsonFileWriter.SerializeData("PARTY_TABLE");
        guild_id = PlayerPrefs.GetString("current_guild_id");
        LoadFrontData();
        name.text = guild_front_name;
        front.text = guild_front_text[1];
        Char_Face_init();
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void Error()
    {
        front.text = guild_front_text[2];
    }
    public void Char_Face_init()
    {
        for (int i = 0; i < Int32.Parse(guild_party_num); i++)
        {
            for (int j = 0; j < loadPartyData.Count; j++)
            {
                if (loadPartyData[j]["ID"].ToString() == guild_party_id[i + 1])
                {
                    if (loadPartyData[j]["IS_PARTY"].ToString() == "0")
                    {
                        party_face[i].gameObject.SetActive(true);
                        party_face[i].sprite = Resources.Load<Sprite>("초상화/" + guild_party_face[i + 1]);
                        break;
                    }
                    else
                    {
                        party_face[i].gameObject.SetActive(false);

                    }

                }
            }
        }
    }
    public void PartyWindowOn()
    {
        FrontWindow.SetActive(false);
        PartyWindow.SetActive(true);
       
    }
    public void PartyWindowOff()
    {
        FrontWindow.SetActive(true);
        PartyWindow.SetActive(false);
        front.text = guild_front_text[1];
    }
    public void PartyInfoOn(int i)
    {
        InfoWindow.SetActive(true);
        PartyWindow.SetActive(false);
        LoadPartyInfo(guild_party_id[i]);
        info_name.text = party_name;
        info_text.text = party_guild_text;
        info_info.text = 
            "[이름]: "+party_name+
            "\n[등급]: "+party_grade+
            "\n[타입]: "+party_type_a+" "+party_type_b+
            "\n[공격력]: " + party_phy_atk +
            " [마력]: " + party_mag_atk +
            " [방어력]: " + party_def +
            " [항마력]: " + party_mag_def +
            "\n[HP]: " + party_hp +
            " [MP]: " + party_mp +
            " [속도]: " + party_sp +
            " [집중]: " + party_sp2 +
            " [명성]: " + party_fame +
            "\n[배경]: " + party_story +
            "\n\n[고용비용]: " + party_price + "G";
        party_standing.sprite = Resources.Load<Sprite>("캐릭터스탠딩/" + party_stand);
    }
    public void PartyInfoOff()
    {
        InfoWindow.SetActive(false);
        PartyWindow.SetActive(true);

       
    }
    public void BackToWorld()
    {
     
        ingameManager.TurnOnWorldObjects();
    
        SceneManager.LoadScene("WorldMap");

    }
    public void Hire()
    {
        for (int i = 0; i < loadPartyData.Count; i++)
        {
            if (loadPartyData[i]["ID"].ToString() == party_id)
            {
                if (loadPartyData[i]["IS_PARTY"].ToString() == "0")
                {

                    loadPartyData[i]["IS_PARTY"] = 1;
                    break;
                }

            }
        }

        jsonFileWriter.DeserializeData(loadPartyData);
        Char_Face_init();
        PartyInfoOff();
    }
    private void LoadFrontData()
    {
        
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["GUILD_ID"].ToString() == guild_id)
            {
                guild_front_name = loadData[i]["GUILD_FRONT_NAME"].ToString();
                guild_front_character = loadData[i]["GUILD_FRONT_CHARACTER"].ToString();
                guild_front_text = new string[Int32.Parse(loadData[i]["GUILD_FRONT_TEXT_NUM"].ToString()) + 1];
                for (int j = 1; j<=Int32.Parse(loadData[i]["GUILD_FRONT_TEXT_NUM"].ToString()); j++)
                     guild_front_text[j] = loadData[i]["GUILD_FRONT_TEXT_" + j].ToString();

                guild_party_num = loadData[i]["GUILD_PARTY_NUM"].ToString();
                guild_party_id = new string[Int32.Parse(guild_party_num)+1];
                guild_party_face = new string[Int32.Parse(guild_party_num)+1];
                for (int j = 1; j<= Int32.Parse(guild_party_num); j++)
                    guild_party_id[j] = loadData[i]["GUILD_PARTY_"+j+"_ID"].ToString();

                for (int j = 1; j <= Int32.Parse(guild_party_num); j++)
                    guild_party_face[j] = loadData[i]["GUILD_PARTY_" + j + "_FACE"].ToString();
                break;
            }
        }
    }
    private void LoadPartyInfo(string id)
    {
        for (int i = 0; i < loadPartyData.Count; i++)
        {
            if (loadPartyData[i]["ID"].ToString() == id)
            {
                party_id = id;
                party_name = loadPartyData[i]["PARTY_NAME"].ToString();
                party_grade = loadPartyData[i]["PARTY_GRADE"].ToString();
                party_type_a = loadPartyData[i]["PARTY_TYPE_A"].ToString();
                party_type_b = loadPartyData[i]["PARTY_TYPE_B"].ToString();
                party_phy_atk = loadPartyData[i]["PARTY_PHY_ATK"].ToString();
                party_mag_atk = loadPartyData[i]["PARTY_MAG_ATK"].ToString();
                party_def = loadPartyData[i]["PARTY_DEF"].ToString();
                party_mag_def = loadPartyData[i]["PARTY_MAG_DEF"].ToString();
                party_hp = loadPartyData[i]["PARTY_HP"].ToString();
                party_mp = loadPartyData[i]["PARTY_MP"].ToString();
                party_sp = loadPartyData[i]["PARTY_SP"].ToString();
                party_sp2 = loadPartyData[i]["PARTY_SP2"].ToString();
                party_fame = loadPartyData[i]["PARTY_FAME"].ToString();
                party_story = loadPartyData[i]["PARTY_STORY"].ToString();
                party_price = loadPartyData[i]["PARTY_PRICE"].ToString();
                party_guild_text = loadPartyData[i]["PARTY_GUILD_TEXT"].ToString();
                party_stand = loadPartyData[i]["PARTY_STANDING"].ToString();
                break;
            }
        }
    }
  
}
