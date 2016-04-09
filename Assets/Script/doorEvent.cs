using UnityEngine;
using System.Collections;

public class doorEvent : MonoBehaviour {


	// Use this for initialization
	void Start () {
		// Initialiser en fermeture
		// initialiser collider physique enable
	}

	private void OnTriggerEnter2D(){
		// lancer animation ouverture porte
		// cacher collider (physique, non pas trigger)
	}

	private void OnTriggerExit2D(){
		// lancer annimation fermeture porte
		///reafficher collider Physique
	}
}
