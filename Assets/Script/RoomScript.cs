using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour {

	private List<RoomScript>	RoomListScript= new List<RoomScript>();
	public List<GameObject>		RoomList = new List<GameObject>();
	public bool				isPlayerInRoom = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetRoomCost() {

	}


	public void SetPlayerInRoom(bool isHere) {
		isPlayerInRoom = isHere;
	}
}
