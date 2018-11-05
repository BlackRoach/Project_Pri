using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
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
    public InfoPanel infoPanel; // 날짜 요일 정보 패널

    public GameObject NewYearFes; // 신년축제
    public GameObject HarvestFes; // 수학제
    public GameObject CherryBlossomFes; // 벚꽃축제
    public GameObject BirthdayParty; // 생일파티
    public GameObject Ending; // 엔딩

    public GameObject main_Panel;
    public GameObject[] market_Prefabs; // 시장 스케쥴 실행시 프리펩 작동
    public GameObject schedules_Mode_Parent;

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

    private JsonData vacance_CG_Data;

    private void Start()
    {
        Calender_Event_Json_Parsing();
        backGroundPosition = 0;
        CheckFestivalEvent();
    }

    private void Calender_Event_Json_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/CALENDER_EVENT");

        vacance_CG_Data = JsonMapper.ToObject(json_File.text);
    }
    // 다음 버튼을 눌렀을 때 화면을 변화시킨다.
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

    // 스케줄을 추가한다.
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

    // 스케줄을 지운다.
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

    // 달마다 참가 가능한 축제만 선택해서 보여준다.
    public void CheckFestivalEvent()
    {
        int festival = CalendarManager.instance.GetCurrentFestivalEvent();
        //Debug.Log(festival);

        NewYearFes.SetActive(false);
        HarvestFes.SetActive(false);
        CherryBlossomFes.SetActive(false);
        BirthdayParty.SetActive(false);
        Ending.SetActive(false);

        switch (festival)
        {
            case 0:
                NewYearFes.SetActive(true);
                break;
            case 1:
                NewYearFes.SetActive(true);
                break;
            case 2:
                HarvestFes.SetActive(true);
                break;
            case 3:
                CherryBlossomFes.SetActive(true);
                break;
            case 4:
                BirthdayParty.SetActive(true);
                break;
            case 5:
                Ending.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void ClearSchedule()
    {
        activities.Clear();
        DrawSchedule();
    }

    // 일정실행전에 스케줄을 화면에 그려준다.
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

    // 한달 동안의 스케줄을 생성한다.
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
       // Debug.Log(firstStart);
       // Debug.Log(firstEnd);
       // Debug.Log(middleStart);
       // Debug.Log(middleEnd);
       // Debug.Log(lastStart);
       // Debug.Log(lastEnd);
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

    // 스케줄을 실행하고 메인씬으로 넘어간다.
    private void RunSchedule()
    {
        bool notEnd = day < decidedActivities.Count;

        if (notEnd)
        {
            if (decidedActivities[day].title == "무술도장")
            {
                decidedActivities[day].title = "무술학교";
            }

            performingPanel.SetActive(true);

            if(decidedActivities[day].title == "시장")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(0);
                if (day == 0)
                {
                    Market_Event_Panel_Randomaize();
                }
                if(day == 10)
                {
                    Market_Event_Panel_Randomaize();
                } 
                if(day == 20)
                {
                    Market_Event_Panel_Randomaize();
                }
            }
            else if(decidedActivities[day].title == "학교")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(1);
            }
            else if(decidedActivities[day].title == "술집")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(2);
            }
            else if (decidedActivities[day].title == "무술학교")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(3);
            }
            else if (decidedActivities[day].title == "무용학교")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(4);
            }
            else if (decidedActivities[day].title == "요리학교")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(5);
            }
            else if (decidedActivities[day].title == "농장")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(6);
            }
            else if (decidedActivities[day].title == "극장")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(7);
            }
            else if (decidedActivities[day].title == "예절학교")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(8);
            }
            else if (decidedActivities[day].title == "바캉스")
            {
                Schedules_Mode_Setting();
                Schedule_Current_Mode_Setting_Image(9);
                for(int i =0; i< vacance_CG_Data.Count; i++)
                {
                    if(CalendarManager.instance.CurrentYear == (int)vacance_CG_Data[i]["GAME_YEAR"] &&
                        CalendarManager.instance.CurrentMonth == (int)vacance_CG_Data[i]["GAME_MONTH"])
                    {
                        schedules_Mode_Parent.transform.GetChild(9).gameObject.transform.GetChild(0).GetComponent<Image>().
                            sprite = Resources.Load<Sprite>("JHM.Img/VACANCE_CG/"+ vacance_CG_Data[i]["VACANCE_CG"]);

                    }
                }
            }
            else
            {
                schedules_Mode_Parent.transform.GetChild(0).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(1).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(2).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(3).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(4).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(5).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(6).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(7).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(8).gameObject.SetActive(false);
                schedules_Mode_Parent.transform.GetChild(9).gameObject.SetActive(false);
                performingPanel.transform.GetChild(1).gameObject.SetActive(true);
                performingPanel.transform.GetChild(2).gameObject.SetActive(true);

                infoPanel.transform.SetParent(main_Panel.transform);
                infoPanel.transform.GetChild(2).gameObject.SetActive(true);
            }

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
    private void Schedule_Current_Mode_Setting_Image(int input)
    {
        for(int i = 0; i < schedules_Mode_Parent.transform.childCount; i++)
        {
            schedules_Mode_Parent.transform.GetChild(i).gameObject.SetActive(false);
        }
        schedules_Mode_Parent.transform.GetChild(input).gameObject.SetActive(true);
    }


    private void Market_Event_Panel_Randomaize()
    {
        int random = Random.Range(0, market_Prefabs.Length);
        Debug.Log(random);
        switch (random)
        {
            case 0:
                {
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Market_Event_One>().Default_Pos();
                } break;
            case 1:
                {
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Market_Event_Two>().Default_Pos();
                } break;
            case 2:
                {
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                    schedules_Mode_Parent.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Market_Event_Three>().Default_Pos();
                }
                break;
        }
    }
    private void Schedules_Mode_Setting()
    {
        performingPanel.transform.GetChild(1).gameObject.SetActive(false);
        performingPanel.transform.GetChild(2).gameObject.SetActive(false);

        infoPanel.transform.SetParent(performingPanel.transform);
        infoPanel.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void ChangeScene(string sceneMane)
    {
        SceneManager.LoadSceneAsync(sceneMane);
    }
}
