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

    [SerializeField] private GameObject monster_ground;
    [SerializeField] private GameObject ally_ground;


    [SerializeField] private Image[] skillButton = new Image[4];
    [SerializeField] private Image[] skillButtonCool = new Image[4];

    private GameObject current_char;
    private Battle_Character character;
    private string[] skill_list;
    private string skill_icon;
    private string skill_target;
    private string skill_id;

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


    private bool is_interval;
    private bool is_click;
    private bool projectile_hit;

    public int isAttack = 0;  // 0 = false 1= monster 2=ally

    public Sprite normal_icon;
    public GameObject current_clicked_chr;  //현재 선택된(스킬) 캐릭터
    // Use this for initialization
    void Start () {
        battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadSkillData = jsonFileWriter.SerializeData("SKILL_LIST");
        is_click = false;
        projectile_hit = false;
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
                // 여기서 단일 타겟 스킬 이펙트, 데미지 처리
                if (hit.transform.CompareTag("Monster") && isAttack == 1)
                {
                    Time.timeScale = 1;
                    current_clicked_chr = hit.transform.gameObject;
                    if (!Projectile_skill())
                        Skill_Effect(skill_id, current_clicked_chr);

                    isAttack = 0;

                }
                else if (hit.transform.CompareTag("Ally") && isAttack == 2)
                {
                    Time.timeScale = 1;
                    current_clicked_chr = hit.transform.gameObject;
                    if(!Projectile_skill())
                        Skill_Effect(skill_id,current_clicked_chr);
             
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

        if(is_interval)
        {
            Debug.Log(1);
            StartCoroutine(IntervalWait(current_clicked_chr));
            is_interval = false;
        }

     
    }
    IEnumerator IntervalWait(GameObject target_obj)
    {
        for (int i = 1; i < num_repet; i++)
        {
            yield return new WaitForSeconds(repeat_interval);
            if (skill_target == "MONSTER_ALL" || skill_target == "ALLY_ALL")
                Set_DMG_ALL(skill_target);
     
            else if (skill_target == "MONSTER_TARGET" || skill_target == "ALLY_TARGET")
                Skill_Type_setDMG(target_obj);
        }
    }
   
    public void OpenSkillPanel(int i)
    {
        current_char = battleManager.GetParty(i);
        character= current_char.GetComponent<Battle_Character>();
        current_atknum = character.attack_num;
        skill_list = new string[character.attack_num];

        for (int j = 0; j < character.attack_num; j++)
        {
            skill_id = character.attack_id[j];
            skill_list[j] = skill_id;
            LoadSkill(skill_id);                           
            character.SetCoolTime(j, cool_down_time);       // 캐릭터 쿨타임 지정
            skillButton[j].sprite = Resources.Load<Sprite>("skill_icon/"+skill_icon);
            skillButtonCool[j].sprite = Resources.Load<Sprite>("skill_icon/" + skill_icon+ "_2");
          
          
        }
        if(character.attack_num == 0)
        {
            for (int j = 0; j < 4; j++)
                skillButton[0].sprite = normal_icon;
      
        }
        is_click = true;
        skill_panel.SetActive(true);
    }
    public void ClickSkillButton(int i)
    {
       
        if (i >= current_atknum || character.skillCoolAmount[i] < 1||
            character.skill_guage<100||Time.timeScale == 0 || !character.isFight)
            return;

        character.skill_guage = 0;
        skill_id = skill_list[i];
        LoadSkill(skill_id);

        // 여기서 단일 타겟은 한번더 골라야되니 거기서 처리
      
     
        if (skill_target == "MONSTER_TARGET")
        {
            Time.timeScale = 0;
            isAttack = 1;
        }
        else if(skill_target == "ALLY_TARGET")
        {
            Time.timeScale = 0;
            isAttack = 2;   
        }
        else
        {
            Skill_Effect(skill_id);
        }

        if (repeat_interval > 0f && projectile_type == 0) 
            is_interval = true;

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

    public void Projectile_Hit(GameObject target, string id)
    {
        Skill_Effect(id, target);
        if (num_repet > 1)
            is_interval = true;
    }
    private void Skill_Effect(string id, GameObject target = null)
    {
        LoadSkill(id);
        GameObject effect = Instantiate(Resources.Load("skill_effect/"+hit_effect)) as GameObject;
      
        if (effect_target == "THE_TARGET"&&target!=null)
        {
            // 이게 몬스터 올이랑 그라운드 효과랑 좀 다르더라 나중에 고쳐
            effect.transform.parent = target.transform;
            effect.transform.localPosition = Vector3.zero;
            Skill_Type_setDMG(target);
        }
        else if (effect_target == "MONSTER_GROUND")
        {
            effect.transform.parent = monster_ground.transform;
            effect.transform.localPosition = Vector3.zero;
            Set_DMG_ALL(effect_target);
        }
        else if (effect_target == "ALLY_GROUND")
        {
            effect.transform.parent = ally_ground.transform;
            effect.transform.localPosition = Vector3.zero;
            Set_DMG_ALL(effect_target);
        }
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

            ms1 = ((character.mag + (a * b)) * c) -
                target.GetComponent<Battle_Character>().rep;
            ms2 = 1;
            ms = ms1 > ms2 ? ms1 : ms2;




            _as1 = ((character.atk + (a * b)) * c) -
                target.GetComponent<Battle_Character>().def;
            _as2 = 1;
            _as = _as1 > _as2 ? _as1 : _as2;

            dmg_skill = _as + ms;
            dmg_skill *= (skill_type == "ATTACK" ? -1 : 1);
            target.GetComponent<Battle_Character>().hp += dmg_skill;
            DamageTextContoller.CreateDamageText(dmg_skill, target.transform);

        }
        else if (skill_type == "BUFF")
        {
            string e_a = effect_ability;
            b = up_down == "UP" ? 1 : -1;
            c = ability_value;

            if (e_a == "DEF")
                target.GetComponent<Battle_Character>().def += b * c;
            else if (e_a == "HP")
            {
                target.GetComponent<Battle_Character>().hp += b * c;
                DamageTextContoller.CreateDamageText(b * c, target.transform);
            }

        }
        else if (skill_type == "TOOL")
        {
            string e_a = effect_ability;
            a = up_down == "UP" ? 1 : -1;
            b = ability_value;
            if (e_a == "HP")
                target.GetComponent<Battle_Character>().hp += a * b;
            DamageTextContoller.CreateDamageText(a * b, target.transform);
        }

    }

    private void Set_DMG_ALL(string target)
    {
        if (target == "MONSTER_GROUND")
        {
            for (int i = 0; i < battleManager.monster_cnt_readonly; i++)
            {
                Skill_Type_setDMG(battleManager.GetMonster(i));
            }
        }
        else if (target == "ALLY_GROUND")
        {
            for (int i = 0; i < battleManager.ally_cnt_readonly; i++)
            {
                Skill_Type_setDMG(battleManager.GetParty(i));
            }
        }
    }


    private bool Projectile_skill()
    {
        if (projectile_type == 0)
            return false;

        GameObject pro_obj = Instantiate(Resources.Load("Prefabs/Battle/" + projectile_model)) as GameObject;
        pro_obj.GetComponent<projectile>().Setprojectile(current_char.transform.position,current_clicked_chr.transform.position,
            projectile_speed,projectile_time,skill_id);
      
        return true;
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
                return;
            }
        }
    }

}
