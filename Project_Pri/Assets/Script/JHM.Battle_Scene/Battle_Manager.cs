using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Battle_Manager : MonoBehaviour {

    // 케릭터 위치와 이미지 오브젝트
    public GameObject main_Character_Pos;
    public GameObject target_Character_Pos; 
    // 케릭터 능력치 value
    public GameObject main_Character_Value;
    public GameObject target_Character_Value;

    public Text text_Announcement;

    private JsonData battle_Data;

    [SerializeField]
    private int gambling;
    [SerializeField]
    private int mainChar_Muscularstrength; // 주인공 근력값

    private Vector3 current_Pos_1;
    private Vector3 current_Pos_2;

    private void Start()
    {
        gambling = 0;
        Defualt_Json_Battle_Data_Parsing();

        mainChar_Muscularstrength = PlayerPrefs.GetInt("s_muscular_strength");
        // 케릭터 이미지 빈공간  실행시
        main_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = null;
        target_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = null;

    }

    private void Update()
    { 
    }

    private void Defualt_Json_Battle_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/SPARRING_DATA");

        battle_Data = JsonMapper.ToObject(json_File.text);

    }

    public void Button_Start_Character_Selection()
    {
        int temp = 0;
        temp = Random.Range(1, 4);
        gambling = temp;
        Set_Sparring_Character();
        Set_Main_Character();

        // -------------------------------

        target_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" +
            battle_Data[gambling - 1]["CHARACTER_SD"]);

        target_Character_Pos.transform.GetChild(0).transform.localScale = new Vector3(-1f, 1f, 1f);

        main_Character_Pos.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" +
            "character_sd_eileen");
    }

    private void Set_Sparring_Character()
    {
        target_Character_Value.transform.GetChild(0).GetComponent<Text>().text = battle_Data[gambling - 1]["NAME"].ToString();
        target_Character_Value.transform.GetChild(1).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE"].ToString();
        target_Character_Value.transform.GetChild(2).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_BASIC"].ToString();
        target_Character_Value.transform.GetChild(3).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_PLUS"].ToString();
    }
    private void Set_Main_Character()
    {
        main_Character_Value.transform.GetChild(1).GetComponent<Text>().text = mainChar_Muscularstrength.ToString();
        main_Character_Value.transform.GetChild(2).GetComponent<Text>().text = 10.ToString();
        main_Character_Value.transform.GetChild(3).GetComponent<Text>().text = 10.ToString();
    }
    // -------------------
    public void Button_Sparring_Is_Start()
    {
        text_Announcement.text = battle_Data[gambling - 1]["START_TEXT"].ToString();


    }







} // class












