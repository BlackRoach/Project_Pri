using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Player : Battle_Character
{


    [SerializeField] private float skill_max_guage;
    [SerializeField] private float skill_filled_speed;
    [SerializeField] Image skillGuagebar;
    
 
    private float skill_guage = 0;

    // Use this for initialization
    void Start()
    {
        if (battleManager == null)
            battleManager = BattleManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {

        update();
        if (skill_guage >= max_gauge && !battleManager.isCommandOn)
        {
            // 커맨드 버튼 활성화
            battleManager.CommandButtonOn();
            battleManager.isCommandOn = true;


        }
        else if (skill_guage <= max_gauge && !battleManager.isCommandOn)
        {
            skill_guage += Time.deltaTime * skill_filled_speed;
            skillGuagebar.fillAmount = skill_guage * 0.01f;


        }

      

    }
    public void Skillused()
    {
        skill_guage = 0;
    }
}
