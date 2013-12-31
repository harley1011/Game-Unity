using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerAttack : MonoBehaviour {

	public float meleeCoolDown;
	public int playerMeleeAttackDamage;
	private List<GameObject> rangeObjects = new List<GameObject>();
	private float attackTimer;
	
	void Start () 
	{
		attackTimer = 0f;
	}
	
	void Update () {
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // Get the new list of enemys in the world
		if ( attackTimer > 0 )
			attackTimer -= Time.deltaTime;
		if ( attackTimer < 0 )
			attackTimer = 0;
		if ( Input.GetButtonDown("Fire1" ) && attackTimer == 0f ) 
		{
			attackTimer = meleeCoolDown;
			foreach ( GameObject target in enemyList ) // Go through all enemys and check if they are within require range and direction of player to be damaged
			{
				float distance = Vector3.Distance(target.transform.position, transform.position);
				Vector3 dir = (target.transform.position - transform.position + Vector3.one).normalized;
				float direction = Vector3.Dot(dir, transform.forward);
				if ( distance < 8f && direction > .6)
				{
					EnemyHealth enemyClass = (EnemyHealth)target.GetComponent("EnemyHealth");
					enemyClass.AdjustHealth(playerMeleeAttackDamage * -1);	

				}			
			}

		}
	}
}
