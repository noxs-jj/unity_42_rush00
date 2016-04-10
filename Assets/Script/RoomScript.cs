using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour {

	[HideInInspector]  public int		LocalCost = -1;

	public RoomScript[]		RoomListScript;
	public List<GameObject>		RoomList = new List<GameObject>();
	public bool					isPlayerInRoom = false;
	private IEnumerator			routineCost;
	private	bool				isRoutineStarted = false;
	
	// Use this for initialization
	void Start () {
		isPlayerInRoom = false;
		int i = 0;
		RoomListScript = new RoomScript[RoomList.Count];
		foreach (GameObject room in RoomList) {
			RoomListScript[i] = room.GetComponent<RoomScript>();
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerInRoom == true && isRoutineStarted == false) {
			isRoutineStarted = true;
			routineCost = GetRoomCost ();
			StartCoroutine (routineCost);
		} else if (isPlayerInRoom == false && isRoutineStarted == true)
			StopCoroutine (routineCost);
	}
	
	
	public void CalcCost(int cost) {
		if (LocalCost > cost || LocalCost == -1)
			LocalCost = cost;
		foreach (RoomScript room in RoomListScript) {
			if (room.LocalCost == -1 || room.LocalCost > cost + 1)
				room.CalcCost(cost + 1);
		}
	}
	
	public void ResetCost() {
		LocalCost = -1;
		foreach (RoomScript room in RoomListScript) {
			if (room.LocalCost != -1)
				room.ResetCost();
		}
	}

	public IEnumerator GetRoomCost() {
		ResetCost ();
		CalcCost(0);
		Debug.Log ("CalcCostFinished");
		yield return new WaitForSeconds (1);
	}

	public void SetPlayerInRoom(bool isHere) {
		isPlayerInRoom = isHere;
	}
}
