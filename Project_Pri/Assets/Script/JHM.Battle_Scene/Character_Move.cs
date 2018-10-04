using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour {


    public Transform pos_1, pos_2;



    private void Start()
    {
        transform.position = new Vector3(pos_1.position.x,transform.position.y,transform.position.z);
    }


    private void Update()
    {
        Debug.DrawLine(pos_1.position, pos_2.position, Color.green);
    }

    private void Character_Image_Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos_2.position.x,
            transform.position.y, transform.position.z), 0.5f);
    }









} // class

