// HAND_INTERACT
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Interact : MonoBehaviour {

	void OnTriggerEnter(Collider interact_object)
	{
		if (interact_object.tag == "Interact") 
		{
			interact_object.SendMessage ("Activate");
		}
	}
}
