using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour {

    public GameObject player;
    public float CamZ = -10;
    private Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }
    private void Update()
    {
        // transform.position = player.transform.position + offset;
        Vector3 TargetPos = new Vector3(player.transform.position.x, player.transform.position.y,transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * 2f);
        transform.position = TargetPos;
    }
}
