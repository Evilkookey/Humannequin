// ELEVATOR_CONTROL.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Control : MonoBehaviour 
{
	// Variables
	// This holds the animator for the elevator doors to be opened
	Animator door_animator;

	// The lights in the main menu scene
	public GameObject elevator_light;

	// The elevator mannequin
	GameObject mannequin;

	// Use this for initialization
	void Start ()
	{
		// Set door_animator to be the elevators animator
		door_animator = gameObject.GetComponentInParent<Animator> ();

		// Play the animation
		door_animator.SetBool ("opening", true);

		// Set the mannequin tot be the mannequin in the elevator
		mannequin = GameObject.Find("idle_mannequin");
		mannequin.SetActive (false);
	}

	// Check for collisions with the player
	void OnTriggerEnter(Collider col)
	{
		// Checks if colliding with player, if so then move player object to next scene
		//if(col.gameObject.name == "[CameraRig]")
		if(col.gameObject.name == "FPSController")
		{
			// Make mannequin active
			mannequin.SetActive (true);

			// Play the animation
			door_animator.SetBool("closing", true);
			door_animator.SetBool("opening", false);

			// Turn off elevator light
			elevator_light.SendMessage("Light_Off"); 
		}
	}
}
