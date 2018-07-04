using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class QuestPanel : InteractionObject, IInteractive
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
            inGameManager.CloseQuestWindow();
            inGameManager.EnableQuestButton();

        }
    }
  
    void Update () {
		
	}

    void IInteractive.Interaction()
    {
        inGameManager.OpenQuestWindow();
    }

    
    
}
