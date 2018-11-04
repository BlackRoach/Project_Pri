using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using LitJson;

public class NpcTalking : MonoBehaviour {

    public int num;
    public int npc_id;
    private InGamemanager ingamemanager;
    private JsonFileWriter jsonFileWriter;
    private JsonData loadData;
 
    private string npc_type;
    private string npc_move_type;
    private string npc_guild_id;
    private string npc_shop_id;
    private string[] npc_text;

    private void Start()
    {
        ingamemanager = InGamemanager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadData = jsonFileWriter.SerializeData("JsonDB/NPC_TABLE");

        LoadData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << LayerMask.NameToLayer("InteractiveObject");
           // layerMask = ~layerMask;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);

            if (hit)
            {                
                if (hit.transform.name == "talk"+num)
                {
                  
                    if (npc_type == "2")
                    {
                        PlayerPrefs.SetString("current_guild_id", npc_guild_id);
                        ingamemanager.GuildScene();
                    }
                    
                }


            }


        }
    }
    private void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["NPC_ID"].ToString() == npc_id.ToString())
            {
                npc_type = loadData[i]["NPC_TYPE"].ToString();
                npc_move_type = loadData[i]["NPC_MOVE_TYPE"].ToString();
                npc_guild_id = loadData[i]["NPC_GUILD_ID"].ToString();
                npc_shop_id = loadData[i]["NPC_SHOP_ID"].ToString();
                for(int j = 1; loadData[i]["NPC_TEXT_"+j] == null; i++)
                    npc_text[j] = loadData[i]["NPC_TEXT_"+j].ToString();
            }
        }
    }
}
