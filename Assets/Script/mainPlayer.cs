using UnityEngine;
using System.Collections;

public class mainPlayer : MonoBehaviour {

	private float speed = 5;
//	private Rigidbody2D	rbody;

	// Use this for initialization
	void Start () {
//		rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
//			transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f);
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		}else if (Input.GetKey (KeyCode.S)) {
//			transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
			transform.Translate(Vector3.down * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.A)) {
//			transform.position = new Vector2(transform.position.x - 1.0f, transform.position.y);
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		} else if (Input.GetKey (KeyCode.D)) {
//			transform.position = new Vector2(transform.position.x + 1.0f, transform.position.y);
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}
	}
}
