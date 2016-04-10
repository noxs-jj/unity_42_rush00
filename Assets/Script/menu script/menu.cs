using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public void startGame () {
		Debug.Log ("loadLevel");
		Application.LoadLevel (1);
	}

	public void exitGame () {
		Debug.Log ("QuitGame");
		Application.Quit ();
	}
}
