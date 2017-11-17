// ACTIVATE_SCREWABLE_OBJECT.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Screwable_Object : MonoBehaviour 
{
	// Variables 
	string object_name;
	GameObject screw_controller;
	bool is_activated;

	void Start()
	{
		// Finds the screw_controller
		screw_controller = GameObject.Find ("screw_controller");
	}

	public void Activate()
	{
		// Sets activated to true
		is_activated = true;

		Testing ();
	}

	void Testing()
	{
		if (is_activated == true) 
		{
			// Checks if it has been activated previously
			if (gameObject.GetComponent<Animator> ().enabled == true) 
			{
				// Gets the objects name
				object_name = gameObject.name;
				// Calls the interact function 
				screw_controller.SendMessage ("Interact", object_name);
			}
			// Sets the activated bool to false so only calls the function once
			is_activated = false;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		// Checks if the object collided with it has been tagged "screwdriver"
		if (col.gameObject.CompareTag ("Screwdriver")) 
		{
			// Checks if the object has been activated 
			if (is_activated == true) 
			{
				// Checks if it has been activated previously
				if (gameObject.GetComponent<Animator> ().enabled == true) 
				{
					// Gets the objects name
					object_name = gameObject.name;
					// Calls the interact function 
					screw_controller.SendMessage ("Interact", object_name);
				}
				// Sets the activated bool to false so only calls the function once
				is_activated = false;
			}
		}
	}
}
