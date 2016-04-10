using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hudInGame : MonoBehaviour {

	private GameObject	player_object;
	private mainPlayer	player_script;
	private GameObject	image_ammo_full;
	private GameObject	image_ammo_empty;

	private GameObject	weapon_name_object;
	private GameObject	ammo_numer_object;

	private	Text		text_weapon_name;
	private	Text		text_ammo_numer;

	// Use this for initialization
	void Start () {
		this.player_object = GameObject.Find ("Player");
		this.player_script = this.player_object.GetComponent<mainPlayer> ();

		this.image_ammo_full = GameObject.Find ("legend_ammo_full");
		this.image_ammo_empty = GameObject.Find ("legend_ammo_empty");

		this.weapon_name_object = GameObject.Find ("value_weapon_name");
		this.text_weapon_name = this.weapon_name_object.GetComponent<Text> ();

		this.ammo_numer_object = GameObject.Find ("value_ammo_number");
		this.text_ammo_numer = this.ammo_numer_object.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		update_weapon_name ();
		update_ammo_number ();
		update_ammo_image ();
	}

	private void update_weapon_name(){
		this.text_weapon_name.text = this.player_script.get_weapon_name();
	}

	private void update_ammo_number(){
		if ("Saber" == this.text_weapon_name.text) {
			this.text_ammo_numer.text = "The One";
		} else if (-999 == this.player_script.get_weapon_ammo()) {
			this.text_ammo_numer.text = "No Ammo";
		} else {
			this.text_ammo_numer.text = this.player_script.get_weapon_ammo().ToString();
		}
	}

	private void update_ammo_image(){
		if ("Saber" == this.text_weapon_name.text || ("Saber" != this.text_weapon_name.text && this.player_script.get_weapon_ammo() > 0)) {
			this.image_ammo_full.SetActive (true);
			this.image_ammo_empty.SetActive (false);
		} else {
			this.image_ammo_empty.SetActive (true);
			this.image_ammo_full.SetActive (false);
		}
	}
}
