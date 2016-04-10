using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zoneDetectPlayer : MonoBehaviour {

	private RoomScript[]	objs;

	void Start () {
		this.objs = GetComponentsInChildren<RoomScript> ();
	}

	private void OnTriggerEnter2D (Collider2D entity) {
		if ("Player" == entity.tag) {
			foreach (RoomScript room in this.objs) {
				room.SetPlayerInRoom (true);
			}
		} else if ("enemie" == entity.tag) {
			(entity.gameObject.GetComponent<Enemy>()).roomptr = objs;
		}
	}

	private void OnTriggerExit2D(Collider2D entity){
		if ("Player" == entity.tag) {
			foreach (RoomScript room in this.objs) {
				room.SetPlayerInRoom (false);
			}
		}
	}
}
