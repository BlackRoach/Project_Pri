using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarWhatDay
{
    private int id;
    private int year;
    private int month;
    private int calendarDay;
    private string whatDay; // 요일
    private string whatMonth; // 월

    public CalendarWhatDay(int id, int year, int month,
        int calendarDay, string whatDay, string whatMonth)
    {
        this.id = id;
        this.year = year;
        this.month = month;
        this.calendarDay = calendarDay;
        this.whatDay = whatDay;
        this.whatMonth = whatMonth;
    }

    public int ID
    {
        get { return id; }
    }
    public int YEAR
    {
        get { return year; }
    }
    public int MONTH
    {
        get { return month; }
    }
    public int CALENDAR_DAY
    {
        get { return calendarDay; }
    }
    public string WHAT_DAY
    {
        get { return whatDay; }
    }
    public string WHAT_MONTH
    {
        get { return whatMonth; }
    }


}
