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

    [SerializeField] private GameObject resultWindow;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private GameObject defeatPopup;
    [SerializeField] private GameObject skillButtons;
    [SerializeField] private GameObject commandButton;
    [SerializeField] private GameObject megami;
    [SerializeField] private Image attackButton;
    [SerializeField] private Image backGround;
    [SerializeField] private Queue<GameObject> attackQueue = new Queue<GameObject>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] partys;
    [SerializeField] private GameObject[] enemys;

    private GameObject presentlyAttackChar;
    private InGamemanager ingameManager;
    private JsonFileWriter jsonFileWriter;
    private JsonData loadData;

    private string group_id;
    private string stage_background;
    private int monster_cnt;
    private int[] monster_pos;
    private string[] monster_id;
   
    private float fillamount;
    private float currentCoolTime;
    private float cooltime = 10f;
    private bool isAttack = false;
    private bool isResult = false;

    public bool isFightWhile = false;
    public bool isCommandOn = false;
    public bool isAttack_readonly { get { return isAttack; } }

    public GameObject enemy;
    public GameObject party;

    public Transform[] enemypos;
    public Transform[] partypos;

    // Use this for initialization
    void Start()
    {
        resultWindow.SetActive(false);
        ingameManager = InGamemanager.Instance;
        jsonFileWriter = JsonFileWriter.Instance;
        group_id = PlayerPrefs.GetString("Current_group_id");
        loadData = jsonFileWriter.SerializeData("MONSTER_GROUP_TABLE");

        LoadData(group_id);
        enemys = new GameObject[monster_cnt];
        backGround.sprite = Resources.Load<Sprite>("전투배경/" + stage_background);
        SpawnMonster();
        
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;

            if (hit)
            {
                if (hit.transform.CompareTag("Monster") && isAttack)
                {
                    Time.timeScale = 1;
                    hit.transform.gameObject.GetComponent<Battle_Monster>().Attacked();
                    player.GetComponent<Battle_Player>().Skillused();
                    isAttack = false;
                    isCommandOn = false;
                    skillButtons.SetActive(false);
                    StartCoroutine(Cooltime(attackButton));
                }


            }


        }
        // 큐에 의한 신호등
        if (attackQueue.Count != 0 && !isFightWhile && !isResult)
        {
            
            isFightWhile = true;
            presentlyAttackChar = attackQueue.Dequeue();
            if (presentlyAttackChar.tag == "Monster")
                presentlyAttackChar.GetComponent<Battle_Character>().MoveToEnemy(player);
            else if (presentlyAttackChar.tag == "Player")
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
    public void AttackButton()
    {
        Time.timeScale = 0;
        if (attackButton.fillAmount >= 1)
            isAttack = true;
    }
    // 배틀 매니저에 있는 큐 자료구조에 공격할 캐릭터를 집어넣는다.
    public void AddToArray(GameObject character)
    {
        attackQueue.Enqueue(character);
    }
    public void CommandButtonOn()
    {
        commandButton.SetActive(true);
    }
    public void SkillStatusOn()
    {
        commandButton.SetActive(false);
        skillButtons.SetActive(true);
    }
    public void BackToWorld()
    {
        ingameManager.opponent.SetActive(false);
        ingameManager.TurnOnWorldObjects();
        ingameManager.isRespawn = true;
        ingameManager.isFight = true;
        SceneManager.LoadScene("WorldMap");
       
    }
    private void LoadData(string id)
    {
        for (int i = 0; i < loadData.Count; i++)
        {
            if (loadData[i]["ID"].ToString() == id)
            {
                stage_background = loadData[i]["STAGE_BACKGROUND"].ToString();
                monster_cnt = Int32.Parse(loadData[i]["MONSTER_COUNT"].ToString());
                monster_id = new string[monster_cnt];
                monster_pos = new int[monster_cnt];
                for(int k = 0; k<monster_cnt;k++)
                {
                    monster_id[k] = loadData[i]["MONSTER" + (k + 1) + "_ID"].ToString();
                    monster_pos[k] = Int32.Parse(loadData[i]["MONSTER" + (k + 1) + "_POINT"].ToString());
                }
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
}

  
