using UnityEngine;
using System.Collections;

public class TriggerScriptBoss : MonoBehaviour 
{
	public GameObject door;
	public GameObject boss;
	private bool doorUp = false;
	void OnTriggerEnter ( Collider collision )
		{
		
			if (collision.gameObject.tag == "Player")
			{
				doorUp = true;
				Boss setBoolBoss = (Boss)boss.GetComponent("Boss");
				setBoolBoss.playerInRoom = true;
			}
		}
	void Update ()
	{
		if (doorUp && door.transform.position.y < 14)
		{
			door.transform.position = Vector3.Lerp ( door.transform.position, door.transform.position + Vector3.up * 16, Time.deltaTime * 1 );
			
		}
		
		
		
	}
}
