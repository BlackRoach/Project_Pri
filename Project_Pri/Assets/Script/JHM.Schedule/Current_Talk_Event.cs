using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Current_Talk_Event  {

    public string event_Name;

    public int end_Event_Count;

    public int current_Event_Count;

    public string event_Fuction;

    public string input_Value;

    public Current_Talk_Event()
    {
        event_Name = " ";
        end_Event_Count = 0;
        current_Event_Count = 0;
        event_Fuction = " ";
        input_Value = " ";
    }
	
} // class







