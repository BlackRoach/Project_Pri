using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class Json_Controller : MonoBehaviour {

    private string json_File_1;
    private JsonData json_Data;

    public void Load_Json_Files()
    {
        json_File_1 = File.ReadAllText(Application.dataPath + "/JHM.Assets/Resources/JsonFile01.json");

        json_Data = JsonMapper.ToObject(json_File_1);

        Debug.Log(json_Data);

    }
    

    
} // class









