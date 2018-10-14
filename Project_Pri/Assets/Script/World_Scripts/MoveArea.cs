using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea : MonoBehaviour {

    public int tilenum;

    private TileMapLoader tileMapLoader;
	
	void Start () {
        tileMapLoader = TileMapLoader.Instance;	
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tileMapLoader.ChangeMap(tilenum);
        }

    }

}
