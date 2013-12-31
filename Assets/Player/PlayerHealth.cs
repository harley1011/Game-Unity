using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float _maxHealth = 100f;
	public float _currentHealth = 100f;
	public bool dying;
	public bool dead;
	
	void Start () 
	{
	  dying = false;
	  dead = false;
	}
	
	void Update () 
	{
		if( dead ) {	
		  Debug.Log ("Is Dead No Ani");
		}
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, 20, Screen.width / 4 , 20), _currentHealth + "/" + _maxHealth);
		GUI.Box(new Rect(10, 20, Screen.width / 4 / (_maxHealth / _currentHealth ), 20), ""  );	
	}
	
	public void AdjustHealth ( int adjust) 
	{
		_currentHealth += adjust;
		if ( _currentHealth < 1 ) // If player is < 1 then he is dead
		{
			_currentHealth = 0;
			if (  !dead && !dying )
			{
				dying = true;
				animation.Play("diehard");
				StartCoroutine(WaitAndCallback(animation["diehard"].length));
			}
		}
		if ( _currentHealth > _maxHealth )
			_currentHealth = _maxHealth;
		
	}
	IEnumerator WaitAndCallback(float waitTime)
	{
    	yield return new WaitForSeconds(waitTime);
   	    AnimationFinished();
	}

	void AnimationFinished()
	{
	   dead = true;
	   dying = false;	
	   animation.Stop("diehard");
	}
	
}
