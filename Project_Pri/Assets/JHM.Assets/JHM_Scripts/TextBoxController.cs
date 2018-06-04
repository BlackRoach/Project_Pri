using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxController : MonoBehaviour {

    public static TextBoxController instance = null;

    private DialogueText dialogue_Text;

    public GameObject img_Tail_left, img_Tail_right;

    public Text text_Box; 

    public int current_Text,end_Text;

    private bool is_Texting;

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

        dialogue_Text = transform.Find("Dialogue_Text").GetComponent<DialogueText>();
    }

    private void Start()
    {        

        current_Text = 0;
        end_Text = 0;
        is_Texting = false;
    }

    // 텍스트를 게임상에 출력하기
    public void Text_Output()
    {
        if (!is_Texting)
        {
            if (current_Text <= end_Text)
            {
                if (EventController.instance.current_Count == 10 || EventController.instance.is_Trigger_Four
                    || EventController.instance.is_Trigger_Five || EventController.instance.is_Trigger_Six
                    || EventController.instance.is_Trigger_Seven || EventController.instance.is_Trigger_Eight)
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
        if (EventController.instance.current_Count == 10) {
            foreach (char text in dialogue_Text.textFiles_01[current_Text].ToCharArray())
            {
                is_Texting = true;

                text_Box.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        if (EventController.instance.current_Count == 17)
        {
            if (EventController.instance.is_Trigger_Four && !EventController.instance.can_Access_01)
            {
                foreach (char text in dialogue_Text.textFiles_02[current_Text].ToCharArray())
                {
                    is_Texting = true;

                    text_Box.text += text;

                    yield return new WaitForSeconds(0.1f);
                }
            }
            if (EventController.instance.is_Trigger_Five && EventController.instance.can_Access_01)
            {
                foreach (char text in dialogue_Text.textFiles_03[current_Text].ToCharArray())
                {
                    is_Texting = true;

                    text_Box.text += text;

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        if (EventController.instance.current_Count == 20 && EventController.instance.is_Trigger_Six
            && !EventController.instance.can_Access_02)
        {
            foreach (char text in dialogue_Text.textFiles_04[current_Text].ToCharArray())
            {
                is_Texting = true;

                text_Box.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        if (EventController.instance.is_Trigger_Seven && EventController.instance.can_Access_02)
        {
            if(current_Text == 1)
            {
                EventController.instance.trigger_Seven_Anim.SetBool("isState", true);
            }
            foreach (char text in dialogue_Text.textFiles_05[current_Text].ToCharArray())
            {
                is_Texting = true;

                text_Box.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        if (EventController.instance.is_Trigger_Eight && EventController.instance.can_Access_02)
        {
            foreach (char text in dialogue_Text.textFiles_06[current_Text].ToCharArray())
            {
                is_Texting = true;

                text_Box.text += text;

                yield return new WaitForSeconds(0.1f);
            }
        }
        is_Texting = false;
    }
    // 텍스트가 끝났는지 감지해주는 메소드
    private void CheckForText_Array()
    {
        if(current_Text > end_Text)
        {
            EventController.instance.img_Text_Box.SetActive(false);
            
            current_Text = 0;
            end_Text = 0;

            if (EventController.instance.current_Count != 10)
            {
                if (EventController.instance.current_Count == 17)
                {
                    if (EventController.instance.is_Trigger_Four)
                    {
                        EventController.instance.Event_ExitButton_Pressed();
                        EventController.instance.can_Access_01 = true;
                    }
                    if (EventController.instance.is_Trigger_Five)
                    {
                        EventController.instance.Event_ExitButton_Pressed();
                    }
                }
                if (EventController.instance.is_Trigger_Six && !EventController.instance.can_Access_02)
                {
                    EventController.instance.choice_Button_Panel.SetActive(true);
                    EventController.instance.can_Access_02 = true;
                }
                if (EventController.instance.is_Trigger_Seven)
                {
                    EventController.instance.Event_ExitButton_Pressed();
                }
                if (EventController.instance.is_Trigger_Eight)
                {
                    EventController.instance.Event_ExitButton_Pressed();
                }
            }
        }
    }

} // class










