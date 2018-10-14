using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class GameManager : MonoBehaviour {
   
    public MyPathNode[,] grid;
	public GameObject enemy;
	public GameObject gridBox;
    public List<GameObject> spawnpos;
	public int gridWidth;
	public int gridHeight;
    public int monsterNum;

    public int[] spawnx;
    public int[] spawny;
    public float gridSize;
	public GUIStyle lblStyle;

	public static string distanceType;

    public int targetx;
    public int targety;

    
    [SerializeField] private GameObject WorldObject;

    //This is what you need to show in the inspector.
    public static int distance = 2;

    string[] splitter = new string[2];

    void Start() {


        //Generate a grid - nodes according to the specified size
        grid = new MyPathNode[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                //Boolean isWall = ((y % 2) != 0) && (rnd.Next (0, 10) != 8);
                Boolean isWall = false;
                grid[x, y] = new MyPathNode()
                {
                    IsWall = isWall,
                    X = x,
                    Y = y,
                };
            }
        }

        //instantiate grid gameobjects to display on the scene
        createGrid();

        //instantiate enemy object
        StartCoroutine(MonsterSpawn());
        
	
	}



    public void TargetPosition(int x, int y) { targetx = x; targety = y; }
   
    public void createGrid()
	{
	//Generate Gameobjects of GridBox to show on the Screen
		for (int i =0; i<gridHeight; i++) {
			for (int j =0; j<gridWidth; j++) {
				GameObject nobj = (GameObject)GameObject.Instantiate(gridBox);
				nobj.transform.position = new Vector2(gridBox.transform.position.x + (gridSize*j), gridBox.transform.position.y + (1f*i));
				nobj.name = j+","+i;

				nobj.gameObject.transform.parent = gridBox.transform.parent;
				nobj.SetActive(true);

			}
		}
	}
    public void destroyGrid()
    {
        Transform[] childList = GameObject.Find("TracePanel").GetComponentsInChildren<Transform>(true);
        if (childList != null)
        {
          
            for (int i = 3; i < childList.Length; i++)
            {
                //if (childList[i] != transform&&childList[i].name !="GridBox")
                //    Destroy(childList[i].gameObject);
                if (childList[i].name == "check")
                    childList[i].GetComponent<TurnToWall>().Initgrid();
            }
        }
    }
    void createEnemy(int x,int y)
	{
		GameObject nb = (GameObject)GameObject.Instantiate (enemy);
        nb.transform.parent = WorldObject.transform;
        nb.GetComponent<EnemyAStar>().spawnx = x;
        nb.GetComponent<EnemyAStar>().spawny = y;
        nb.SetActive (true);
	}


	
	public void addWall (int x, int y)
	{
		grid [x, y].IsWall = true;
	}
	
	public void removeWall (int x, int y)
	{
		grid [x, y].IsWall = false;
	}

    IEnumerator MonsterSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < monsterNum; i++)
        {
            splitter = spawnpos[i].gameObject.name.Split(',');
            createEnemy(int.Parse(splitter[0]), int.Parse(splitter[1]));
           
        }
        yield return null;
    }
}
