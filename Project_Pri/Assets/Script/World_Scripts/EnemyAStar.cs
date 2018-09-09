using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum State { Idle, Left, Right, Up, Down}

public class EnemyAStar : MonoBehaviour {


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
    [SerializeField] private checkObstacle up;
    [SerializeField] private checkObstacle down;
    [SerializeField] private checkObstacle left;
    [SerializeField] private checkObstacle right;
    private RaycastHit hit;
    private Vector3 movement;
    private Vector3 playerpos;
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private bool isTracing = false;
   
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    private Color myColor;
    private Vector3 moveVelocity = Vector3.zero;
    private GameObject traceTarget;
   [SerializeField] private int movementState = 0;
    private int quadrant = 0;
    private float xpoint = 0;
    private float ypoint = 0;
    private float distance = 0;

    private string dist = "";
    
    private InGamemanager ingamemanager;

    public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, 
	TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
	{
		protected override Double Heuristic(PathNode inStart, PathNode inEnd)
		{


			int formula = GameManager.distance;
			int dx = Math.Abs (inStart.X - inEnd.X);
			int dy = Math.Abs(inStart.Y - inEnd.Y);

			if(formula == 0)
				return Math.Sqrt(dx * dx + dy * dy); //Euclidean distance

			else if(formula == 1)
				return (dx * dx + dy * dy); //Euclidean distance squared

			else if(formula == 2)
				return Math.Min(dx, dy); //Diagonal distance

			else if(formula == 3)
				return (dx*dy)+(dx + dy); //Manhatten distance

		

			else 
				return Math.Abs (inStart.X - inEnd.X) + Math.Abs (inStart.Y - inEnd.Y);

			//return 1*(Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y) - 1); //optimized tile based Manhatten
			//return ((dx * dx) + (dy * dy)); //Khawaja distance
		}
		
		protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
		{
			return Heuristic(inStart, inEnd);
		}

		public MySolver(TPathNode[,] inGrid)
			: base(inGrid)
		{
		}
	} 
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
    void Start () {
   
	
		myColor = getRandomColor();

		startGridPosition = new gridPosition(spawnx,spawny);
        initializePosition(spawnx,spawny);
        InitPath();

        StartCoroutine(ChangeState());

    }
    private void OnEnable()
    {

        
            ingamemanager = InGamemanager.Instance;
        if (!isTracing)
        {
            StartCoroutine(ChangeState());

        }
        else
            StartCoroutine(move());
        
    }
    void InitPath()
    {
     
       
        endGridPosition = new gridPosition(Game.targetx, Game.targety);
        startGridPosition = new gridPosition(startx, starty);
        


        MySolver<MyPathNode, System.Object> aStar = new MySolver<MyPathNode, System.Object>(Game.grid);
        IEnumerable<MyPathNode> path = aStar.Search(new Vector2(startGridPosition.x, startGridPosition.y), new Vector2(endGridPosition.x, endGridPosition.y), null);



        //foreach (GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
        //{
        //    g.GetComponent<Renderer>().material.color = Color.white;
        //}


        updatePath();

        this.GetComponent<Renderer>().material.color = myColor;
    }

