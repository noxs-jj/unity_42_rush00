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
	private WeaponScriptEnemy weapon;

	//audio
	private AudioSource  audioSrc;
	public AudioClip 	 deathSound;
	
	//propriete
	private int life = 10;
	private bool haveWeapons = false;
	public bool alert = false;

	public Sprite[] headBodySprite = new Sprite[10];

	// Use this for initialization
	void Start () {

		//body Head random
		gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = headBodySprite[Random.Range (0, 3)];
		gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = headBodySprite[Random.Range (4, 10)];

		this.audioSrc = gameObject.GetComponent<AudioSource> ();
		this.animate = gameObject.GetComponent<Animator> ();
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
		if (cible && alert) {
			awake ();
		}
		if ( isShoot == true && !alert)
			StopShoot ();
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
		float step = 0 * Time.deltaTime;
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

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.transform.CompareTag ("Player")) {
			alert = true;
			StartCoroutine(isFollowing());
			animate.SetBool ("move", false);
			setCible (collider.gameObject);
			awake ();
		} else if (collider.tag == "Weapons") {
			if (haveWeapons == false) {
				onWeapon = collider.gameObject;
				weapon = onWeapon.GetComponent<WeaponScriptEnemy> ();
				weapon.DoTakeWeapon (gameObject, cible);
				haveWeapons = true;
			}
		} else if (collider.tag == "Shoot") {
			Debug.Log(life);
			HitEnemy(collider.GetComponent<ShootScript>().degat);
		}
	}


	public void HitEnemy(int hit) {
		if (life - hit <= 0) {
			hit = 0;
			Death ();
		} else {
			life -= hit;
		}
	}
	
	public void Death() {
		if (isShoot == true)
			StopShoot ();
		audioSrc.PlayOneShot(deathSound);
		weapon.DoDropWeapon(transform.position);
		GameObject.Destroy (gameObject);
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
}
