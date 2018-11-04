using UnityEngine;
using System.Collections;

public class TurnToWall : MonoBehaviour {

	public GameManager Game;
    public GameObject grid;
  
    string[] splitter = new string[2];

    bool isWall;
    // Use this for initialization
    void Start () {

     
	}
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        splitter = grid.gameObject.name.Split(',');
        if (collision.gameObject.tag == "obstacle")
        {
            Game.addWall(int.Parse(splitter[0]), int.Parse(splitter[1]));
            isWall = true;
            grid.GetComponent<Renderer>().material.color = Color.red;
            this.GetComponent<BoxCollider2D>().enabled = false;

        }
        else if (collision.gameObject.tag == "Player")
        {

            Game.TargetPosition(int.Parse(splitter[0]), int.Parse(splitter[1]));
        }
        else if(collision.gameObject.tag == "Monster")
        {


        }
        else if (collision.gameObject.tag == "MonsterSpawn")
        {
            Game.spawnpos.Add(this.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        splitter = grid.gameObject.name.Split(',');
        if (collision.gameObject.tag == "obstacle")
        {

            Game.addWall(int.Parse(splitter[0]), int.Parse(splitter[1]));
            isWall = true;
            grid.GetComponent<Renderer>().material.color = Color.red;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "Player")
        {

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        splitter = grid.gameObject.name.Split(',');
        if (collision.gameObject.tag == "obstacle")
        {

       
        }
        else if (collision.gameObject.tag == "Player")
        {

        }
    }
    public void Initgrid()
    {
        isWall = false;
        grid.GetComponent<Renderer>().material.color = Color.white;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
