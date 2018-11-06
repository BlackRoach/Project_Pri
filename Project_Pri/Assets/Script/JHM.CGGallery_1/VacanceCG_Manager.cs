using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VacanceCG_Manager : MonoBehaviour {

    [SerializeField]
    private bool[] is_CG = new bool[29];
    public GameObject[] cg_Slot;

    public Image FullSizeCG;

    public GameObject page_Panel;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < is_CG.Length; i++)
        {
            is_CG[i] = false;
        }
        for (int i = 0; i < is_CG.Length; i++)
        {
            is_CG[i] = CG_2_Manager.CgInfoList[i].IS_UNLOCK; 
        }

        LoadCG();
    }

    private void LoadCG()
    {
        for(int i = 0; i < is_CG.Length; i++)
        {
            if (is_CG[i])
            {
                int temp = i + 1;
                cg_Slot[i].GetComponent<Image>().sprite =
                Resources.Load<Sprite>("JHM.Img/VACANCE_CG/VACANCE_CG_"+temp.ToString());
                cg_Slot[i].transform.GetChild(0).gameObject.SetActive(false);
                cg_Slot[i].GetComponent<CGSlot_1>().active = true;
            }
        }
    }
    public void Button_Page_1_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(true);
        page_Panel.transform.GetChild(1).gameObject.SetActive(false);
        page_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Page_2_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(false);
        page_Panel.transform.GetChild(1).gameObject.SetActive(true);
        page_Panel.transform.GetChild(2).gameObject.SetActive(false);
    }
    public void Button_Page_3_Pressed()
    {
        page_Panel.transform.GetChild(0).gameObject.SetActive(false);
        page_Panel.transform.GetChild(1).gameObject.SetActive(false);
        page_Panel.transform.GetChild(2).gameObject.SetActive(true);
    }

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











