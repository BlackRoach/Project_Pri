using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Talk_Event_Trigger_Manager  {

    private int trigger_Num;
    private int play_Count;
    private string command_Event;


    public Talk_Event_Trigger_Manager()
    {
        this.trigger_Num = 0;
        this.play_Count = 0;
        this.command_Event = " ";
    }

    public int Trigger_Num
    {
        get { return trigger_Num; }
        set { trigger_Num = value; }
    }
    public int Play_Count
    {
        get { return play_Count; }
        set { play_Count = value; }
    }
    public string Command_Event
    {
        get { return command_Event; }
        set { command_Event = value; }
    }
} // class













