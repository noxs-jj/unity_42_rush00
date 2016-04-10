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
	public AudioClip		audioShoot;
	
	public WeaponType		weaponType = 0;
	private	float			power = 0;
	private Vector3			dropDir;
	private GameObject		shoot;
	private float			fireRate = 1;
	
	// Use this for initialization
	void Start () {
		if (weaponType == WeaponType.SABER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SaberShoot") as GameObject;
			ammo = -1;
			fireRate = 0.5f;
		} else if (weaponType == WeaponType.UZI) {
			shoot = Resources.Load("Prefabs/WeaponShoot/UziShoot") as GameObject;
			ammo = 150;
			fireRate = 0.1f;
		} else if (weaponType == WeaponType.SHOTGUN) {
			shoot = Resources.Load("Prefabs/WeaponShoot/ShotgunShoot") as GameObject;
			ammo = 50;
			fireRate = 1;
		} else if (weaponType == WeaponType.SNIPER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SniperShoot") as GameObject;
			ammo = 30;
			fireRate = 3;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (power > 0) {
			transform.position = Vector2.MoveTowards (transform.position, dropDir, Time.deltaTime * power);
			power =- 0.1f;
		}
	}
	
	public void DoDropWeapon (Vector3 playerPos) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		transform.parent = null;
		transform.position = playerPos;
		power = 125;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		dropDir = mousePos;	
	}

	public void DoTakeWeapon (GameObject obj) {
		transform.parent = obj.transform;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		if (obj.tag == "enemie")
			ammo = -1;
	}
	
	public IEnumerator DoShoot(GameObject master) {
		Vector3 pos;
		Vector3 dir;
		while (ammo != 0) {
			if (ammo != -1)
				ammo -= 1;
			AudioSource.PlayClipAtPoint(audioShoot, transform.position);
			dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
			pos = transform.position + dir;
			GameObject obj = Instantiate(shoot, pos, Quaternion.identity) as GameObject;
			if (weaponType == WeaponType.SABER)
				obj.GetComponent<ShootScript>().InitShoot(dir, false, 0, transform.position);
			else
				obj.GetComponent<ShootScript>().InitShoot(dir, true, 0, transform.position);
			yield return new WaitForSeconds (fireRate);
		}
	}

}
