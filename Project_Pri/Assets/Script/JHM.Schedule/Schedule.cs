using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Schedule : MonoBehaviour {

    private List<string> schedules; // 예약된 정보
    private List<string> decidedSchedules; // 결정된 스케줄
    public GameObject[] selectedSchedules; // UI에서 상순, 중순, 하순
    public Text ResultText; // 실행 결과 상자

    public Sprite arbeit1; // 알바1 그림
    public Sprite arbeit2; // 알바2 그림
    public Sprite arbeit3; // 알바3 그림
    public Sprite DoArbeit1; // 알바1을 하는 그림
    public Sprite DoArbeit2; // 알바2를 하는 그림
    public Sprite DoArbeit3; // 알바3을 하는 그림
    public Sprite first10Days; // 상순 그림
    public Sprite middle10Days; // 중순 그림
    public Sprite last10Days; // 하순 그림

    public Calendar calendar; // Calendar 클래스에 접근하기 위한 용도
    public Player player; // Player 클래스에 접근하기 위한 용도

    public GameObject PerformingPanel; // 스케줄 실행 패널
    public Image DoSchedule; // 스케줄 하는 이미지UI
    public Text HpText; // SchedulePanel 안
    public Text HpText2; // PerformingPanel 안
    public Text DetailsText;
    private int day = 0;
    private System.Random rnd;

    // Use this for initialization
    void Start () {
        schedules = new List<string>();
        decidedSchedules = new List<string>();
        HpText.text = player.HP.ToString();
        HpText2.text = player.HP.ToString();
        rnd = new System.Random();

        // 테스팅
        //selectedSchedules[0].GetComponent<Image>().sprite =
        //    Resources.Load<Sprite>("Sprite/CGGallery/" + "VACANCE_CG_1");
    }

    public void AddSchedule(int i) // 스케줄 추가
    {
        if(schedules.Count >= 3)
        {
            Debug.Log("스케줄이 가득 찾습니다.");
            return;
        }
        switch(i)
        {
            case 1:
                schedules.Add("arbeit1");
                break;
            case 2:
                schedules.Add("arbeit2");
                break;
            case 3:
                schedules.Add("arbeit3");
                break;
        }
        DrawSchedule();
    }

    public void DeleteSchedule() // 스케줄 제거 (클릭하면 맨 마지막 스케줄을 지운다.)
    {
        if(schedules.Count <= 0)
        {
            Debug.Log("스케줄이 비었습니다.");
        }
        else
        {
            int lastIndex = schedules.Count - 1;
            schedules.RemoveAt(lastIndex);
        }
        DrawSchedule();
    }

    public void DeleteSchedule(int i) // 스케줄 제거 (클릭하면 클릭한 스케줄을 지운다.)
    {
        // 두개의 스케줄이 예약되어 있을 때 첫번째를 클릭하면 두번쨰가 첫번쨰로 와야 한다. 
        if(schedules.Count <= 0)
        {
            Debug.Log("스케줄이 비었습니다.");
        }
        else if(schedules.Count == 1)
        {
            if(i == 1) // 상순 클릭
            {
                schedules.RemoveAt(0);
            }
        }
        else if(schedules.Count == 2)
        {
            if (i == 1) // 상순 클릭
            {
                schedules.RemoveAt(0);
            }
            else if (i == 2) // 중순 클릭
            {
                schedules.RemoveAt(1);
            }
        }
        else if(schedules.Count == 3)
        {
            if (i == 1) // 상순 클릭
            {
                schedules.RemoveAt(0);
            }
            else if (i == 2) // 중순 클릭
            {
                schedules.RemoveAt(1);
            }
            else if(i == 3) // 하순 클릭
            {
                schedules.RemoveAt(2);
            }
        }

        DrawSchedule();

    }

    private void DrawSchedule() // 스케줄 예약상황을 화면에 그려줍니다.
    {
        // 예약된 스케줄 이미지를 그려줍니다. 
        for (int i = 0; i < schedules.Count; i++)
        {
            string tmp = schedules[i];
            switch (tmp)
            {
                case "arbeit1":
                    selectedSchedules[i].GetComponent<Image>().sprite = arbeit1;
                    break;
                case "arbeit2":
                    selectedSchedules[i].GetComponent<Image>().sprite = arbeit2;
                    break;
                case "arbeit3":
                    selectedSchedules[i].GetComponent<Image>().sprite = arbeit3;
                    break;       
            }   
        }


        // 예약되지 않은 스케줄 이미지를 그려줍니다.
        if(schedules.Count == 0)
        {
            selectedSchedules[0].GetComponent<Image>().sprite = first10Days;
            selectedSchedules[1].GetComponent<Image>().sprite = middle10Days;
            selectedSchedules[2].GetComponent<Image>().sprite = last10Days;
        }
        else if(schedules.Count == 1)
        {
            selectedSchedules[1].GetComponent<Image>().sprite = middle10Days;
            selectedSchedules[2].GetComponent<Image>().sprite = last10Days;
        }
        else if(schedules.Count == 2)
        {
            selectedSchedules[2].GetComponent<Image>().sprite = last10Days;
        }
        calendar.PreviewSchedule(schedules);
    }

    public void ResetShedule() // 스케줄을 초기상태로 돌린다.
    {
        schedules.Clear();

        selectedSchedules[0].GetComponent<Image>().sprite = first10Days;
        selectedSchedules[1].GetComponent<Image>().sprite = middle10Days;
        selectedSchedules[2].GetComponent<Image>().sprite = last10Days;

        ResultText.text = string.Empty;
    }

    public void ShowResult()
    {
        if(schedules.Count < 3)
        {
            Debug.Log("스케줄을 전부 지정해주세요.");
            return;
        }
        ResultText.text = "";

        string[] result = new string[3];
        for(int i = 0; i < 3; i++)
        {
            if (schedules[i] == "arbeit1")
            {
                result[i] = "알바1";
            }
            else if (schedules[i] == "arbeit2")
            {
                result[i] = "알바2";
            }
            else if (schedules[i] == "arbeit3")
            {
                result[i] = "알바3";
            }
        }

        ResultText.text = "상순 : " + result[0] + "\n";
        ResultText.text += "중순 : " + result[1] + "\n";
        ResultText.text += "하순 : " + result[2] + "\n";
    }

    // RunButton Onclick 이벤트에서 접근함
    public void RunSchedules() // 스케줄 실행
    {
        // 스케줄칸에 스케줄이 꽉차있어야 한다.
        if (schedules.Count < 3)
        {
            Debug.Log("스케줄을 먼저 채워주세요.");
            return;
        }

        // 상순, 중순, 하순에 할당된 스케줄이 무엇인가?
        // 상순, 중순은 10일간이고 하순은 날짜가 달마다 다르다.
        for (int i = 0; i < 10; i++) // 상순 스케줄 저장
            decidedSchedules.Add(schedules[0]);

        for (int i = 0; i < 10; i++) // 중순 스케줄 저장
            decidedSchedules.Add(schedules[1]);

        int lastdays = calendar.LastEnd - calendar.LastStart + 1; // 첫날도 포함 (+1)
        for (int i = 0; i < lastdays; i++) // 하순 스케줄 저장
            decidedSchedules.Add(schedules[2]);
        //Debug.Log(calendar.LastEnd);
        //Debug.Log(calendar.LastStart);
        //for (int i = 0; i < decidedSchedules.Count; i++)
        //    Debug.Log(decidedSchedules[i]);
        //Debug.Log(decidedSchedules.Count); // 31

        InvokeRepeating("executeSchedules", 0.1f, 1.0f);

    }

    private void executeSchedules() // 스케줄을 실행한다
    {
        //Debug.Log("day " + day);
        if (day >= decidedSchedules.Count)
        {
            Debug.Log("스케줄이 종료되었습니다.");
            PerformingPanel.SetActive(false);
            CancelInvoke("executeSchedules");
            day = 0;
            decidedSchedules.Clear();
            // 스케줄이 다 실행되면 다음달로 넘어간다.
            calendar.IncreaseMonth();
            calendar.ShowCalendar();
            ResetShedule();
            HpText.text = player.HP.ToString();
        }
        else
        {
            switch (decidedSchedules[day])
            {
                case "arbeit1":
                    GoArbeit("arbeit1");
                    break;
                case "arbeit2":
                    GoArbeit("arbeit2");
                    break;
                case "arbeit3":
                    GoVacance();
                    break;
            }
            day++;
        }   
    }

    private void GoArbeit(string arbeitName)
    {
        string tmp = String.Empty;

        if(arbeitName == "arbeit1")
        {
            tmp = "알바1";
            DoSchedule.sprite = DoArbeit1;
        }
        else if(arbeitName == "arbeit2")
        {
            DoSchedule.sprite = DoArbeit2;
            tmp = "알바2";
        }

        // 원래 창 사이즈
        DoSchedule.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(-140, 52);
        DoSchedule.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 800f);
        DoSchedule.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical, 222f);

        int HpIncrement = rnd.Next(1, 4); // 체력 증가량 (랜덤 1~3)
        PerformingPanel.SetActive(true);
        DetailsText.text = calendar.CurrentMonth + "월 " + (day + 1) + "일, ";
        DetailsText.text += tmp + "을[를] 하였는데..\n";
        DetailsText.text += "체력이 " + HpIncrement + " 증가 하였습니다.";
        player.HP += HpIncrement;
        HpText2.text = player.HP.ToString();

    }

    private void GoVacance()
    {
        
        // 현재 몇년 몇월인가
        // calendarEventList에서 현재 년월에 맞는 CG이미지를 불러온다.
        for (int i=0; i<calendar.EventList.Count; i++) // 85
        {
            if(calendar.EventList[i].GAME_YEAR == calendar.CurrentYear
                && calendar.EventList[i].GAME_MONTH == calendar.CurrentMonth)
            {

                //Debug.Log(calendar.EventList[i].GAME_MONTH);
                DoSchedule.sprite = calendar.EventList[i].VACANCE_CG;
                CheckCG(DoSchedule.sprite.name);
                break; // 이미지를 찾으면 더 검색할 필요가 없으니 나온다.
            }
            //Debug.Log(calendar.CurrentYear + ", " + calendar.CurrentMonth);
        }

        // 풀스크린 모드
        DoSchedule.GetComponent<RectTransform>().anchoredPosition
            = new Vector2(0, 0);
        DoSchedule.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 1280f);
        DoSchedule.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical, 720f);

        int HpIncrement = rnd.Next(1, 4); // 체력 증가량 (랜덤 1~3)
        PerformingPanel.SetActive(true);
        DetailsText.text = calendar.CurrentMonth + "월 " + (day + 1) + "일, ";
        DetailsText.text += "바캉스를 갔다...";
        player.HP += HpIncrement;
        HpText2.text = player.HP.ToString();

    }

    private void CheckCG(string cgName)
    {
        //CGManager cgm = FindObjectOfType<CGManager>();
        //for (int i = 0; i < cgm.CgInfoList.Count; i++)
        //{
        //    if (cgm.CgInfoList[i].CG_NAME == cgName)
        //    {
        //        cgm.CgInfoList[i].IS_UNLOCK = true;
        //    }
        //}
        for (int i = 0; i < CGManager.CgInfoList.Count; i++)
        {
            if (CGManager.CgInfoList[i].CG_NAME == cgName)
            {
                CGManager.CgInfoList[i].IS_UNLOCK = true;
            }
        }
        Debug.Log(cgName);
        //Debug.Log(cgm.CgInfoList[0].IS_UNLOCK);
        //Debug.Log(cgm.CgInfoList[1].IS_UNLOCK);
        //Debug.Log(cgm.CgInfoList[2].IS_UNLOCK);
        //Debug.Log(cgm.CgInfoList[3].IS_UNLOCK);
        //Debug.Log(cgm.CgInfoList[4].IS_UNLOCK);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title 1");
    }
}

// <이벤트 CG처리>
// 1. calendarEvent.Json 파일을 불러온다.
//   - JsonCalendarEventLoad() - Calendar.cs
//   - calendarEvent.Json의 항목들을 List<CalendarEvent> eventList에 넣어준다.
//   - 이미지를 날짜별로 제대로 들어가게 한다.
//
// 2. JSON 정보를 검색해서 어떤 이미지를 넣어 줄지를 결정한다.
//   - GoVacance() - Schedules.cs
//   - 바캉스일 때만 이미지가 전체화면이 되게 한다.
//   - 다른 알바를 할때는 이미지가 원래 사이즈로 돌아온다.
//   - 알바 혹은 바캉스에 따라 오른쪽 글씨를 다르게 출력되도록 한다.
// 
//  3. 한번본 CG는 CGGallery 씬에서 다시 볼 수 있게 만든다.
//   - CGManager 클래스를 이용해 해당 CG를 봤는지 안봤는지 관리한다.

