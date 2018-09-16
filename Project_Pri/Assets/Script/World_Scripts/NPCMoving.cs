using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class NPCMoving : MonoBehaviour
{


    public GameManager Game;
   

    public MyPathNode nextNode;
    bool gray = false;
    public MyPathNode[,] grid;


    public gridPosition currentGridPosition = new gridPosition();
    public gridPosition startGridPosition = new gridPosition();
    public gridPosition endGridPosition = new gridPosition();

    public int spawnx;
    public int spawny;

    private int startx;
    private int starty;
    [SerializeField] private float speed = 1f;
    private Vector3 movement;
    private Vector3 playerpos;
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
 
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
   
    private Vector3 moveVelocity = Vector3.zero;
    private GameObject traceTarget;
    [SerializeField] private int movementState = 0;
 
    private float xpoint = 0;
    private float ypoint = 0;
 
    private string dist = "";

    private InGamemanager ingamemanager;
    private Animator anim;

    public void StartPosition(int x, int y)
    {
        startx = x;
        starty = y;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

       // startGridPosition = new gridPosition(spawnx, spawny);
       // initializePosition(spawnx, spawny);
      

        //StartCoroutine(ChangeState());

    }
    private void Update()
    {
   
    }
    private void OnEnable()
    {
        //StartCoroutine(ChangeState());

    }
   


    public float moveSpeed;

    public class gridPosition
    {
        public int x = 0;
        public int y = 0;

        public gridPosition()
        {
        }

        public gridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    };

    IEnumerator ChangeState()
    {
       
        State c = (State)UnityEngine.Random.Range(0, 5);
       
        startPosition = transform.position;


        input.x = 0;
        input.y = 0;
        switch (c)
        {
            case State.Left:
                if (GameObject.Find((currentGridPosition.x - 1) + "," + currentGridPosition.y) != null)
                {
                    if (GameObject.Find((currentGridPosition.x - 1) + "," + currentGridPosition.y).GetComponent<Renderer>().material.color != Color.red)
                    {
                        input.x = -1;
                        anim.Play("Npc1_MoveLeft");
                    }
                    else
                       anim.Play("Npc1_MoveLeftIdle");
                }


                break;
            case State.Right:
                if (GameObject.Find((currentGridPosition.x + 1) + "," + currentGridPosition.y) != null)
                {
                    if (GameObject.Find((currentGridPosition.x + 1) + "," + currentGridPosition.y).GetComponent<Renderer>().material.color != Color.red)
                    {
                        input.x = 1;
                        anim.Play("Npc1_MoveRight");
                    }
                    else
                     anim.Play("Npc1_MoveRightIdle");
                }


                break;
            case State.Up:
                if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y + 1)) != null)
                {
                    if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y + 1)).GetComponent<Renderer>().material.color != Color.red)
                    {
                        input.y = 1;
                        anim.Play("Npc1_MoveUp");
                    }
                    else
                        anim.Play("Npc1_MoveUpIdle");
                }


                break;
            case State.Down:
                if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y - 1)) != null)
                {
                    if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y - 1)).GetComponent<Renderer>().material.color != Color.red)
                    {
                        input.y = -1;
                        anim.Play("Npc1_MoveDown");
                    }
                    else
                        anim.Play("Npc1_MoveDownIdle");
                }
                break;
            case State.Idle:
                input.x = 0;
                input.y = 0;
                anim.Play("Npc1_MoveDownIdle");
                break;
         

        }
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
                                      startPosition.y);
            currentGridPosition.x += System.Math.Sign(input.x);
        }
        else
        {
            endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
                                      startPosition.y + System.Math.Sign(input.y) * Game.gridSize);

            currentGridPosition.x += System.Math.Sign(input.x);
            currentGridPosition.y += System.Math.Sign(input.y);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.9071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            //  if (!isTracing)
            //  {
            t += Time.deltaTime * (speed / Game.gridSize) * factor;
            transform.position = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
            // }
        }

        //yield return new WaitForSeconds(1f);


        //if (movementState == 0)
        //    Debug.Log("stop");
        //else
        //    Debug.Log("play");

        
        StartCoroutine(ChangeState());

        yield return 0;
    }

    private enum Orientation
    {
        Horizontal,
        Vertical
    };




    public Vector2 getGridPosition(int x, int y)
    {
        float contingencyMargin = Game.gridSize * .10f;
        float posX = Game.gridBox.transform.position.x + (Game.gridSize * x) - contingencyMargin;
        float posY = Game.gridBox.transform.position.y + (Game.gridSize * y) + contingencyMargin;
        return new Vector2(posX, posY);

    }
    public void JustInitPosition(int x, int y)
    {
        this.gameObject.transform.position = getGridPosition(x, y);
        currentGridPosition.x = x;
        currentGridPosition.y = y;
    }

    public void initializePosition(int x, int y)
    {
        this.gameObject.transform.position = getGridPosition(x, y);
        currentGridPosition.x = x;
        currentGridPosition.y = y;

        GameObject.Find(x + "," + y).GetComponent<Renderer>().material.color = Color.black;

    }

    bool once = false;
    string[] splitter = new string[2];
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkBox")
        {
            if (!once)
            {
                splitter = collision.gameObject.transform.parent.gameObject.name.Split(',');
                once = true;
             
                currentGridPosition.x = int.Parse(splitter[0]);
                currentGridPosition.y = int.Parse(splitter[1]);
                StartCoroutine(ChangeState());
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }



}