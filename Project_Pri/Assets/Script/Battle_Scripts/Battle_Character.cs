using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class Battle_Character : MonoBehaviour
{

    [SerializeField] private GameObject effect;
    [SerializeField]protected GameObject StatusUI;
    protected Image guageBar;
    protected Image hpBar;
    [SerializeField]protected GameObject status_t;
    [SerializeField] protected float filled_speed = 30;
    [SerializeField] protected float max_gauge = 100;
    protected string sd_model;
   

    protected BattleManager battleManager;
    protected JsonFileWriter jsonFileWriter;
    protected JsonData loadData;
    protected float c_hp;
    public float hp;
    public float atk;
    public float def;
    public float mag;
    public float rep;
    public float sp;


    protected bool isAlive = true;

    protected string name;
    protected float progress_gauge = 0;

    public GameObject status;       //생성용
    public GameObject status_T;     //생성용
    public int num;
    public int attack_num;
    public int[] attack_val;
    public int[] skillCoolTime;
    public float[] skillCoolAmount;
    public string[] attack_id;
    public string id;

    public bool isInQ = false;
    public bool isAlive_readonly { get { return isAlive; } }
    public bool isFight = false;
    public float skill_guage;
    private Vector2 own_position;

 
   
    private void Awake()
    {
        
        own_position = this.gameObject.transform.position;
        battleManager = BattleManager.Instance;
    }
    protected void StatusInit()
    {
        GameObject nStatus = (GameObject)GameObject.Instantiate(status);
        nStatus.gameObject.transform.parent = status.transform.parent;
        StatusUI = nStatus;
        hpBar = StatusUI.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        guageBar = StatusUI.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        StatusUI.transform.localScale = new Vector3(1, 1, 1);
        StatusUI.transform.localPosition = Vector3.zero;
        StatusUI.SetActive(true);

        GameObject nStatus_t = (GameObject)GameObject.Instantiate(status_T);
        nStatus_t.gameObject.transform.parent = status_T.transform.parent;
        status_t = nStatus_t;
        status_t.transform.localScale = new Vector3(1, 1, 1);
        status_t.SetActive(true);
    }
    protected void update()
    {
        StatusUI.transform.position = new Vector2(this.transform.position.x+.1f,
                                             this.transform.position.y-1.4f);
        status_t.transform.position = new Vector2(this.transform.position.x + 2f,
                                             this.transform.position.y);
        hpBar.fillAmount = hp * (1/c_hp);
        if (progress_gauge >= max_gauge && !isInQ)
        {
            battleManager.AddToArray(this.gameObject);
            isInQ = true;
        }
        else if (progress_gauge <= max_gauge)
        {
            progress_gauge += Time.deltaTime * filled_speed;
            guageBar.fillAmount = progress_gauge * 0.01f;
        }
        //if (hp < 0)
        //{
        //    isAlive = false;
        //    this.gameObject.SetActive(false);
        //    StatusUI.SetActive(false);
        //}


    }
    public void MoveToEnemy(GameObject enemy)
    {
        StartCoroutine(ImoveToEnemy(enemy));
    }
    public void BackToOwnPosition()
    {
        StartCoroutine(IbackToOwnPosition());
        
    }
    public void Attack(GameObject enemy)
    {
        float ms, ms1, ms2;
        float _as, _as1, _as2;

        float dmg;

        ms1 = mag - enemy.GetComponent<Battle_Character>().rep;
        ms2 = 1;
        ms = ms1 > ms2 ? ms1 : ms2;




        _as1 = atk - enemy.GetComponent<Battle_Character>().def;
        _as2 = 1;
        _as = _as1 > _as2 ? _as1 : _as2;
        dmg = _as + ms;



        enemy.GetComponent<Battle_Character>().Attacked(dmg);

    }
    public void Attacked(float val)
    {
        effect.SetActive(true);
        DamageTextContoller.CreateDamageText(-val, transform);
        hp -= val;
        if (hp < 0)
            isAlive = false;

    }
    public void SetCoolTime(int i, int time)
    {
        skillCoolTime[i] = time;
    }

    public IEnumerator IbackToOwnPosition()
    {
        Vector2 thisPosition = this.gameObject.transform.position;
        float t = 0.0f;
        while (t < 1.0f)
        {

            t += Time.deltaTime * 1.0f;
            Vector3 pos = this.gameObject.transform.localPosition;
            pos = Vector2.Lerp(thisPosition, own_position, t);
            this.gameObject.transform.localPosition = pos;
            yield return 0;
        }
      
        isInQ = false;
        battleManager.isFightWhile = false;
        progress_gauge = 0;
        isFight = false;
        yield return null;
    }
    public IEnumerator ImoveToEnemy(GameObject enemy)
    {
        isFight = true;
        Vector2 enemypos;

        if (enemy.transform.position.x > own_position.x)
            enemypos = new Vector2(enemy.transform.position.x - 1, enemy.transform.position.y);
        else
            enemypos = new Vector2(enemy.transform.position.x + 1, enemy.transform.position.y);

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * 1.0f;
            Vector3 pos = this.gameObject.transform.localPosition;
            pos = Vector2.Lerp(own_position, enemypos, t);
            this.gameObject.transform.localPosition = pos;
            yield return 0;
        }
        Attack(enemy);
        yield return null;
        StartCoroutine(IbackToOwnPosition());
    }
}
