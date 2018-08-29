using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScheduleManager : MonoBehaviour {

    private int backGroundPosition;
    public GameObject background; // 배경 이미지
    public GameObject studyPanel; // 공부 패널
    public GameObject arbeitPanel; // 알바 패널
    public GameObject restPanel; // 휴식 패널
    public GameObject nextButton; // 다음 버튼
    public GameObject performingPanel; // 실행 패널
    public Text DetailsText; // 실행 패널의 글자정보
    public InfoPanel infoPanel;

    public List<GameObject> schedules; // 일정표안에 표시되는 활동버튼들
    public List<Activity> activities = new List<Activity>(); // 일정표안에 들어갈 활동정보들
    public List<Activity> decidedActivities = new List<Activity>(); // 최종적으로 선택된 활동

    private int firstStart; // 상순 시작일
    private int firstEnd; // 상순 종료일
    private int middleStart; // 중순 시작일
    private int middleEnd; // 중순 종료일
    private int lastStart; // 하순 시작일
    private int lastEnd; // 하순 종료일
    private int day = 0; // 스케줄 진행 날짜

    private void Start()
    {
        backGroundPosition = 0;
    }

    public void NextButton()
    {
        if (backGroundPosition < 2)
        {
            backGroundPosition++;
        }
        else
        {
            backGroundPosition = 0;
        }

        if (backGroundPosition == 1)
        {
            background.GetComponent<Animation>().Play("MoveBackgroundToRight");
            studyPanel.SetActive(false);
            nextButton.GetComponent<Button>().enabled = false;
            Invoke("ActiveArbeitPanel", 1.7f);
            Invoke("ActiveNextButton", 1.8f);

        }
        if (backGroundPosition == 2)
        {
            arbeitPanel.SetActive(false);
            nextButton.GetComponent<Button>().enabled = false;
            Invoke("ActiveRestPanel", 0.5f);
            Invoke("ActiveNextButton", 0.6f);
        }
        if (backGroundPosition == 0)
        {
            background.GetComponent<Animation>().Play("MoveBackgroundToLeft");
            restPanel.SetActive(false);
            nextButton.GetComponent<Button>().enabled = false;
            Invoke("ActiveStudyPanel", 1.7f);
            Invoke("ActiveNextButton", 1.8f);
        }
        //Debug.Log(backGroundPosition);

    }

    private void ActiveArbeitPanel()
    {
        arbeitPanel.SetActive(true);
    }

    private void ActiveStudyPanel()
    {
        studyPanel.SetActive(true);
    }

    private void ActiveRestPanel()
    {
        restPanel.SetActive(true);
    }

    private void ActiveNextButton()
    {
        nextButton.GetComponent<Button>().enabled = true;
    }

    public void AddSchedule(GameObject activity)
    {
        Activity tmp = activity.GetComponent<Activity>();

        bool result = CheckSpecialCard(tmp);

        if (result)
            return;

        if (activities.Count >= 3)
        {
            Debug.Log("스케줄이 가득 찾습니다.");
            return;
        }
        activities.Add(tmp);
        DrawSchedule();
    }

    // 동일한 이름의 특수카드가 일정안에 있는지를 검사한다.
    private bool CheckSpecialCard(Activity card)
    {
        // 특수카드이고 동일한 이름이 일정안에 있으면 true
        //Debug.Log(card.title);
        if (card.special == false)
            return false;

        for (int i = 0; i < activities.Count; i++)
        {
            if (activities[i].title == card.title)
            {
                Debug.Log("선택한 특수카드가 이미 일정에 존재합니다.");
                return true;
            }

        }

        return false;
    }

    public void DeleteSchedule()
    {
        if (activities.Count <= 0)
        {
            Debug.Log("스케줄이 비었습니다.");
        }
        else
        {
            int lastIndex = activities.Count - 1;
            activities.RemoveAt(lastIndex);
        }
        DrawSchedule();
    }

    private void ClearSchedule()
    {
        activities.Clear();
        DrawSchedule();
    }

    private void DrawSchedule()
    {
        for(int i = 0; i < activities.Count; i++)
        {
            schedules[i].SetActive(true);
            schedules[i].GetComponent<Activity>().ChangeValues(activities[i]);
        }

        if(activities.Count == 0)
        {
            for(int i = 0; i < schedules.Count; i++)
            {
                schedules[i].SetActive(false);
            }
        }

        if(activities.Count == 1)
        {
            schedules[1].SetActive(false);
            schedules[2].SetActive(false);
        }

        if(activities.Count == 2)
        {
            schedules[2].SetActive(false);
        }
    }

    // 일정실행 버튼 클릭
    public void RunButtonClick()
    {
        if(activities.Count < 3)
        {
            Debug.Log("일정을 전부 채워야 합니다.");
            return;
        }

        MakeSchedule();
        InvokeRepeating("RunSchedule", 0.1f, 1.0f);
    }

    private void MakeSchedule()
    {
        // 현재 년도 월에 해당하는 Year정보를 받아온다.
        Year currentYear = CalendarManager.instance.GetCurrentYear();

        // 상순, 중순, 하순에 해당하는 날짜를 알아낸다.
        // IndexOf(T) : 제일 처음 만나는 T의 인덱스 값을 반환한다.
        firstStart = currentYear.DAYSLIST.IndexOf(currentYear.DAY1_MIN); // 상순 첫날 인덱스
        firstEnd = currentYear.DAYSLIST.IndexOf(currentYear.DAY1_MAX); // 상순 마지막날 인덱스
        middleStart = currentYear.DAYSLIST.IndexOf(currentYear.DAY2_MIN); // 중순 첫날 인덱스
        middleEnd = currentYear.DAYSLIST.IndexOf(currentYear.DAY2_MAX); // 중순 마지막날 인덱스
        lastStart = currentYear.DAYSLIST.IndexOf(currentYear.DAY3_MIN); // 하순 첫날 인덱스
        lastEnd = currentYear.DAYSLIST.IndexOf(currentYear.DAY3_MAX); // 하순 마지막날 인덱스
        //Debug.Log(firstStart);
        //Debug.Log(firstEnd);
        //Debug.Log(middleStart);
        //Debug.Log(middleEnd);
        //Debug.Log(lastStart);
        //Debug.Log(lastEnd);

        // 상순, 중순, 하순 기간을 알아낸다.
        int firstDays = firstEnd - firstStart + 1;
        int middleDays = middleEnd - middleStart + 1;
        int lastDays = lastEnd - lastStart + 1;

        //Debug.Log(firstDays);
        //Debug.Log(middleDays);
        //Debug.Log(lastDays);

        for (int i = 0; i < firstDays; i++) // 상순 스케줄 저장
            decidedActivities.Add(activities[0]);
        for (int i = 0; i < middleDays; i++) // 중순 스케줄 저장
            decidedActivities.Add(activities[1]);
        for (int i = 0; i < lastDays; i++) // 하순 스케줄 저장
            decidedActivities.Add(activities[2]);
    }

    private void RunSchedule()
    {
        bool notEnd = day < decidedActivities.Count;

        if (notEnd)
        {
            performingPanel.SetActive(true);

            DetailsText.text = CalendarManager.instance.CurrentMonth + "월"
                + (day + 1) + "일, ";
            DetailsText.text += decidedActivities[day].title + " 활동을 하였는데..";
            CalendarManager.instance.CurrentDate = day + 1;
            CalendarManager.instance.CurrentDay = CalendarManager.instance.GetCurrentDay();
            infoPanel.ShowDateInfo();
            day++;
        }
        else
        {
            performingPanel.SetActive(false);
            day = 0;
            decidedActivities.Clear();
            ClearSchedule();
            CancelInvoke("RunSchedule");
            CalendarManager.instance.CurrentDate = 1;
            int currentMonth = CalendarManager.instance.CurrentMonth;
            if(currentMonth >= 12)
            {
                CalendarManager.instance.CurrentMonth = 1;
                CalendarManager.instance.CurrentYear++;
            }
            else
            {
                CalendarManager.instance.CurrentMonth++;
            }
            CalendarManager.instance.CurrentDay = CalendarManager.instance.GetCurrentDay();
            CalendarManager.instance.CurrentStrMonth = CalendarManager.instance.GetCurrentStrMonth();
            ChangeScene("Main");
        }
        
    }
    public void ChangeScene(string sceneMane)
    {
        SceneManager.LoadSceneAsync(sceneMane);
    }

    // 활동(Activity)정보를 받아온다. 
    // 리스트에 넣는다. 
    // 일정표에 그려준다. 
    // - 리스트에 들어있는 것까지만 그려준다. (활성화시킨다.)
    // - 나머지는 안보이게 한다. (비활성화시킨다.)
    // - 그려준 슬롯을 다시 클릭하면 제일 뒤에 슬롯부터 차례대로 비활성화 시킨다.
    // 특수카드는 일정에 한번만 등록가능하다. 
    // 다음 버튼을 누르면 화면 애니메이션이 움직인다. 
    // 일정버튼을 누르면 일정을 실행한다.
}
