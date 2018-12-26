using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class NewInventory_Manager : MonoBehaviour {


    public GameObject rena_Attire_Status_Panel; // 의상 패널
    public GameObject party_Status_Panel; // 모험 패널
    public GameObject rena_Status_Panel; // 레나 의상 스테이터스 패널

    private bool is_Changed; // 레나 의상스테이터스 보여줄때 on / off 해주는 변수

    private void Start()
    {
        is_Changed = false;
        rena_Attire_Status_Panel.SetActive(true);
        party_Status_Panel.SetActive(false);
        rena_Status_Panel.SetActive(false);
    }
    // 레나 의상 스테이터스 on / off 용 버튼
    public void Button_Pressed_Showing_Rena_Status()
    {
        is_Changed = !is_Changed;
        if (is_Changed)
        {
            rena_Status_Panel.SetActive(true);
        }
        else
        {
            rena_Status_Panel.SetActive(false);
        }
    }
    // 레나 의상 패널  ON
    public void Button_Pressed_Rena_Attire_Status()
    {
        rena_Attire_Status_Panel.SetActive(true);
        party_Status_Panel.SetActive(false);
    }
    // 레나 모험 패널  ON
    public void Button_Pressed_Party_Status()
    {
        rena_Attire_Status_Panel.SetActive(false);
        party_Status_Panel.SetActive(true);
    }

    // 나가기 버튼 (메인씬)
    public void Button_Pressed_Load_To_MainScene()
    {
        SceneManager.LoadScene("Main");
    }










} // class
















