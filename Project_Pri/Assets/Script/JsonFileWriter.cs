using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class JsonFileWriter : MonoBehaviour
{
    private static JsonFileWriter instance = null;
    public static JsonFileWriter Instance
    {
        get
        {
            return instance;

        }
    }


    private JsonData data;



    private TextAsset jsonfile;
    private string load;
    private string path;
    private string filename;
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject.GetComponent<JsonFileWriter>());
            return;
        }
        instance = this;
       
    }

    public JsonData SerializeData(string filename)
    {
        path = Path.Combine(Application.persistentDataPath, filename + ".json");
        this.filename = filename;
        if (!File.Exists(path))
        {

            jsonfile = Resources.Load<TextAsset>("JsonDB/" + filename) as TextAsset;
          
            data = JsonMapper.ToObject(jsonfile.text);
        }
        else
        {
            
            load = File.ReadAllText(path);
           
            data = JsonMapper.ToObject(load);
        }
        return data;
    }

    public void DeserializeData(JsonData data)
    {
        string save;
        path = Path.Combine(Application.persistentDataPath, filename + ".json");

        save = JsonMapper.ToJson(data);
        File.WriteAllText(path, save);


    }
   

}
