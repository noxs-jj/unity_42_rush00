using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	[HideInInspector] public int		id;
	public int type = 0;
	private Vector3 shootDir = Vector3.zero;
	private float speed = 2;
	private bool	isMoving;
	private IEnumerator life;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving == true)
			transform.Translate(shootDir * Time.deltaTime * speed);
	}

	public void InitShoot(Vector3 dir, bool move, int idMaster) {
		shootDir = dir;
		isMoving = move;
		id = idMaster;
		if (isMoving == false) {
			life = lifeTime();
			StartCoroutine(life);
		}


	}

	public int GetMasterId() {
		return id;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Wall")
			EndShoot ();
	}

	void EndShoot () {
		if (isMoving == false)
			StopCoroutine(life);
		GameObject.Destroy (gameObject);
	}

	IEnumerator lifeTime() {
		yield return new WaitForSeconds (0.1f);
		EndShoot ();
	}
}
