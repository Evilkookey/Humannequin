// HAND_INTERACT
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Interact : MonoBehaviour {

	void OnTriggerEnter(Collider interact_object)
	{
        // Check if interaction
		if (interact_object.tag == "Interact") 
		{
            // Send activate;
			interact_object.SendMessage ("Activate");
		}
	}
}
