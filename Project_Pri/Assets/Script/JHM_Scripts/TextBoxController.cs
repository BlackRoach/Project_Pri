using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxController : MonoBehaviour {

    public static TextBoxController instance;

    public GameObject img_Tail_left, img_Tail_right;

    public DialogueText dialogueText;

    public Text text_Box;

    public List<string> text_Array = new List<string>();

    public int current_Text,end_Text;

    private bool is_Texting;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        current_Text = 0;
        end_Text = 0;
        is_Texting = false;
    }
    // 텍스트를 리스트에 담기
    public void Text_Input()
    {
        if(EventController.instance.current_Count == 10)
        {
            foreach(string text in dialogueText.textFiles_01)
            {
                text_Array.Add(text);
            }
            end_Text = 5;
        }

        if (EventController.instance.is_Trigger_Four)
        {
            foreach (string text in dialogueText.textFiles_02)
            {
                text_Array.Add(text);
            }
            end_Text = 3;
        }
        if (EventController.instance.is_Trigger_Five)
        {
            foreach (string text in dialogueText.textFiles_03)
            {
                text_Array.Add(text);
            }
            end_Text = 3;           
        }
        if (EventController.instance.is_Trigger_Six)
        {
            foreach (string text in dialogueText.textFiles_04)
            {
                text_Array.Add(text);
            }
            end_Text = 2;
        }
    }

    // 텍스트를 게임상에 출력하기
    public void Text_Output()
    {
        if (!is_Texting)
        {
            if (current_Text <= end_Text)
            {
                if (EventController.instance.current_Count == 10 || EventController.instance.is_Trigger_Four
                    || EventController.instance.is_Trigger_Five || EventController.instance.is_Trigger_Six)
                {
                    StartCoroutine(AutoTyping());
                    current_Text++;                
                }

                text_Box.text = " ";
            }
        }
        CheckForText_Array();
    }
    // 오토 타이핑 메소드
    IEnumerator AutoTyping()
    {

        if (EventController.instance.is_Trigger_Five)
        {
            if (current_Text % 2 == 0)
            {
                img_Tail_left.SetActive(false);
                img_Tail_right.SetActive(true);
            }
            else
            {
                img_Tail_left.SetActive(true);
                img_Tail_right.SetActive(false);
            }

        }
        foreach (char text in text_Array[current_Text].ToCharArray())
        {
            is_Texting = true;

            text_Box.text += text;

            yield return new WaitForSeconds(0.1f);
        }
        is_Texting = false;
    }
    // 텍스트가 끝났는지 감지해주는 메소드
    private void CheckForText_Array()
    {
        if(current_Text > end_Text)
        {
            EventController.instance.img_Text_Box.SetActive(false);

            text_Array.Clear();

            current_Text = 0;
            end_Text = 0;

            if (EventController.instance.current_Count != 10)
            {
                if (EventController.instance.is_Trigger_Four)
                {
                    EventController.instance.Event_ExitButton_Pressed();
                    EventController.instance.can_Access = true;
                }
                if (EventController.instance.is_Trigger_Five)
                {
                    EventController.instance.Event_ExitButton_Pressed();
                }
                if (EventController.instance.is_Trigger_Six)
                {
                    EventController.instance.Event_ExitButton_Pressed();
                }
            }
        }
    }

} // class










