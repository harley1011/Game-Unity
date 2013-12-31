using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {
	public float destroySeconds = 5.0f;
	public int damage;
	void Start () 
	{
		Destroy(gameObject, destroySeconds); // Destorys the projectile at a specificed time
	}
	void OnCollisionEnter (Collision collision ) {
		if ( collision.gameObject.tag == "Player" ) // Was the thing we hit and enemy?
			collision.rigidbody.SendMessageUpwards("AdjustHealth", damage * -1, SendMessageOptions.DontRequireReceiver); // Call the function of the enemy class to reduce his health
		Destroy(gameObject); 
	}
}
