using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkObstacle : MonoBehaviour {

    public string tag;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            tag = "obstacle";
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "obstacle")
        {

            tag = "obstacle";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            tag = " ";
        }
    }
}
