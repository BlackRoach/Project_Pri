using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status_Rena_Manager : MonoBehaviour {

    public GameObject rena_Status_Panel;

    private GameObject rena_Status;
    private GameObject advanture_Status;


    private bool status_Control;

    private void Start()
    {
        status_Control = false;
        rena_Status_Panel.SetActive(false);
        rena_Status = rena_Status_Panel.transform.GetChild(0).gameObject;
        advanture_Status = rena_Status_Panel.transform.GetChild(1).gameObject;
        Rena_Status_Data_List();
        Advanture_Status_Data_List();
    }
    public void Button_Rena_Status_Panel()
    {
        status_Control = !status_Control;
        if (status_Control)
        {
            rena_Status_Panel.SetActive(true);
        }
        else
        {
            rena_Status_Panel.SetActive(false);
        }
    }
    private void Rena_Status_Data_List()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Rena_Status_Text_Data_Input(0);
                    Rena_Status_Slider_Data_Input(0);
                }
                break;
            case 2:
                {
                    Rena_Status_Text_Data_Input(1);
                    Rena_Status_Slider_Data_Input(1);
                }
                break;
            case 3:
                {
                    Rena_Status_Text_Data_Input(2);
                    Rena_Status_Slider_Data_Input(2);
                }
                break;
        }
    }
    // 레나 스테이터스 텍스트 INPUT
    private void Rena_Status_Text_Data_Input(int i)
    {
        rena_Status.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "근력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].MUSCULAR_STRENGTH.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "마법력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].MAGIC_POWER.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "체력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].STAMINA.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "지력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].INTELLECT.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = "매력: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].CHARM.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = "센스: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].SENSE.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(6).GetComponent<Text>().text = "자존감: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].PRIDE.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(7).GetComponent<Text>().text = "예술성: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].ARTISTIC.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(8).GetComponent<Text>().text = "기품: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].ELEGANCE.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(9).GetComponent<Text>().text = "도덕성: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].MORALITY.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(10).GetComponent<Text>().text = "신뢰도: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].RELIABILITY.ToString();
        rena_Status.transform.GetChild(0).transform.GetChild(11).GetComponent<Text>().text = "스트레스: " +
            NewInventory_JsonData.instance.rena_Attire_Status[i].STRESS.ToString();
    }
    // 레나 스테이터스 슬라이더 INPUT
    private void Rena_Status_Slider_Data_Input(int i)
    {
        rena_Status.transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].MUSCULAR_STRENGTH;
        rena_Status.transform.GetChild(1).transform.GetChild(1).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].MAGIC_POWER;
        rena_Status.transform.GetChild(1).transform.GetChild(2).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].STAMINA;
        rena_Status.transform.GetChild(1).transform.GetChild(3).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].INTELLECT;
        rena_Status.transform.GetChild(1).transform.GetChild(4).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].CHARM;
        rena_Status.transform.GetChild(1).transform.GetChild(5).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].SENSE;
        rena_Status.transform.GetChild(1).transform.GetChild(6).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].PRIDE;
        rena_Status.transform.GetChild(1).transform.GetChild(7).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].ARTISTIC;
        rena_Status.transform.GetChild(1).transform.GetChild(8).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].ELEGANCE;
        rena_Status.transform.GetChild(1).transform.GetChild(9).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].MORALITY;
        rena_Status.transform.GetChild(1).transform.GetChild(10).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].RELIABILITY;
        rena_Status.transform.GetChild(1).transform.GetChild(11).GetComponent<Slider>().value =
            NewInventory_JsonData.instance.rena_Attire_Status[i].STRESS;
    }
    private void Advanture_Status_Data_List()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();

        switch (NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE)
        {
            case 1:
                {
                    Advanture_Data_Text_Input(0);
                }
                break;
            case 2:
                {
                    Advanture_Data_Text_Input(4);
                }
                break;
            case 3:
                {
                    Advanture_Data_Text_Input(8);
                }
                break;
        }
    }
    // 파티 스테이터스 텍스트 INPUT
    private void Advanture_Data_Text_Input(int i)
    {
        advanture_Status.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "GRADE: " +
            NewInventory_JsonData.instance.party_Status[i].PARTY_GRADE.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "FAME: " +
            NewInventory_JsonData.instance.party_Status[i].FAME.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "ATK: " +
            NewInventory_JsonData.instance.party_Status[i].ATK.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = "DEF: " +
            NewInventory_JsonData.instance.party_Status[i].DEF.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text = "MAG: " +
            NewInventory_JsonData.instance.party_Status[i].MAG.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(5).GetComponent<Text>().text = "REP: " +
            NewInventory_JsonData.instance.party_Status[i].REP.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(6).GetComponent<Text>().text = "SP: " +
            NewInventory_JsonData.instance.party_Status[i].SP.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(7).GetComponent<Text>().text = "SP2: " +
            NewInventory_JsonData.instance.party_Status[i].SP2.ToString();
        advanture_Status.transform.GetChild(0).transform.GetChild(8).GetComponent<Text>().text = "HP: " +
            NewInventory_JsonData.instance.party_Status[i].HP.ToString();
    }

} // class











