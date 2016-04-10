﻿using UnityEngine;
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

	private SpriteRenderer	uzi_weared_sprite;
	private SpriteRenderer	shotgun_weared_sprite;
	private SpriteRenderer	sword_weared_sprite;
	private SpriteRenderer	sniper_weared_sprite;

	private AudioClip		weaponAction;
	private AudioClip		deathSound;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		this.player_cam_object = GameObject.Find("MainCamera");
		this.player_camera = this.player_cam_object.GetComponent<Camera> ();
		weaponAction = Resources.Load ("Audio/Sounds/eject") as AudioClip;
		deathSound = Resources.Load ("Audio/Sounds/death1") as AudioClip;
		FindWeaponArms ();
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

	void FindWeaponArms() {
		SpriteRenderer[] list = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer elem in list) {
			if (elem.name == "weared_Uzi_1")
				this.uzi_weared_sprite = elem;
			else if (elem.name == "weared_Shotgun_2")
				this.shotgun_weared_sprite = elem;
			else if (elem.name == "weared_Saber_5")
				this.sword_weared_sprite = elem;
			else if (elem.name == "weared_Sniper_11")
				this.sniper_weared_sprite = elem;
		}
		DoDropWearedWeaponSkin ();
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
			DoTakeWearedWeaponSkin();
		}
		AudioSource.PlayClipAtPoint (weaponAction, transform.position);
	}
	
	void DropWeapon() {
		if (weapon != null) {
			if (isShoot == true)
				StopShoot();
			DoDropWearedWeaponSkin ();
			haveWeapons = false;
			weapon.DoDropWeapon(transform.position);
			weapon = null;
			AudioSource.PlayClipAtPoint (weaponAction, transform.position);
		}
	}
	
	void StartShoot() {
		Collider2D[] noiseCircleTab = Physics2D.OverlapCircleAll ( new Vector2(transform.position.x, transform.position.y ), 5);
		foreach(Collider2D enemy in noiseCircleTab)
		{
			if (enemy.tag == "enemie") {
				enemy.GetComponent<Enemy>().OnTriggerStay2D(gameObject.GetComponent<Collider2D>());
			}
		}
		isShoot = true;
		shoot = weapon.DoShoot(gameObject);
		StartCoroutine (shoot);
	}
	
	void StopShoot() {
		isShoot = false;
		StopCoroutine (shoot);
	}
	
	void	die() {
		Debug.Log("helloworld");
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

	//Sceen of weapon on in hand
	private void DoDropWearedWeaponSkin () {
		this.uzi_weared_sprite.enabled = false;
		this.shotgun_weared_sprite.enabled = false;
		this.sword_weared_sprite.enabled = false;
		this.sniper_weared_sprite.enabled = false;
	}
	
	private void DoTakeWearedWeaponSkin(){
		if (weapon.weaponType == WeaponScript.WeaponType.UZI) {
			this.uzi_weared_sprite.enabled = true;
		} else if (weapon.weaponType == WeaponScript.WeaponType.SHOTGUN) {
			this.shotgun_weared_sprite.enabled = true;
		} else if (weapon.weaponType == WeaponScript.WeaponType.SABER) {
			this.sword_weared_sprite.enabled = true;
		} else if (weapon.weaponType == WeaponScript.WeaponType.SNIPER) {
			this.sniper_weared_sprite.enabled = true;
		}
	}

	public string get_weapon_name(){
		if (null == weapon)
			return "Empty";
		else
			return weapon.name;
	}

	public int get_weapon_ammo(){
		if (null == weapon)
			return -999;
		else
			return weapon.ammo;
	}
}
