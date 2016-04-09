using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	
	public enum WeaponType
	{
		SABER,
		UZI,
		SHOTGUN,
		SNIPER,
	}
	
	[HideInInspector]
	public int 				ammo = 0;
	
	public WeaponType		weaponType = 0;
	private	float			power = 0;
	private Vector3			dropDir;
	private GameObject		shoot;
	private float			fireRate = 1;
	
	private SpriteRenderer	uzi_weared_sprite;
	private SpriteRenderer	shotgun_weared_sprite;
	private SpriteRenderer	sword_weared_sprite;
	private SpriteRenderer	sniper_weared_sprite;
	
	
	
	// Use this for initialization
	void Start () {
		if (weaponType == WeaponType.SABER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SaberShoot") as GameObject;
			ammo = -1;
			fireRate = 0.5f;
		} else if (weaponType == WeaponType.UZI) {
			shoot = Resources.Load("Prefabs/WeaponShoot/UziShoot") as GameObject;
			ammo = 100;
			fireRate = 0.1f;
		} else if (weaponType == WeaponType.SHOTGUN) {
			shoot = Resources.Load("Prefabs/WeaponShoot/ShotgunShoot") as GameObject;
			ammo = 30;
			fireRate = 1;
		} else if (weaponType == WeaponType.SNIPER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SniperShoot") as GameObject;
			ammo = 20;
			fireRate = 3;
		}
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
			transform.Translate(dropDir * Time.deltaTime * power);
			power =- 0.1f;
		}
	}
	
	public void DoDropWeapon (Vector3 playerPos) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		transform.parent = null;
		transform.position = playerPos;
		power = 100;
		dropDir = Vector3.up; // change this with mouse dir
		DoDropWearedWeaponSkin ();
		//		Vector3 mouse = Ca
		//		Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)
		
	}
	
	public void DoTakeWeapon (GameObject obj) {
		transform.parent = obj.transform;
		DoTakeWearedWeaponSkin ();
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	public IEnumerator DoShoot(GameObject master) {
		Vector3 pos;
		Vector3 dir;
		while (ammo != 0) {
			if (ammo != -1)
				ammo -= 1;
			pos = master.transform.position;
			dir = Vector3.right; // change with mouse dir
			pos += dir;
			GameObject obj = Instantiate(shoot, pos, Quaternion.identity) as GameObject;
			if (weaponType == WeaponType.SABER)
				obj.GetComponent<ShootScript>().InitShoot(dir, false, 0);
			else
				obj.GetComponent<ShootScript>().InitShoot(dir, true, 0);
			yield return new WaitForSeconds (fireRate);
		}
	}
	
	//Sceen of weapon on in hand
	private void DoDropWearedWeaponSkin () {
		this.uzi_weared_sprite.enabled = false;
		this.shotgun_weared_sprite.enabled = false;
		this.sword_weared_sprite.enabled = false;
		this.sniper_weared_sprite.enabled = false;
	}
	
	private void DoTakeWearedWeaponSkin(){
		if (weaponType == WeaponType.UZI) {
			this.uzi_weared_sprite.enabled = true;
		} else if (weaponType == WeaponType.SHOTGUN) {
			this.shotgun_weared_sprite.enabled = true;
		} else if (weaponType == WeaponType.SABER) {
			this.sword_weared_sprite.enabled = true;
		} else if (weaponType == WeaponType.SNIPER) {
			this.sniper_weared_sprite.enabled = true;
		}
	}
}