	public void findUpdatedPath(int currentX,int currentY)
	{


		MySolver<MyPathNode, System.Object> aStar = new MySolver<MyPathNode, System.Object>(Game.grid);
		IEnumerable<MyPathNode> path = aStar.Search(new Vector2(currentX, currentY), new Vector2(endGridPosition.x, endGridPosition.y), null);


		int x = 0;

		if (path != null) {
		
			foreach (MyPathNode node in path)
			{
				if(x==1)
				{
					nextNode = node;
					break;
				}

				x++;

			}


			foreach(GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
			{
				if(g.GetComponent<Renderer>().material.color != Color.red && g.GetComponent<Renderer>().material.color == myColor)
					g.GetComponent<Renderer>().material.color = Color.white;
			}


			foreach (MyPathNode node in path)
			{
				GameObject.Find(node.X + "," + node.Y).GetComponent<Renderer>().material.color = myColor;
			}
		}
		
		
		


		
	}


	Color getRandomColor()
	{
		Color tmpCol = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
		return tmpCol;

	}
	// Update is called once per frame
	void Update () {
        endGridPosition = new gridPosition(Game.targetx, Game.targety);
        quadrant = xpoint > 0 ? (ypoint > 0 ? 1 : 4) : (ypoint > 0 ? 2 : 3);

        this.gameObject.transform.GetComponentInChildren<Transform>().position = this.gameObject.transform.position;
        if (!isTracing)
        {

          //  transform.position += moveVelocity * speed * Time.deltaTime;

        }
        else if (isMoving) {

            isMoving = false;
            isTracing = true;
         
            initializePosition(startx, starty);
            updatePath();
           // StopAllCoroutines();
			//StartCoroutine(move());
		}
       
    }


	
	public float moveSpeed;
	
	public class gridPosition{
		public int x =0;
		public int y=0;

		public gridPosition()
		{
		}

		public gridPosition (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};

    IEnumerator ChangeState()
    {
        if (isTracing)
        {
            StopAllCoroutines();
            StartCoroutine(move());
            yield break;
        }
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
                        input.x = -1;

                }

             
                break;
            case State.Right:
                if (GameObject.Find((currentGridPosition.x + 1) + "," + currentGridPosition.y) != null)
                {
                    if (GameObject.Find((currentGridPosition.x + 1) + "," + currentGridPosition.y).GetComponent<Renderer>().material.color != Color.red)
                        input.x = 1;

                }


                break;
            case State.Up:
                if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y + 1)) != null)
                {
                    if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y + 1)).GetComponent<Renderer>().material.color != Color.red)
                        input.y = 1;
                }


                break;
            case State.Down:
                if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y - 1)) != null)
                {
                    if (GameObject.Find(currentGridPosition.x + "," + (currentGridPosition.y - 1)).GetComponent<Renderer>().material.color != Color.red)
                        input.y = - 1;
                }
                break;
            case State.Idle:
                input.x = 0;
                input.y = 0;

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

    private enum Orientation {
		Horizontal,
		Vertical
	};

	
	public IEnumerator move() {
         isMoving = false;
        // TraceDir();
        getNextMovement();
        startPosition = transform.position;
		t = 0;
		
		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
			                          startPosition.y);
			currentGridPosition.x += System.Math.Sign(input.x);
		} else {
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
			                          startPosition.y + System.Math.Sign(input.y) * Game.gridSize);
			
			currentGridPosition.x += System.Math.Sign(input.x);
			currentGridPosition.y += System.Math.Sign(input.y);
		}
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.9071f;
		} else {
			factor = 1f;
		}

	
		while (t < 1f) {
            
			t += Time.deltaTime * (moveSpeed/Game.gridSize) * factor;
			transform.position = Vector2.Lerp(startPosition, endPosition, t);
			yield return null;
		}

        if (!isTracing)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeState());
            yield break;
        }
		
	
        StartCoroutine(move());
        yield return 0;
		
		
		
		
		
	}
	
	void updatePath()
	{
		findUpdatedPath (currentGridPosition.x, currentGridPosition.y);
	}
	
	void getNextMovement()
	{
		updatePath();
		

		input.x = 0;
		input.y = 0;
		if (nextNode.X > currentGridPosition.x) {
			input.x = 1;
			
		}
		if (nextNode.Y > currentGridPosition.y) {
			input.y = 1;
		
		}
		if (nextNode.Y < currentGridPosition.y) {
			input.y = -1;
		
		}
		if (nextNode.X < currentGridPosition.x) {
			input.x = -1;
		
		}
		
	
	}
	
	public Vector2 getGridPosition(int x, int y)
	{
		float contingencyMargin = Game.gridSize*.10f;
		float posX = Game.gridBox.transform.position.x + (Game.gridSize*x) - contingencyMargin;
		float posY = Game.gridBox.transform.position.y + (Game.gridSize*y) + contingencyMargin;
		return new Vector2(posX,posY);	
		
	}
	public void JustInitPosition(int x, int y)
    {
        this.gameObject.transform.position = getGridPosition(x, y);
        currentGridPosition.x = x;
        currentGridPosition.y = y;
    }
	
	public void initializePosition(int x, int y)
	{
		this.gameObject.transform.position = getGridPosition (x, y);
        currentGridPosition.x = x;
		currentGridPosition.y = y;
		
		GameObject.Find(x + "," + y).GetComponent<Renderer>().material.color = Color.black; 

	}

   
    void TraceDir()
    {
        playerpos = traceTarget.transform.position;
        xpoint = playerpos.x - transform.position.x;
        ypoint = playerpos.y - transform.position.y;
        distance = Vector2.Distance(playerpos, transform.position);

        //if (quadrant == 1)
        //{

        //    if (up.tag == "obstacle" && right.tag == "obstacle")
        //    {
        //        StopAllCoroutines();

        //        StartCoroutine(ChangeState());
        //        isTracing = false;
        //    }
        //    else
        //        isTracing = true;

        //}
        //else if (quadrant == 2)
        //{

        //    if (up.tag == "obstacle" && left.tag == "obstacle")
        //    {
        //        StopCoroutine(move());
        //        StartCoroutine(ChangeState());
        //        isTracing = false;
        //    }
        //    else
        //        isTracing = true;
        //}
        //else if (quadrant == 3)
        //{
        //    if (down.tag == "obstacle" && left.tag == "obstacle")
        //    {
        //        StopAllCoroutines();
        //        StartCoroutine(ChangeState());
        //        isTracing = false;
        //    }
        //    else
        //        isTracing = true;
        //}
        //else if (quadrant == 4)
        //{
        //    if (down.tag == "obstacle" && right.tag == "obstacle")
        //    {
        //        StopAllCoroutines();
        //        StartCoroutine(ChangeState());
        //        isTracing = false;
        //    }
        //    else
        //        isTracing = true;
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            dist = "";
            moveVelocity = Vector3.zero;
            traceTarget = collision.gameObject;
            isMoving = true;
            isTracing = true;
           
        }
      
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //isMoving = false;
            //if (isMoving == true)
            //    isMoving = false;
            //else if (isTracing && isMoving)
            //    StartCoroutine(move());
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMoving = true;
            isTracing = false;
        
       //     StartCoroutine(ChangeState());

       
        }
    }



}
