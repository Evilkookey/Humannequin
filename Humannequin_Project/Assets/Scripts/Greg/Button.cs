using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public int input_number;	// The number of the button
	public GameObject puzzle_object;		// The object the puzzle is on

	//start is called when the scene starts
	void Start ()
	{
		puzzle_object = GameObject.Find("control_panel");
	}

	//Activate is called when the player interacts with this object
	void Activate (string active_tool)
	{
		//Debug.Log("input in button");
		// If the player is not holding a tool
		if (active_tool == "NONE")
		{
			// Send the colour to the puzzle script
			gameObject.SendMessageUpwards ("Get_Player_Input", input_number);
		}

	}
}