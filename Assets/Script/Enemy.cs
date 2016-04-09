using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Animation
	private Animator animate;
	private Vector3	ciblePosition;
	private int   	time;
	
	// Control
	public GameObject cible;
	
	private AudioSource audioSrc;
	public AudioClip attackSound;
	
	
	// Use this for initialization
	void Start () {
		 //this.audioSrc = gameObject.GetComponent<AudioSource> ();
		//this.cible = null;
		this.animate = gameObject.GetComponent<Animator> ();
		
		//this.animate.SetTrigger ("idle");
		time = Mathf.RoundToInt(Time.time);
	}
	
	public void    rotateEnemy(Vector3 pToGo)
	{
		Vector3 lookPos = pToGo - transform.position;
		
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		if (cible && cible.transform.position != transform.position)
			moveToPoint (cible.transform.position.x, cible.transform.position.y);
		else if (cible == null || cible && cible.transform.position == transform.position) {
			animate.SetTrigger ("move");
		}
		if (this.transform.position == this.ciblePosition)
			this.animate.SetTrigger ("move");
		if (cible && cible.transform.position != transform.position) {
			float step = 1 * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(this.transform.position, this.ciblePosition, step);
		}
	}
	
	public void moveToPoint (float pMouseX, float pMouseY) {
		if (cible)
			ciblePosition = cible.transform.position;
		else {
			//this.mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (pMouseX, pMouseY, 0));
			this.ciblePosition.z = 0;
		}
		rotateEnemy (this.ciblePosition);
		Debug.Log ("move");
		animate.SetTrigger ("move");
	}
	
	public void setCible(GameObject target) {
		this.cible = target;
	}
	
	/*public void OnTriggerStay2D(Collider2D collider) {
		if ((collider.transform.CompareTag("player") || collider.transform.CompareTag("Orc")) && cible && cible.gameObject == collider.gameObject)
		{
			animate.SetBool ("isFighting", true);
			animate.SetBool ("isWalking", false);
			this.mousePosition = this.transform.position;
			
			if (Mathf.RoundToInt(Time.time) - time > 1) {
				if (collider.transform.CompareTag("OrcHouse")) {
					collider.gameObject.GetComponent<House>().life -= 5;
					if (collider.transform.name == "MainHouse")
						Debug.Log ("Town hall Orc [" + cible.GetComponent<House> ().life + "/400]HP has been attacked." );
					else
						Debug.Log ("House Orc [" + cible.GetComponent<House> ().life + "/100]HP has been attacked." );
				}
				else {
					collider.gameObject.GetComponent<Orc>().life -= 10;
					Debug.Log ("Orc Unit [" + cible.GetComponent<Orc>().life + "/50]HP has been attacked." );
				}
				audioSrc.PlayOneShot(attackSound);
				time = Mathf.RoundToInt(Time.time);
			}
		}
	}*/
	
	/*public void OnTriggerExit2D(Collider2D collider)
	{
		animate.SetBool ("isFighting", false);
	}*/
}
