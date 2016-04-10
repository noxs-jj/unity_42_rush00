using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonPause : MonoBehaviour {

	private AudioClip loseSong;
	private AudioClip winSong;

	void Start () {
		loseSong = Resources.Load ("Audio/Sounds/youWin") as AudioClip;
		winSong = Resources.Load ("Audio/Sounds/youLose") as AudioClip;
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
		Debug.Log("lose");
		AudioSource.PlayClipAtPoint (loseSong, Vector3.zero);
		GameObject.Find ("rangScore").GetComponent<Text> ().text = "Loser";
		GameObject.Find ("score").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("loadScene2").GetComponent<Button> ().enabled = false;
	}

	public void winPlayer () {
		AudioSource.PlayClipAtPoint (winSong, Vector3.zero);
		GameObject.Find ("rangScore").GetComponent<Text> ().text = "Winner";
		GameObject.Find ("score").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("loadScene2").GetComponent<Button> ().enabled = true;
	}
}
