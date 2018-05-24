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

            EventController.instance.is_Trigger_Five = false;
        }
    }


    public void Text_Output()
    {
        if (!is_Texting)
        {
            if (current_Text <= end_Text)
            {
                if (EventController.instance.current_Count == 10 || EventController.instance.is_Trigger_Four
                    || !EventController.instance.is_Trigger_Five)
                {
                    StartCoroutine(AutoTyping());
                    current_Text++;                
                }

                text_Box.text = " ";
            }
        }
        CheckForText_Array();
    }

    IEnumerator AutoTyping()
    {

        if (!EventController.instance.is_Trigger_Five)
        {
            if (current_Text % 2 == 0)
            {
                img_Tail_left.SetActive(true);
                img_Tail_right.SetActive(false);
            }
            else
            {
                img_Tail_left.SetActive(false);
                img_Tail_right.SetActive(true);
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
    
    private void CheckForText_Array()
    {
        if(current_Text > end_Text)
        {
            EventController.instance.img_Text_Box.SetActive(false);
            text_Array.Clear();
            current_Text = 0;
            end_Text = 0;

            if (EventController.instance.is_Trigger_Four)
            {
                EventController.instance.Event_ExitButton_Pressed();
                EventController.instance.is_Trigger_Five = EventController.instance.is_Trigger_Four;
                EventController.instance.is_Trigger_Four = false;
                EventController.instance.is_Trigger_Two = false;
                EventController.instance.is_Trigger_Three = false;
            }
            if (!EventController.instance.is_Trigger_Five)
            {
                EventController.instance.Event_ExitButton_Pressed();
            }
        }
    }

} // class










