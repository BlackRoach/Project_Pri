using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler{

    private bool bMovable;

    public ActorMove mainActor;

    public Vector3 moveDirection;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown"); // 게임오브젝트 범위 안에서 마우스 버튼을 눌렀을 때
        mainActor.AddDirection(moveDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp"); // 게임오브젝트 범위 안에서 마우스 버튼을 땠을 때
        mainActor.SubtractDirection(moveDirection);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit"); // 게임오브젝트 범위 안에서 나갔을 때
    }

    // Use this for initialization
    void Start () {
		
	}
	


}
