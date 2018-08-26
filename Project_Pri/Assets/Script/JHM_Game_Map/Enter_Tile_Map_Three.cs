using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Tile_Map_Three : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Tile_Map_Manager.instance.count = 5;
            Tile_Map_Manager.instance.Button_Pressed_Tile_Map_Result();
        }
    }

} // class










