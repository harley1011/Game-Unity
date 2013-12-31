using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	public int sizeOfMap;
	
	private float wallScaleX;
	private float wallScaleY;
	private float wallScaleZ;
	//public List<List<cellBlock>> maze = new List<List<cellBlock>>();
	
	//public ArrayList maze = new ArrayList();
	private cellBlock[,] maze;
	private Position currentPosition;
	private Position endPosition;
	private Position next;
	private Position current;
	private directions aDirection;
	private Position playerPosition;
	public GameObject[,] mazeWalls;
	public GameObject wallPrefab;

	
	public enum directions { Up, Right, Down, Left };
	public struct cellBlock
	{
		public bool top;
		public bool left;
		public bool bottom;
		public bool right;
		public bool visited;
	};
	//public List<List<cellBlock>> maze = new List<List<cellBlock>>();
	public struct Position
	{
		public int x;
		public int y;
	};
	// Use this for initialization
	public void Start () {
		wallScaleX = wallPrefab.transform.localScale.x;
		wallScaleY = wallPrefab.transform.localScale.y;
		wallScaleZ = wallPrefab.transform.localScale.z;
		currentPosition.x  = 0;
		currentPosition.y = 0;
		maze = new cellBlock[sizeOfMap,sizeOfMap];
		initilize();
		
		//GameObject wallPrefab = (GameObject)Resources.Load ("wall"); 
		endPosition = makeStartAndEnd();
		buildMaze();
		makeWalls();
		
	}
	public void makeWalls()
	{
		Vector3 positionOfWall;
		mazeWalls = new GameObject[sizeOfMap,sizeOfMap];
		for ( int i = 0; i <= sizeOfMap - 1; i++ )
		{
			positionOfWall = new Vector3( wallScaleX * i + (wallScaleX/2 - wallScaleZ), 0, 5 );
			mazeWalls[i,0] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.identity);
			
			for ( int k = 0; k <= sizeOfMap - 1; k++ )
			{
				if ( i == 0 )
				{
					positionOfWall = new Vector3( 5 * i + 1, 0, wallScaleX * k + (wallScaleX/2 - wallScaleZ));
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.AngleAxis(90, Vector3.up));		
				}
				if ( maze[i,k].bottom )
				{
					positionOfWall = new Vector3( wallScaleX * i + 40, 0, 40 * k  + 25);
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.AngleAxis(90, Vector3.up));	
				}
				if ( maze[i,k].right )
				{
					positionOfWall = new Vector3( wallScaleX * i + 20, 0, 40 * k + 45);
					mazeWalls[i,k] = (GameObject)Instantiate(wallPrefab, positionOfWall, Quaternion.identity);	
					
				}
			}
			
		}		
		
	}
	public Position makeStartAndEnd()
	{
		//Random randomGenerate = new Random();
		int random =  UnityEngine.Random.Range(0,4);
		int random2 = UnityEngine.Random.Range(0,4);
		
		if ( random == 0 ) // top Left corner
		{
			playerPosition.x = 0;
			playerPosition.y = 0;
			random2 = 2;
		}
		if ( random == 1 ) //top right corner
		{
			playerPosition.x = sizeOfMap - 1;
			playerPosition.y = 0;
			random2 = 3;
	
		}
		if ( random == 2 ) // bottom right corner
		{
			playerPosition.x = sizeOfMap - 1;
			playerPosition.y = sizeOfMap - 1;	
			random2 = 0;
		}
		if ( random == 3 ) // bottom left corner
		{
			playerPosition.x = 0;
			playerPosition.y = sizeOfMap - 1;
		    random2 = 1;
		}
		while ( random == random2 )
		{
			random2 = UnityEngine.Random.Range(0,4);
		}
	
	
		if ( random2 == 0 ) 
		{
			endPosition.x = 0;
			endPosition.y = 0;
			return endPosition;
		}
		if ( random2 == 1 ) 
		{
			endPosition.x = sizeOfMap - 1;
			endPosition.y = 0;
			return endPosition;
		}
		
		if ( random2 == 2 ) 
		{
			endPosition.x = sizeOfMap - 1;
			endPosition.y = sizeOfMap - 1;
			return endPosition;
		}
		if ( random2 == 3 ) 
		{
			endPosition.x = 0;
			endPosition.y = sizeOfMap - 1;
			return endPosition;
		}
		return endPosition;
		
	}
	public void buildMaze()
	{
		Stack theStack = new Stack();	
		int visited = 1;
		current.x = 0;
		current.y = 0;

	    maze[0,0].visited = true;// .visited= true;
		
		while ( visited < sizeOfMap * sizeOfMap )
		{
			next = randomDirection();
			if ( next.x == -1 && next.y == -1 )
			{
				current = (Position)theStack.Pop();
			}
			else
			{
				theStack.Push(current);
				visited++;
				next.x += current.x;
				next.y += current.y;
				maze[next.x,next.y].visited= true;
				if (aDirection == directions.Up)
				{
					maze[current.x,current.y].top = false;
					maze[next.x,next.y].bottom = false;
				}
				else if (aDirection == directions.Right)
				{
					maze[current.x,current.y].right = false;
					maze[next.x,next.y].left = false;
				}
				else if (aDirection == directions.Down)
				{
					maze[current.x,current.y].bottom = false;
					maze[next.x,next.y].top = false;
				}
				else if (aDirection == directions.Left)
				{
					maze[current.x,current.y].left = false;
					maze[next.x,next.y].right = false;
				}
				current = next;			
			}	
		}
		
		
	}
	public Position randomDirection()
	{
		bool[] ar = new bool[] {false,false,false,false};
		Position Chosen;
		Chosen.x = 0;
		Chosen.y = 0;

		while( !ar[0] || !ar[1] || !ar[2] || !ar[3] )
		{
			int random = UnityEngine.Random.Range(0,4);;
			if ( random == 0 && !ar[0])
			{
				ar[0] = true;

				if (current.x > 0 && maze[current.x - 1,current.y].visited == false)
				{
					aDirection = directions.Up;
					Chosen.x--;
					return Chosen;
				}
			}
			else if (random == 1 && !ar[1] )
			{
				ar[1] = true;
				
				if ( current.y < ( sizeOfMap - 1) && maze[current.x,current.y + 1].visited == false)
				{
					aDirection = directions.Right;
					Chosen.y++;
					return Chosen;
				}
			}
			else if (random == 2 && !ar[2] )
			{
				ar[2] = true;
				
				if (current.x <(sizeOfMap  - 1) && maze[current.x + 1,current.y].visited == false)
				{
					aDirection = directions.Down;
					Chosen.x++;		
					return Chosen;
				}
			}
            else if (random == 3 && !ar[3] )
			{
				ar[3] = true;
			
				if (current.y > 0 && maze[current.x,current.y - 1].visited == false)
				{
					aDirection = directions.Left;
					Chosen.y--;
					return Chosen;
				}
			}
		}
		Chosen.x = -1;
		Chosen.y = -1;
		return Chosen;
	}
	public void initilize()
	{
		for ( int i = 0; i < sizeOfMap; i++ )
		{
			for ( int n = 0; n < sizeOfMap; n++)
			{
				maze[i,n].left = true;
				maze[i,n].top = true;
				maze[i,n].right = true;
				maze[i,n].bottom = true;
				maze[i,n].visited = false;
			}
	}
		
	}
}
