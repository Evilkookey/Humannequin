// ACTIVATE_SCREWABLE_OBJECT.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Screwable_Object_new : MonoBehaviour 
{
	// Variables 
	string object_name;
	GameObject screw_controller;
	bool is_activated;
	Rigidbody object_rigidbody;
	Animator object_animator;


	GameObject colliding_object;

	void Start()
	{
		// Finds the screw_controller
		screw_controller = GameObject.Find ("screw_controller");
		object_animator = gameObject.GetComponent<Animator> ();
		object_rigidbody = gameObject.GetComponent<Rigidbody> ();
		is_activated = false;
		colliding_object = null;
	}

	public void Activate(string tag)
	{
		// Sets activated to true
		is_activated = true;

		Debug.Log(tag);

		if(tag == "SCREWDRIVER")
		{
			// Checks if it has been activated previously
			if (gameObject.GetComponent<Animator> ().enabled == true) 
			{
				// Gets the objects name
				object_name = gameObject.name;
				// Calls the interact function 
				//Debug.Log(object_name);
				screw_controller.SendMessage ("Interact", object_name);
			}
		}
	}

	void Fall_Down()
	{
		// Turn off animator
		object_animator.enabled = false;

		// Turn on gravity
		object_rigidbody.isKinematic = false;
		object_rigidbody.useGravity = true;
	}
}
