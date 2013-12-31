using UnityEngine;
using System.Collections.Generic;

public class PlatformMove : MonoBehaviour {
	
	public List<GameObject> Points = new List<GameObject>();
	public GameObject Platform;
	private int current;
 	public float radius;
	public float moveSpeed;
	public float length;

	// Use this for initialization
	void Start () {
		current = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance(Points[current].transform.position,Platform.transform.position) - length);
		//Platform.rigidbody.AddForce(transform.forward * projectileSpeed );
		if ( Vector3.Distance(Points[current].transform.position,Platform.transform.position) - length < radius )
		{
			
			Debug.Log("radius is less");
			if(current == Points.Count - 1)
			{
				current = 0;
				moveSpeed = moveSpeed *-1;
			}
			else
			{
				current++;
				moveSpeed = moveSpeed *-1;
				
			}
			
			
		}
		else
		{
			Platform.transform.position = Vector3.Slerp(Platform.transform.position, Points[current].transform.position, Time.deltaTime * .5f);
			//Platform.rigidbody.AddForce(Platform.transform.forward * moveSpeed );
		}
		
	
	}
}
