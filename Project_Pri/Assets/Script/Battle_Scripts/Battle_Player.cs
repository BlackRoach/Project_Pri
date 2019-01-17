using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Player : Battle_Character
{


    [SerializeField] private float skill_max_guage;
    [SerializeField] private float skill_filled_speed;
    [SerializeField] private Image skillGuagebar;
    
 
    private float skill_guage = 0;

    // Use this for initialization
    void Start()
    {
        if (battleManager == null)
            battleManager = BattleManager.Instance;
        StatusInit();
        skillGuagebar = battleManager.PartyPanel[0].gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        update();
        if (skill_guage <= max_gauge)
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
