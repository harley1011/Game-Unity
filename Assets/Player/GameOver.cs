using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	public bool gameOver = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		if ( gameOver )
		{
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = 40;
			myStyle.alignment = TextAnchor.UpperCenter;
			GUI.Label (new Rect (Screen.width / 2, Screen.height /2 - 50, 100, 200), "Game Over!", myStyle);
			if (GUI.Button(new Rect(Screen.width / 2, Screen.height /2 , 100, 200), "Play Again", myStyle))
            	resetLevel();

		}
	}
	private void resetLevel()
	{
		Debug.Log ("start again");
		
		
	}
}
