using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call the script on the controller to tell it there has been 

public class Hand_Call : MonoBehaviour 
{
	public bool is_pliers;

	// When something enters the trigger
	void OnTriggerEnter(Collider other)
	{
		// Call the parent object to say it has entered
		gameObject.SendMessageUpwards("ParentOnTriggerEnter", other);
	}

	// When something exits the trigger
	void OnTriggerExit(Collider other)
	{
		// Call the parent object to say it has exited
		gameObject.SendMessageUpwards("ParentOnTriggerExit", other);
	}


}
