using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour {

    public Animator animator;
    private Text damageText; 
    void Start () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
    }
	
    public void SetText(float val)
    {
        animator.GetComponent<Text>().text = "" + val;

        if (val > 0)
        {
            animator.GetComponent<Text>().color = Color.green;
        }
        else
        {
         
            animator.GetComponent<Text>().color = Color.red;
        }
    }
}
