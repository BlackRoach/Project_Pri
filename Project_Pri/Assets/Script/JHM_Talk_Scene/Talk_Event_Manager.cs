using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;


public class Talk_Event_Manager : MonoBehaviour {


    public GameObject Button_State_One;
    public GameObject Button_State_Two;
    public GameObject Text_Box;

    private JsonData talk_Data;

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
        charm = 12;
        elegance = 6;
        // ---------
    }


    private void Defualt_Json_Talk_Data_Parsing()
    {
        TextAsset json_File = Resources.Load<TextAsset>("JHM.Resources.Json/Defualt_Json_Data/TALK_SCENE_DATA");

        talk_Data = JsonMapper.ToObject(json_File.text);
    }


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





} // class











