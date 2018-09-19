using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Talk_Value  {


    public int charm_Value;

    public int elegance_Value;


    // using player.pref 으로 저장
    public void Save_Talk_Value_Data()
    {
        PlayerPrefs.SetInt("Charm_Value", charm_Value);
        PlayerPrefs.SetInt("Elegance_Value", elegance_Value);

        PlayerPrefs.Save();
    }

} // class



















