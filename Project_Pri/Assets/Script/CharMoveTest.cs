using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveTest : MonoBehaviour {


    [SerializeField] private Transform playerTrans;
    [SerializeField] private float speed;

    private Transform playertransform;
	void Awake () {
        playertransform = playerTrans.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Move(Vector2 direction)
    {
        playerTrans.Translate(direction * speed * Time.deltaTime);

        if (!(direction.x > 0 && playerTrans.localScale.x > 0) && 
            !(direction.x < 0 && playerTrans.localScale.x < 0))
        {
            playerTrans.localScale = new Vector3(playerTrans.localScale.x * -1,
                                                             playerTrans.localScale.y,
                                                             playerTrans.localScale.z);
        }
    }
}
