using UnityEngine;
using System.Collections;

public class RotatePlatform : MonoBehaviour {
	public float rotatePlatformSpeed;
	
	// Update is called once per frame
	void Update () {
	transform.Rotate(Vector3.right * Time.deltaTime * rotatePlatformSpeed);
	}
}
