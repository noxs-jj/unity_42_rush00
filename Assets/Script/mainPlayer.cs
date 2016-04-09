using UnityEngine;
using System.Collections;

public class mainPlayer : MonoBehaviour {
	
	private float			speed =5;
	private bool			haveWeapons = false;
	private GameObject		onWeapon = null;
	private WeaponScript	weapon = null;
	private Animator		animator;
	private IEnumerator		shoot;
	private bool			isShoot = false;
	
	//mouseCursor
	private mouseCursor		mouse_script;
	public GameObject		player_cam_object;
	private Camera			player_camera;
	
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		this.player_cam_object = GameObject.Find("MainCamera");
		this.player_camera = this.player_cam_object.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool isMoving = false;
		if (Input.GetKey (KeyCode.W)) {
			isMoving = true;
			transform.position += Vector3.up * Time.deltaTime * speed;
		}else if (Input.GetKey (KeyCode.S)) {
			isMoving = true;
			transform.position += Vector3.down * Time.deltaTime * speed;
		}
		if (Input.GetKey (KeyCode.A)) {
			isMoving = true;
			transform.position += Vector3.left * Time.deltaTime * speed;
		} else if (Input.GetKey (KeyCode.D)) {
			isMoving = true;
			transform.position += Vector3.right * Time.deltaTime * speed;
		}
		if (isMoving == true)
			animator.SetBool ("move", true);
		else
			animator.SetBool ("move", false);
		if (Input.GetKeyDown (KeyCode.E) && haveWeapons == false)
			TakeWeapon ();
		if (Input.GetMouseButtonDown (1) && haveWeapons == true)
			DropWeapon ();
		if (Input.GetMouseButton (0) && haveWeapons == true && isShoot == false)
			StartShoot ();
		else if (!Input.GetMouseButton (0) && isShoot == true)
			StopShoot ();
		
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
			if (isShoot == true)
				StopShoot();
			haveWeapons = false;
			weapon.DoDropWeapon(transform.position);
			weapon = null;
		}
	}
	
	void StartShoot() {
		isShoot = true;
		shoot = weapon.DoShoot(gameObject);
		StartCoroutine (shoot);
	}
	
	void StopShoot() {
		isShoot = false;
		StopCoroutine (shoot);
	}
	
	void	die() {
		if (isShoot == true)
			StopCoroutine (shoot);
		GameObject.Destroy (gameObject);
		
	}
	
	void OnTriggerEnter2D(Collider2D collide) {
		if (collide.tag == "Weapons") {
			onWeapon = collide.gameObject;
		} else if (collide.tag == "Shoot" && collide.gameObject.GetComponent<ShootScript>().GetMasterId() != 0) {
			die();
		}
	}
	
	void OnTriggerExit2D(Collider2D collide) {
		onWeapon = null;
	}
}
