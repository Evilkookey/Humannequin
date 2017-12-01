using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call the script on the controller to tell it there has been 

public class Hand_Call : MonoBehaviour 
{
	// When something enters the trigger
	void OnTriggerEnter(Collider other)
	{
		// Call the parent object
		gameObject.SendMessageUpwards("ParentOnTriggerEnter", other);
	}

	// When something exits the trigger
	void OnTriggerExit(Collider other)
	{
		// Call the parent object
		gameObject.SendMessageUpwards("ParentOnTriggerExit", other);
	}
}
