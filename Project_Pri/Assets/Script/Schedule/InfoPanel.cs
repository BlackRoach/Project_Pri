using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text Month;
    public Text Day;
    public Text Date;
    public Text Year;

	// Use this for initialization
	void Start () {
        ShowDateInfo();
	}

    public void ShowDateInfo()
    {
        Month.text = CalendarManager.instance.CurrentMonth.ToString();
        Day.text = CalendarManager.instance.CurrentDay.ToString();
        Date.text = CalendarManager.instance.CurrentDate.ToString();
        Year.text = CalendarManager.instance.CurrentYear.ToString();
    }
	
}
