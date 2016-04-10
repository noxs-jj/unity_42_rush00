using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	// Animation
	private Animator animate;
	private Vector3	ciblePosition;
	
	// Control
	public GameObject 	 cible;
	private IEnumerator	 shoot;
	private bool		 isShoot = false;
	private GameObject 	 onWeapon;
	private WeaponScript weapon;

	//audio
	private AudioSource  audioSrc;
	public AudioClip 	 deathSound;
	public AudioClip 	 targetSound;
	
	//propriete
	private int life = 10;
	private bool haveWeapons = false;
	public bool alert = false;
	public Sprite[] headBodySprite = new Sprite[10];
	
	//roundEnemy
	public bool pathDefined;
	private float distance;
	private float time;
	private float frameTime;
	private float distancePerSec;
	private bool direction;
	private bool upDown = false;

	private SpriteRenderer	uzi_weared_sprite;
	private SpriteRenderer	shotgun_weared_sprite;
	private SpriteRenderer	sword_weared_sprite;
	private SpriteRenderer	sniper_weared_sprite;

	// Use this for initialization
	void Start () {

		//body Head random
		int i;
		i = Random.Range (0, 10);
		pathDefined = false;
		if (i > 2) {
			pathDefined = true;
			if (i > 7)
				upDown = true;
		}
		gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = headBodySprite[Random.Range (0, 3)];
		gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = headBodySprite[Random.Range (4, 10)];

		//audio
		this.audioSrc = gameObject.GetComponent<AudioSource> ();
		this.animate = gameObject.GetComponent<Animator> ();

		ciblePosition = transform.position;
		//chemin de ronde
		time = Mathf.RoundToInt(Time.time);
		distance = 2;
		time = 3;
		distancePerSec = distance / time;

		FindWeaponArms ();
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

	public void    rotateEnemy(Vector3 pToGo)
	{
		Vector3 lookPos = pToGo - transform.position;
		
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		if (cible && alert) {
			awake ();
		} else {
			if (pathDefined) {
				RoundTrip ();
			}
		}
		if ( isShoot == true && !alert)
			StopShoot ();
	}

	public void RoundTrip () {
		frameTime += Time.deltaTime;
		float translate = (distancePerSec * Time.deltaTime);
		if(frameTime >= time)
		{
			translate -= (distancePerSec * (Time.deltaTime - (frameTime - time)));
			frameTime = 0;
			direction = !direction;
		}
		translate = (direction ? translate : -translate);
		if (upDown == false) {
			rotateEnemy( transform.position + new Vector3 (translate, 0, 0));
			transform.position += new Vector3 (translate, 0, 0);
		}
		else {
			rotateEnemy(transform.position + new Vector3 (0, translate, 0));
			transform.position += new Vector3 (0, translate, 0);
		}
	}

	void awake () {
		if (life <= 0) {
			Death ();
		}
		if (cible) {
			ciblePosition = cible.transform.position;
			moveToPoint (ciblePosition);
			if ( cible.transform.CompareTag ("Player") && haveWeapons == true && isShoot == false)
				StartShoot ();
		} else {
			ciblePosition = this.transform.position;
		}
	}
	
	public void moveToPoint (Vector3 posTarget) {
		rotateEnemy (this.ciblePosition);
		animate.SetBool ("move", true);
		float step = 1 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, posTarget, step);
	}
	

	public void setCible(GameObject target) {
		if (target)
			this.cible = target;
	}

	IEnumerator isFollowing()
	{
		yield return new WaitForSeconds (5.0f);
		alert = false;
	}
	public void OnTriggerExit2D(Collider2D collider) {
		StartCoroutine (isFollowing ());
	}
	public void OnTriggerStay2D(Collider2D collider) {
		if (collider.transform.CompareTag ("Player")) {
			alert = true;
			animate.SetBool ("move", false);
			setCible (collider.gameObject);
			awake ();
		} else if (collider.tag == "Weapons") {
			if (haveWeapons == false) {
				onWeapon = collider.gameObject;
				weapon = onWeapon.GetComponent<WeaponScript> ();
  				weapon.DoTakeWeapon (gameObject);
				haveWeapons = true;
				DoTakeWearedWeaponSkin();
			}
		} else if (collider.tag == "Shoot") {
			Debug.Log(life);
			HitEnemy(collider.GetComponent<ShootScript>().degat);
		}
	}

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.transform.CompareTag ("Player")) {
			if (!alert) {
				audioSrc.PlayOneShot (targetSound);
			}
		} else if (collider.tag == "Shoot" && collider.gameObject.GetComponent<ShootScript> ().GetMasterId () != 1) {

			HitEnemy(collider.GetComponent<ShootScript>().degat);
		}
	}


	public void HitEnemy(int hit) {
		Debug.Log("hey");
		Death ();
//		if (life - hit <= 0) {
//			hit = 0;
//			Death ();
//		} else {
//			life -= hit;
//		}
	}
	
	public void Death() {
		if (isShoot == true)
			StopShoot ();
		DoDropWearedWeaponSkin ();
		audioSrc.PlayOneShot(deathSound);
		weapon.DoDropWeapon(transform.position);
		GameObject.Destroy (gameObject);
	}

	void StartShoot() {
		isShoot = true;
		shoot = weapon.DoShootEnemy(gameObject, cible);
		StartCoroutine (shoot);
	}

	void StopShoot() {
		isShoot = false;
		StopCoroutine (shoot);
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
}
