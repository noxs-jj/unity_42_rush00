using UnityEngine;
using System.Collections;

public class doorEvent : MonoBehaviour {
	private EdgeCollider2D	trigger_collider;
	private BoxCollider2D	door_collider_physic;
	private Animator		animator_object;
	
	void Start () {
		this.door_collider_physic = gameObject.GetComponent<BoxCollider2D> ();
		this.animator_object = gameObject.GetComponent<Animator> ();
	}

	private void OnTriggerEnter2D(Collider2D obj){
		Debug.Log ("OnTriggerEnter2D IN");
		StartCoroutine (remove_collider_physic_after_time (0.40f));
		this.animator_object.SetTrigger ("open_launcher");
		
	}

	private IEnumerator remove_collider_physic_after_time(float delay) {
		yield return new WaitForSeconds (delay);
		this.door_collider_physic.enabled = false;
	}

	private void OnTriggerExit2D(Collider2D obj){
		Debug.Log ("OnTriggerExit2D OUT");
		StartCoroutine(put_collider_physic_after_time(0.40f));
		this.animator_object.SetTrigger ("close_launcher");
	}

	private IEnumerator put_collider_physic_after_time(float delay) {
		yield return new WaitForSeconds (delay);
		this.door_collider_physic.enabled = true;
	}
}
