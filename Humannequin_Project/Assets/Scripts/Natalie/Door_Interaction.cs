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

	// Stores the animator for the door
	Animator door_animator;

	void Start()
	{
		// Initialise the animator
		door_animator = gameObject.GetComponent<Animator> ();
	}

	public void Activate()
	{
		// Play animation
		door_animator.SetBool("opening", true);
			
		// Call change scene
		Scene_Controller.Change_Scene(door_type);
	}
}
