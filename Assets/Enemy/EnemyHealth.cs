using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int _currentHealth;
	public int _maxHealth;
	private float deadLength;
	public bool dead = false;
	void Start () {
	 deadLength = animation["dead"].length + 5;
	}
	

	void Update () {	
		if ( _currentHealth == 0 )
		{
			if ( !dead)
			{
				dead = true;
				animation.Play("dead");
				gameObject.GetComponent<EnemyAttack>().enabled = false;
				try
				{
					gameObject.GetComponent<EnemyPathfinding>().enabled = false;
				}
				catch
				{
					gameObject.GetComponent<EnemyRangeAttack>().enabled = false;
				}
			}
			else if ( deadLength > 0 )
				deadLength -= Time.deltaTime;
			else
				Destroy(gameObject);
		}
	}
	
	
	public void AdjustHealth ( int adjust)
	{
		_currentHealth += adjust;
		if ( _currentHealth < 1 )
			_currentHealth = 0;
		
		if ( _currentHealth > _maxHealth )
			_currentHealth = _maxHealth;
				Debug.Log(_currentHealth);
	}
}
