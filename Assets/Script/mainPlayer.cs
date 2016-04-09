using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainPlayer : MonoBehaviour {
	private float			speed = 5;
	private bool			haveWeapons = false;
	private GameObject		onWeapon = null;
	private WeaponScript	weapon = null;
	private Animator		animator;
	
	//mouseCursor
	private mouseCursor		mouse_script;
	public GameObject		player_cam_object;
	private Camera			player_camera;


	// Use this for initialization
	void Start () {
		this.animator = GetComponentInChildren<Animator>();
		//this.mouse_script = gameObject.GetComponent<mouseCursor>();
		this.player_cam_object = GameObject.Find("Main Camera");
		this.player_camera = this.player_cam_object.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool isMoving = false;
		if (Input.GetKey (KeyCode.W)) {
			isMoving = true;
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		} else if (Input.GetKey (KeyCode.S)) {
			isMoving = true;
			transform.Translate(Vector3.down * Time.deltaTime * speed);
		}
		if (Input.GetKey (KeyCode.A)) {
			isMoving = true;
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		} else if (Input.GetKey (KeyCode.D)) {
			isMoving = true;
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}
		if (isMoving == true)
			animator.SetTrigger ("move");
		else
			animator.SetTrigger ("idle");
		if (Input.GetKeyDown (KeyCode.E) && haveWeapons == false)
			TakeWeapon ();
		if (Input.GetMouseButtonDown (1) && haveWeapons == true)
			DropWeapon ();

		//mouse
		update_mouse_position ();
	}

	void update_mouse_position(){
		Vector3 mousePos = this.player_camera.ScreenToWorldPoint(Input.mousePosition);
		gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -(mousePos - transform.position));
	}


	void TakeWeapon() {
		if (onWeapon != null) {
			haveWeapons = true;
			weapon = onWeapon.GetComponent<WeaponScript>();
			weapon.DoTakeWeapon(gameObject);
		}
	}

	void DropWeapon() {
		if (weapon != null) {
			haveWeapons = false;
			weapon.DoDropWeapon(transform.position);
			weapon = null;
		}
	}

	void OnTriggerEnter2D(Collider2D collide) {
		if (collide.tag == "Weapons") {
			onWeapon = collide.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D collide) {
		onWeapon = null;
	}
}
