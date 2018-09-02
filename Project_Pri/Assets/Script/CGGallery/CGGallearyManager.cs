using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class CGGallearyManager : MonoBehaviour {

    public List<CGInfo> cgList = new List<CGInfo>();

    public bool bCG1;
    public bool bCG2;
    public bool bCG3;
    public bool bCG4;
    public bool bCG5;

    public GameObject CG1;
    public GameObject CG2;
    public GameObject CG3;
    public GameObject CG4;
    public GameObject CG5;

    public Image FullSizeCG;

    // Use this for initialization
    void Start () {
        //CGManager cgm = FindObjectOfType<CGManager>();

        //bCG1 = cgm.CgInfoList[0].IS_UNLOCK;
        //bCG2 = cgm.CgInfoList[1].IS_UNLOCK;
        //bCG3 = cgm.CgInfoList[2].IS_UNLOCK;
        //bCG4 = cgm.CgInfoList[3].IS_UNLOCK;
        //bCG5 = cgm.CgInfoList[4].IS_UNLOCK;
        bCG1 = CGManager.CgInfoList[0].IS_UNLOCK;
        bCG2 = CGManager.CgInfoList[1].IS_UNLOCK;
        bCG3 = CGManager.CgInfoList[2].IS_UNLOCK;
        bCG4 = CGManager.CgInfoList[3].IS_UNLOCK;
        bCG5 = CGManager.CgInfoList[4].IS_UNLOCK;

        LoadCG();

    }
	
    private void LoadCG()
    {
        if(bCG1)
        {
            CG1.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("VACANCE_CG/VACANCE_CG_1");
            CG1.transform.GetChild(0).gameObject.SetActive(false);
            CG1.GetComponent<CGSlot>().active = true;
        }
        if(bCG2)
        {
            CG2.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("VACANCE_CG/VACANCE_CG_2");
            CG2.transform.GetChild(0).gameObject.SetActive(false);
            CG2.GetComponent<CGSlot>().active = true;
        }
        if(bCG3)
        {
            CG3.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("VACANCE_CG/VACANCE_CG_3");
            CG3.transform.GetChild(0).gameObject.SetActive(false);
            CG3.GetComponent<CGSlot>().active = true;
        }
        if(bCG4)
        {
            CG4.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("VACANCE_CG/VACANCE_CG_4");
            CG4.transform.GetChild(0).gameObject.SetActive(false);
            CG4.GetComponent<CGSlot>().active = true;
        }
        if(bCG5)
        {
            CG5.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("VACANCE_CG/VACANCE_CG_5");
            CG5.transform.GetChild(0).gameObject.SetActive(false);
            CG5.GetComponent<CGSlot>().active = true;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void GoToSchedule()
    {
        SceneManager.LoadScene("schedule");
    }

    public void ShowFullSizeCG(Sprite image)
    {
        FullSizeCG.gameObject.SetActive(true);
        FullSizeCG.sprite = image;
    }
}

// CGManager 클래스에서 맨처음에 Slot를 모두 LOCK(잠금)한다.
// 스케줄 씬에서 해당 이벤트 CG를 보면 해당 Slot이 UNLOCK(잠금해제)된다.

