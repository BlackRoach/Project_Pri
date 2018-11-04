using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class TreasureBox : MonoBehaviour {
    public string id;

    public Sprite[] box = new Sprite[2];
    private InGamemanager ingameManager;
    
    private TextAsset jsonFile;
    private JsonData loadData;

    private GameObject textbox;
    private Text textbox_t;
    
    private string name;
    private string trigger;
    private string[] text = new string[3];
    private string item_id;
    private string count;

    private int current_num;
    // Use this for initialization
    void Start () {
        ingameManager = InGamemanager.Instance;
        textbox = ingameManager.textBox;
        textbox_t = ingameManager.textBox.GetComponentInChildren<Text>();
        jsonFile = Resources.Load<TextAsset>("JsonDB/TILEMAP_EVENT");
        loadData = JsonMapper.ToObject(jsonFile.text);
        LoadData();
        if(trigger == "0")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = box[0];
        }
        else if (trigger == "1")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = box[1];
        }
        else if (trigger == "2")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void OpenBox()
    {

        if (trigger == "0")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = box[1];
            loadData[current_num]["TRIGER"] = 1;
            trigger = "1";
            textbox.SetActive(true);
            textbox_t.text = text[0];
            
            ingameManager.additemManager.Add_Item_Value(Int32.Parse(item_id));
        }
        else if (trigger == "1")
        {
            textbox.SetActive(true);
            textbox_t.text = text[1];
        }
    }
    private void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                current_num = i;
                name = loadData[i]["ID"].ToString();
                trigger = loadData[i]["TRIGER"].ToString();
                for (int j = 1; j <= 3; j++)
                    text[j - 1] = loadData[i]["TEXT_" + j].ToString();
                item_id = loadData[i]["ITEM_ID"].ToString();
                count = loadData[i]["COUNT"].ToString();
                break;
            }


        }
    }
    private void SaveData(JsonData mudoMember)
    {
        string save;
        save = JsonMapper.ToJson(mudoMember);
        File.WriteAllText(Application.persistentDataPath + "/" + "PARTY_TABLE.json", save);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           if(trigger == "2")
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                textbox.SetActive(true);
                textbox_t.text = text[2];
                trigger = "0";
            }
        }

    }
   
}
