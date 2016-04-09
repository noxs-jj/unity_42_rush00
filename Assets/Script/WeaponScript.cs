using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public int		weaponType = 0;
	private	float	power = 0;
	private Vector3	dropDir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (power > 0) {
			transform.Translate(Vector3.up * Time.deltaTime * power);
			power =- 0.1f;
		}
	}

	public void DoDropWeapon (Vector3 playerPos) {
		transform.parent = null;
		transform.position = playerPos;
		power = 100;
		dropDir = Vector3.up; // change this with mouse pos
//		Vector3 mouse = Ca
//		Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)

	}

	public void DoTakeWeapon (GameObject obj) {
		transform.parent = obj.transform;
	}
}
