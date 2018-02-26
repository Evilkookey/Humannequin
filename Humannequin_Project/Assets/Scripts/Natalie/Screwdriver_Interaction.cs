// SCREWDRIVER_INTERACTION.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver_Interaction : MonoBehaviour 
{
	// Variables
	// An int to hold the number of screws that have been activated
	int screw_counter;

	// A game object to store the current activated object
	GameObject activated_object;

	// This hold the activated object's animator
	Animator object_animator;

	// This is to identify which of the objects is the cover
	public GameObject cover;

	// The mannequin's script
	public Teleport_Mannequin_timer mannequin;

	// The line puzzle
	public GameObject line_puzzle;

	void Start()
	{
		// Initialises screw counter to 0
		screw_counter = 0;
	}

	public void Interact(string object_name)
	{
		// Finds all the required components of the activated object
		activated_object = GameObject.Find (object_name);
		object_animator = activated_object.GetComponent<Animator>();

		// If the object is not a cover, it must be a screw
		if (object_name != "cover") 
		{
			// Play animation
			object_animator.SetBool ("play", true);

			// Add one to the screw counter
			screw_counter = screw_counter + 1;
			// Check screw count
			if (screw_counter >= 4)
			{
				// Set the tag of the cover
				cover.tag = "Interact";
			}
			Debug.Log(screw_counter);
		}
		// If the object is a cover 
		if (object_name == "cover") 
		{
			// If screw counter is 4 then all screws have been interacted with
			if (screw_counter >= 4) 
			{
				// Play animation
				object_animator.SetBool ("play", true);

				// Activate the line puzzle
				//line_puzzle.SetActive(true);


				// Enable the mannequin
				mannequin.Enable_enemy();
			}
		}
	}
}