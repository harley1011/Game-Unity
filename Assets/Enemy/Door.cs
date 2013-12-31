using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	//public Transform player;
	private Vector3 playerLastPosition;
	public bool open = false;
	
	// Update is called once per frame
	void Update () { 
		
		if ( open )
       	{
			animation.Play("Door Down");
			open = false;
		}
	
	}
	public void setBool ()
	{
		if ( open )
			open = false;
		else
			open = true;
	}
}
