using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public int				weaponType = 0;
	private	float			power = 0;
	private Vector3			dropDir;
	private SpriteRenderer	uzi_weared_sprite;
	private SpriteRenderer	shotgun_weared_sprite;
	private SpriteRenderer	sword_weared_sprite;
	private SpriteRenderer	sniper_weared_sprite;

	// Use this for initialization
	void Start () {
		this.uzi_weared_sprite = GameObject.Find ("weared_Uzi_1").GetComponent<SpriteRenderer>();
		this.uzi_weared_sprite.enabled = false;
		this.shotgun_weared_sprite = GameObject.Find ("weared_Shotgun_2").GetComponent<SpriteRenderer>();
		this.shotgun_weared_sprite.enabled = false;
		this.sword_weared_sprite = GameObject.Find ("weared_Saber_5").GetComponent<SpriteRenderer>();
		this.sword_weared_sprite.enabled = false;
		this.sniper_weared_sprite = GameObject.Find ("weared_Sniper_11").GetComponent<SpriteRenderer>();
		this.sniper_weared_sprite.enabled = false;
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
		DoDropWearedWeaponSkin ();
//		Vector3 mouse = Ca
//		Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)
	}

	public void DoTakeWeapon (GameObject obj) {
		transform.parent = obj.transform;
		DoTakeWearedWeaponSkin (obj.name);
	}

	//Sceen of weapon on in hand
	private void DoDropWearedWeaponSkin () {
		this.uzi_weared_sprite.enabled = false;
		this.shotgun_weared_sprite.enabled = false;
		this.sword_weared_sprite.enabled = false;
		this.sniper_weared_sprite.enabled = false;
	}

	private void DoTakeWearedWeaponSkin(string nameWeapon){
		if ("weared_Uzi_1".Contains (nameWeapon)) {
			this.uzi_weared_sprite.enabled = true;
		} else if ("weared_Shotgun_2".Contains (nameWeapon)) {
			this.shotgun_weared_sprite.enabled = true;
		} else if ("weared_Saber_5".Contains (nameWeapon)) {
			this.sword_weared_sprite.enabled = true;
		} else if ("weared_Uzi_1".Contains (nameWeapon)) {
			this.sniper_weared_sprite.enabled = true;
		}
	}
}
