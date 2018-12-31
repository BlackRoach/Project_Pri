using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Rena_Manager : MonoBehaviour {

    public GameObject rena_Status_Panel;

    private bool status_Control;

    private void Start()
    {
        status_Control = false;
        rena_Status_Panel.SetActive(false);
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


} // class











