using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Monster : Battle_Character {

  
    [SerializeField] private GameObject cursur;

	// Use this for initialization
	void Start () {
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        if (battleManager == null)
            battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadData = jsonFileWriter.SerializeData("MONSTER_TABLE");
        LoadData();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SD몬스터/" + sd_model);
        StatusInit();
    }
	
	// Update is called once per frame
	void Update () {

        update();
      

        status_t.GetComponent<Text>().text =
           "이름: " + c_name +
           "\nATK: " + atk +
           "\nDEF: " + def +
           "\nMAG: " + mag +
           "\nREP: " + rep +
           "\nSP: " + sp +
           "\nHP: " + hp;

    }

    public void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                sd_model = loadData[i]["MONSTER_MODEL"].ToString();
                c_name = loadData[i]["MONSTER_NAME"].ToString();
                hp = (int)loadData[i]["MONSTER_HP"];
                c_hp = hp;
                atk = (int)loadData[i]["MONSTER_PHY_ATK"];
                def = (int)loadData[i]["MONSTER_DEF"];
                mag = (int)loadData[i]["MONSTER_MAG_ATK"];
                rep = (int)loadData[i]["MONSTER_MAG_DEF"];
                sp = float.Parse(loadData[i]["MONSTER_SP"].ToString());
                attack_num = (int)loadData[i]["MONSTER_ATTACK_NUM"];
                attack_val = new int[attack_num];
                attack_id = new string[attack_num];
                for(int j = 1; j<=attack_num;j++)
                {
                    attack_id[j-1] = loadData[i]["MONSTER_ATTACK"+j].ToString();
                    attack_val[j-1] = (int)loadData[i]["MONSTER_ATTACK"+j+"_VALUE"];
                }
                break;
            }
        }
    }
}
