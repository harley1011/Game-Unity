using UnityEngine;
using System.Collections;

public class PlayerFire : MonoBehaviour {

	public GameObject fire;
	public GameObject player;
	public GameObject instantiatedFire;
	public bool burning = false;
	public bool firstTime = true; 
	public float burntime = 5;
	public int burnDamage = 2;
	int count = 0;

	void Update () {
		if ( burning )
		{
			if (count != 1)
			{
				instantiatedFire = (GameObject)Instantiate(fire, transform.position, transform.rotation);
				instantiatedFire.transform.position = player.transform.position;
				count = 1;
			}
			else
				instantiatedFire.transform.position = player.transform.position;				
			if ( burntime >= 0 )
			{
				burntime -= Time.deltaTime;
				if ( firstTime )
				{
					PlayerHealth pH = (PlayerHealth)player.GetComponent("PlayerHealth");
					InvokeRepeating("BurnPlayer",0, 1);	
					firstTime = false;
				}
			}
			else if (burntime < 0)
			{
				CancelInvoke("BurnPlayer");
				firstTime = true;
				count = 0;
				Destroy(instantiatedFire);
			}
		}
	}
	void BurnPlayer ()
	{
		PlayerHealth pH = (PlayerHealth)player.GetComponent("PlayerHealth");
		pH.AdjustHealth(-burnDamage);
	}
}
