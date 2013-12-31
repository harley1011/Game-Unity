using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	private float buttonWidthPosition= Screen.width / 2 - 50;
	private float buttonHeightPosition = Screen.height /2;

	private int buttonHeight = 50;
	private int buttonWidth = 100;
	// Use this for initialization
	void Start () {

	}
	void OnGUI(){
		if (GUI.Button(new Rect(buttonWidthPosition , buttonHeightPosition - 150f, buttonWidth, buttonHeight),"Play"))
		{
			Application.LoadLevel("test1");
		}
		GUI.Button(new Rect(buttonWidthPosition , buttonHeightPosition - 75f, buttonWidth, buttonHeight),"Multiplayer");
		GUI.Button(new Rect(buttonWidthPosition , buttonHeightPosition , buttonWidth, buttonHeight),"Help");
		GUI.Button(new Rect(buttonWidthPosition , buttonHeightPosition + 75f, buttonWidth, buttonHeight),"Options");
		GUI.Button(new Rect(buttonWidthPosition , buttonHeightPosition + 150f, buttonWidth, buttonHeight),"Credits");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
