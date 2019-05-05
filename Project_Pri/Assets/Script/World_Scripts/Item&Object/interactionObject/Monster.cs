using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private InGamemanager inGameManager;
	// Use this for initialization
	void Start () {
        inGameManager = InGamemanager.Instance;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerPrefs.SetString("Current_group_id", "40006");
            inGameManager.opponent = this.transform.parent.gameObject;
            inGameManager.BattleButton();
        }
    }
   
  
 
    
    
}
