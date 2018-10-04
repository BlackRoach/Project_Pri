using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour {

    public Transform greenPos_1, greenPos_2;
    public Transform bluePos_1, bluePos_2;
    public Transform purplePos_1, purplePos_2;
    public Transform redPos_1, redPos_2;

    private Vector3 new_Direction;
    [SerializeField]
    private int do_Again; // 케릭터가 동선따라 이동할때 어느 색 동선은 한번더 반복

    private float _speed;

    private void Start()
    {
        transform.position = new Vector3(greenPos_1.position.x,transform.position.y,transform.position.z);
        new_Direction= new Vector3(greenPos_2.position.x,transform.position.y, transform.position.z);

        do_Again = 0;
        _speed = 0.9f;
    }
    private void Update()
    {
        DrawLine();

        if (Battle_Manager.instance.battle_Start)
        {
            StartCoroutine(Character_Image_Movement());
        }
        else
        {
            StopAllCoroutines();
        }
    }
    // 동선 그리기
    private void DrawLine()
    {
        Debug.DrawLine(greenPos_1.position, greenPos_2.position, Color.green);
        Debug.DrawLine(bluePos_1.position, bluePos_2.position, Color.blue);
        Debug.DrawLine(purplePos_1.position, purplePos_2.position, new Color(128f, 0f, 128f));
        Debug.DrawLine(redPos_1.position, redPos_2.position, Color.red);
    }
    // ----------------
    IEnumerator Character_Image_Movement()
    {
         transform.position = Vector3.MoveTowards(transform.position, new_Direction, _speed);

         yield return new WaitForSeconds(1f);
        if(transform.position.x == greenPos_2.position.x)
        {
            transform.position = new Vector3(bluePos_1.position.x, transform.position.y, transform.position.z);
            new_Direction = new Vector3(bluePos_2.position.x, transform.position.y, transform.position.z);
        }
        if (do_Again <= 3){
            if (transform.position.x == bluePos_2.position.x)
            {
                do_Again++;
                transform.position = new Vector3(bluePos_2.position.x, transform.position.y, transform.position.z);
                new_Direction = new Vector3(bluePos_1.position.x, transform.position.y, transform.position.z);
            }
            if (transform.position.x == bluePos_1.position.x)
            {
                do_Again++;
                transform.position = new Vector3(bluePos_1.position.x, transform.position.y, transform.position.z);
                new_Direction = new Vector3(bluePos_2.position.x, transform.position.y, transform.position.z);
            }
        }
        if(do_Again == 4 && transform.position.x == bluePos_1.position.x)
        {
            _speed = 1.8f;
            transform.position = new Vector3(redPos_1.position.x, transform.position.y, transform.position.z);
            new_Direction = new Vector3(redPos_2.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.x == redPos_2.position.x)
        {
            _speed = 0.9f;
            transform.position = new Vector3(purplePos_2.position.x, transform.position.y, transform.position.z);
            new_Direction = new Vector3(purplePos_1.position.x, transform.position.y, transform.position.z);
        }
        if(transform.position.x == purplePos_1.position.x)
        {         
            Battle_Manager.instance.battle_Start = false;

            transform.position = new Vector3(greenPos_1.position.x, transform.position.y, transform.position.z);
            new_Direction = new Vector3(greenPos_2.position.x, transform.position.y, transform.position.z);

            do_Again = 0;
            _speed = 0.9f;           
        }
    }






} // class

