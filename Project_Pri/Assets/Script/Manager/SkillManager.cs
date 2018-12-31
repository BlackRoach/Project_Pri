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
    [SerializeField] private Image[] skillButtonCool = new Image[4];

    private GameObject current_char;

    private string[] skill_list;
    private string skill_icon;
    private string skill_target;
    private string projectile_type; // 투사체 여부
    private string skill_type;      //스킬 타입
    private string effect_ability;  //영향을 주는 능력치
    private string up_down;         //effect abil 과 ability_value 에 따른 수치만큼 가산 차감
    private string casting_motion;  // 캐릭터 스킬캐스팅 애니메이션
    private string hit_effect;      // 스킬 타격 애니메이션
    private string effect_target;   // 스킬 이펙트 위치
    private string projectile_model;// 투사체 모델 이름

    private int ability_value;
    private int cool_down_time;     // 쿨타임 시간
    private int num_repet;          // 스킬 연속 사용? 아마 
    private int projectile_speed;   // 투사체 속도
    private int projectile_time;    // 투사체가 빗나갔을 경우 지속시간
    private int current_atknum;     

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
    IEnumerator Cooltime(Image skillFilter, float cooltime)
    {
        skillFilter.fillAmount = 0;
        while (skillFilter.fillAmount < 1)
        {
            skillFilter.fillAmount += 1 * Time.smoothDeltaTime / cooltime;

            yield return null;
        }

        yield break;
    }
    public void OpenSkillPanel(int i)
    {
        current_char = battleManager.GetParty(i);
        Battle_Character character = current_char.GetComponent<Battle_Character>();
        current_atknum = character.attack_num;
        skill_list = new string[character.attack_num];
        string skill_id;
        for (int j = 0; j < character.attack_num; j++)
        {
            skill_id = character.attack_id[j];
         
            LoadSkill(skill_id);
            skillButton[j].sprite = Resources.Load<Sprite>("skill_icon/"+skill_icon);
            skillButtonCool[j].sprite = Resources.Load<Sprite>("skill_icon/" + skill_icon+"_2");

        }
        skill_panel.SetActive(true);
    }
    public void ClickSkillButton(int i)
    {
        if (i >= current_atknum)
            return;
        string id = skill_list[i];
        battleManager.AttackButton();

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
