using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    private InGamemanager ingamemanager;
    [SerializeField] GameObject talkingIcon;
    

    void Start () {
        ingamemanager = InGamemanager.Instance;
        talkingIcon.SetActive(false);
       
    }
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkingIcon.SetActive(true);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkingIcon.SetActive(false);
            ingamemanager.textBox.SetActive(false);
        }
    }
}
