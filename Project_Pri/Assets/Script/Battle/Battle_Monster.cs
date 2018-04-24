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
        
        progress_gauge += Time.deltaTime * filled_speed;
        if (progress_gauge >= max_gauge && !isInQ)
        {
            battleManager.AddToArray(this.gameObject);
            isInQ = true;
        }

        if (BattleManager.Instance.isAttack_readonly)
        {
            cursur.SetActive(true);
         
        }
        else
            cursur.SetActive(false);
    }


}
