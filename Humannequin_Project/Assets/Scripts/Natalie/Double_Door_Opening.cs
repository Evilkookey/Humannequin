// DOUBLE_DOOR_OPENING.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Door_Opening : MonoBehaviour 
{
	// Variables
	// This holds the animator for the door to be opened
	Animator door_animator;
	// This bool is for checking whether or not the door is opening
	bool is_opening;

	void Start()
	{
		// Initialise variables
		door_animator = gameObject.GetComponent<Animator> ();
	}

	public void Activate()
	{
		// Play animation
		door_animator.SetBool("opening", true);

		// Play the sound
		gameObject.GetComponent<AudioSource>().Play();

		Debug.Log("DOOR");
	}
}
