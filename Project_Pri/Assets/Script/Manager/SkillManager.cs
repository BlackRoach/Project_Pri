using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class SkillManager : MonoBehaviour {

    private static SkillManager instance = null;
    public static SkillManager Instance
    {
        get
        {
            return instance;

        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject.GetComponent<SkillManager>());
            return;
        }
        instance = this;
    }

    private JsonFileWriter jsonFileWriter;
    private BattleManager battleManager;
    private JsonData loadSkillData;

    [SerializeField] private GameObject skill_panel;
    [SerializeField] private GameObject item_panel;
    [SerializeField] private GameObject attack_panel;
    [SerializeField] private Image[] skillButton = new Image[4];
    [SerializeField] private Image[] skillButtonCool = new Image[4];

    private GameObject current_char;
    private Battle_Character character;
    private string[] skill_list;
    private string skill_icon;
    private string skill_target;

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
    private int projectile_type;    // 투사체 여부
    private int projectile_speed;   // 투사체 속도
    private int projectile_time;    // 투사체가 빗나갔을 경우 지속시간
    private int current_atknum;

 


    private float coefficient;
    private float repeat_interval;
    private float casting_time;
    private float effect_time;


 
    private bool is_click;

    public int isAttack = 0;  // 0 = false 1= monster 2=ally

    public Sprite normal_icon;
    public GameObject current_clicked_chr;  //현재 선택된(스킬) 캐릭터
    // Use this for initialization
    void Start () {
        battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadSkillData = jsonFileWriter.SerializeData("SKILL_LIST");
        is_click = false;
      
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;

            if (hit) //대상 선택
            {
                if (hit.transform.CompareTag("Monster") && isAttack == 1)
                {
                    Time.timeScale = 1;
                    current_clicked_chr = hit.transform.gameObject;
                    isAttack = 0;

                }
                else if (hit.transform.CompareTag("Ally") && isAttack == 2)
                {
                    Time.timeScale = 1;
                    current_clicked_chr = hit.transform.gameObject;
                    isAttack = 0;
                }



            }


        }
        if (is_click) // 쿨타임 이미지 채우는 기능 
        {
            for(int i = 0; i<character.attack_num;i++)
            {
                skillButton[i].fillAmount = character.skillCoolAmount[i];
            }
        }
       
    }
   
    public void OpenSkillPanel(int i)
    {
        current_char = battleManager.GetParty(i);
        character= current_char.GetComponent<Battle_Character>();
        current_atknum = character.attack_num;
        skill_list = new string[character.attack_num];
        string skill_id;
        for (int j = 0; j < character.attack_num; j++)
        {
            skill_id = character.attack_id[j];
         
            LoadSkill(skill_id);                           
            character.SetCoolTime(j, cool_down_time);       // 캐릭터 쿨타임 지정
            skillButton[j].sprite = Resources.Load<Sprite>("skill_icon/"+skill_icon);
            skillButtonCool[j].sprite = Resources.Load<Sprite>("skill_icon/" + skill_icon+ "_2");
          
          
        }
        is_click = true;
        skill_panel.SetActive(true);
    }
    public void ClickSkillButton(int i)
    {
       
        if (i >= current_atknum || character.skillCoolAmount[i] < 1)
            return;

        string id = skill_list[i];
        LoadSkill(id);

        if (skill_target == "MONSTER_ALL")
        {
            //캐스팅

            //전체효과 및 이펙트 달고 끝내
            return;
        }
        else if (skill_target == "MONSTER_TARGET")
        {
            Time.timeScale = 0;
            isAttack = 1;
            //캐스팅
            Projectile_skill();
            Skill_Type_setDMG(current_clicked_chr);
        }
        else if (skill_target == "ALLY_ALL")
        {
            //캐스팅
            //전체효과 및 이펙트 달고 끝내
            return;
        }
        else if(skill_target == "ALLY_TARGET")
        {
            Time.timeScale = 0;
            isAttack = 2;
            //캐스팅
            Skill_Type_setDMG(current_clicked_chr);
        }

        

        character.skillCoolAmount[i] = 0;
      
        is_click = true;
     

       
    }
   

    public void CloseSkillPanel()
    {
        for(int i = 0; i< 4;i++)
        {
            skillButton[i].sprite = normal_icon;
        }
        skill_panel.SetActive(false);
        is_click = false;
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


    private void Skill_Type_setDMG(GameObject target)
    {
        int a;
        int b;
        float c;
        if (skill_type == "ATTACK"|| skill_type == "HEAL")
        {
            a = up_down == "UP" ? 1 : -1;
            b = ability_value;
            c = coefficient;

            float ms, ms1, ms2;
            float _as, _as1, _as2;

            float dmg_skill;

            ms1 = (current_char.GetComponent<Battle_Character>().mag + (a * b) * c -
                target.GetComponent<Battle_Character>().rep);
            ms2 = 1;
            ms = ms1 > ms2 ? ms1 : ms2;




            _as1 = (current_char.GetComponent<Battle_Character>().atk + (a * b) * c -
                target.GetComponent<Battle_Character>().def);
            _as2 = 1;
            _as = _as1 > _as2 ? _as1 : _as2;

            dmg_skill = _as + ms;


        }
        else if (skill_type == "BUFF")
        {
            string e_a = effect_ability;
            b = up_down == "UP" ? 1 : -1;
            c = ability_value;

            if (e_a == "DEF")
                target.GetComponent<Battle_Character>().def =
                    target.GetComponent<Battle_Character>().def + (b * c);
            else if (e_a == "HP")
                target.GetComponent<Battle_Character>().hp =
                    target.GetComponent<Battle_Character>().hp + (b * c);

        }
        else if (skill_type == "TOOL")
        {
            string e_a = effect_ability;
            a = up_down == "UP" ? 1 : -1;
            b = ability_value;
            if(e_a == "HP")
                target.GetComponent<Battle_Character>().hp =
                   target.GetComponent<Battle_Character>().hp + (a * b);

        }

    }


    private void Projectile_skill()
    {
        if (projectile_type == 0)
            return;

        Skill_Type_setDMG(current_clicked_chr);
    }


    private void LoadSkill(string id)
    {
        for (int i = 0; i < loadSkillData.Count; i++)
        {
            if (loadSkillData[i]["ID"].ToString() == id)
            {
                skill_icon = loadSkillData[i]["SKILL_ICON"].ToString();
                skill_target = loadSkillData[i]["SKILL_TARGET"].ToString();
                projectile_type = Int32.Parse(loadSkillData[i]["PROJECTILE_TYPE"].ToString());
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
