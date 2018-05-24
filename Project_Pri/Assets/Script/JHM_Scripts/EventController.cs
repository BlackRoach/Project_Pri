using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class EventController : MonoBehaviour {


    public static EventController instance;

    public GameObject img_Text_Box;

    public GameObject origin_Panel,count_Five_Panel,count_Ten_Panel,trigger_One_Panel,trigger_Four_Panel,trigger_Five_Panel;


    public Text text_Trigger_One, text_Trigger_Two, text_Trigger_Three, text_Trigger_Four, text_Trigger_Five;

    public Text show_count;

    public int current_Count;
    private int calculate_One;
    private int defualt_Count;

    public bool is_Trigger_One, is_Trigger_Two, is_Trigger_Three, is_Trigger_Four, is_Trigger_Five;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        calculate_One = 1;
        current_Count = 0;
        defualt_Count = 0;
        show_count.text = current_Count.ToString();

        is_Trigger_One = is_Trigger_Two = is_Trigger_Three = is_Trigger_Four = is_Trigger_Five = false;
    }
    private void Update()
    {
        Event_Trigger_Count_Join();
    }

    public void Count_AddOne()
    {
        current_Count += calculate_One;
        show_count.text = current_Count.ToString();
    }

    public void Count_SubOne()
    {
        current_Count -= calculate_One;
        show_count.text = current_Count.ToString();
    }


    public void Event_ClickButton_Pressed()
    {
        if(current_Count == 5)
        {
            origin_Panel.SetActive(false);
            count_Ten_Panel.SetActive(false);
            trigger_One_Panel.SetActive(false);
            trigger_Five_Panel.SetActive(false);
            trigger_Four_Panel.SetActive(false);
            count_Five_Panel.SetActive(true);
        }
        if(current_Count == 10)
        {
            count_Five_Panel.SetActive(false);
            origin_Panel.SetActive(false);
            trigger_Four_Panel.SetActive(false);
            trigger_Five_Panel.SetActive(false);
            trigger_One_Panel.SetActive(false);
            count_Ten_Panel.SetActive(true);

            //  ----------------------------

            img_Text_Box.SetActive(true);
            TextBoxController.instance.Text_Input();
            TextBoxController.instance.Text_Output();
        }
    }
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
        if(is_Trigger_Two && is_Trigger_Three)
        {
            if (!is_Trigger_Four)
            {
                is_Trigger_Four = true;
                text_Trigger_Four.text = calculate_One.ToString();
                StartCoroutine(Auto_Event_System());
            }
        }

        if (is_Trigger_Five)
        {
            text_Trigger_Five.text = calculate_One.ToString();
            StartCoroutine(Auto_Event_System());
        }

    }
    public void Event_ExitButton_Pressed()
    {
        origin_Panel.SetActive(true);
        trigger_One_Panel.SetActive(false);
        trigger_Five_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
        // ------------------------------


        TextBoxController.instance.text_Array.Clear();
        TextBoxController.instance.current_Text = 0;
        TextBoxController.instance.end_Text = 0;
        img_Text_Box.SetActive(false);
    }

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
    }

    private void Trigger_One_Event()
    {
        trigger_One_Panel.SetActive(true);
        trigger_Five_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);
        trigger_Four_Panel.SetActive(false);
    }
    private void Trigger_Four_Event()
    {
        trigger_Four_Panel.SetActive(true);
        trigger_Five_Panel.SetActive(false);
        trigger_One_Panel.SetActive(false);
        origin_Panel.SetActive(false);
        count_Five_Panel.SetActive(false);
        count_Ten_Panel.SetActive(false);

        // ----------------------

        img_Text_Box.SetActive(true);
        TextBoxController.instance.Text_Input();
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

        // -----------------------

        img_Text_Box.SetActive(true);
        TextBoxController.instance.Text_Input();
        TextBoxController.instance.Text_Output();
    }
} // class
















