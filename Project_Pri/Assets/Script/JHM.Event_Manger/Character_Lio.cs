using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Lio : MonoBehaviour {

    private Animator anim;

    private float time;
    private float time_temp;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (time < time_temp)
        {
            transform.Translate(Vector3.right);
            time += Time.deltaTime;
        }

        
    }
    public void Character_Action()
    {
        time_temp = Time.deltaTime + 2f;
        time = Time.deltaTime;
    }

} // class












