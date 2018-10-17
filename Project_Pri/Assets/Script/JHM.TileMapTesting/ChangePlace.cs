using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlace : MonoBehaviour {

    public TileMapLoader tmLoader;
    public int tilemapID; // 바꿀 타일맵 ID
    public GameObject player;
    public Vector3 playerPosition; // 바꿀 플레이캐릭터 위치
	// Use this for initialization
	void Start () {
        tmLoader = FindObjectOfType<TileMapLoader>(); 
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Debug.Log("Player");
          //  tmLoader.ChangeMap(tilemapID);
            player.transform.SetPositionAndRotation(playerPosition,
                Quaternion.identity);
        }
    }




}
