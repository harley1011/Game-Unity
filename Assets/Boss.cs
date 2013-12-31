using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	private GameObject player;
	public int bossDamage;
	private float chargeStun = 10;
	public bool playerInRoom {get; set;} //false;
	private RaycastHit hit;
	private  Vector3 runTo;
	void Start () {
		chargeStun = 0;
		playerInRoom = false;
	}
	
	void Update () 
	{
		if ( playerInRoom )
		{
			player = GameObject.FindGameObjectWithTag("Player");
			//Debug.DrawRay(transform.position + Vector3.up * 10, new Vector3(0,player.transform.position.y,0 ) -  new Vector3(0,transform.position.y,0 ), Color.green, 2, false);
			//Debug.DrawRay(transform.position + Vector3.up * 5, (player.transform.position + (Vector3.up *5 )) - transform.position, Color.red);
			if ( chargeStun == 10 )
			{
				player = GameObject.FindGameObjectWithTag("Player");
				//Debug.DrawRay(transform.position + Vector3.up * 5, player.transform.position - transform.position, Color.green);
				Physics.Raycast(transform.position + (Vector3.up *5), player.transform.position - transform.position, out hit);
				runTo = hit.transform.position;
				animation.Play("roar");
				animation.PlayQueued("run", QueueMode.CompleteOthers);
				chargeStun = 7;
				
			}
			else if ( chargeStun == 7 && !animation.IsPlaying("roar") )
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 4 * Time.deltaTime );
				Debug.Log (Vector3.Dot((player.transform.position - transform.position + Vector3.one).normalized, transform.forward));
				if ( Vector3.Dot((player.transform.position - transform.position + Vector3.one).normalized, transform.forward) > .99f )
					chargeStun = 5;
			}
			else if ( chargeStun == 5 && !animation.IsPlaying("roar")  )
			{
				animation.Play("run");
				//transform.position = Vector3.Lerp(transform.position, runTo, Time.deltaTime * 1f);
				transform.Translate(Vector3.forward);
			}
			else if ( chargeStun > 0 && !animation.IsPlaying("roar") )
			{				
				animation.Play("idle");
				chargeStun -= Time.deltaTime;
				
			}
			else if ( !animation.IsPlaying("roar") )
				chargeStun = 10;	
		}

	}
	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log (collision.gameObject.tag);
		if ( collision.gameObject.tag == "wall" && chargeStun == 5 )
			chargeStun = 2;
		else if ( collision.gameObject.tag == "Player")
		{
			PlayerHealth pH = (PlayerHealth)player.GetComponent("PlayerHealth");
			pH.AdjustHealth(-bossDamage);
		}
		
	}
}
