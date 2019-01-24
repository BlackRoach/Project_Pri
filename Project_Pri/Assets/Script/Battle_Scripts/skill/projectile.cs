using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{


    private string id;

    private float x;
    private float y;
    private float angle;
    private float speed;
    private float max_t;
    private float time;
    private bool complete = false;


    void Update()
    {
        if (complete)
        {
            time += Time.deltaTime;
            this.gameObject.transform.Translate(0, 0.1f, 0);
            if (max_t < time)
                Destroy(this.gameObject);
        }
    }
    public void Setprojectile(Vector2 s_pos, Vector2 t_pos, float speed,float t, string id)
    {
        x = t_pos.x - s_pos.x;
        y = t_pos.y - s_pos.y;

        angle = (y / x) * (180 / Mathf.PI);
        this.id = id;
        this.max_t = t;
        this.speed = speed;


        this.gameObject.transform.position = s_pos;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, (x < 0 ? 90 : -90) + angle);
        time = 0;
        complete = true;
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.tag == "Monster")
        {
            SkillManager.Instance.Projectile_Hit(collision.gameObject, id);
            Destroy(this.gameObject);
        }
    
    }
}