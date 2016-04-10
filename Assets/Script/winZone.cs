using UnityEngine;
using System.Collections;

public class winZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collide) {
		if (collide.tag == "Player") {
			collide.gameObject.SetActive (false);
			GameObject.Find ("score").transform.GetChild (2).GetComponent<buttonPause> ().winPlayer ();
		}
	}
}