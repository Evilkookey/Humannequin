// DOUBLE_DOOR_OPENING.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Door_Opening : MonoBehaviour 
{
	// Variables
	Animator door_animator;
	bool is_opening;

	void Start()
	{
		// Initialise variables
		door_animator = gameObject.GetComponent<Animator> ();
		//is_opening = door_animator.GetBool("opening");
	}

	public void Activate()
	{
		// Play animation
		//is_opening = true;
		door_animator.SetBool("opening", true);
	}
}
