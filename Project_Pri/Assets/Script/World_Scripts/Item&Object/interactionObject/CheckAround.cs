using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAround : MonoBehaviour {

    public bool isIn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isIn = true;
        }

    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
}
