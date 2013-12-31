using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public GameObject player;
	public float attackTimer;
	public float coolDown;
	public float distanceOfAttack;
	public float angleOfAttack; 
	private float missTime = 1000f;
	public int damage = 10;
	

	void Start () 
	{
		coolDown = 3f;
		attackTimer = 0f;
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	void Update () 
	{
		if ( attackTimer > 0 ) // Check if Enemy attack  is still on cooldown
			attackTimer -= Time.deltaTime; // Reduce time of cooldown
		else if ( attackTimer <= 0 && Vector3.Distance( player.transform.position, transform.position ) < distanceOfAttack // Checks if enemy attack isn't on cooldown, and is near the player
			&& Vector3.Dot((player.transform.position - transform.position + Vector3.one).normalized, transform.forward) > angleOfAttack) // and is facing the player 
		{
			if ( missTime == 1000f )// The player has a chance to avoid the attack by moving
			{
				missTime =  animation["attack"].length - .55f; //
				animation.Play("attack");
			}
			else if ( missTime < 0 ) // the player did not get out of the way in time so apply damage to player
			{
				attackTimer = 0;
				attackTimer = coolDown;
				PlayerHealth pH = (PlayerHealth)player.GetComponent("PlayerHealth");
				pH.AdjustHealth(-damage);
				missTime = 1000f;
			}		
		}
		else 
			missTime = 1000f;	
		if ( missTime < 20 && missTime > 0 ) 
			missTime -= Time.deltaTime;			
	}
	
}
