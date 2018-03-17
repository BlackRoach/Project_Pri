using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LayerState
{
    Up = 2,
    Down = 0
};
public class LayerChange : MonoBehaviour {

    private InGamemanager inGamemanager;
    private Transform playerTrans;
    private Transform objectTrans;
    private SpriteRenderer objectSprite;

    private bool isUp = true;

	void Start () {
        inGamemanager = InGamemanager.Instance;
        playerTrans = inGamemanager.PlayerDataContainer_readonly.PlayerTrans;
        objectTrans = this.transform;
        objectSprite = this.GetComponent<SpriteRenderer>();

        if (playerTrans.localPosition.y - 0.4 > objectTrans.localPosition.y - 0.4)
            isUp = true;
        else if (playerTrans.localPosition.y - 0.4 < objectTrans.localPosition.y - 0.4)
            isUp = false; 
    }

    // Update is called once per frame
    void Update () {
		if(playerTrans.localPosition.y - 0.4 > objectTrans.localPosition.y - 0.4 && !isUp)
        {
            // 플레이어가 현재 오브젝트보다 위에 있을때 현재 오브젝트의 레이어를 높인다.
            isUp = true;
            objectSprite.sortingOrder = (int)LayerState.Up;
        }
        else if (playerTrans.localPosition.y - 0.4 < objectTrans.localPosition.y - 0.4 && isUp)
        {
            // 플레이어가 현재 오브젝트보다 아래에 있을때 현재 오브젝트의 레이어를 낮춘다.
            isUp = false;
            objectSprite.sortingOrder = (int)LayerState.Down;
        }
    }
}
