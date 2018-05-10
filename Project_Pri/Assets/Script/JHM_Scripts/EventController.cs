using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class EventController : MonoBehaviour {



    // layout_Panel
    public GameObject ui_Count_Ten_Panel;
    public GameObject ui_Count_Panel;
    public GameObject ui_Count_Five_Panel;



    public Text text_Count;

    [SerializeField]
    private int increase_count;

    private int StartCount = 0;

    private int current_Count;
    

    private void Start()
    {

        current_Count = StartCount;
        text_Count.text = StartCount.ToString();
    }

    // 1씩 더하기
    public void AddOne()
    {
        current_Count += increase_count;
        text_Count.text = current_Count.ToString();
    }

    // 1씩 빼기
    public void SubOne()
    {
        current_Count -= increase_count;
        text_Count.text = current_Count.ToString();
    }

    // 이벤트 실행 버튼 
    public void EventJoinClicked()
    {
        if(current_Count == 5)
        {
            ui_Count_Panel.SetActive(false);
            ui_Count_Ten_Panel.SetActive(false);
            ui_Count_Five_Panel.SetActive(true);
        }
        if(current_Count == 10)
        {
            ui_Count_Panel.SetActive(false);
            ui_Count_Five_Panel.SetActive(false);
            ui_Count_Ten_Panel.SetActive(true);
        }
    }

    // 이벤트 나가기 버튼
    public void EventQuitClicked()
    {
        ui_Count_Panel.SetActive(true);
        ui_Count_Five_Panel.SetActive(false);
        ui_Count_Ten_Panel.SetActive(false);
    }

    
} // class
















