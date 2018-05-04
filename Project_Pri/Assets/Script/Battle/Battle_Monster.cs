using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Monster : Battle_Character {

  
    [SerializeField] private GameObject cursur;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        StatusUI.transform.position = new Vector2(this.transform.position.x + 0.3f,
                                               this.transform.position.y - 1.7f);


        hpBar.fillAmount = hp * 0.01f;
        if (progress_gauge >= max_gauge && !isInQ)
        {
            battleManager.AddToArray(this.gameObject);
            isInQ = true;
        }
        else if(progress_gauge <= max_gauge)
        {
            progress_gauge += Time.deltaTime * filled_speed;
            guageBar.fillAmount = progress_gauge * 0.01f;
        }


        if (BattleManager.Instance.isAttack_readonly)
        {
            cursur.SetActive(true);
         
        }
        else
            cursur.SetActive(false);

        if (hp < 0)
        {
            this.gameObject.SetActive(false);
            StatusUI.SetActive(false);
        }

    }


}
