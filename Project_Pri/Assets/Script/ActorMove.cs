using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour {

    public float moveSpeed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector3 moveDirection)
    {
        moveDirection = moveDirection.normalized; // 크기가 1이상인 벡터를 무조건 1로 만든다.
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

// <캐릭터(액터) 이동 처리>
// - 버튼으로 처리하면 누르는 순간만 잠깐 움직인다.
