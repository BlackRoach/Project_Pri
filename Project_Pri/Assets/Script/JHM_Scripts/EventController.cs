using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class EventController : MonoBehaviour {


    public static EventController instance = null;

    public GameObject img_Text_Box;

    public GameObject origin_Panel,count_Five_Panel,count_Ten_Panel,trigger_One_Panel,trigger_Four_Panel,trigger_Five_Panel;
    public GameObject trigger_Six_Panel,trigger_Seven_Panel,trigger_Eight_Panel;
    public GameObject choice_Button_Panel;

    public Text text_Trigger_One, text_Trigger_Two, text_Trigger_Three, text_Trigger_Four, text_Trigger_Five
        ,text_Trigger_Six, text_Trigger_Seven, text_Trigger_Eight;

    public Text show_count;
    public Text face_Show_Count;

    public int current_Count;
    public int face_Current_State_Count;

    private int calculate_One;
    private int defualt_Count;

    public bool is_Trigger_One, is_Trigger_Two, is_Trigger_Three, is_Trigger_Four, is_Trigger_Five,
        is_Trigger_Six,is_Trigger_Seven,is_Trigger_Eight;

    public bool can_Access_01,can_Access_02;

    public Animator trigger_Seven_Anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        calculate_One = 1;
        current_Count = 0;
        defualt_Count = 0;
        face_Current_State_Count = 0;
        show_count.text = current_Count.ToString();
        face_Show_Count.text = face_Current_State_Count.ToString();

        // ---------------------------

        is_Trigger_One = is_Trigger_Two = is_Trigger_Three = is_Trigger_Four = is_Trigger_Five = false;
        is_Trigger_Six = is_Trigger_Seven = is_Trigger_Eight = false;

        can_Access_01 = false;
        can_Access_02 = false;
        // --------------------------
        choice_Button_Panel.SetActive(false);
    }
    private void Update()
    {
        Event_Trigger_Count_Join();
    }

    // 1 증가
    public void Count_AddOne()
    {
        current_Count += calculate_One;
        show_count.text = current_Count.ToString();
    }

    // 1 감소
    public void Count_SubOne()
    {
        current_Count -= calculate_One;
        show_count.text = current_Count.ToString();
    }

    // 조건과 맞을경우 이벤트 버튼 작동
    public void Event_ClickButton_Pressed()
    {
        if(current_Count == 5)
        {
            trigger_Seven_Panel.SetActive(false);
            trigger_Eight_Panel.SetActive(false);
            origin_Panel.SetActive(false);
            count_Ten_Panel.SetActive(false);
            trigger_One_Panel.SetActive(false);
            trigger_Five_Panel.SetActive(false);
            trigger_Four_Panel.SetActive(false);
            trigger_Six_Panel.SetActive(false);
            count_Five_Panel.SetActive(true);
        }
        if(current_Count == 10)
        {
            trigger_Seven_Panel.SetActive(false);
            trigger_Eight_Panel.SetActive(false);
            count_Five_Panel.SetActive(false);
            origin_Panel.SetActive(false);
            trigger_Four_Panel.SetActive(false);
            trigger_Five_Panel.SetActive(false);
            trigger_One_Panel.SetActive(false);
            trigger_Six_Panel.SetActive(false);
            count_Ten_Panel.SetActive(true);

            //  ----------------------------

            img_Text_Box.SetActive(true);
            TextBoxController.instance.end_Text = 5;
            TextBoxController.instance.Text_Output();
        }
    }
    // 이벤트 트리거 매번 프레임 마다 자동 작동 (조건에 맞으면 이벤트 실행)
    private void Event_Trigger_Count_Join()
    {

        if (current_Count == 15)
        {
            if (!is_Trigger_One)
            {
                is_Trigger_One = true;
                text_Trigger_One.text = calculate_One.ToString();
                StartCoroutine(Auto_Event_System());
            }
        }
        if(current_Count == 16)
        {
            if (!is_Trigger_Two)
            {
                is_Trigger_Two = true;
                text_Trigger_Two.text = calculate_One.ToString();
            }
        }
        if(current_Count == 17)
        {
            if (!is_Trigger_Three)
            {
                is_Trigger_Three = true;
                text_Trigger_Three.text = calculate_One.ToString();
            }
        }
        if (is_Trigger_Two && is_Trigger_Three)
        {
            if (!is_Trigger_Four)
            {
                is_Trigger_Four = true;
                text_Trigger_Four.text = calculate_One.ToString();
                StartCoroutine(Auto_Event_System());
            }
        }
        if (is_Trigger_Four && can_Access_01)
        {
            if (!is_Trigger_Five)
            {
                is_Trigger_Five = true;
                text_Trigger_Five.text = calculate_One.ToString();
                StartCoroutine(Auto_Event_System());
            }
        }      
        if(current_Count == 20)
        {
            if (!is_Trigger_Six && !can_Access_02)
            {
                is_Trigger_Six = true;
                text_Trigger_Six.text = calculate_One.ToString();
                StartCoroutine(Auto_Event_System());
            }
        }
    }

    // 나가기 버튼 
    public void Event_ExitButton_Pressed()
    {
        origin_Panel.SetActive(true);
        trigger_One_Panel.SetActive(false);
        trigger_Five_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        trigger_Six_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
        // ------------------------------

        TextBoxController.instance.current_Text = 0;
        TextBoxController.instance.end_Text = 0;
        img_Text_Box.SetActive(false);       
    }
    // 1초후 이벤트 진입 
    IEnumerator Auto_Event_System()
    {

        yield return new WaitForSeconds(calculate_One);

        if(current_Count == 15)
        {
            Trigger_One_Event();
        }
        if (is_Trigger_Four)
        {
            Trigger_Four_Event();
        }
        if (is_Trigger_Five)
        {
            Trigger_Five_Event();
        }
        if (is_Trigger_Six)
        {
            Trigger_Six_Event();
        }
    }
    // 자동 이벤트 메소드
    private void Trigger_One_Event()
    {
        trigger_One_Panel.SetActive(true);
        trigger_Five_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        trigger_Six_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
    }
    private void Trigger_Four_Event()
    {
        trigger_Four_Panel.SetActive(true);
        trigger_Five_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Six_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
        // ----------------------

        img_Text_Box.SetActive(true);
        TextBoxController.instance.end_Text = 3;
        TextBoxController.instance.Text_Output();
    }
    private void Trigger_Five_Event()
    {
        trigger_Five_Panel.SetActive(true);
        trigger_Four_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Six_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
        // -----------------------

        img_Text_Box.SetActive(true);
        TextBoxController.instance.end_Text = 3;
        TextBoxController.instance.Text_Output();
    }
    private void Trigger_Six_Event()
    {
        trigger_Six_Panel.SetActive(true);
        trigger_Five_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
        // ----------------  
        img_Text_Box.SetActive(true);
        TextBoxController.instance.end_Text = 2;
        TextBoxController.instance.Text_Output();
    }
    private void Trigger_Seven_Event()
    {
        trigger_Seven_Panel.SetActive(true);
        trigger_Six_Panel.SetActive(false);
        trigger_Five_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Eight_Panel.SetActive(false);
        // -----------------
        img_Text_Box.SetActive(true);
        TextBoxController.instance.end_Text = 2;
        TextBoxController.instance.Text_Output();
    }
    private void Trigger_Eight_Event()
    {
        trigger_Eight_Panel.SetActive(true);
        trigger_Six_Panel.SetActive(false);
        trigger_Five_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Seven_Panel.SetActive(false);
        // -----------------
        img_Text_Box.SetActive(true);
        TextBoxController.instance.end_Text = 1;
        TextBoxController.instance.Text_Output();
    }

    // 선택 버튼에 따라 트리거_7 이나 트리거_8로 이동하는 함수
    public void Choice_Button_Trigger_7_Pressed()
    {
        if (!is_Trigger_Seven)
        {
            is_Trigger_Seven = true;
            // -------------------------
            choice_Button_Panel.SetActive(false);
            // --------------------------
            text_Trigger_Seven.text = calculate_One.ToString();
            // --------------------------
            Trigger_Seven_Event();
        }
    }
    public void Choice_Button_Trigger_8_Pressed()
    {
        if (!is_Trigger_Eight)
        {
            is_Trigger_Eight = true;
            // --------------------------
            choice_Button_Panel.SetActive(false);
            // --------------------------
            text_Trigger_Eight.text = calculate_One.ToString();
            // --------------------------
            Trigger_Eight_Event();
        }
    }
    // ---------------------------------------------------

    // 초기화
    public void All_Default_Value()
    {
        text_Trigger_One.text = defualt_Count.ToString();
        text_Trigger_Two.text = defualt_Count.ToString();
        text_Trigger_Three.text = defualt_Count.ToString();
        text_Trigger_Four.text = defualt_Count.ToString();
        text_Trigger_Five.text = defualt_Count.ToString();
        text_Trigger_Six.text = defualt_Count.ToString();
        text_Trigger_Seven.text = defualt_Count.ToString();
        text_Trigger_Eight.text = defualt_Count.ToString();

        // -----------------

        current_Count = defualt_Count;
        face_Current_State_Count = defualt_Count;
        show_count.text = current_Count.ToString();
        face_Show_Count.text = face_Current_State_Count.ToString();

        // -----------------

        is_Trigger_One = is_Trigger_Two = is_Trigger_Three = is_Trigger_Four = is_Trigger_Five = false;
        is_Trigger_Six = is_Trigger_Seven = is_Trigger_Eight = false;
        can_Access_01 = can_Access_02 = false;

        // -----------------

        choice_Button_Panel.SetActive(false);

    }
} // class
















