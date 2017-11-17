using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public string input_text;	//the colour of the button in text
	public GameObject puzzle_object;		//the object the puzzle is on

	//start is called when the scene starts
	void Start ()
	{
		puzzle_object = GameObject.Find("control_panel");
	}

	//Activate is called when the player interacts with this object
	void Activate ()
	{
		//Debug.Log("input in button");
		//send the colour to the puzzle script
		gameObject.SendMessageUpwards ("Get_Player_Input", input_text);
	}
}
