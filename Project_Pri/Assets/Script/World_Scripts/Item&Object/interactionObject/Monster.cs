using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : InteractionObject, IInteractive
{
    private InGamemanager inGameManager;
	// Use this for initialization
	void Start () {
        inGameManager = InGamemanager.Instance;
	}

  
    protected override void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            base.OnTriggerExit2D(col);
            inGameManager.CloseBattleWindow();
            inGameManager.EnableInfoButton();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void OpenBattleInfoWindow()
    {
        inGameManager.OpenBattleWindow();
    }

    void IInteractive.Interaction()
    {
        OpenBattleInfoWindow();
    }
    
}
