using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_manager : MonoBehaviour {

	//Ambient Music
	public AudioSource ambience;


	// Use this for initialization
	void Start () {
		ambience = GameObject.Find ("Ambience_Loop").GetComponent<AudioSource> ();

		ambience.Play ();
	}
	
	// Update is called once per frame
	void Update () {

		//Make random haunt sound happen here, using a random event script GREG
	}
}
