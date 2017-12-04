//MUSIC_MANAGER.CS
//MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_manager : MonoBehaviour {

	//Ambient Music
	public AudioSource ambience;

	// This script will be used the future to play other ambient music 
	// and sfx using the random event manager script


	// Use this for initialization
	void Start () 
	{
		// Finds audio source
		ambience = GameObject.Find ("Ambience_Loop").GetComponent<AudioSource> ();

		// Plays sound on loop
		ambience.Play ();
	}
	
	// Update is called once per frame
	void Update () {

		//Make random haunt sound happen here, using a random event script
	}
}
