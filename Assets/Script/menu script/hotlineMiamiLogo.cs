using UnityEngine;
using System.Collections;

public class hotlineMiamiLogo : MonoBehaviour {
	
	private float distance;
	private float time;
	private float frameTime;
	private float distancePerSec;
	private bool direction;
	private bool upDown;
	// Use this for initialization
	void Start () {
		time = Mathf.RoundToInt(Time.time);
		distance = 3;
		time = 3;
		distancePerSec = distance / time;
	}
	
	// Update is called once per frame
	void Update () {
		
		frameTime += Time.deltaTime;
		float translate = (distancePerSec * Time.deltaTime);
		if(frameTime >= time)
		{
			translate -= (distancePerSec * (Time.deltaTime - (frameTime - time)));
			frameTime = 0;
			direction = !direction;
		}
		translate = (direction ? translate : -translate);
		if (upDown == false) {
			transform.position += new Vector3 (translate, 0, 0);
			this.upDown = true;
		}
		else {
			transform.position += new Vector3 (0, translate, 0);
		}
	}
}
