using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
public class PartyManager : MonoBehaviour {

    private InGamemanager ingameManager;
    private JsonFileWriter jsonFileWriter;
    private JsonData loadPartyData;
    [SerializeField] private Image[] partyImg = new Image[3];
    [SerializeField] private Text status;
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
    private string party_face;

    [SerializeField]private string current_party;
    private List<string> partyid = new List<string>();

    // Use this for initialization
    void Start () {
        ingameManager = InGamemanager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadPartyData = jsonFileWriter.SerializeData("PARTY_TABLE");
        Getparty();
        Party_Face_init();
    }


    private void Getparty()
    {
     
        for (int i = 0,n=0; i < loadPartyData.Count; i++)
        {
            
            if (loadPartyData[i]["IS_PARTY"].ToString() == "1")
            {
                partyid.Add(loadPartyData[i]["ID"].ToString());
                n++;
                if (n == 2)
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
                
                break;
            }
        }
    }
  

    private void Party_Face_init()
    {
        for (int i = 0; i < partyid.Count; i++)
        {
            for (int j = 0; j < loadPartyData.Count; j++)
            {
                if (loadPartyData[j]["ID"].ToString() == partyid[i])
                {
                    
                    partyImg[i].gameObject.SetActive(true);
                    partyImg[i].sprite = Resources.Load<Sprite>("초상화/" + loadPartyData[j]["PARTY_FACE"].ToString());
                    break;

                }
            }
        }
        for(int i = partyid.Count; i<3;i++)
            partyImg[i].gameObject.SetActive(false);
    }
    public void Select(int id)
    {
        LoadPartyInfo(partyid[id]);
        current_party = partyid[id];
        status.text =
           "[이름]: " + party_name +
           "\n[등급]: " + party_grade +
           "\n[타입]: " + party_type_a + " " + party_type_b +
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

    }
    public void Fire()
    {
        for (int i = 0; i < loadPartyData.Count; i++)
        {
            if (loadPartyData[i]["ID"].ToString() == current_party)
            {
                loadPartyData[i]["IS_PARTY"] = 0;
                partyid.Remove(current_party);
                break;

            }
        }
        jsonFileWriter.DeserializeData("PARTY_TABLE");
        status.text = "";
        Party_Face_init();
        current_party = "";
    }
    public void Exit()
    {
        ingameManager.TurnOnWorldObjects();
        SceneManager.LoadScene("WorldMap");
    }

}
