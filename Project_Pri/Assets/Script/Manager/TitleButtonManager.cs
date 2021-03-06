﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;

public class TitleButtonManager : MonoBehaviour {

    // 싱글톤 패턴
    private static TitleButtonManager instance = null;

    public GameObject save_Location_Panel;
    public GameObject load_Location_Panel;
    public GameObject game_Start_Panel;
    public GameObject game_LoadError_Panel;
    

    public static TitleButtonManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("*Manager").AddComponent<TitleButtonManager>();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private GameObject selectableButtons;
    [SerializeField] private GameObject touchToStartButton;
    [SerializeField] private GameObject optionPanel;

    [SerializeField] private float playingTime = 1f;

    private Image touchToStartImg;
    private float time = 0f;
    private bool isTitle = false;
    void Start()
    {
        touchToStartButton.SetActive(true);
        selectableButtons.SetActive(false);
        // ----------------------------------------
        save_Location_Panel.SetActive(false);
        load_Location_Panel.SetActive(false);
        game_Start_Panel.SetActive(false);
        game_LoadError_Panel.SetActive(false);

        touchToStartImg = touchToStartButton.GetComponent<Image>();
        StartCoroutine(TouchButtonInvisible());
    }

    public void TouchToStartButtonOn()
    {
        isTitle = true;
        touchToStartButton.SetActive(false);
        selectableButtons.SetActive(true);
    }
    public void NewGameButtonOn()
    {
        // 새게임 버튼
        //초기화 진행

        SceneManager.LoadScene("WorldMap");
    }
    public void LoadGameButtonOn()
    {
        // 이어하기 버튼
        SceneManager.LoadScene("");
    }
    public void CgModeButtonOn()
    {
        // cg모드 버튼
        SceneManager.LoadScene("CGGallery 1");

    }
    public void OptionButtonOn()
    {
        // 옵션 버튼
        optionPanel.SetActive(true);
    }
    public void ExitButtonOn()
    {
        // 끝내기 버튼
        Application.Quit();
    }
    // 세이브 버튼 누를시 데이터 초기화 후 게임 실행 
    public void Button_Save_Data_Play_Game()
    {
        NewInventory_Items_Data.instance.is_Defualt = true;
        switch (NewInventory_JsonData.instance.selected_Save_Location)
        {
            case 1:
                {
                    NewInventory_JsonData.instance.Default_Save_Data_Rena_Attire_Status(NewInventory_JsonData.instance.selected_Save_Location);
                    NewInventory_JsonData.instance.Default_Save_Data_Party_Status(NewInventory_JsonData.instance.selected_Save_Location);
                }
                break;
            case 2:
                {
                    NewInventory_JsonData.instance.Default_Save_Data_Rena_Attire_Status(NewInventory_JsonData.instance.selected_Save_Location);
                    NewInventory_JsonData.instance.Default_Save_Data_Party_Status(NewInventory_JsonData.instance.selected_Save_Location);
                }
                break;
            case 3:
                {
                    NewInventory_JsonData.instance.Default_Save_Data_Rena_Attire_Status(NewInventory_JsonData.instance.selected_Save_Location);
                    NewInventory_JsonData.instance.Default_Save_Data_Party_Status(NewInventory_JsonData.instance.selected_Save_Location);
                }
                break;
        }
        NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE = NewInventory_JsonData.instance.selected_Save_Location;
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Rena_Attire_Status();
        NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Party_Status();
        NewInventory_Items_Data.instance.Selected_Save_Type_Include_Defualt_Data();
        // -----------------------------
        SceneManager.LoadScene("Main");
    }
    // 로드 버튼 누를시 기존 데이터 살리고 게임 실행 
    public void Button_Load_Data_Play_Game()
    {
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Save_Type_Option();
        NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Rena_Attire_Status();

        NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE = NewInventory_JsonData.instance.selected_Save_Location;
        if (NewInventory_JsonData.instance.rena_Attire_Status[NewInventory_JsonData.instance.select_Type_Option.SAVE_TYPE - 1].ID == 0)
        {
            game_LoadError_Panel.transform.GetChild(0).GetComponent<Text>().text =
                NewInventory_JsonData.instance.selected_Save_Location + "번 위치에 저장된 데이터가 없습니다.";
            game_LoadError_Panel.SetActive(true);
        }
        else
        {
            NewInventory_JsonData.instance.SAVE_NEW_DATA_JSON_Save_Type_Option();
            NewInventory_JsonData.instance.LOAD_NEW_DATA_JSON_Party_Status();
            SceneManager.LoadScene("Main");
        }
    }
    public void SoundEffectTest(AudioClip clip)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AM.SFPlay(clip);
    }

    IEnumerator TouchButtonInvisible()
    {
        Color color = touchToStartImg.color;
        time = 0f;
        color.a = Mathf.Lerp(1f, 0.5f, time);
        while (color.a > 0.5f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(1f, 0.5f, time);
            touchToStartImg.color = color;
            yield return null;
        }
        yield return null;
        if (!isTitle)
            StartCoroutine(TouchButtonVisible());
    }
    IEnumerator TouchButtonVisible()
    {
        Color color = touchToStartImg.color;
        time = 0f;
        color.a = Mathf.Lerp(0.5f, 1f, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / playingTime;
            color.a = Mathf.Lerp(0.5f, 1f, time);
            touchToStartImg.color = color;
            yield return null;
        }
        yield return null;
        if (!isTitle)
            StartCoroutine(TouchButtonInvisible());
    }
    // ---------------------------------------------
    // 수정 코드 여기서부터 끝까지 ( 형만 )

    public void Button_Save_Location_Panel()
    {
        save_Location_Panel.SetActive(true);
        load_Location_Panel.SetActive(false);
        game_Start_Panel.SetActive(false);
    }
    public void Button_Load_Location_Panel()
    {
        save_Location_Panel.SetActive(false);
        load_Location_Panel.SetActive(true);
        game_Start_Panel.SetActive(false);
    }
    public void Button_Select_Location_Panel_Out()
    {
        save_Location_Panel.SetActive(false);
        load_Location_Panel.SetActive(false);
        game_Start_Panel.SetActive(false);
    }
    public void Button_Game_Load_Error_Panel_Exit()
    {
        game_LoadError_Panel.SetActive(false);
        game_Start_Panel.SetActive(false);
    }
    public void Button_Select_Save_Location(int i)
    {
        game_Start_Panel.SetActive(true);
        game_Start_Panel.transform.GetChild(1).gameObject.SetActive(true);
        game_Start_Panel.transform.GetChild(2).gameObject.SetActive(false);
        // ---------------------------------------
        NewInventory_JsonData.instance.selected_Save_Location = i;
        game_Start_Panel.transform.GetChild(0).GetComponent<Text>().text = i.ToString() + " 번위치에 데이터를 저장합니다";
    }
    public void Button_Select_Load_Location(int i)
    {
        game_Start_Panel.SetActive(true);
        game_Start_Panel.transform.GetChild(1).gameObject.SetActive(false);
        game_Start_Panel.transform.GetChild(2).gameObject.SetActive(true);
        // ---------------------------------------
        NewInventory_JsonData.instance.selected_Save_Location = i;
        game_Start_Panel.transform.GetChild(0).GetComponent<Text>().text = i.ToString() + " 번위치에 데이터를 로드합니다";
    }
    public void Button_Select_Location_Panel_Exit()
    {
        game_Start_Panel.SetActive(false);
    }
}
