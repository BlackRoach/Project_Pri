using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Year
{
    private int id; // ID
    private int game_year; // 년도
    private int game_month; // 달
    private int day1_min; // 상순 시작
    private int day1_max; // 상순 끝
    private int day2_min; // 중순 시작
    private int day2_max; // 중순 끝
    private int day3_min; // 하순 시작
    private int day3_max; // 하순 끝
    private List<int> daysList; // 일자 리스트

    public Year(int id, int game_year, int game_month,
        int day1_min, int day1_max, int day2_min, int day2_max,
        int day3_min, int day3_max, List<int> days)
    {
        this.id = id;
        this.game_year = game_year;
        this.game_month = game_month;
        this.day1_min = day1_min;
        this.day1_max = day1_max;
        this.day2_min = day2_min;
        this.day2_max = day2_max;
        this.day3_min = day3_min;
        this.day3_max = day3_max;
        //daysList = days; // 얉은 복사 : days를 Clear하면 daysList도 Clear되버린다.
        //Debug.Log("생성자안에서 : " + days.Count); // 42
        daysList = new List<int>();

        for (int i = 0; i < days.Count; i++)
        {
            daysList.Add(days[i]); // 깊은 복사
        }
    }

    public int GAME_YEAR
    {
        get { return game_year; }
    }
    public int GAME_MONTH
    {
        get { return game_month; }
    }
    public int DAY1_MIN
    {
        get { return day1_min; }
    }
    public int DAY1_MAX
    {
        get { return day1_max; }
    }
    public int DAY2_MIN
    {
        get { return day2_min; }
    }
    public int DAY2_MAX
    {
        get { return day2_max; }
    }
    public int DAY3_MIN
    {
        get { return day3_min; }
    }
    public int DAY3_MAX
    {
        get { return day3_max; }
    }
    public List<int> DAYSLIST
    {
        get { return daysList; }
    }
}
