using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Rena_Manager : MonoBehaviour {



    public Char_Rena rena_Info;

    [SerializeField]
    private int old_Count, state_Count, dress_Count;
    [SerializeField]
    private GameObject rena_Char_1, rena_Char_2, rena_Char_3, rena_Char_4;
    public Text text_Old;
    public Text text_State;
    public Text text_Dress;
    [SerializeField]
    private bool is_Showing;

    public string mobile_Path;

    private JsonData rena_Data;
    private void Start()
    {
        old_Count = state_Count = dress_Count = 1;

        text_Old.text = old_Count.ToString();
        text_State.text = state_Count.ToString();
        text_Dress.text = dress_Count.ToString();


        rena_Char_1.SetActive(false);
        rena_Char_2.SetActive(false);
        rena_Char_3.SetActive(false);
        rena_Char_4.SetActive(false);

        is_Showing = false;
        
        rena_Char_1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS1");
        rena_Char_2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "H1");
        rena_Char_3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS1_1");
        rena_Char_4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "STATE_2_DRESS2");

        mobile_Path = Application.persistentDataPath;

        rena_Info.old = -1;
        rena_Info.state = -1;
        rena_Info.dress = -1;
        Json_Save_Rena_Data();
    }

    // 레나 상태 < , >  plus , minus 버튼
    public void Button_Minus_Old()
    {
        old_Count = int.Parse(text_Old.text.ToString());
        if(old_Count == 2)
        {
            old_Count--;
            text_Old.text = old_Count.ToString();
        }
    }
    public void Button_Minus_State()
    {
        state_Count = int.Parse(text_State.text.ToString());
        if (state_Count == 2)
        {
            state_Count--;
            text_State.text = state_Count.ToString();
        }
    }
    public void Button_Minus_Dress()
    {
        dress_Count = int.Parse(text_Dress.text.ToString());
        if (dress_Count == 2)
        {
            dress_Count--;
            text_Dress.text = dress_Count.ToString();
        }
    }
    public void Button_Plus_Old()
    {
        old_Count = int.Parse(text_Old.text.ToString());
        if (old_Count == 1)
        {
            old_Count++;
            text_Old.text = old_Count.ToString();
        }
    }
    public void Button_Plus_State()
    {
        state_Count = int.Parse(text_State.text.ToString());
        if (state_Count == 1)
        {
            state_Count++;
            text_State.text = state_Count.ToString();
        }
    }
    public void Button_Plus_Dress()
    {
        dress_Count = int.Parse(text_Dress.text.ToString());
        if (dress_Count == 1)
        {
            dress_Count++;
            text_Dress.text = dress_Count.ToString();
        }
    }
    // -------------------------------------

    // 버튼 케릭터 출력
    public void Button_Pressed_Apply_Rena_Character()
    {
        if (!is_Showing)
        {
            is_Showing = true;
            rena_Info.old = old_Count;
            rena_Info.state = state_Count;
            rena_Info.dress = dress_Count;
            // 1
            if (rena_Info.old == 1 && rena_Info.state == 1 && rena_Info.dress == 1)
            {
                rena_Char_1.SetActive(true);
                // ------------------
                rena_Char_1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS1");
            }
            else if (rena_Info.old == 1 && rena_Info.state == 1 && rena_Info.dress == 2)
            {
                rena_Char_1.SetActive(true);
                // ------------------
                rena_Char_1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS2");
            }
            // 2
            if (rena_Info.old == 1 && rena_Info.state == 2 && rena_Info.dress == 1)
            {
                rena_Char_2.SetActive(true);
                //--------------------
                rena_Char_2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "H1");
            }
            else if (rena_Info.old == 1 && rena_Info.state == 2 && rena_Info.dress == 2)
            {
                rena_Char_2.SetActive(true);
                //--------------------
                rena_Char_2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "H2");
            }
            // 3
            if (rena_Info.old == 2 && rena_Info.state == 1 && rena_Info.dress == 1)
            {
                rena_Char_3.SetActive(true);
                //--------------------
                rena_Char_3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS1_1");
            }
            else if (rena_Info.old == 2 && rena_Info.state == 1 && rena_Info.dress == 2)
            {
                rena_Char_3.SetActive(true);
                //--------------------
                rena_Char_3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "DRESS2_1");
            }
            // 4
            if (rena_Info.old == 2 && rena_Info.state == 2 && rena_Info.dress == 1)
            {
                rena_Char_4.SetActive(true);
                // ------------------
                rena_Char_4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "STATE_2_DRESS2");
            }
            else if (rena_Info.old == 2 && rena_Info.state == 2 && rena_Info.dress == 2)
            {
                rena_Char_4.SetActive(true);
                // ------------------
                rena_Char_4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("JHM.Img/" + "STATE_2_DRESS1");
            }

            Json_Save_Rena_Data();
        }
    }

    // 버튼 케릭터 삭제
    public void Button_Pressed_Exit_Rena_Character()
    {
        rena_Char_1.SetActive(false);
        rena_Char_2.SetActive(false);
        rena_Char_3.SetActive(false);
        rena_Char_4.SetActive(false);

        is_Showing = false;
    }

    // Json_Save Rena_Data
    public void Json_Save_Rena_Data()
    {
        rena_Data = JsonMapper.ToJson(rena_Info);

        File.WriteAllText(mobile_Path + "/" + "Rena_Data.json", rena_Data.ToString());
    }

} // class












