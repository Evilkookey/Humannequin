// DOOR_INTERACTION.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Interaction : MonoBehaviour 
{
	// Variables
	// Stores which type of door - exit or play
	public string door_type;

	// Stores the scene controller
	public GameObject scene_controller;

	AudioSource door_open_sound;

	// Stores the animator for the door (in the parent)
	Animator door_animator;

	void Start()
	{
		// Initialise the animator 
		door_animator = gameObject.GetComponentInParent<Animator> ();

		door_open_sound = gameObject.GetComponentInParent<AudioSource>();
	}

	public void Activate()
	{
		// Play animation
		door_animator.SetBool("opening", true);

		// Play sound
		door_open_sound.Play();


		// Call change scene
		Scene_Controller.Change_Scene(door_type);
	}
}
