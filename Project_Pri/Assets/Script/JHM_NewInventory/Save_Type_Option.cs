using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save_Type_Option {

    private int language;

    private int save_Type;

    public Save_Type_Option(int _language,int _save_Type)
    {
        this.language = _language;
        this.save_Type = _save_Type;
    }

    public Save_Type_Option()
    {
        language = 1;
        save_Type = 0;
    }
	public int Language
    {
        get { return this.language;  }
        set { this.language = value; }
    }
    public int SAVE_TYPE
    {
        get { return this.save_Type; }
        set { this.save_Type = value; }
    }
} // class










