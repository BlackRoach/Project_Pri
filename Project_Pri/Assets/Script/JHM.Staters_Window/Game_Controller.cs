using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game_Controller : MonoBehaviour
{

    public static Game_Controller instance = null;

    public GameObject character_Skills_Panel;
    public GameObject statement_Panel;
    public GameObject origin_Panel;
    public GameObject condition_Panel;
    public GameObject save_List_Panel;
    public GameObject load_List_Panel;
    public GameObject save_Description;
    public GameObject error_Panel;
    public Text text_Save_Location_Number;

    private int count_One;

    private bool load_Exitway;

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
        Clicked_Button_Origin_Panel();
        Clicked_Button_To_Condition_Panel_From_Load();
        // -------------------------

        count_One = 1;
        load_Exitway = false;
    }
    // Condition_Panel에 있는 능력치 조절
    public int Add_One(int temp)
    {
        temp += count_One;

        return temp;
    }
    public int Sub_One(int temp)
    {
        temp -= count_One;

        return temp;
    }
    // ---------------
    public void Clicked_Button_Origin_Panel()
    {
        statement_Panel.SetActive(false);
        character_Skills_Panel.SetActive(false);
        origin_Panel.SetActive(true);
        condition_Panel.SetActive(false);
        save_List_Panel.SetActive(false);
        save_Description.SetActive(false);
        load_List_Panel.SetActive(false);
        error_Panel.SetActive(false);
    }
    public void Clicked_Button_Condition_Panel()
    {
        origin_Panel.SetActive(false);
        condition_Panel.SetActive(true);

        // -----------------------

        Json_Controller.instance.Defualt_Json_Data();
    }
    public void Clicked_Button_To_Condition_Panel_From_Load()
    {
        statement_Panel.SetActive(false);
        character_Skills_Panel.SetActive(false);
        origin_Panel.SetActive(true);
        condition_Panel.SetActive(false);
        load_List_Panel.SetActive(false);
        save_List_Panel.SetActive(false);
        save_Description.SetActive(false);
        error_Panel.SetActive(false);
    }
    public void Button_Load_To_Statement_Panel()
    {
        statement_Panel.SetActive(true);
        character_Skills_Panel.SetActive(true);
        origin_Panel.SetActive(false);
        condition_Panel.SetActive(false);
        load_List_Panel.SetActive(false);
        save_List_Panel.SetActive(false);
        save_Description.SetActive(false);
        error_Panel.SetActive(false);
    }
    public void Clicked_Button_Exit_From_Load_Panel()
    {
        if(load_Exitway)
        {
            Clicked_Button_Origin_Panel();
        } else
        {
            Clicked_Button_To_Condition_Panel_From_Load();
        }
    }

    public void Clicked_Button_Load_List_Panel(bool way)
    {
        load_Exitway = way;
        load_List_Panel.SetActive(true);
    }
    public void Clicked_Button_Save_List_Panel_Open()
    {
        save_List_Panel.SetActive(true);
    }
    public void Clicked_Button_Save_List_Panel_Close()
    {
        save_List_Panel.SetActive(false);
    }
    public void Clicked_Button_Save_Description_Panel_Open(int numberOfLocation)
    {
        save_List_Panel.SetActive(false);
        save_Description.SetActive(true);

        // -----------------------

        text_Save_Location_Number.text = numberOfLocation.ToString();
    }
    public void Clicked_Button_Save_Description_Panel_Close()
    {
        save_Description.SetActive(false);
    }
    public void Clicked_Button_Erorr_Panel()
    {
        load_List_Panel.SetActive(false);
        error_Panel.SetActive(true);
    }

    public void Clicked_Button_Exit_From_Erorr_Panel()
    {
        Debug.Log("게임 종료 작동 했음");
        Application.Quit();
    }


} // class











