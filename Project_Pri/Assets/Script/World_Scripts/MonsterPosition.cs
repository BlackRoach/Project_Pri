using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPosition : MonoBehaviour {
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkBox")
        {
            string[] splitter = new string[2];
            splitter = collision.gameObject.transform.parent.gameObject.name.Split(',');
            this.transform.parent.gameObject.GetComponent<EnemyAStar>().StartPosition(int.Parse(splitter[0]), int.Parse(splitter[1]));
       

        }
    }
}
