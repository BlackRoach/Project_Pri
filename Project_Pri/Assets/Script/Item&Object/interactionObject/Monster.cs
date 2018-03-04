using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : InteractionObject, IInteractive
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OpenBattleInfoWindow()
    {

    }

    void IInteractive.Interaction()
    {
        OpenBattleInfoWindow();
    }
    
}
