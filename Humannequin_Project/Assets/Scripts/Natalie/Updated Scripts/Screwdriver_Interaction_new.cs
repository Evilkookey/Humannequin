// SCREWDRIVER_INTERACTION.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver_Interaction_new : MonoBehaviour 
{
	// Variables
	int screw_counter;
	GameObject activated_object;
	Animator object_animator;

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
		}
		// If the object is a cover 
		if (object_name == "cover") 
		{
			// If screw counter is 4 then all screws have been interacted with
			if (screw_counter == 4) 
			{
				// Play animation
				object_animator.SetBool ("play", true);
			}
		}
	}
}
