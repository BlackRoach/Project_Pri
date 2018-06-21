using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Controller : MonoBehaviour {

    public static Game_Controller instance = null;

    public GameObject origin_Panel;
    public GameObject condition_Panel;


    private int count_One;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        origin_Panel.SetActive(true);
        condition_Panel.SetActive(false);

        // -------------------------

        count_One = 1; 
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

    
    public void Clicked_Button_Condition_Panel()
    {
        origin_Panel.SetActive(false);
        condition_Panel.SetActive(true);

        // -----------------------

        Json_Controller.instance.Defualt_Json_Data();
    }
    









} // class












