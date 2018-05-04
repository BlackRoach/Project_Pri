using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;
    public static BattleManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = GameObject.Find("*Manager").AddComponent<BattleManager>();
        }
    }
    private void Awake()
    {
        instance = this;
    }


    [SerializeField] private GameObject skillButtons;
    [SerializeField] private GameObject commandButton;
    [SerializeField] private Image attackButton;
    [SerializeField] private Queue<GameObject> attackQueue = new Queue<GameObject>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemys;
    private GameObject presentlyAttackChar;
    private float fillamount;
    private float currentCoolTime;
    private float cooltime = 10f;
    private bool isAttack = false;
    public bool isFightWhile = false;
    public bool isCommandOn = false;
    public bool isAttack_readonly { get { return isAttack; } }
    // Use this for initialization
    void Start()
    {
     
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
                Debug.Log(1);
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
        if (attackQueue.Count != 0 && !isFightWhile)
        {
            
            isFightWhile = true;
            presentlyAttackChar = attackQueue.Dequeue();
            if (presentlyAttackChar.tag == "Monster")
                presentlyAttackChar.GetComponent<Battle_Character>().MoveToEnemy(player);
            else if (presentlyAttackChar.tag == "Player")
            {

                presentlyAttackChar.GetComponent<Battle_Character>().MoveToEnemy(enemys[Random.Range(0,2)]);
            }
        }
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
   
}

  
