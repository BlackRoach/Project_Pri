using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_Mode_Ending : MonoBehaviour {

    public static List<CG_Ending> ending_Data;

    public static bool one_Time_Load = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<CG_Mode_Ending>().Length > 1)
            Destroy(gameObject);

        if (!one_Time_Load)
        {
            ending_Data = new List<CG_Ending>();
            for(int i = 0; i < 50; i++)
            {
                ending_Data.Add(new CG_Ending(" ", false));
            }
            one_Time_Load = true; 
        }
    }
} // class


public class CG_Ending
{
    private string img_Name;

    private bool is_Unlock;
    public CG_Ending(string _name, bool _lock)
    {
        img_Name = _name;
        is_Unlock = _lock;
    }
    public string CG_Ending_Name
    {
       get { return img_Name; }
       set { img_Name = value; }
    }
    public bool IS_UNLOCK
    {
        get { return is_Unlock; }
        set { is_Unlock = value; }
    }
}











