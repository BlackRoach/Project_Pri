using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalking : MonoBehaviour {

    public int num;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << LayerMask.NameToLayer("InteractiveObject");
           // layerMask = ~layerMask;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);

            if (hit)
            {
                
                if (hit.transform.name == "talk"+num)
                {
                    Debug.Log(num);
                    
                }


            }


        }
    }
}
