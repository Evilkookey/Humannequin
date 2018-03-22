// NEW_TOOL.CS
// GREG BALBIRNIE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Tool : MonoBehaviour {

	Toolbelt toolbelt;

	// Use this for initialization
	void Start () 
	{
		// Find the toolbelt
		toolbelt = GameObject.Find("Toolbelt").GetComponent<Toolbelt>();
	}
	
	// Update is called once per frame
/*	void Update () 
	{
		
	}*/

	// Activate is called when the player clicks on this object
	void Activate(string tool_used)
	{
		// If player has no tool active
		if (tool_used == "NONE")
		{
			// Put new tool in belt
			if (toolbelt.New_Tool(gameObject.name))
			{
				// Delete the loose tool
				//Destroy (gameObject);
				// Deactivate the loose tool
				gameObject.SetActive(false);
			}
			else
			{
				print ("Could not pick up new tool");
			}
		}
	}
}