using UnityEngine;
using System.Collections;

public class EnemyRangeAttack : MonoBehaviour {
	public float coolDown;
	private float attackTimer;
	public float damage;
	public float rangeOfAttack;
	public float projectileSpeed;
	private GameObject player;
	public GameObject projectile;
	void Update () 
	{
		if ( attackTimer > 0 )
			attackTimer -= Time.deltaTime;
		if ( attackTimer < 0 )
			attackTimer = 0;
		if ( Vector3.Distance( player.transform.position, transform.position ) < rangeOfAttack && attackTimer == 0f )
       	{
			attackTimer = coolDown;
			Vector3 direction =  player.transform.position - transform.position;
			direction.Normalize();
			RaycastHit hit;
			Physics.Raycast(transform.position + (Vector3.up *5), direction, out hit);
			Debug.DrawLine(transform.position, player.transform.position );
			Vector3 dir = (player.transform.position - transform.position + Vector3.one).normalized;
			float faceDirection = Vector3.Dot(dir, transform.forward);
			if ( hit.collider.gameObject.tag == "Player" && faceDirection > .4 )
			{
				GameObject temp = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
				temp.rigidbody.AddForce(transform.forward * projectileSpeed ); 
				Physics.IgnoreCollision( temp.collider, transform.root.collider );

			}
		}
	}
	void Start ()
	{
		player = GameObject.FindWithTag("Player");
	}
}
