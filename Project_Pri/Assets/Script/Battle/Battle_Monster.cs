using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Monster : MonoBehaviour {

    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject cursur;
    
    
    private int hp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(BattleManager.Instance.isAttack_readonly)
        {
            cursur.SetActive(true);
        }
        else
            cursur.SetActive(false);
    }
    public void Effect()
    {
        effect.SetActive(true);
    }
   
}
