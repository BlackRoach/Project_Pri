using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_Event_Three : MonoBehaviour {

    public GameObject[] sd_Character;
    public Transform[] sd_Pos;

    private Vector3[] NewPos = new Vector3[3];

    private void Start()
    {
        sd_Character[0].transform.localPosition = sd_Pos[0].localPosition;
        NewPos[0] = sd_Pos[2].localPosition;

        NewPos[1] = sd_Pos[2].localPosition;

        sd_Character[4].transform.localPosition = sd_Pos[5].localPosition;
        NewPos[2] = sd_Pos[2].localPosition;
    }

    private void Update()
    {
        StartCoroutine(Character_Move_Toward_Root());
    }


    IEnumerator Character_Move_Toward_Root()
    {
        sd_Character[0].transform.localPosition = Vector3.MoveTowards(sd_Character[0].transform.localPosition,
            NewPos[0], 2f);
        sd_Character[2].transform.localPosition = Vector3.MoveTowards(sd_Character[2].transform.localPosition,
            NewPos[1], 2f);
        sd_Character[4].transform.localPosition = Vector3.MoveTowards(sd_Character[4].transform.localPosition,
            NewPos[2], 2f);
        yield return new WaitForSeconds(0f);

        if(sd_Character[0].transform.localPosition.x == sd_Pos[2].localPosition.x &&
            sd_Character[0].transform.localPosition.y == sd_Pos[2].localPosition.y)
        {
            NewPos[0] = sd_Pos[5].localPosition;
        }
        if (sd_Character[2].transform.localPosition.x == sd_Pos[2].localPosition.x &&
            sd_Character[2].transform.localPosition.y == sd_Pos[2].localPosition.y)
        {
            NewPos[1] = sd_Pos[0].localPosition;
        }
        if (sd_Character[4].transform.localPosition.x == sd_Pos[2].localPosition.x &&
            sd_Character[4].transform.localPosition.y == sd_Pos[2].localPosition.y)
        {
            NewPos[2] = sd_Pos[1].localPosition;
        }
    }




} // class







