using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapInformation : MonoBehaviour {

   [SerializeField] private InGamemanager inGamemanager;
   
    public Transform[] spawnpos;
 
    private void Start()
    {
        inGamemanager = InGamemanager.Instance;
    }

    public void SpawnPlayer(int i)
    {
       GameObject.Find("@Player").gameObject.transform.position = spawnpos[i].position;
    }
  
}
