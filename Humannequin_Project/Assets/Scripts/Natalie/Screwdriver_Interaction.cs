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

	// An int to hold the number of screws to be unscrewed before the cover can be interacted with
	public int screws;

	// This hold the activated object's animator
	Animator object_animator;

	// Cover object
	public GameObject cover;

	void Start()
	{
		// Initialises screw counter to 0
		screw_counter = 0;
	}

	public void Interact(GameObject activated_object, Activate_Screwable_Object.Object_Type object_type)
	{
		// Finds all the required components of the activated object
		object_animator = activated_object.GetComponent<Animator>();

		// If the object is not a cover, it must be a screw
		if (object_type != Activate_Screwable_Object.Object_Type.COVER) 
		{
			// Play animation
			object_animator.SetBool ("play", true);

			// Add one to the screw counter
			screw_counter = screw_counter + 1;
			// Check screw count
			if (screw_counter >= screws)
			{
				// Set the tag of the cover
				cover.tag = "Interact";
			}
			Debug.Log(screw_counter);
		}
		// If the object is a cover 
		if (object_type == Activate_Screwable_Object.Object_Type.COVER) 
		{
			// If screw counter is 4 then all screws have been interacted with
			if (cover.tag == "Interact") 
			{
                // Play animation
                print("interact with cover now pls");
                //object_animator.SetBool ("play", true);
                cover.GetComponent<Rigidbody>().isKinematic = false;

			}
		}
	}
}