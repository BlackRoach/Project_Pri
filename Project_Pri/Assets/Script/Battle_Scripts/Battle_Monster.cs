﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Monster : Battle_Character {

  
    [SerializeField] private GameObject cursur;

	// Use this for initialization
	void Start () {
        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        if (battleManager == null)
            battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadData = jsonFileWriter.SerializeData("MONSTER_TABLE");
        LoadData();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SD몬스터/" + sd_model);
        GameObject nobj = (GameObject)GameObject.Instantiate(status);
        nobj.gameObject.transform.parent = status.transform.parent;
        StatusUI = nobj;
        hpBar = nobj.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        guageBar = nobj.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        nobj.transform.localScale = new Vector3(1, 1, 1);
        nobj.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        update();
        if (BattleManager.Instance.isAttack_readonly)
        {
            cursur.SetActive(true);
         
        }
        else
            cursur.SetActive(false);

       

    }

    public void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                sd_model = loadData[i]["MONSTER_MODEL"].ToString();

                break;
            }
        }
    }
}
