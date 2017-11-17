// DOOR_INTERACTION.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Interaction : MonoBehaviour 
{
	// Variables
	public string door_type;
	public GameObject scene_controller;
	Animator door_animator;

	void Start()
	{
		// Initialise variables
		door_animator = gameObject.GetComponent<Animator> ();
	}

	public void Activate()
	{
		// Play animation
		door_animator.SetBool("opening", true);
		// Call change scene
		scene_controller.SendMessage ("Change_Scene", door_type);
	}
}
