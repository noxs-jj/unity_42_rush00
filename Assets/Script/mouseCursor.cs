using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mouseCursor : MonoBehaviour {
//	private mainPlayer	player_script;
	private Texture2D	mouse_cursor;

	// Use this for initialization
	void Start () {
		CursorMode	cursorMode = CursorMode.Auto;

//		this.player_script = gameObject.GetComponent<mainPlayer> ();
		this.mouse_cursor = Resources.Load ("Sprites/hud/cursor") as Texture2D;
		Cursor.SetCursor(this.mouse_cursor, Vector2.zero, cursorMode);
	}
	
	// Update is called once per frame
	void Update () {}

	public Vector3 get_mouse_position(){
		return new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}
}
