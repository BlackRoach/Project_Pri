using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour {

    private static BattleManager instance = null;
    public static BattleManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("*Manager").AddComponent<BattleManager>();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private Image attackButton;
    private float fillamount;
    private float currentCoolTime;
    private float cooltime = 10f;
    private bool isAttack = false;

    public bool isAttack_readonly{   get{return isAttack;}}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = 0;
         
            if (hit)
            {
                Debug.Log(1);
                if(hit.transform.CompareTag("Monster")&& isAttack)
                {
                    hit.transform.gameObject.GetComponent<Battle_Monster>().Effect();
                    isAttack = false;
                    StartCoroutine(Cooltime(attackButton));
                }
               

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
        if(attackButton.fillAmount >= 1)
            isAttack = true;
    }

}
