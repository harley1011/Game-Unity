using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float cameraY = 50f;
	public float cameraX = -10f;
	public float cameraZ = -10f;
	
	private bool isWalkingRev = false;
	void Start ()
	{
		Physics.gravity = new Vector3(0, -75.0F, 0);	
	}
	void FixedUpdate () 
	{	
		PlayerHealth ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");
		float x = Input.GetAxis("Horizontal"); //* Time.deltaTime * speed;
		float z = Input.GetAxis("Vertical"); //* Time.deltaTime * speed;
		
		if ( !ph.dead && !ph.dying && transform.position.y > -25f )
		{
			//if (Input.GetButtonDown("Sprint")) for later use
		
			if ( Input.GetButtonDown("Fire1" ))
			{
				animation.Play ("attack");
			}
			else if ( z != 0 || x != 0  /*&&!animation.IsPlaying("attack")*/ )
			{
				if ( z < 0 )
				{
				    z = z /3;
					x = x /3;
					
					if( animation.isPlaying == false) //reset animation cycle
					{  isWalkingRev = false;
					}
					if( isWalkingRev == false)
					{	animation["walk"].speed = -1.0f;
						animation["walk"].time = animation["walk"].length;
						animation.Play("walk");
						isWalkingRev =true;
					}
          
				}
				else
				{
					isWalkingRev = false ;
					
					animation.Play("run");
				}
			
			
				//used for fixing diagonal movement speed
				Vector3 newPos = new Vector3(x, 0, z);
   				if( newPos.magnitude > 1.0f ) 
    			{
         			newPos = newPos.normalized;
    			}
				newPos *= Time.deltaTime;
    			newPos *= speed;
				
				transform.Translate(newPos);
			}
			else if ( !animation.IsPlaying("attack"))
			{
				animation.Play("idle");
			}
		
			//Vector3 nextPosition = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed);
			//transform.position += nextPosition;	
			
			Vector3 cameraPosition = new Vector3( transform.position.x + cameraX, transform.position.y + cameraY, transform.position.z + cameraZ);
			Camera.mainCamera.transform.position = cameraPosition;
			// Generate a plane that intersects the transform's position with an upwards normal.
	    	
			Plane playerPlane = new Plane(Vector3.up, transform.position);
	     	// Generate a ray from the cursor position
	    	Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Ray ray  = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			
	     	// Determine the point where the cursor ray intersects the plane.
	    	// This will be the point that the object must look towards to be looking at the mouse.
	    	// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
	    	//   then find the point along that ray that meets that distance.  This will be the point
	    	//   to look at.
	    	float hitdist = 0.0f;
	    	// If the ray is parallel to the plane, Raycast will return false.
	    	if (playerPlane.Raycast (ray, out hitdist)) 
			{
	        	// Get the point along the ray that hits the calculated distance.
	        	Vector3 targetPoint = ray.GetPoint(hitdist);
	 	        
	        	// Determine the target rotation.  This is the rotation if the transform looks at the target point.
	        	Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
	 
	        	// Smoothly rotate towards the target point.
	        	transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.time);
			}
		}
		else if ( transform.position.y < -25f)
			gameObject.GetComponent<GameOver>().gameOver = true;
		
    }
	
	

}
