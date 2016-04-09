using UnityEngine;
using System.Collections;

public class mainPlayer : MonoBehaviour {

	private float			speed = 5;
	private bool			haveWeapons = false;
	private GameObject		onWeapon = null;
	private WeaponScript	weapon = null;
	private Animator		animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		bool isMoving = false;
		if (Input.GetKey (KeyCode.W)) {
			isMoving = true;
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		}else if (Input.GetKey (KeyCode.S)) {
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
