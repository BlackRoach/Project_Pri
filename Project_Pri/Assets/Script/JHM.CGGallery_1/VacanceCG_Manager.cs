using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VacanceCG_Manager : MonoBehaviour {

    [SerializeField]
    private bool[] is_Vacance_CG = new bool[29];
    public GameObject[] cg_Vacance_Slot;
    [SerializeField]
    private bool[] is_Ending_CG = new bool[50];
    public GameObject[] cg_Ending_Slot;


    public Image FullSizeCG;

    public GameObject vacance_Page_Btn;
    public GameObject ending_Page_Btn;


    public GameObject vacance_Page;
    public GameObject ending_Page;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < is_Vacance_CG.Length; i++)
        {
            is_Vacance_CG[i] = false;
        }
        for (int i = 0; i < is_Vacance_CG.Length; i++)
        {
            is_Vacance_CG[i] = CG_2_Manager.CgInfoList[i].IS_UNLOCK; 
        }
        for (int i = 0; i < is_Ending_CG.Length; i++)
        {
            is_Ending_CG[i] = false;
        }
        for (int i = 0; i < is_Ending_CG.Length; i++)
        {
            is_Ending_CG[i] = CG_Mode_Ending.ending_Data[i].IS_UNLOCK;
        }
        LoadCG();

        Button_Vacance_List_Pressed(); // 초기화면

        Button_Vacance_Page_1_Pressed();
    }
    private void LoadCG()
    {
        for(int i = 0; i < is_Vacance_CG.Length; i++)
        {
            if (is_Vacance_CG[i])
            {
                int temp = i + 1;
                cg_Vacance_Slot[i].GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/VACANCE_CG/VACANCE_CG_"+temp.ToString());
                cg_Vacance_Slot[i].transform.GetChild(0).gameObject.SetActive(false);
                cg_Vacance_Slot[i].GetComponent<CGSlot_1>().active = true;
            }
        }

        for(int i = 0; i < is_Ending_CG.Length; i++)
        {
            if (is_Ending_CG[i])
            {
                int temp = i + 1;
                cg_Ending_Slot[i].GetComponent<Image>().sprite =
                    Resources.Load<Sprite>("JHM.Img/" + CG_Mode_Ending.ending_Data[i].CG_Ending_Name);
                cg_Ending_Slot[i].transform.GetChild(0).gameObject.SetActive(false);
                cg_Ending_Slot[i].GetComponent<CGSlot_1>().active = true;
            }
        }
    }
    // 바캉스 , 엔딩 버튼 누를시 화면 출력
    public void Button_Vacance_List_Pressed()
    {
        vacance_Page_Btn.SetActive(true);
        ending_Page_Btn.SetActive(false);
        vacance_Page.SetActive(true);
        ending_Page.SetActive(false);
    }
    public void Button_Ending_List_Pressed()
    {
        vacance_Page_Btn.SetActive(false);
        ending_Page_Btn.SetActive(true);
        vacance_Page.SetActive(false);
        ending_Page.SetActive(true);
    }
    // 바캉스 페이지 버튼 리스트
    public void Button_Vacance_Page_1_Pressed()
    {
        vacance_Page.transform.GetChild(0).gameObject.SetActive(true);
        vacance_Page.transform.GetChild(1).gameObject.SetActive(false);
        vacance_Page.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Vacance_Page_2_Pressed()
    {
        vacance_Page.transform.GetChild(0).gameObject.SetActive(false);
        vacance_Page.transform.GetChild(1).gameObject.SetActive(true);
        vacance_Page.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Vacance_Page_3_Pressed()
    {
        vacance_Page.transform.GetChild(0).gameObject.SetActive(false);
        vacance_Page.transform.GetChild(1).gameObject.SetActive(false);
        vacance_Page.transform.GetChild(2).gameObject.SetActive(true);
    }
    // ------------------------
    // 엔딩 페이지 버튼 리스트
    public void Button_Ending_Page_1_Pressed()
    {
        for(int i = 0; i < ending_Page.transform.childCount; i++)
        {
            ending_Page.transform.GetChild(i).gameObject.SetActive(false);
        }
        ending_Page.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Button_Ending_Page_2_Pressed()
    {
        for (int i = 0; i < ending_Page.transform.childCount; i++)
        {
            ending_Page.transform.GetChild(i).gameObject.SetActive(false);
        }
        ending_Page.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void Button_Ending_Page_3_Pressed()
    {
        for (int i = 0; i < ending_Page.transform.childCount; i++)
        {
            ending_Page.transform.GetChild(i).gameObject.SetActive(false);
        }
        ending_Page.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void Button_Ending_Page_4_Pressed()
    {
        for (int i = 0; i < ending_Page.transform.childCount; i++)
        {
            ending_Page.transform.GetChild(i).gameObject.SetActive(false);
        }
        ending_Page.transform.GetChild(3).gameObject.SetActive(true);
    }
    public void Button_Ending_Page_5_Pressed()
    {
        for (int i = 0; i < ending_Page.transform.childCount; i++)
        {
            ending_Page.transform.GetChild(i).gameObject.SetActive(false);
        }
        ending_Page.transform.GetChild(4).gameObject.SetActive(true);
    }
    // ------------------------
    public void ShowFullSizeCG(Sprite image)
    {
        FullSizeCG.gameObject.SetActive(true);
        FullSizeCG.sprite = image;
    }

    public void Load_To_Exit()
    {
        SceneManager.LoadScene("Title 1");
    }

} // class











