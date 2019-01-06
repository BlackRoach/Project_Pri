using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status_Rena_Manager : MonoBehaviour {

    public GameObject rena_Status_Panel;

    private GameObject rena_Status;

    private bool status_Control;

    private void Start()
    {
        status_Control = false;
        rena_Status_Panel.SetActive(false);
        rena_Status = rena_Status_Panel.transform.GetChild(0).gameObject;

        Rena_Status_Data_List();
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
                }
                break;
            case 2:
                {
                    Rena_Status_Text_Data_Input(1);
                }
                break;
            case 3:
                {
                    Rena_Status_Text_Data_Input(2);
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


} // class











