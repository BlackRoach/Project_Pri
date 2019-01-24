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
    [SerializeField] private Image skillGuagebar;

   

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
        skillGuagebar = battleManager.PartyPanel[num].gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        skillCoolTime = new int[attack_num];
        skillCoolAmount = new float[attack_num];
        for (int i = 0; i < attack_num; i++)
            skillCoolAmount[i] = 1;
    }

    // Update is called once per frame
    void Update () {
        update();
        status_t.GetComponent<Text>().text =
          "이름: " + name +
          "\nATK: " + atk +
          "\nDEF: " + def +
          "\nMAG: " + mag +
          "\nREP: " + rep +
          "\nSP: " + sp +
          "\nSP2: " + sp2 +
          "\nHP: " + hp;

        if (skill_guage <= max_gauge)
        {
            skill_guage += Time.deltaTime * skill_filled_speed;
            skillGuagebar.fillAmount = skill_guage * 0.01f;
     

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
                name = loadData[i]["PARTY_NAME"].ToString();
                hp = Int32.Parse(loadData[i]["PARTY_HP"].ToString());
                c_hp = hp;
                atk = Int32.Parse(loadData[i]["PARTY_PHY_ATK"].ToString());
                def = Int32.Parse(loadData[i]["PARTY_DEF"].ToString());
                mag = Int32.Parse(loadData[i]["PARTY_MAG_ATK"].ToString());
                rep = Int32.Parse(loadData[i]["PARTY_MAG_DEF"].ToString());
                sp = float.Parse(loadData[i]["PARTY_SP"].ToString());
                sp2 = float.Parse(loadData[i]["PARTY_SP2"].ToString());
                attack_num = Int32.Parse(loadData[i]["PARTY_ATTACK_NUM"].ToString());
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
