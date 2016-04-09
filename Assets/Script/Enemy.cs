using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	// Animation
	private Animator animate;
	private Vector3	ciblePosition;
	private int   	time;
	
	// Control
	public GameObject 	 cible;
	private bool 	  	 trigger;
	private bool	  	 wait;
	private IEnumerator	 shoot;
	private bool		 isShoot = false;
	private GameObject 	 onWeapon;
	private WeaponScriptEnemy weapon;

	//audio
	private AudioSource  audioSrc;
	public AudioClip 	 deathSound;
	
	//propriete
	private int life = 10;
	private bool haveWeapons = false;
	public bool alert = false;

	public enum headType
	{
		BODY_1,
		BODY_2,
		BODY_3,
		HEAD_1,
		HEAD_2,
		HEAD_3,
		HEAD_4,
		HEAD_5,
		HEAD_6,
		HEAD_7
	}

	private SpriteRenderer	body;
	private SpriteRenderer	head;

	// Use this for initialization
	void Start () {

		///body random
		/*int body = Random.Range (0, 3);
		if (body == 2) {
			gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameObject.Find ("body_1").GetComponent<Sprite> ();
		} else if (body == 2) {
			gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameObject.Find ("body_2").GetComponent<Sprite> ();
		} else {
			gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameObject.Find ("body_3").GetComponent<Sprite> ();
		}*/

		this.audioSrc = gameObject.GetComponent<AudioSource> ();
		this.animate = gameObject.GetComponent<Animator> ();
		trigger = false;
		wait = false;
		time = Mathf.RoundToInt(Time.time);
		ciblePosition = transform.position;
	}
	
	public void    rotateEnemy(Vector3 pToGo)
	{
		Vector3 lookPos = pToGo - transform.position;
		
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		if (alert && cible) {
			awake ();
		} else {
			alert = false;
		}
	}

	void awake () {
		if (life <= 0) {
			death ();
		}
		if (cible) {
			ciblePosition = cible.transform.position;
			moveToPoint (ciblePosition);
			if ( cible.transform.CompareTag ("Player") && haveWeapons == true && isShoot == false)
				StartShoot ();
		} else {
			ciblePosition = this.transform.position;
		}
		if ( isShoot == true && !cible)
			StopShoot ();
	}
	
	public void moveToPoint (Vector3 posTarget) {
		rotateEnemy (this.ciblePosition);
		animate.SetBool ("move", true);
		float step = 3 * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, posTarget, step);
	}
	
	public void setCible(GameObject target) {
		this.cible = target;
	}
	
	public void OnTriggerStay2D(Collider2D collider) {
		if (collider.transform.CompareTag ("Player")) {
			animate.SetBool ("move", false);
			setCible(collider.gameObject);
			awake();
		} else if (collider.tag == "Weapons") {
			if (haveWeapons == false) {
				onWeapon = collider.gameObject;
				weapon = onWeapon.GetComponent<WeaponScriptEnemy>();
				weapon.DoTakeWeapon(gameObject, cible);
				haveWeapons = true;
			}
		}
	}

	public void death() {
		audioSrc.PlayOneShot(deathSound);
		weapon.DoDropWeapon(transform.position);
		GameObject.Destroy (gameObject);
	}

	void StartShoot() {
		Debug.Log("shot");
		isShoot = true;
		shoot = weapon.DoShoot(gameObject);
		StartCoroutine (shoot);
	}

	void StopShoot() {
		Debug.Log("unshot");
		isShoot = false;
		StopCoroutine (shoot);
	}
}
