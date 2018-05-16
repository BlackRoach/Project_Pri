using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxController : MonoBehaviour {

    public static TextBoxController instance;

    public GameObject dialogueBox;

    public Text text_Line;

    public DialogueText textDialogues;


    public List<string> textArray = new List<string>();

    public int currentText;
    [SerializeField]
    private int endText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }



    private void Start()
    {
        // text 먼저 가져오기
        foreach(string text in textDialogues.textFiles_01){
            textArray.Add(text);
        }

        currentText = 0;
        endText = textArray.Count - 1;

        dialogueBox.SetActive(true);
    }

    // text 작동 메소드
    public void DialogueText()
    {
        if(currentText > endText)
        {
            StopCoroutine(AutoTyping());
            dialogueBox.SetActive(false);
        }
        if (currentText <= endText)
        {
            text_Line.text = " ";

            StartCoroutine(AutoTyping());

            currentText++;
        }
    }
    // text 줄줄이 나오도록 하는 메소드
    IEnumerator AutoTyping()
    {
        foreach (char letter in textArray[currentText].ToCharArray())
        {
            text_Line.text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }




} // class










