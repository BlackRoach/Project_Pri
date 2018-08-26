using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGManager : MonoBehaviour {

    
    public static List<CGInfo> CgInfoList; //= new List<CGInfo>();
    public static bool bSetting;

    private void Start()
    {
        Debug.Log("Start");
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<CGManager>().Length > 1)
            Destroy(gameObject);

        if(!bSetting)
        {
            CgInfoList = new List<CGInfo>();
            CgInfoList.Add(new CGInfo("VACANCE_CG_1", false));
            CgInfoList.Add(new CGInfo("VACANCE_CG_2", false));
            CgInfoList.Add(new CGInfo("VACANCE_CG_3", false));
            CgInfoList.Add(new CGInfo("VACANCE_CG_4", false));
            CgInfoList.Add(new CGInfo("VACANCE_CG_5", false));
            bSetting = true; // Start()에서 최초 한번만 들어온다.
        }

        Debug.Log(CgInfoList[0].IS_UNLOCK);
        Debug.Log(CgInfoList[1].IS_UNLOCK);
        Debug.Log(CgInfoList[2].IS_UNLOCK);
        Debug.Log(CgInfoList[3].IS_UNLOCK);
        Debug.Log(CgInfoList[4].IS_UNLOCK);

        //Debug.Log(CgInfoList.Count);
        
    }

    public void ShowMeTheUnlockStatus()
    {

        Debug.Log(CgInfoList[0].IS_UNLOCK);
        Debug.Log(CgInfoList[1].IS_UNLOCK);
        Debug.Log(CgInfoList[2].IS_UNLOCK);
        Debug.Log(CgInfoList[3].IS_UNLOCK);
        Debug.Log(CgInfoList[4].IS_UNLOCK);
    }

}

// 스태틱 멤버변수는 씬에 넣지 않아도 작동함을 확인함.

public class CGInfo
{
    private string cgName; // CG이름
    private bool isUnlock; // CG개방여부

    public CGInfo(string cgName, bool isUnlock)
    {
        this.cgName = cgName;
        this.isUnlock = isUnlock;
    }

    public string CG_NAME
    {
        get { return cgName; }
    }

    public bool IS_UNLOCK
    {
        get { return isUnlock; }
        set { isUnlock = value; }
    }
}
