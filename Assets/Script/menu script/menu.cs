using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {
	
	public void startGame () {
		Debug.Log ("loadLevel");
		Application.LoadLevel (0);
	}

	public void exitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
