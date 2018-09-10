using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Ending_Manager : MonoBehaviour {

    public GameObject btn_Loading_Panel;
    public GameObject ending_Event_Panel;
    public Text text_Loading;

    private JsonData ending_Data;

    private void Start()
    {
        Ending_Data_Json_Parsing();
        btn_Loading_Panel.SetActive(true);
        ending_Event_Panel.SetActive(false);
        Loading_Text_Auto_Typing();
    }
    private void Ending_Data_Json_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Ending_Data");

        ending_Data = JsonMapper.ToObject(json_File.text);
    }
    private void Loading_Text_Auto_Typing()
    {
        text_Loading.text = " \n";
        string _text = "  딸과 함께한지 8년이라는 시간이 지났다.\n" +
            "  딸은 당신의 희망과 기여로 무럭무럭 성장하였으며..\n" +
            "  어느덧 그녀의 운명을 선택할수있는 나이가 되었다.\n" +
            "  당신은 그녀의 운명이 어떻게 되기를 바라는가? ";

        StartCoroutine(Auto_Typing(_text));
    }
    IEnumerator Auto_Typing(string sr)
    {
        foreach (char text in sr.ToCharArray())
        {
            text_Loading.text += text.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Button_Ending_Event_Load()
    {
        btn_Loading_Panel.SetActive(false);
        ending_Event_Panel.SetActive(true);
    }







} // class








