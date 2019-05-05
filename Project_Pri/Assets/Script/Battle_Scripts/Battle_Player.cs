using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Player : Battle_Character
{

    private SkillManager skillmanager;

    [SerializeField] private float skill_max_guage;
    [SerializeField] private float skill_filled_speed;
    [SerializeField] private RectTransform skillGuagebar;
    
 
  

    // Use this for initialization
    void Start()
    {
        if (battleManager == null)
            battleManager = BattleManager.Instance;
        hp = 100;
        c_hp = 100;
        skillmanager = SkillManager.Instance;
        StatusInit();
        skillGuagebar = battleManager.PartyPanel[0].transform.GetChild(0).GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {

        update();
        if (skill_guage <= max_gauge)
        {
            skill_guage += Time.deltaTime * skill_filled_speed;
            skillGuagebar.sizeDelta = new Vector2(skill_guage, 100);


        }

      

    }
    
}
