using UnityEngine;
using System.Collections.Generic;

public class EnemyPathfinding : MonoBehaviour {
	public Transform player;
	public List<GameObject> Waypoints = new List<GameObject>();
	private int currentWaypoint;
	public bool loop = true;
	private Vector3 playerLastPosition;
	private bool enemySawPlayer = false;
	public float moveSpeed;
	public float rangeOfAttack;

	void MoveToPlayerOrWaypoint ()
	{
		Vector3 direction = player.position - transform.position;
		direction.Normalize();
        if ( Vector3.Distance( player.position, transform.position ) < 50 && !animation.IsPlaying("attack") )
       	{
			RaycastHit hit;
			Physics.Raycast(transform.position + (Vector3.up *5), direction, out hit);
			if ( hit.collider.gameObject.tag == "Player" && Vector3.Distance( player.position, transform.position ) > rangeOfAttack )
			{
				playerLastPosition = player.position;
				enemySawPlayer = true;
				if (Vector3.Distance( player.position, transform.position ) > 7f)
				{	
					animation.Play ("walk");
					Vector3 delta = player.position - transform.position;
					delta.Normalize();
				    transform.position = transform.position + (delta * moveSpeed * Time.deltaTime); 	
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(player.position - transform.position), 2 * Time.deltaTime );
				}
				else
				{
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(player.position - transform.position), 2 * Time.deltaTime );
					
				}
			}
			else if ( Vector3.Distance( player.position, transform.position ) < rangeOfAttack )
			{
				enemySawPlayer = false;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(player.position - transform.position), 10 * Time.deltaTime );
			}
		}
			else if (!animation.IsPlaying("attack") && Waypoints.Count != 0)
			{
				if (currentWaypoint < Waypoints.Count)
				{
					Vector3 target = Waypoints[currentWaypoint].transform.position;
					target.y = 0;
					Vector3 tempEnemy = transform.position;
					tempEnemy.y = 0;
					Vector3 moveDirection = target - tempEnemy;
					if ( moveDirection.magnitude < 2f)
					{
						animation.Play ("idle");
						currentWaypoint++; // goes through array
					}
					else 
					{
						animation.Play ("walk");
						Vector3 delta = target - transform.position;
						delta.Normalize();
						delta.y = 0f;
				    	transform.position = transform.position + (delta * moveSpeed * Time.deltaTime); 
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(target - transform.position), 2 * Time.deltaTime );
					}	
				}
				else 
					{ 
						animation.Play ("idle");
						currentWaypoint = 0;
						Waypoints.Reverse();
					}
			
		}
	}
	void Start () {
		player = GameObject.FindWithTag("Player").transform;
		foreach ( GameObject target in Waypoints )
		{
			target.active = false;	
		}
	}

	void Update () {
		
		EnemyHealth eh = (EnemyHealth)gameObject.GetComponent("EnemyHealth");
		
		if (!eh.dead)
		{
			MoveToPlayerOrWaypoint ();
		}
		
     	
    }   	
}
