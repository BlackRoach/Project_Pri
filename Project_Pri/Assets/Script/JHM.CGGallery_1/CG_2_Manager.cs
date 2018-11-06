using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CG_2_Manager : MonoBehaviour {

    public static List<CGInfo_2> CgInfoList;
    public static bool one_Time_Load; // 최초 오브젝트 생성시에만 들어오게 하는 장치
  
    private void Awake()
    {
        //Debug.Log("Start");
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType<CG_2_Manager>().Length > 1)
            Destroy(gameObject);

        if (!one_Time_Load)
        {
            CgInfoList = new List<CGInfo_2>();
            for(int i =0; i< 29; i++)
            {
                int temp = i + 1;
                CgInfoList.Add(new CGInfo_2("VACANCE_CG_" + temp, false));
            }
            one_Time_Load = true; // Start()에서 최초 한번만 들어온다.
        }
    }
    
} // class

public class CGInfo_2
{
    private string cgName; // CG이름
    private bool isUnlock; // CG개방여부

    public CGInfo_2(string cgName, bool isUnlock)
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



