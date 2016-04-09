using UnityEngine;
using System.Collections;

public class cameraPlayer : MonoBehaviour {

	private GameObject	main_player_object;
	private mainPlayer	main_player_script;

	// Use this for initialization
	void Start () {
		this.main_player_object = GameObject.Find ("main_player_group");
		this.main_player_script = this.main_player_object.GetComponent<mainPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (this.main_player_object.transform.position.x, this.main_player_object.transform.position.y, -10);
	}
}
