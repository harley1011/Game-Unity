using UnityEngine;
using System.Collections;

public class PlayerRangeAttack : MonoBehaviour {

	public float projectileSpeed;
	public GameObject projectile;
	void Update () 
	{
		if ( Input.GetButtonDown("Fire2" ) )
		{
			GameObject temp = (GameObject)Instantiate(projectile, transform.position, transform.rotation); 
			temp.rigidbody.AddForce(transform.forward * projectileSpeed ); 
			Physics.IgnoreCollision( temp.collider, transform.root.collider ); // Ignore collision between player and projectile
		}
	}
}
