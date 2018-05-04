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

    }

    // Update is called once per frame
    void Update()
    {
        StatusUI.transform.position = new Vector2(this.transform.position.x+0.3f,
                                                  this.transform.position.y-1.7f);

        hpBar.fillAmount = hp*0.01f;
        if (progress_gauge >= max_gauge && !isInQ)
        {
            battleManager.AddToArray(this.gameObject);
            isInQ = true;
        }
        else if (progress_gauge <= max_gauge)
        {
            progress_gauge += Time.deltaTime * filled_speed;
            guageBar.fillAmount = progress_gauge * 0.01f;
           

           
        }

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

        if (hp < 0)
        {
            this.gameObject.SetActive(false);
            StatusUI.SetActive(false);
        }

    }
    public void Skillused()
    {
        skill_guage = 0;
    }
}
