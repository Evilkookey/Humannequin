﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Input2 : MonoBehaviour 
{
	//define the buttons
	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	public Valve.VR.EVRButtonId pause_button = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;	// NAT

	//define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	public enum Interaction_Type
	{
		NONE,
		INTERACT,
		HOLD,
		TOOL
	}
	public Interaction_Type type_identifier;

	//object to save what object is held
	public GameObject held_object;
	public GameObject interact_object;

	public Transform tool_transform;

	//using tools
	public enum Tool
	{
		NONE,
		WRENCH,
		SCREWDRIVER,
		PLIERS,
		TORCH
	}
	public Tool active_tool;

	// Pause menu controller
	GameObject pause_menu_controller;	// NAT

	// Use this for initialization
	void Start () 
	{
		//get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();

		//initialise interact type
		type_identifier = Interaction_Type.NONE;

		// Get the pause menu controller
		pause_menu_controller = GameObject.Find("pause_controller");
	}

	// Update is called once per frame
	void Update () 
	{
		//take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);

		//press trigger
		if (device.GetPressDown(trigger_button))
		{
			if (type_identifier != Interaction_Type.NONE)
			{
				switch (type_identifier)
				{
				case Interaction_Type.INTERACT:
					Debug.Log("activate was sent");
					interact_object.SendMessage("Activate", active_tool.ToString());
					break;
				case Interaction_Type.HOLD:
					Debug.Log("object picked up");
					held_object = interact_object.gameObject;
					held_object.transform.parent = gameObject.transform;
					Rigidbody rb = held_object.GetComponent<Rigidbody> ();
					rb.isKinematic = true;
					break;
				case Interaction_Type.TOOL:
					if (active_tool == Tool.NONE)
					{
						//remove tool from belt
						Debug.Log("tool selected");
						interact_object.SetActive(false);

						//check the name of the tool and apply to hand 
						switch (interact_object.name)
						{
						case "Wrench":
							active_tool = Tool.WRENCH;
							break;
						case "Screwdriver":
							active_tool = Tool.SCREWDRIVER;
							break;
						case "Torch":
							active_tool = Tool.TORCH;
							break;
						case "Pliers":
							active_tool = Tool.PLIERS;
							break;
						}
					}
					//if the player has a tool and the tool is not in the slot
					else if (active_tool != Tool.NONE && interact_object.activeInHierarchy == false)
					{
						//put the tool back in the belt
						interact_object.SetActive(true);
						Debug.Log("tool put back");
						active_tool = Tool.NONE;
					}
					break;
				}
			}
		}

		//release trigger
		if (device.GetPressUp(trigger_button))
		{
			//if the player is holding an object drop it
			if (held_object)
			{
				Debug.Log("object dropped");				
				Rigidbody rb = held_object.GetComponent<Rigidbody> ();
				rb.isKinematic = false;
				held_object.transform.parent = null;
				held_object = null;
			}
		}

		if (device.GetPressDown (pause_button)) 
		{
			Debug.Log("pause button pressed");
			// enables the pause menu
			pause_menu_controller.SendMessage ("Activate");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		//set the type of object it is
		if (other.tag == "Interact")
		{
			//set the object to the one to be interacted with
			interact_object = other.gameObject;

			type_identifier = Interaction_Type.INTERACT;
		}
		if (other.tag == "Pick_Up")
		{
			//set the object to the one to be interacted with
			interact_object = other.gameObject;

			type_identifier = Interaction_Type.HOLD;
		}
		if (other.tag == "ToolSlot")
		{
			//find tool from transform
			///*Transform*/ tool_transform = other.GetComponentInChildren<Transform>();
			//interact_object = tool_transform.gameObject;
			interact_object = other.transform.GetChild(0).gameObject;

			type_identifier = Interaction_Type.TOOL;
		}
	}

	void OnTriggerExit(Collider other)
	{
		//set the object to the one to be interacted with
		if (interact_object)
		{
			interact_object = null;
			type_identifier = Interaction_Type.NONE;
		}
		
		//set the type of object it is
		/*if (other.tag == "Interact")
		{
			type_identifier = Interaction_Type.INTERACT;
		}
		if (other.tag == "Pick_Up")
		{
			type_identifier = Interaction_Type.HOLD;
		}*/
	}
}