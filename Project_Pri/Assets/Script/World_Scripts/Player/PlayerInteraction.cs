using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    private IInteractive interaction_Object = null;

    public IInteractive _Interaction_Object
    {
        set
        {
            this.interaction_Object = value;
        }
        get
        {
            return this.interaction_Object;
        }
    }
    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
    }
    public void Interaction()
    {
        if (interaction_Object == null)
            return;
        interaction_Object.Interaction();
    }

   
}
public interface IInteractive
{
    void Interaction();
}