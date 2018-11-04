using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market_Event_One : MonoBehaviour {
    public GameObject[] sd_Character;
    public Transform[] sd_Pos;
    private Vector3[] New_Pos = new Vector3[3];

    private void Start()
    {
        Default_Pos();
    }

    private void Update()
    {
        StartCoroutine(Character_Move_Toward_Root());
    }
    // market_1_root
    IEnumerator Character_Move_Toward_Root()
    {
        sd_Character[0].transform.localPosition = Vector3.MoveTowards(sd_Character[0].transform.localPosition,
            New_Pos[0], 2f);
        sd_Character[1].transform.localPosition = Vector3.MoveTowards(sd_Character[1].transform.localPosition,
            New_Pos[1], 2f);
        sd_Character[3].transform.localPosition = Vector3.MoveTowards(sd_Character[3].transform.localPosition,
            New_Pos[2], 2f);
        yield return new WaitForSeconds(0f);
        if (sd_Character[0].transform.localPosition.x == sd_Pos[2].localPosition.x &&
           sd_Character[0].transform.localPosition.y == sd_Pos[2].localPosition.y)
        {
            New_Pos[0] = sd_Pos[5].localPosition;
        }
        if (sd_Character[1].transform.localPosition.x == sd_Pos[2].localPosition.x &&
           sd_Character[1].transform.localPosition.y == sd_Pos[2].localPosition.y)
        {
            New_Pos[1] = sd_Pos[1].localPosition;
        }
        if (sd_Character[3].transform.localPosition.x == sd_Pos[4].localPosition.x &&
           sd_Character[3].transform.localPosition.y == sd_Pos[4].localPosition.y)
        {
            New_Pos[2] = sd_Pos[2].localPosition;
        }
    }

    public void Default_Pos()
    {
        sd_Character[0].transform.localPosition = sd_Pos[0].localPosition;
        New_Pos[0] = sd_Pos[2].localPosition;
        sd_Character[1].transform.localPosition = sd_Pos[1].localPosition;
        New_Pos[1] = sd_Pos[2].localPosition;
        sd_Character[3].transform.localPosition = sd_Pos[5].localPosition;
        New_Pos[2] = sd_Pos[4].localPosition;
    }












} // class







