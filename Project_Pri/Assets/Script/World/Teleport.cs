using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {


    [SerializeField] private Transform[] trans_list;
    [SerializeField] private int[] position_money;
    private Transform player_trans;
	void Start () {
        player_trans = InGamemanager.Instance.PlayerDataContainer_readonly.PlayerTrans;
    }
	
	
	void Update () {
		
	}
    void Teleportation(int pos)
    {
        player_trans.position = trans_list[pos].position;
        // position_money[pos]를 이용해 돈 깎기
    }
    void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
