using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Party : Battle_Character
{
    private SkillManager skillmanager;


    [SerializeField] private float sp2;
    [SerializeField] private float skill_max_guage;
    [SerializeField] private float skill_filled_speed;
    [SerializeField] private RectTransform skillGuagebar;

   

    // Use this for initialization
    void Start () {
        if (battleManager == null)
            battleManager = BattleManager.Instance;
        skillmanager = SkillManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadData = jsonFileWriter.SerializeData("PARTY_TABLE");
        LoadData();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SD캐릭터/" + sd_model);
        StatusInit();
        skillGuagebar = battleManager.PartyPanel[num].gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        skillCoolTime = new int[attack_num];
        skillCoolAmount = new float[attack_num];
        for (int i = 0; i < attack_num; i++)
            skillCoolAmount[i] = 1;
    }

    // Update is called once per frame
    void Update () {
        update();
        status_t.transform.GetChild(0).GetComponent<Text>().text =
           "<size=30>" + "<color=#ff9500>" + c_name +"</color>" + "</size>" +
          "\nATK: " + atk +
          "\nDEF: " + def +
          "\nMAG: " + mag +
          "\nREP: " + rep +
          "<color=#00b3ff>" + "\nSP: " + sp + "</color>" +
          "<color=#01709f>" + "\nSP2: " + sp2 + "</color>" +
          "<color=#3bf600>" + "\nHP: " + hp + "</color>";

        if (skill_guage <= max_gauge)
        {
            skill_guage += Time.deltaTime * skill_filled_speed;
            skillGuagebar.sizeDelta = new Vector2(skill_guage, 100);
     

        }
    
        for (int i = 0; i < attack_num; i++)
        {
            if(skillCoolAmount[i]<1)
                skillCoolAmount[i] += 1 * Time.smoothDeltaTime / skillCoolTime[i];
        }

    }

    public void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
     
                sd_model = loadData[i]["PARTY_SD_MODEL"].ToString();
                c_name = loadData[i]["PARTY_NAME"].ToString();
                hp = (int)loadData[i]["PARTY_HP"];
                c_hp = hp;
                atk = (int)loadData[i]["PARTY_PHY_ATK"];
                def = (int)loadData[i]["PARTY_DEF"];
                mag = (int)loadData[i]["PARTY_MAG_ATK"];
                rep = (int)loadData[i]["PARTY_MAG_DEF"];
                sp = float.Parse(loadData[i]["PARTY_SP"].ToString());
                sp2 = float.Parse(loadData[i]["PARTY_SP2"].ToString());
                attack_num = (int)loadData[i]["PARTY_ATTACK_NUM"];
                attack_id = new string[attack_num];
                for (int j = 1; j <= attack_num; j++)
                {
                    attack_id[j - 1] = loadData[i]["PARTY_ATTACK" + j].ToString();
                }
                break;
            }
        }
    }

    
}
