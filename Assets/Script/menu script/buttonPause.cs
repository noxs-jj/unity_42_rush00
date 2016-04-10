using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonPause : MonoBehaviour {


	void Start () {
	}

	public void pause(bool paused) {
		if (paused == true) {
			GameObject.Find ("pause").GetComponent<Canvas> ().enabled = paused;
		} else {
			GameObject.Find ("pause").GetComponent<Canvas> ().enabled = paused;
			GameObject.Find ("confirm").GetComponent<Canvas>().enabled = paused;
		}
	}

	public void quitLevel () {
		Application.LoadLevel (0);
	}
	
	public void continuePause () {
		pause (false);
	}
	
	public void quitPause () {
		GameObject.Find ("confirm").GetComponent<Canvas>().enabled = true;
		GameObject.Find ("pause").GetComponent<Canvas>().enabled = false;
	}
	
	public void confirmQuitPause () {
		Application.LoadLevel (0);
	}
	
	public void noQuitPause () {
		GameObject.Find ("confirm").GetComponent<Canvas>().enabled = false;
		GameObject.Find ("pause").GetComponent<Canvas>().enabled = true;
	}

	public void loadLevel (int level) {
		Application.LoadLevel (level);
	}

	public void deadPlayer () {

		GameObject.Find ("rangScore").GetComponent<Text> ().text = "Vous avez perdu";
		GameObject.Find ("score").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("loadScene2").GetComponent<Button> ().enabled = false;
	}
}
