using UnityEngine;
using System.Collections;

public class hotlineMiamiLogo : MonoBehaviour {
	
	private float distance;
	private float time;
	private float frameTime;
	private float distancePerSec;
	private bool  direction;
	private bool  upDown;
	private int  inverse;
	// Use this for initialization

	//audio
	private AudioSource  audioSrc;
	public AudioClip 	 sound;

	void Start () {
		this.audioSrc = gameObject.GetComponent<AudioSource> ();
		time = Mathf.RoundToInt(Time.time);
		distance = 3;
		time = 3;
		distancePerSec = distance / time;
		inverse = 0;
		this.upDown = false;
		audioSrc.PlayOneShot(sound);
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
			inverse++;
		}
		if (inverse < 2) {
			translate = (direction ? translate : -translate);
		} else {
			translate = (direction ? -translate : translate);
		}
		if (inverse > 3)
			inverse = 0;
		if (upDown == false) {
			transform.position += new Vector3 (translate, 0, 0);
		} else {
			transform.position += new Vector3 (0, translate, 0);
		}
	}
}
