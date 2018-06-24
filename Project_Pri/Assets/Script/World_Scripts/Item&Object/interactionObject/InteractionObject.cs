using System.Collections;
using UnityEngine;

public class InteractionObject : MonoBehaviour, IInteractive
{

    private PlayerInteraction playerInteraction;
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
          
           
            playerInteraction = col.gameObject.GetComponent<PlayerInteraction>();

            playerInteraction._Interaction_Object = this;
            if(this.GetComponent<Collider2D>().CompareTag("Monster"))
                InGamemanager.Instance.AbleInfoButton();
            else if (this.GetComponent<Collider2D>().CompareTag("Teleport"))
                InGamemanager.Instance.OpenTeleportWindow();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInteraction._Interaction_Object == null)
            {
                playerInteraction = other.gameObject.GetComponent<PlayerInteraction>();

                playerInteraction._Interaction_Object = this;
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
          

            playerInteraction._Interaction_Object = null;
        }
    }

    void IInteractive.Interaction()
    {
       
    }
}
