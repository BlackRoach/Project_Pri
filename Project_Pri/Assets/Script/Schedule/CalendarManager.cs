using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class CalendarManager : MonoBehaviour {

    public static CalendarManager instance;

    private int currentYear;    // 년
    private int currentMonth;   // 월
    private int currentDate;    // 일
    private string currentDay;  // 요일

    public int CurrentYear
    {
        get { return currentYear; }
        set { currentYear = value; }
    }
    public int CurrentMonth
    {
        get { return currentMonth; }
        set { currentMonth = value; }
    }
    public int CurrentDate
    {
        get { return currentDate; }
        set { currentDate = value; }
    }
    public string CurrentDay
    {
        get { return currentDay; }
        set { currentDay = value; }
    }

    private List<Year> yearList = new List<Year>();
    private List<CalendarWhatDay> dayList = new List<CalendarWhatDay>();

    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        currentYear = 192;
        currentMonth = 3;
        currentDate = 17;
        currentDay = "TUE";
    }

    // Use this for initialization
    void Start () {
        GAME_CALENDAR_LOAD();
        GAME_WAHT_DAY_LOAD();
	}

    private int JsonDataToInt(JsonData json)
    {
        return Int32.Parse(json.ToString());
    }

    private void GAME_CALENDAR_LOAD()
    {
        TextAsset file = Resources.Load<TextAsset>("JsonDB/GAME_CALENDAR");
        JsonData yearData = JsonMapper.ToObject(file.text);

        //Debug.Log("GAME_YEAR : " + yearData[0]["GAME_YEAR"]); // 190

        List<int> tmpDays = new List<int>();

        for(int i=0; i < yearData.Count; i++)
        {
            for(int j = 1; j <= 42; j++)
            {
                int tmpOneDay = JsonDataToInt(yearData[i]["DAY" + j.ToString()]);
                tmpDays.Add(tmpOneDay);
            }

            Year tmpYear = new Year(JsonDataToInt(yearData[i]["ID"]),
                JsonDataToInt(yearData[i]["GAME_YEAR"]),
                JsonDataToInt(yearData[i]["GAME_MONTH"]),
                JsonDataToInt(yearData[i]["DAY1_MIN"]),
                JsonDataToInt(yearData[i]["DAY1_MAX"]),
                JsonDataToInt(yearData[i]["DAY2_MIN"]),
                JsonDataToInt(yearData[i]["DAY2_MAX"]),
                JsonDataToInt(yearData[i]["DAY3_MIN"]),
                JsonDataToInt(yearData[i]["DAY3_MAX"]),
                tmpDays);

            yearList.Add(tmpYear);
            tmpDays.Clear();
        }
        //Debug.Log(yearList.Count); // 85
        //Debug.Log(yearList[0].DAYSLIST.Count); // 42
    }

    private void GAME_WAHT_DAY_LOAD()
    {
        TextAsset file = Resources.Load<TextAsset>("JsonDB/CALENDAR_WHAT_DAY");
        JsonData dayData = JsonMapper.ToObject(file.text);

        //Debug.Log("WHAT_MOMTH : " + dayData[0]["WHAT_MONTH"]);
        
        for(int i = 0; i < dayData.Count; i++)
        {
            CalendarWhatDay tmpDay = new CalendarWhatDay(
                JsonDataToInt(dayData[i]["ID"]),
                JsonDataToInt(dayData[i]["CALENDER_YEAR"]),
                JsonDataToInt(dayData[i]["CALENDER_MONTH"]),
                JsonDataToInt(dayData[i]["CALENDER_DAY"]),
                dayData[i]["WHAT_DAY"].ToString(),
                dayData[i]["WHAT_MONTH"].ToString()
                );

            dayList.Add(tmpDay);
        }
        //Debug.Log(dayList.Count);
    }

    public Year GetCurrentYear()
    {
        Year tmpYear = null;

        for(int i = 0; i < yearList.Count; i++)
        {
            if (yearList[i].GAME_YEAR == currentYear &&
                yearList[i].GAME_MONTH == currentMonth)
            {
                tmpYear = yearList[i];
            }
        }
        return tmpYear;
    }

    // JSON 정보를 불러온다. o
    // 현재 날짜를 저장한다.
    // 씬이 바껴도 현재 날짜를 유지한다.
    // - CalendarManager 클래스를 DontDestroy 처리해주어야 한다.
    // 날짜 정보가 필요한 오브젝트에 날짜 정보를 제공해주어야 한다.
    // 년/월/일/요일 정보는 어떤 씬에서는 유지되야 한다.
    // 필요한 Json 파일
    // - GAME_CALENDAR / CALENDAR_WHAT_DAY
}
