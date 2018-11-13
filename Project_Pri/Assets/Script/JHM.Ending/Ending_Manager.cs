using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class Ending_Manager : MonoBehaviour {

    public GameObject ending_Page_1, ending_Page_2, ending_Page_3, ending_Page_4;
    public GameObject btn_Loading_Panel;
    public GameObject ending_Event_Panel;
    public GameObject ending_Last_Panel;
    public GameObject last_Text_Box;
    public Text text_Loading;
    public Text text_Last_Ending;
    public Text text_Current_Page;

    public Image img_BG; // 조건에 맞는 ending img 출력

    public Animator anim;
    [SerializeField]
    private Statement load_State_Data;

    private JsonData ending_Data;

    private int current_Page;
    [SerializeField]
    private bool[] btn_Array;
    private string ending_Text;
    private bool is_Ending;

    private bool is_Lock;
    private void Start()
    {
        Current_State_List_Load_Data();
        Ending_Data_Json_Parsing();
        ending_Last_Panel.SetActive(false);
        btn_Loading_Panel.SetActive(true);
        ending_Event_Panel.SetActive(false);
        Loading_Text_Auto_Typing();

        current_Page = 1;
        btn_Array = new bool[ending_Data.Count];
        Input_Ending_List_Page_Fuction();
        is_Ending = false;
        last_Text_Box.SetActive(false);
        is_Lock = false;
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

        // ------------------------
        ending_Page_1.SetActive(true);
        ending_Page_2.SetActive(false);
        ending_Page_3.SetActive(false);
        ending_Page_4.SetActive(false);
    }
    public void Button_Ending_Next_Page()
    {
        if(current_Page == 1)
        {
            current_Page = 2;
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(true);
            ending_Page_3.SetActive(false);
            ending_Page_4.SetActive(false);
        } else if(current_Page == 2)
        {
            current_Page = 3;
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(false);
            ending_Page_3.SetActive(true);
            ending_Page_4.SetActive(false);
        } else if (current_Page == 3)
        {
            current_Page = 4;
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(false);
            ending_Page_3.SetActive(false);
            ending_Page_4.SetActive(true);
        }
        text_Current_Page.text = current_Page.ToString() + " / 4";
    }
    public void Button_Ending_Previous_Page()
    {
        if (current_Page == 2)
        {
            current_Page = 1;
            ending_Page_1.SetActive(true);
            ending_Page_2.SetActive(false);
            ending_Page_3.SetActive(false);
            ending_Page_4.SetActive(false);
        }
        else if (current_Page == 3)
        {
            current_Page = 2;
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(true);
            ending_Page_3.SetActive(false);
            ending_Page_4.SetActive(false);
        }
        else if (current_Page == 4)
        {
            current_Page = 3;
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(false);
            ending_Page_3.SetActive(true);
            ending_Page_4.SetActive(false);
        }
        text_Current_Page.text = current_Page.ToString() + " / 4";
    }
    // 씬 이동할때 데이터 로드 Player.Prefs 이용
    private void Current_State_List_Load_Data()
    {
        load_State_Data.s_muscular_strength =  PlayerPrefs.GetInt("s_muscular_strength");
        load_State_Data.s_magic_power = PlayerPrefs.GetInt("s_magic_power");
        load_State_Data.s_intellect = PlayerPrefs.GetInt("s_intellect");
        load_State_Data.s_charm =  PlayerPrefs.GetInt("s_charm");
        load_State_Data.s_sense = PlayerPrefs.GetInt("s_sense");
        load_State_Data.s_reliability = PlayerPrefs.GetInt("s_reliability");
        load_State_Data.s_pride = PlayerPrefs.GetInt("s_pride");
        load_State_Data.s_artistic = PlayerPrefs.GetInt("s_artistic");
        load_State_Data.s_elegance = PlayerPrefs.GetInt("s_elegance");
        load_State_Data.s_morality = PlayerPrefs.GetInt("s_morality");
        load_State_Data.s_stamina = PlayerPrefs.GetInt("s_stamina");
    }
    // ----------------------------
    
    private void Input_Ending_List_Page_Fuction()
    {
        for(int i = 0; i < ending_Data.Count; i++)
        {
            // page_1
            if( i < 4)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_1.transform.GetChild(0).transform.GetChild(i).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_1.transform.GetChild(0).transform.GetChild(i).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 4 && i < 8)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_1.transform.GetChild(1).transform.GetChild(i - 4).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_1.transform.GetChild(1).transform.GetChild(i - 4).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 8 && i < 12)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_1.transform.GetChild(2).transform.GetChild(i - 8).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_1.transform.GetChild(2).transform.GetChild(i - 8).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 12 && i < 16)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_1.transform.GetChild(3).transform.GetChild(i - 12).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_1.transform.GetChild(3).transform.GetChild(i - 12).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            // page_2
            if (i >= 16 && i < 20)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_2.transform.GetChild(0).transform.GetChild(i - 16).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_2.transform.GetChild(0).transform.GetChild(i - 16).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 20 && i < 24)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_2.transform.GetChild(1).transform.GetChild(i - 20).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_2.transform.GetChild(1).transform.GetChild(i - 20).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 24 && i < 28)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_2.transform.GetChild(2).transform.GetChild(i - 24).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_2.transform.GetChild(2).transform.GetChild(i - 24).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 28 && i < 32)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_2.transform.GetChild(3).transform.GetChild(i - 28).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_2.transform.GetChild(3).transform.GetChild(i - 28).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            // page_3
            if (i >= 32 && i < 36)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_3.transform.GetChild(0).transform.GetChild(i - 32).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_3.transform.GetChild(0).transform.GetChild(i - 32).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 36 && i < 40)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_3.transform.GetChild(1).transform.GetChild(i - 36).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_3.transform.GetChild(1).transform.GetChild(i - 36).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 40 && i < 44)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_3.transform.GetChild(2).transform.GetChild(i - 40).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_3.transform.GetChild(2).transform.GetChild(i - 40).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            if (i >= 44 && i < 48)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_3.transform.GetChild(3).transform.GetChild(i - 44).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_3.transform.GetChild(3).transform.GetChild(i - 44).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }
            // page_4
            if (i >= 48 && i < 50)
            {
                if (Ending_List_Page_Boolean(i))
                {
                    ending_Page_4.transform.GetChild(0).transform.GetChild(i - 48).GetComponent<Image>().color = Color.green;
                    btn_Array[i] = true;
                }
                else
                {
                    ending_Page_4.transform.GetChild(0).transform.GetChild(i - 48).GetComponent<Image>().color = Color.red;
                    btn_Array[i] = false;
                }
            }

            // ------------------------------
            // Text_Input
            // page_1
            if (i < 3)
            {
                ending_Page_1.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            } else if( i == 3)
            {
                ending_Page_1.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 4 && i < 8)
            {
                ending_Page_1.transform.GetChild(1).transform.GetChild(i - 4).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if(i >= 8 && i < 12)
            {
                ending_Page_1.transform.GetChild(2).transform.GetChild(i - 8).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 12 && i < 16)
            {
                ending_Page_1.transform.GetChild(3).transform.GetChild(i - 12).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            // page_2
            if (i >= 16 && i < 20)
            {
                ending_Page_2.transform.GetChild(0).transform.GetChild(i - 16).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 20 && i < 24)
            {
                ending_Page_2.transform.GetChild(1).transform.GetChild(i - 20).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 24 && i < 28)
            {
                ending_Page_2.transform.GetChild(2).transform.GetChild(i - 24).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 28 && i < 32)
            {
                ending_Page_2.transform.GetChild(3).transform.GetChild(i - 28).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            // page_3
            if (i >= 32 && i < 36)
            {
                ending_Page_3.transform.GetChild(0).transform.GetChild(i - 32).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 36 && i < 40)
            {
                ending_Page_3.transform.GetChild(1).transform.GetChild(i - 36).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 40 && i < 44)
            {
                ending_Page_3.transform.GetChild(2).transform.GetChild(i - 40).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            if (i >= 44 && i < 48)
            {
                ending_Page_3.transform.GetChild(3).transform.GetChild(i - 44).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
            // page_4
            if (i >= 48 && i < 50)
            {
                ending_Page_4.transform.GetChild(0).transform.GetChild(i - 48).transform.GetChild(0).GetComponent<Text>()
                    .text = ending_Data[i]["ENDING_NAME"].ToString();
            }
        }
    }
    private bool Ending_List_Page_Boolean(int index)
    {
        bool set = false;
        if(load_State_Data.s_muscular_strength >= (int)ending_Data[index]["MUSCULAR_STRENGTH"] &&
            load_State_Data.s_magic_power >= (int)ending_Data[index]["MAGIC_POWER"] &&
            load_State_Data.s_intellect >= (int)ending_Data[index]["INTEELECT"] &&
            load_State_Data.s_charm >= (int)ending_Data[index]["CHARM"] &&
            load_State_Data.s_sense >= (int)ending_Data[index]["SENSE"] &&
            load_State_Data.s_reliability >= (int)ending_Data[index]["RELIABILITY"] &&
            load_State_Data.s_pride >= (int)ending_Data[index]["PRIDE"] &&
            load_State_Data.s_artistic >= (int)ending_Data[index]["ARTISTIC"] &&
            load_State_Data.s_elegance >= (int)ending_Data[index]["ELEGANCE"] &&
            load_State_Data.s_morality >= (int)ending_Data[index]["MORALITY"] &&
            load_State_Data.s_stamina >= (int)ending_Data[index]["STAMINA"])
        {
            set = true;
        }
        else
        {
            set = false;
        }
        return set;
    }
    public void Button_Ending_Pressed(int i)
    {
        if (btn_Array[i] == true)
        {
            ending_Page_1.SetActive(false);
            ending_Page_2.SetActive(false);
            ending_Page_3.SetActive(false);
            ending_Page_4.SetActive(false);

            btn_Loading_Panel.SetActive(false);
            ending_Event_Panel.SetActive(false);
            ending_Last_Panel.SetActive(true);
            StartCoroutine(Ending_Text_Box());
            // ------------------------
            string last_Text = ending_Data[i]["CONDITION_COMENT_1"].ToString();
            ending_Text = ending_Data[i]["CONDITION_COMENT_2"].ToString();
            text_Last_Ending.text = " ";
            StartCoroutine(Auto_Typing_Last(last_Text));

            // --------------------------
            // 조건에 맞는 이미지 출력
            if (i == 0)
            {
                img_BG.sprite = Resources.Load<Sprite>("JHM.Img/" + "ending_1");
            } else if(i == 1)
            {
                img_BG.sprite = Resources.Load<Sprite>("JHM.Img/" + "ending_2");
            }
            else if (i == 2)
            {
                img_BG.sprite = Resources.Load<Sprite>("JHM.Img/" + "ending_3");
            }
            else
            {
                img_BG.sprite = Resources.Load<Sprite>("JHM.Img/" + "ending_1");
            }
            
            if(img_BG.sprite.name == "ending_1")
            {
                CG_Mode_Ending.ending_Data[0].CG_Ending_Name = "ending_1";
                CG_Mode_Ending.ending_Data[0].IS_UNLOCK = true;
            } else if (img_BG.sprite.name == "ending_2")
            {
                CG_Mode_Ending.ending_Data[1].CG_Ending_Name = "ending_2";
                CG_Mode_Ending.ending_Data[1].IS_UNLOCK = true;
            } else if (img_BG.sprite.name == "ending_3")
            {
                CG_Mode_Ending.ending_Data[2].CG_Ending_Name = "ending_3";
                CG_Mode_Ending.ending_Data[2].IS_UNLOCK = true;
            }
        }
    }
    IEnumerator Auto_Typing_Last(string sr)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (char text in sr.ToCharArray())
        {
            text_Last_Ending.text += text.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        is_Lock = true;
    }
    IEnumerator Ending_Text_Box()
    {
        yield return new WaitForSeconds(0.5f);
        On_Last_Text_Box();
    }
    public void Button_Last_Ending_Panel_Finished()
    {
        if (is_Lock)
        {
            if (!is_Ending)
            {
                is_Ending = true;
                text_Last_Ending.text = " ";
                StartCoroutine(Auto_Typing_Last(ending_Text));
            }
            else
            {
                Off_Last_Text_Box();
                anim.Play("Ending_Finished");
                StartCoroutine(Load_To_Title_Scene());
            }
        }
    }
    private void On_Last_Text_Box()
    {
        last_Text_Box.SetActive(true);
    }
    private void Off_Last_Text_Box()
    {
        last_Text_Box.SetActive(false);
    }
    IEnumerator Load_To_Title_Scene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Title 1");
    }
} // class








