using UnityEngine;
using System.Collections.Generic;

public class FlameScript : MonoBehaviour {

	private GameObject player;
	public GameObject hiddenObject; 
	public bool animateFlames;
	public List<GameObject> Flames = new List<GameObject>();
	public int currentFlame;
	public bool burning = false;
	public bool flameRev = false;
	public float radius = 2;
	
	void OnTriggerEnter ( Collider collision )
	{
		if (collision.gameObject == player)
		{
			animateFlames = true;
		}
	}
	void Start () 
	{
		animateFlames = false;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (animateFlames)
		{ 
		
			if ( currentFlame > Flames.Count -1)
			{
				currentFlame = 0;
				
			}
			Flame ();
			Hit ();
			/*else
			{ currentFlame = 0;
				Debug.Log("Done Cycle");
				burning = false;
				if (!Flames[currentFlame-1].animation.IsPlaying("Fire Burst") && burning == false)
				{
					currentFlame = 0;
					burning = false;
				}
			}*/
	
			
		}
	
	}
	void Flame ()
	{
		if (!Flames[currentFlame].animation.IsPlaying("Fire Burst") && burning == false)
				{//if its not burning start animation
					Flames[currentFlame].animation.Play("Fire Burst");
					burning = true;
					Flames[currentFlame].animation["Fire Burst"].speed = 1.0f;
					
				}
				else if (!Flames[currentFlame].animation.IsPlaying("Fire Burst") && burning == true)
				{//when animation is done play it in reverse
						Flames[currentFlame].animation["Fire Burst"].speed = -1.0f;
						Flames[currentFlame].animation["Fire Burst"].time = Flames[currentFlame].animation["Fire Burst"].length;
						Flames[currentFlame].animation.Play("Fire Burst");
						Flames[currentFlame].animation.Rewind("Fire Burst"); 
						
						currentFlame ++;//go to next flame
						burning = false;		
				}
	}
	void Hit ()
	{
		for ( int count = 0; count < Flames.Count; count ++)
		{
			
			if( Flames[count].animation.IsPlaying("Fire Burst")&& Vector3.Distance (Flames[count].transform.position,player.transform.position) < radius)
			{ Debug.Log("player is hit");
				PlayerFire playerFire = (PlayerFire)player.GetComponent("PlayerFire");
				playerFire.burning = true;
				playerFire.burntime = 5;
			}
		}
		
	}
			
	
	
}
