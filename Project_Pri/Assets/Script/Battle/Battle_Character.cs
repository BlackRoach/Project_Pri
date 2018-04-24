using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Character : MonoBehaviour
{

    [SerializeField] private GameObject effect;
    [SerializeField] private Image guageBar;
    [SerializeField] private Image hpBar;
    [SerializeField] protected float filled_speed;
    [SerializeField] protected float max_gauge;
    protected BattleManager battleManager;
    protected int hp = 100;
    public bool isInQ = false;
    protected float progress_gauge = 0;
    [SerializeField] Vector2 own_position;
    private void Awake()
    {
        own_position = this.gameObject.transform.position;
        battleManager = BattleManager.Instance;
    }
   

    public void MoveToEnemy(GameObject enemy)
    {
        StartCoroutine(ImoveToEnemy(enemy));
    }
    public void BackToOwnPosition()
    {
        StartCoroutine(IbackToOwnPosition());
        battleManager.DeleteInArray();
    }
    public void Attack(GameObject enemy)
    {
        enemy.GetComponent<Battle_Character>().Attacked();

    }
    public void Attacked()
    {
        effect.SetActive(true);
        hp -= 10;
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
        yield return null;
    }
    public IEnumerator ImoveToEnemy(GameObject enemy)
    {
        Vector2 enemypos = new Vector2(enemy.transform.position.x - 1, enemy.transform.position.y);
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * 1.0f;
            Vector3 pos = this.gameObject.transform.localPosition;
            pos = Vector2.Lerp(own_position, enemypos, t);
            this.gameObject.transform.localPosition = pos;
            yield return 0;
        }

        yield return null;
        StartCoroutine(IbackToOwnPosition());
    }
}
