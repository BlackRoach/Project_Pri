﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarTest : MonoBehaviour {

    private int year;
    private int month;
    private int date;

    public Text yearInput;
    public Text monthInput;
    public Text dateInput;

	// Use this for initialization
	void Start () {
        year = CalendarManager.instance.CurrentYear;
        month = CalendarManager.instance.CurrentMonth;
        date = CalendarManager.instance.CurrentDate;
        ShowDate();
        
	}
	
    private void ShowDate()
    {
        yearInput.text = year.ToString();
        monthInput.text = month.ToString();
        dateInput.text = date.ToString();
    }

	public void AddYear()
    {
        if (year >= 197)
        {
            year = 197;
        }
        else
        {
            year++;
        }
        ShowDate();    
    }

    public void SubYear()
    {
        if(year <= 190)
        {
            year = 190;
        }
        else
        {
            year--;
        }
        ShowDate();
    }

    public void AddMonth()
    {
        if(month >= 12)
        {
            month = 12;
        }
        else
        {
            month++;
        }
        ShowDate();
    }

    public void SubMonth()
    {
        if (month <= 1)
        {
            month = 1;
        }
        else
        {
            month--;
        }
        ShowDate();
    }

    public void AddDate()
    {
        if (date >= 31)
        {
            date = 31;
        }
        else
        {
            date++;
        }
        ShowDate();
    }
    
    public void SubDate()
    {
        if(date <= 1)
        {
            date = 1;
        }
        else
        {
            date--;
        }
        ShowDate();
    }

    public void ApplyDate()
    {
        CalendarManager.instance.CurrentYear = year;
        CalendarManager.instance.CurrentMonth = month;
        CalendarManager.instance.CurrentDate = date;

        FindObjectOfType<InfoPanel>().ShowDateInfo();
        FindObjectOfType<ScheduleManager>().CheckFestivalEvent();

        if(year == 197 && month == 1)
        {
            FindObjectOfType<ScheduleManager>().studyPanel.SetActive(false);
            FindObjectOfType<ScheduleManager>().arbeitPanel.SetActive(false);
            FindObjectOfType<ScheduleManager>().restPanel.SetActive(true);

            for (int i = 0; i < FindObjectOfType<ScheduleManager>().restPanel.transform.childCount; i++)
            {
                FindObjectOfType<ScheduleManager>().restPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
            FindObjectOfType<ScheduleManager>().restPanel.transform.GetChild(
                FindObjectOfType<ScheduleManager>().restPanel.transform.childCount - 1).gameObject.SetActive(true);
        }
        else
        {
            FindObjectOfType<ScheduleManager>().restPanel.transform.GetChild(3).gameObject.SetActive(true);
            FindObjectOfType<ScheduleManager>().restPanel.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}

// 코드가 제대로 동작하는지 확인하는 클래스
