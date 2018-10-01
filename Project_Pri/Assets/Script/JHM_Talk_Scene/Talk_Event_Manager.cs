using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class Talk_Event_Manager : MonoBehaviour {


    public GameObject Button_State_One;
    public GameObject Button_State_Two;
    public GameObject Text_Box;

    private JsonData talk_Data;
    private JsonData talk_Value; // 매력 , 기품 데이터 가져오기

    [SerializeField]
    private Text text_Charm_Value;
    [SerializeField]
    private Text text_Elegance_Value;
    [SerializeField]
    private int charm;
    [SerializeField]
    private int elegance;
    [SerializeField]
    private string text_Massage;

    private void Start()
    {
        Defualt_Json_Talk_Data_Parsing();

        Button_State_One.SetActive(true);
        Button_State_Two.SetActive(false);
        Text_Box.SetActive(false);
        text_Massage = " ";

        // text line
        charm = (int)talk_Value[0]["CHARM"];
        elegance = (int)talk_Value[0]["ELEGANCE"];

        text_Charm_Value.text = charm.ToString();
        text_Elegance_Value.text = elegance.ToString();
        // ---------
    }


    private void Defualt_Json_Talk_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/TALK_SCENE_DATA");

        talk_Data = JsonMapper.ToObject(json_File.text);

        // --------------------------

        TextAsset json_Talk = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/Start_State");

        talk_Value = JsonMapper.ToObject(json_Talk.text);
    }
    // 매력 , 기품 수치 조절 함수
    public void Button_Charm_Value_Add_Five_Input()
    {
        charm += 5;
        text_Charm_Value.text = charm.ToString();
    }
    public void Button_Charm_Value_Sub_Five_Input()
    {
        charm -= 5;
        text_Charm_Value.text = charm.ToString();
    }
    public void Button_Elegance_Value_Add_Five_Input()
    {
        elegance += 5;
        text_Elegance_Value.text = elegance.ToString();
    }
    public void Button_Elegance_Value_Sub_Five_Input()
    {
        elegance -= 5;
        text_Elegance_Value.text = elegance.ToString();
    }
    // ----------------
    public void Button_Select_Castle_Of_Men()
    {
        Button_State_One.SetActive(false);
        Button_State_Two.SetActive(true);
    }
    public void Button_Select_Targeting_NPC_To_Talk(int i)
    {
        if(charm < (int)talk_Data[i]["CHARM"])
        {
            text_Massage = " ";
            text_Massage = talk_Data[i]["FAIL_1"].ToString();
        }
        else
        {
            if(charm >= (int)talk_Data[i]["CHARM"] && elegance < (int)talk_Data[i]["ELEGANCE"])
            {
                text_Massage = " ";
                text_Massage = talk_Data[i]["FAIL_2"].ToString();
            }
            if (charm >= (int)talk_Data[i]["CHARM"] && elegance >= (int)talk_Data[i]["ELEGANCE"])
            {
                text_Massage = " ";
                text_Massage = talk_Data[i]["SUCCESS"].ToString();
            }
        }
        // -------------------------
        Text_Box.SetActive(true);

        Text_Box.transform.GetChild(0).transform.GetComponent<Text>().text = talk_Data[i]["NAME"].ToString();

        Text_Box.transform.GetChild(2).transform.GetChild(0).transform.
            GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("JHM.Img/" + talk_Data[i]["FACE_UI"].ToString());
        StartCoroutine(Text_Auto_Typing());
    }

    IEnumerator Text_Auto_Typing()
    {
        Text_Box.transform.GetChild(1).transform.GetComponent<Text>().text = " ";
        foreach (char input_Char in text_Massage.ToCharArray())
        {
            Text_Box.transform.GetChild(1).transform.GetComponent<Text>().text += input_Char;
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Control_Img_Click_Arrow_Notification());
    }
    IEnumerator Control_Img_Click_Arrow_Notification()
    {
        Text_Box.transform.GetChild(3).transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60f);
        Text_Box.transform.GetChild(3).transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 60f);
        yield return new WaitForSeconds(0.25f);
        Text_Box.transform.GetChild(3).transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50f);
        Text_Box.transform.GetChild(3).transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(Control_Img_Click_Arrow_Notification());
    }

    public void Button_Text_Box_Exit()
    {
        StopAllCoroutines();

        Text_Box.SetActive(false);
        Button_State_One.SetActive(true);
        Button_State_Two.SetActive(false);
    }


    // 씬 이동 
    public void Button_Exit_From_Talk_Scene()
    {
        SceneManager.LoadScene("Main");
    }
    public void Button_Load_To_Sparring_Scene()
    {
        SceneManager.LoadScene("Fight_Scene");
    }


} // class











