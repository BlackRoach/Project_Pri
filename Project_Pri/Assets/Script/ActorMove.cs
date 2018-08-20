using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour {

    public float moveSpeed = 3f;
    private float currentMoveSpeed = 0;
    private Vector3 moveDirection;
    private Rigidbody2D rb;
    bool IsDeadEnd;
    private Animator anim;

    // 테스트
    public bool press_Up;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position + moveDirection, Color.yellow);
        DeadEndCheck();
        SetAnimator();

	}

    public void Move(Vector3 moveDirection)
    {
        //Debug.Log(moveDirection);
        moveDirection = moveDirection.normalized; // 크기가 1이상인 벡터를 무조건 1로 만든다.
        this.moveDirection = moveDirection;
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime; // currentMoveSpeed * Time.deltaTime
        Debug.Log(rb.velocity);
    }

    // 막다른 길 체크
    private void DeadEndCheck()
    {

        //Debug.Log(LayerMask.NameToLayer("Object"));
        IsDeadEnd = Physics2D.Linecast(transform.position,transform.position + moveDirection, 1 << LayerMask.NameToLayer("Object"));
        //Debug.Log(IsDeadEnd);
        if (IsDeadEnd)
            currentMoveSpeed = 0;
        else
            currentMoveSpeed = moveSpeed;
        // 막다른 길에 도달하면 속도를 0으로 만들어 떨림을 없앤다.

    }

    private void SetAnimator()
    {
        if(moveDirection.x < 0)
        {
            anim.SetBool("bLeft", true);
        }
        if(moveDirection.x > 0)
        {
            anim.SetBool("bRight", true);
        }

        if(moveDirection.x == 0)
        {
            anim.SetBool("bLeft", false);
            anim.SetBool("bRight", false);
        }

        if (moveDirection.y > 0)
        {
            anim.SetBool("bUp", true);
        }
        if (moveDirection.y < 0)
        {
            anim.SetBool("bDown", true);
        }

        if (moveDirection.y == 0)
        {
            anim.SetBool("bUp", false);
            anim.SetBool("bDown", false);
        }
        //Debug.Log(moveDirection);
    }

    public void Stop()
    {
        moveDirection = Vector3.zero;
        rb.velocity = Vector2.zero;
    }
}

// <캐릭터(액터) 이동 처리>
// - 버튼으로 처리하면 누르는 순간만 잠깐 움직인다.
