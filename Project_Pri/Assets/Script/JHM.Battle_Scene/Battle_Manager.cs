using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Battle_Manager : MonoBehaviour {


    public GameObject main_Character;
    public GameObject target_Character;

    private JsonData battle_Data;

    [SerializeField]
    private int gambling;
    [SerializeField]
    private int mainChar_Muscularstrength; // 주인공 근력값

    private void Start()
    {
        gambling = 0;
        Defualt_Json_Battle_Data_Parsing();

        mainChar_Muscularstrength = PlayerPrefs.GetInt("s_muscular_strength");
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
    }

    private void Set_Sparring_Character()
    {
        target_Character.transform.GetChild(0).GetComponent<Text>().text = battle_Data[gambling - 1]["NAME"].ToString();
        target_Character.transform.GetChild(1).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE"].ToString();
        target_Character.transform.GetChild(2).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_BASIC"].ToString();
        target_Character.transform.GetChild(3).GetComponent<Text>().text = battle_Data[gambling - 1]["STATE_PLUS"].ToString();
    }
    private void Set_Main_Character()
    {
        main_Character.transform.GetChild(1).GetComponent<Text>().text = mainChar_Muscularstrength.ToString();
        main_Character.transform.GetChild(2).GetComponent<Text>().text = 10.ToString();
        main_Character.transform.GetChild(3).GetComponent<Text>().text = 10.ToString();
    }







} // class












