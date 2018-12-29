using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save_Type_Option {

    private int language_Type;

    private int save_Type;

    public Save_Type_Option(int _language,int _save_Type)
    {
        this.language_Type = _language;
        this.save_Type = _save_Type;
    }

    public Save_Type_Option()
    {
        this.language_Type = 1;
        this.save_Type = 0;
    }
	public int LANGUAGE_TYPE
    {
        get { return this.language_Type;  }
        set { this.language_Type = value; }
    }
    public int SAVE_TYPE
    {
        get { return this.save_Type; }
        set { this.save_Type = value; }
    }
} // class










