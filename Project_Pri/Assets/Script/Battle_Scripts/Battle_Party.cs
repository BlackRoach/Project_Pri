using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battle_Party : Battle_Character
{

	// Use this for initialization
	void Start () {
        if (battleManager == null)
            battleManager = BattleManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        loadData = jsonFileWriter.SerializeData("PARTY_TABLE");
        LoadData();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SD캐릭터/" + sd_model);
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
    }

    public void LoadData()
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                sd_model = loadData[i]["PARTY_SD_MODEL"].ToString();

                break;
            }
        }
    }
}
