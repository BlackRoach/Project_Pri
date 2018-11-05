using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacanceCG_Manager : MonoBehaviour {

    public GameObject page_Panel;

    public void Button_Page_1_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(true);
        page_Panel.transform.GetChild(1).gameObject.SetActive(false);
        page_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Page_2_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(false);
        page_Panel.transform.GetChild(1).gameObject.SetActive(true);
        page_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Page_3_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(false);
        page_Panel.transform.GetChild(1).gameObject.SetActive(false);
        page_Panel.transform.GetChild(2).gameObject.SetActive(true);
    }


} // class











