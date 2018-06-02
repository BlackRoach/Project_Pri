using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueText : MonoBehaviour{
    
    public string[] textFiles_01;
   
    public string[] textFiles_02;
    
    public string[] textFiles_03;
   
    public string[] textFiles_04;

    public string[] textFiles_05;

    public string[] textFiles_06;

    private void Start()
    {
        textFiles_01 = new string[6];
        textFiles_02 = new string[4];
        textFiles_03 = new string[4];
        textFiles_04 = new string[3];
        textFiles_05 = new string[3];
        textFiles_06 = new string[2];
        Text_Input();
    }

    private void Text_Input()
    {
        textFiles_01[0] = " 안녕하세요";
        textFiles_01[1] = " 반갑습니다!";
        textFiles_01[2] = " 잘 나오나요?";
        textFiles_01[3] = " 무엇이요?";
        textFiles_01[4] = " 이 글이요!";
        textFiles_01[5] = " ";

        // -----------------------

        textFiles_02[0] = " 아이우에오!";
        textFiles_02[1] = " 반갑습니다";
        textFiles_02[2] = " 그럼 안녕!";
        textFiles_02[3] = " ";

        // ------------------------

        textFiles_03[0] = " 가나다라";
        textFiles_03[1] = " 마바사";
        textFiles_03[2] = " 축하합니다!";
        textFiles_03[3] = " ";

        // ------------------------

        textFiles_04[0] = " 선택지를 구현하겠습니다";
        textFiles_04[1] = " 1번과 2번중 당신의 선택은?";
        textFiles_04[2] = " ";

        // ------------------------

        textFiles_05[0] = " 당신은 1번을 선택하였습니다.";
        textFiles_05[1] = " 우아아아악";
        textFiles_05[2] = " ";

        // ------------------------

        textFiles_06[0] = " 나는 2번 선택지다!";
        textFiles_06[1] = " ";
    }

} // class










