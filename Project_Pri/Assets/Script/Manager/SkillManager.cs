using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class SkillManager : MonoBehaviour {

    private JsonFileWriter jsonFileWriter;
    private BattleManager battleManager;
    private JsonData loadSkillData;

    [SerializeField] private GameObject skill_panel;
    [SerializeField] private GameObject item_panel;
    [SerializeField] private GameObject attack_panel;
    [SerializeField] private Image[] skillButton = new Image[4];

    private GameObject current_char;

    private string[] skill_list;
    private string skill_icon;
    private string skill_target;
    private string projectile_type;
    private string skill_type;
    private string effect_ability;
    private string up_down;
    private string casting_motion;
    private string hit_effect;
    private string effect_target;
    private string projectile_model;

    private int ability_value;
    private int cool_down_time;
    private int num_repet;
    private int projectile_speed;
    private int projectile_time;

    private float coefficient;
    private float repeat_interval;
    private float casting_time;
    private float effect_time;

    public Sprite normal_icon;

    // Use this for initialization
    void Start () {
        battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadSkillData = jsonFileWriter.SerializeData("SKILL_LIST");

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void OpenSkillPanel(int i)
    {
        current_char = battleManager.GetParty(i);
        Battle_Character character = current_char.GetComponent<Battle_Character>();
   
        skill_list = new string[character.attack_num];
        string skill_id;
        for (int j = 0; j < character.attack_num; j++)
        {
            skill_id = character.attack_id[j];
         
            LoadSkill(skill_id);
            Debug.Log(skill_icon);
            skillButton[j].sprite = Resources.Load<Sprite>("skill_icon/"+skill_icon);

        }
        skill_panel.SetActive(true);
    }
    public void CloseSkillPanel()
    {
        for(int i = 0; i< 4;i++)
        {
            skillButton[i].sprite = normal_icon;
        }
        skill_panel.SetActive(false);
    }
    public void OpenItemPanel()
    {
        skill_panel.SetActive(false);
        item_panel.SetActive(true);
    }
    public void CloseItemPanel()
    {
        item_panel.SetActive(false);
        skill_panel.SetActive(true);
    }

    private void LoadSkill(string id)
    {
        for (int i = 0; i < loadSkillData.Count; i++)
        {
            if (loadSkillData[i]["ID"].ToString() == id)
            {
                skill_icon = loadSkillData[i]["SKILL_ICON"].ToString();
                skill_target = loadSkillData[i]["SKILL_TARGET"].ToString();
                projectile_type = loadSkillData[i]["PROJECTILE_TYPE"].ToString();
                skill_type = loadSkillData[i]["SKILL_TYPE"].ToString();
                effect_ability = loadSkillData[i]["EFFECT_ABILITY"].ToString();
                ability_value = Int32.Parse(loadSkillData[i]["ABILITY_VALUE"].ToString());
                coefficient = float.Parse(loadSkillData[i]["COEFFICIENT"].ToString());
                up_down = loadSkillData[i]["UP_DOWN"].ToString();
                cool_down_time = Int32.Parse(loadSkillData[i]["COOL_DOWN_TIME"].ToString());
                num_repet = Int32.Parse(loadSkillData[i]["NUM_REPET"].ToString());
                repeat_interval = float.Parse(loadSkillData[i]["REPEAT_INTERVAL"].ToString());
                casting_motion = loadSkillData[i]["CASTING_ANIMATION"].ToString();
                casting_time = float.Parse(loadSkillData[i]["CASTING_TIME"].ToString());
                hit_effect = loadSkillData[i]["HIT_EFFECT"].ToString();
                effect_target = loadSkillData[i]["EFFECT_TARGET"].ToString();
                effect_time = float.Parse(loadSkillData[i]["EFFECT_TIME"].ToString());
                projectile_model = loadSkillData[i]["PROJECTILE_MODEL"].ToString();
                projectile_speed = Int32.Parse(loadSkillData[i]["PROJECTILE_SPEED"].ToString());
                projectile_time = Int32.Parse(loadSkillData[i]["PROJECTILE_TIME"].ToString());
            }
        }
    }

}
