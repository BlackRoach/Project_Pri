using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarEvent {

    private int id;
    private int game_year;
    private int game_month;
    private Sprite vacance_CG;
    private int festivalEvent;

    public CalendarEvent(int id, int game_year,
        int game_month, string spritename, int festivalEvent)
    {
        this.id = id;
        this.game_year = game_year;
        this.game_month = game_month;
        this.vacance_CG = Resources.Load<Sprite>("VACANCE_CG/" + spritename);
        this.festivalEvent = festivalEvent;
        // 경로가 틀려도 아무 에러 메시지가 안나온다.
    }

    public int ID
    {
        get { return id; }
    }
    public int GAME_YEAR
    {
        get { return game_year; }
    }
    public int GAME_MONTH
    {
        get { return game_month; }
    }
    public Sprite VACANCE_CG
    {
        get { return vacance_CG; }
    }
    public int FESTIVAL_EVENT
    {
        get { return festivalEvent; }
    }
}
