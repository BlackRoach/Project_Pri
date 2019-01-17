using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;
    public static BattleManager Instance
    {
        get
        {
             return instance;
            
        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject.GetComponent<BattleManager>());
            return;
        }
        instance = this;
    }

    [SerializeField] private GameObject status_all;
    [SerializeField] private GameObject resultWindow;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject defeatPopup;


    [SerializeField] private GameObject megami;
    [SerializeField] private Image attackButton;
    [SerializeField] private Image backGround;
  
    [SerializeField] private Queue<GameObject> attackQueue = new Queue<GameObject>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] partys;
    [SerializeField] private GameObject[] enemys;

    private GameObject presentlyAttackChar;
    private InGamemanager ingameManager;
    private SkillManager skillmanager;
    private JsonFileWriter jsonFileWriter;
    private JsonData loadMonsterData;
    private JsonData loadPartyData;

    private string group_id;
    private string stage_background;
    private int monster_cnt;
    private int[] monster_pos;
    private string[] monster_id;
   
    private float fillamount;
    private float currentCoolTime;
    private float cooltime = 10f;

    private bool isResult = false;
    private List<string> partyid = new List<string>();

    public bool isFightWhile = false;
    public bool isCommandOn = false;
  


    public GameObject enemy;
    public GameObject party;

    public Transform[] enemypos;
    public Transform[] partypos;
    public GameObject[] PartyPanel = new GameObject[4];


    private GameObject atk_chr;
    
    
    // Use this for initialization
    void Start()
    {
   
        resultWindow.SetActive(false);
        ingameManager = InGamemanager.Instance;
        skillmanager = SkillManager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        group_id = PlayerPrefs.GetString("Current_group_id");
        loadMonsterData = jsonFileWriter.SerializeData("MONSTER_GROUP_TABLE");
        loadPartyData = jsonFileWriter.SerializeData("PARTY_TABLE");
        
        Getparty();
        LoadMonsterData(group_id);
        enemys = new GameObject[monster_cnt];
        partys = new GameObject[partyid.Count + 1];
        backGround.sprite = Resources.Load<Sprite>("전투배경/" + stage_background);
        player.transform.position = partypos[0].position;
        partys[0] = player;
        SpawnMonster();
        SpawnParty();
        
    }
    




    // Update is called once per frame
    void Update()
    {
        
       
        // 큐에 의한 신호등
        if (attackQueue.Count != 0 && !isFightWhile && !isResult)
        {
          
            isFightWhile = true;
            presentlyAttackChar = attackQueue.Dequeue();
            Debug.Log(presentlyAttackChar);
            if (presentlyAttackChar.tag == "Monster")
                presentlyAttackChar.GetComponent<Battle_Character>().MoveToEnemy(partys[UnityEngine.Random.Range(0,partyid.Count+1)]);
            else if (presentlyAttackChar.tag == "Ally")
            {

                presentlyAttackChar.GetComponent<Battle_Character>().MoveToEnemy(enemys[UnityEngine.Random.Range(0,monster_cnt)]);
            }
        }
        // else if(enemys.count == 0) -> result 출력

    }




    IEnumerator Cooltime(Image skillFilter)
    {
        skillFilter.fillAmount = 0;
        while (skillFilter.fillAmount < 1)
        {
            skillFilter.fillAmount += 1 * Time.smoothDeltaTime / cooltime;

            yield return null;
        }

        yield break;
    }




    IEnumerator MegamiMove(bool isWin)
    {

        Vector2 thisPosition = megami.gameObject.transform.localPosition;
        float t = 0.0f;
        while (t < 1.0f)
        {

            t += Time.deltaTime * 0.8f;
            Vector2 pos = megami.gameObject.transform.localPosition;
            pos = Vector2.Lerp(thisPosition, new Vector2(470,6), t);
            megami.gameObject.transform.localPosition = pos;
            yield return 0;
        }
        if (isWin)
            victoryPopup.SetActive(true);
        else
            defeatPopup.SetActive(true);
        yield return null;
    }




    public void GetResult(bool isWin)
    {
        isResult = true;
        resultWindow.SetActive(true);
        resultWindow.GetComponent<FadeScript>()._FadeOut();
        StartCoroutine(MegamiMove(isWin));
    }




   




    // 배틀 매니저에 있는 큐 자료구조에 공격할 캐릭터를 집어넣는다.
    public void AddToArray(GameObject character)
    {
        attackQueue.Enqueue(character);
    }
   
    


    public void BackToWorld()
    {
        ingameManager.opponent.SetActive(false);
        ingameManager.TurnOnWorldObjects();
        ingameManager.isRespawn = true;
        ingameManager.isFight = true;
        SceneManager.LoadScene("WorldMap");
       
    }




    public void ScanOn()
    {
        status_all.SetActive(true);
    }




    public void ScanOff()
    {
        status_all.SetActive(false);
    }




    public GameObject GetParty(int i)
    {
        return partys[i];
    }




    private void LoadMonsterData(string id)
    {
        for (int i = 0; i < loadMonsterData.Count; i++)
        {
            if (loadMonsterData[i]["ID"].ToString() == id)
            {
                stage_background = loadMonsterData[i]["STAGE_BACKGROUND"].ToString();
                monster_cnt = Int32.Parse(loadMonsterData[i]["MONSTER_COUNT"].ToString());
                monster_id = new string[monster_cnt];
                monster_pos = new int[monster_cnt];
                for(int k = 0; k<monster_cnt;k++)
                {
                    monster_id[k] = loadMonsterData[i]["MONSTER" + (k + 1) + "_ID"].ToString();
                    monster_pos[k] = Int32.Parse(loadMonsterData[i]["MONSTER" + (k + 1) + "_POINT"].ToString());
                }
                break;
            }
        }

    }





    private void Getparty()
    {

        for (int i = 0, n = 0; i < loadPartyData.Count; i++)
        {

            if (loadPartyData[i]["IS_PARTY"].ToString() == "1")
            {
                partyid.Add(loadPartyData[i]["ID"].ToString());
                n++;
                if (n == 2)
                    break;
            }
        }

    }





    private void SpawnMonster()
    {
        for (int i = 0; i < monster_cnt; i++)
        {
            GameObject nobj = (GameObject)GameObject.Instantiate(enemy);
            nobj.GetComponent<Battle_Monster>().id = monster_id[i];
            nobj.transform.position = enemypos[monster_pos[i]].position;
            nobj.name = "Monster" + i;
            nobj.gameObject.transform.parent = enemy.transform.parent;
            enemys[i] = nobj;
            nobj.SetActive(true);
        }
    }






    private void SpawnParty()
    {
        partys[0] = player;
        for(int i = 0; i < partyid.Count; i++)
        {
            GameObject nobj = (GameObject)GameObject.Instantiate(party);
            nobj.GetComponent<Battle_Party>().id = partyid[i];
            nobj.GetComponent<Battle_Party>().num = i + 1;
            PartyPanel[i + 1].SetActive(true);
            nobj.transform.position = partypos[i+1].position;
            nobj.name = "Party" + i;
            nobj.gameObject.transform.parent = party.transform.parent;
            partys[i+1] = nobj;
            nobj.SetActive(true);
        }
    }
}

  
