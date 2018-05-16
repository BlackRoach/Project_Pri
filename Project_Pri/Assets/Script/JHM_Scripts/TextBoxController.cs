using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxController : MonoBehaviour {

    public GameObject dialogueBox;

    public Text text_Line;

    public DialogueText textDialogues;


    public List<string> textArray = new List<string>();

    private int currentText;
    [SerializeField]
    private int endText;



    private void Start()
    {
        foreach(string text in textDialogues.textFiles_01){
            textArray.Add(text);
        }

        currentText = 0;
        endText = textArray.Count - 1;
    }

    // text 작동 메소드
    public void DialogueText()
    {
        if(currentText > textArray.Count - 1)
        {
            StopCoroutine(AutoTyping());
            dialogueBox.SetActive(false);
        }
        if (currentText <= textArray.Count - 1)
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










