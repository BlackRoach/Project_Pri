using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapInformation : MonoBehaviour {

   [SerializeField] private InGamemanager inGamemanager;
    [SerializeField] private Transform gridpos;
    private GameManager gamemanager;

    public Transform[] spawnpos;
    public int width;
    public int height;
    public int monsternum;
    public string areaName;
    private void Start()
    {
        inGamemanager = InGamemanager.Instance;
     

        gamemanager = GameManager.Instance;
        gamemanager.gridHeight = height;
        gamemanager.gridWidth = width;
        gamemanager.gridBox.transform.position = gridpos.position;
        gamemanager.monsterNum = monsternum;
        gamemanager.Init();
    }

    public void SpawnPlayer(int i)
    {
       GameObject.Find("@Player").gameObject.transform.position = spawnpos[i].position;
    }
  
}
