using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
public GameObject door;
public Transform player;
private bool interact = true;
	
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		if ( Vector3.Distance( player.position, transform.position) < 5 && Input.GetButtonDown("Interact") && interact )
		{
			Debug.Log("interact");
		Door dooropen = (Door)door.GetComponent<Door>();
		dooropen.setBool();
			interact = false;
		}
	
	}
}
