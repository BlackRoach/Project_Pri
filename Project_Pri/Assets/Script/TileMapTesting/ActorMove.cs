using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMove : MonoBehaviour {

    public float moveSpeed = 3f;
    public Vector3 moveDirection;
    public Vector3 finalDirection;
    private Rigidbody2D rb;    
    private Animator anim;
    private string beforeState;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        beforeState = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = finalDirection * moveSpeed;
        SetAnimator();
    }

    public void AddDirection(Vector3 moveDirection)
    {
        finalDirection += moveDirection;
    }

    public void SubtractDirection(Vector3 moveDirection)
    {
        finalDirection -= moveDirection;
    }
   
    private void SetAnimator()
    {
        bool bGoUp = finalDirection.y > 0;
        bool bGoDown = finalDirection.y < 0;
        bool bGoRight = finalDirection.y == 0 && finalDirection.x > 0;
        bool bGoLeft = finalDirection.y == 0 && finalDirection.x < 0;
        bool bStop = finalDirection == Vector3.zero;

        if(bGoUp)
        {
            anim.Play("actorMoveUp");
            beforeState = "moveUp";
        }
        if(bGoDown)
        {
            anim.Play("actorMoveDown");
            beforeState = "moveDown";
        }
        if(bGoRight)
        {
            anim.Play("actorMoveRight");
            beforeState = "moveRight";
        }
        if(bGoLeft)
        {
            anim.Play("actorMoveLeft");
            beforeState = "moveLeft";
        }
        if(bStop)
        {
            if(beforeState == "moveUp")
                anim.Play("actorIdleUp");

            if (beforeState == "moveDown")
                anim.Play("actorIdleDown");

            if (beforeState == "moveRight")
                anim.Play("actorIdleRight");

            if (beforeState == "moveLeft")
                anim.Play("actorIdleLeft");
        }
    }
}
