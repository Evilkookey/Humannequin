// ACTIVATE_SCREWABLE_OBJECT.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Screwable_Object : MonoBehaviour 
{
	// Variables
	// Stores the name of the current object
	string object_name;

	// Stores the controller for the screwing mechanic
	GameObject screw_controller;

	// A bool for checking to see if the object has been interacted with
	bool is_activated;

	// Stores the rigidbody of the current object
	Rigidbody object_rigidbody;

	// Stores the animator of the current object
	Animator object_animator;

	void Start()
	{
		// Finds the screw_controller
		screw_controller = GameObject.Find ("screw_controller");
		// Initialises the animator and the rigidbody
		object_animator = gameObject.GetComponent<Animator> ();
		object_rigidbody = gameObject.GetComponent<Rigidbody> ();

		// Initialise is_activated to false
		is_activated = false;

		// Get the name of the current game object
		object_name = gameObject.name;
	}

	public void Activate(string tag)
	{
		Debug.Log(tag);

		// If the object is being interacted with by a screwdriver
		if(tag == "SCREWDRIVER")
		{
			// Checks if the object is not a cover
			if (gameObject.name != "cover") 
			{
				// Checks if it has been activated previously
				if (is_activated == false) 
				{
					// Sets activated to true
					is_activated = true;
					// Gets the objects name
					object_name = gameObject.name;
					// Calls the interact function 
					Debug.Log (object_name);

					// Calls the Interact function in the screw controller and passes the objects name
					screw_controller.SendMessage ("Interact", object_name);
				
				}
			}
		}
		// If the player is holding no tool
		else if(tag == "NONE")
		{
			// Checks if the game object is a cover
			if(gameObject.name == "cover")
			{
				// Calls the Interact function in the screw controller and passes the objects name
				screw_controller.SendMessage ("Interact", object_name);
			}
		}

	}

	void Fall_Down()
	{
		// Turn off animator
		object_animator.enabled = false;

		// Turn on gravity and make it not kinematic
		object_rigidbody.isKinematic = false;
		object_rigidbody.useGravity = true;
	}
}