using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D rig;

    private Animator anim;
    private bool press_Left, press_Right, press_Up, press_Down;

    private float input;

    
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        press_Left = press_Right = press_Down = press_Up = false;
        input = 1;
        speed = 10;
    }
    private void Update()
    {
        if (press_Left)
        {
            rig.velocity = new Vector2(-input * speed, rig.velocity.y);
            anim.SetTrigger("is_Left");
        }
        else if (press_Right)
        {
            rig.velocity = new Vector2(input * speed, rig.velocity.y);
            anim.SetTrigger("is_Right");
        }
        else if (press_Up)
        {
            rig.velocity = new Vector2(rig.velocity.x,input * speed);
            anim.SetTrigger("is_Up");
        } else if (press_Down)
        {
            rig.velocity = new Vector2(rig.velocity.x, -input * speed);
            anim.SetTrigger("is_Down");
        }
        else
        {
            rig.velocity = Vector2.zero;
        }
    }
    public void Player_Move_Button_Left_Start()
    {
        press_Left = true;

    }
    public void Player_Move_Button_Left_Released()
    {
        press_Left = false;
    }
    public void Player_Move_Button_Right_Start()
    {
        press_Right = true;
    }
    public void Player_Move_Button_Right_Released()
    {
        press_Right = false;
    }
    public void Player_Move_Button_Up_Start()
    {
        press_Up = true;
    }
    public void Player_Move_Button_Up_Released()
    {
        press_Up = false;
    }
    public void Player_Move_Button_Down_Start()
    {
        press_Down = true;
    }
    public void Player_Move_Button_Down_Released()
    {
        press_Down = false;
    }
} // class







