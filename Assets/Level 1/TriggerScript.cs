using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
	private GameObject player;
	public GameObject hiddenObject; 
	
	void OnTriggerEnter ( Collider collision )
	{
		if (collision.gameObject == player) // Checks if the player tirggered the collision
		{
			hiddenObject.renderer.enabled = true;	
			hiddenObject.collider.enabled = true;
		}
	}
	void Start () 
	{
		hiddenObject.renderer.enabled = false;
		hiddenObject.collider.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
}
