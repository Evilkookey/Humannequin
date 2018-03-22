// TEST_BUTTON_INPUT.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_button_input : MonoBehaviour {

	public GameObject[] buttons;
	public GameObject on_button;

	// Use this for initialization
	void Start () 
	{
		buttons = new GameObject[6];
		buttons[0] = GameObject.Find("button_red");
		buttons[1] = GameObject.Find("button_green");
		buttons[2] = GameObject.Find("button_blue");
		buttons[3] = GameObject.Find("button_yellow");
		buttons[4] = GameObject.Find("button_purple");
		buttons[5] = GameObject.Find("button_white");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Take player input (placeholder)
		switch (Input.inputString)
		{
		case "z":
			//Debug.Log("input in test");
			buttons[0].SendMessage("Activate");
			break;
		case "x":
			buttons[1].SendMessage("Activate");
			break;
		case "c":
			buttons[2].SendMessage("Activate");
			break;
		case "v":
			buttons[3].SendMessage("Activate");
			break;
		case "b":
			buttons[4].SendMessage("Activate");
			break;
		case "n":
			buttons[5].SendMessage("Activate");
			break;
		case "o":
			on_button.SendMessage("Activate");
			break;
		}
	}
}

