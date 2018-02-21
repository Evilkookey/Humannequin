﻿//VR_INPUT2_WITH_JOINT.CS
//GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class VR_Input_Ver_4 : MonoBehaviour 
{
	// Define the buttons
	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	public Valve.VR.EVRButtonId pause_button = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;	// NAT

	// Define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	// Define hand models
	public GameObject hand_regular;
	public GameObject hand_point;
	public GameObject hand_wrench;
	public GameObject hand_screwdriver;
	public GameObject hand_pliers;
	public GameObject hand_torch;

	// Object to save what object is held, collided with or used
	public GameObject held_object;

	// The object th player is colliding with and will interact with
	public List<GameObject> collide_objects;
	public GameObject interact_object;

	// Using tools
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
	// How long the pause button is held
	public float pause_timer;

	// The other hand
	GameObject other_hand;

	// Animator for the hand and other variables for hand animations
	public Animator hand_animator;
	float trigger_axis;
	Vector3 default_hand_size, default_hand_center;
	BoxCollider hand_box_collider, point_hand_box_collider;

	// Use this for initialization
	void Start () 
	{
		// Get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();

		// Get the pause menu controller
		pause_menu_controller = GameObject.Find("pause_controller");

		// If this is the left hand
		if (gameObject.name == "Controller (left)")
		{
			// Set the other hand to the right hand
			other_hand = GameObject.Find("Controller (right)");
		}
		// If this is the right hand
		else if (gameObject.name == "Controller (right)")
		{
			// Set the other hand to the left hand
			other_hand = GameObject.Find("Controller (left)");
		}

		// Initialise the pause timer
		pause_timer = 0.0f;

		// Initialise the animator and other variables
		hand_animator = hand_regular.GetComponent<Animator>();
		hand_box_collider = hand_regular.GetComponent<BoxCollider>();
		point_hand_box_collider = hand_point.GetComponent<BoxCollider>();

		default_hand_center = hand_box_collider.center;
		default_hand_size = hand_box_collider.size;


	}

	// Update is called once per frame
	void Update () 
	{
		// Take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);

		// Take trigger axis value for test
		//print (device.GetAxis (trigger_button).x);

		// Sets the grab value in the hand animator 
		trigger_axis = device.GetAxis(trigger_button).x;
		hand_animator.SetFloat("Grab", trigger_axis);
		// TODO
		// You gotta reset this value when you change back to the regular hand model

		/*if(trigger_axis >= 0.5f)
		{
			hand_box_collider.center = new Vector3(default_hand_center.x,default_hand_center.y,0.01045922f);
			hand_box_collider.size = new Vector3(default_hand_size.x,default_hand_size.y,0.2508576f);
		}
		else
		{
			hand_box_collider.center = default_hand_center;
			hand_box_collider.size = default_hand_size;
		}*/

		// Only point if there is no object being held
		if (!held_object)
		{
			
			// Check grip for pointing
			if (device.GetPressDown(grip_button))
			{
				// Make sure you arent holding a tool
				if (active_tool == Tool.NONE)
				{
					// Plays the pointing hand animation in the animator
					hand_animator.SetBool("Pointing",true);				

					hand_box_collider.center = point_hand_box_collider.center;
					hand_box_collider.size = point_hand_box_collider.size;

					//TODO
					// We could enable the box collider when animation is over (somehow)


					// Disable all hands
					//Disable_Hands();

					//if(hand_point.GetComponent<Animator>().
					// Set current hand to point
					//hand_point.SetActive(true);
				}
			}

			// Check release of grip
			if (device.GetPressUp(grip_button))
			{
				// Stops to hand pointing animation;
				hand_animator.SetBool("Pointing", false);

				//Reset the hand box collider
				hand_box_collider.center = default_hand_center;
				hand_box_collider.size = default_hand_size;

				if (hand_point.activeInHierarchy)
				{
					// Disable all hands
					//Disable_Hands();
					// Set current hand to point
					//hand_regular.SetActive(true);
				}
			}
		}

		// Press trigger
		if (device.GetPressDown(trigger_button))
		{
			// Loop through all objects colided with
			foreach (GameObject collide in collide_objects)
			{
				// Check the identifier
				switch (collide.tag)
				{
				case "Interact":
					// Set the interact object to the collide object
					interact_object = collide;

					// Send activate with held tool
					interact_object.SendMessage("Activate", active_tool.ToString());
					break;
				case "Pick_Up":
					// Check if the player has a regular hand, not a tool and there is no current held object and player is not pointing
					if (hand_regular.activeInHierarchy && !held_object && !hand_animator.GetBool("Pointing"))
					{
						// Set the held object to the collide object
						held_object = collide; 

						// Take this object from the other hand
						other_hand.SendMessage("BreakJoint", held_object);

						// Connect the object with a fixed joint
						FixedJoint joint = AddFixedJoint(); //FixedJoint
						joint.connectedBody = held_object.GetComponent<Rigidbody>();
					}
					break;
				case "ToolSlot":	
					// Only change to a tool if holding nothing
					if (!held_object)
					{
						// Set the interact object to the collide object's child (the tool)
						interact_object = collide.transform.GetChild (0).gameObject;

						// If the player has no tool and the tool is in the slot
						if (active_tool == Tool.NONE && interact_object.activeInHierarchy)
						{
							// Remove tool from belt
							interact_object.SetActive(false);

							// Check the name of the tool and apply to hand 
							switch (interact_object.name)
							{
							case "Wrench":
								// Set the active tool to WRENCH
								active_tool = Tool.WRENCH;
								// Disable all hands
								Disable_Hands();
								// Set correct hands to active
								hand_wrench.SetActive(true);
								break;
							case "Screwdriver":
								// Set the active tool to SCREWDRIVER
								active_tool = Tool.SCREWDRIVER;
								// Disable all hands
								Disable_Hands();
								// Set correct hands to active
								hand_screwdriver.SetActive(true);
								break;
							case "Torch":
								// Set the active tool to TORCH
								active_tool = Tool.TORCH;
								// Disable all hands
								Disable_Hands();
								// Set correct hands to active
								hand_torch.SetActive(true);
								break;
							case "Pliers":
								// Set the active tool to PLIERS
								active_tool = Tool.PLIERS;
								// Disable all hands
								Disable_Hands();
								// Set correct hands to active
								hand_pliers.SetActive(true);
								break;
							}

							// Reset hand animation
							hand_animator.SetFloat("Grab", 1.0f);
						}
						// If the player has a tool and the tool is not in the slot
						else if (active_tool != Tool.NONE && !interact_object.activeInHierarchy)
						{
							// Check if the tool in the slot is the active tool
							if (interact_object.name.ToLower() == active_tool.ToString().ToLower())
							{
								// Put the tool back in the belt
								interact_object.SetActive(true);
								// Set active tool to NONE
								active_tool = Tool.NONE;
								// Disable all hands
								Disable_Hands();
								// Set hand back to standard
								hand_regular.SetActive(true);
							}
						}
					}
					break;
				}
				// Reset interact object
				//interact_object = null;
			}
		}
		// Release trigger
		if (device.GetPressUp(trigger_button))
		{
			// If there is an interact object
			if (interact_object) 
			{
				// Send a deactivate call the the interact object
				interact_object.SendMessage("Deactivate");
				// Set interact object to null
				interact_object = null;
			}
			// If the player is holding an object drop it
			if (held_object)
			{
				// Remove the fixed joint
				ReleaseObject();

				//trigger_axis = 1;
				hand_animator.SetFloat("Grab", 1);
			}
		}

		//If the player holds pause
		if (device.GetPress (pause_button)) 
		{
			// Start the pause timer
			pause_timer += Time.deltaTime;

			// If the button is held for longer than a second
			if (pause_timer >= 1.0f)
			{
				//Find the toolbelt and set it to hand height
				GameObject.Find("Toolbelt").SendMessage("Set_Toolbelt_Height", gameObject.transform.position.y);
			}
		} 

		//If the player releases pause
		if (device.GetPressUp (pause_button)) 
		{
			//Check the pause timer is below 1 sec
			if (pause_timer < 1.0f)
			{
				// Enables the pause menu
				pause_menu_controller.SendMessage ("Activate");
			}

			// Reset pause timer
			pause_timer = 0.0f;
		} 
	}

	void OnTriggerEnter(Collider other)
	{
		// If the object can be used in some way
		if (other.tag =="Interact" || other.tag == "Pick_Up" || other.tag == "ToolSlot")
		{
			// If the object has no outline
			if (!other.gameObject.GetComponent<Outline>()) 
			{
				// add an outline and turn it on
				other.gameObject.AddComponent<Outline> ()/*.enabled = true*/;
			}

			// Set the type of object it is
			if (other.tag == "Interact")
			{
				// Set the object to the one collided with
				collide_objects.Add(other.gameObject);
			}
			if (other.tag == "Pick_Up")
			{
				// Set the object to the one collided with
				collide_objects.Add(other.gameObject);
			}
			if (other.tag == "ToolSlot")
			{
				// Add the slot to the list
				collide_objects.Add(other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		// If the object has an outline
		if (other.gameObject.GetComponent<Outline>()) 
		{
			// remove the outline
			Component.Destroy(other.gameObject.GetComponent<Outline> ());
		}

		// Check if there is an object collided with
		if (collide_objects.Contains(other.gameObject))
		{
			// Remove object
			collide_objects.Remove(other.gameObject);
		}
	}

	// Creates joint
	FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		/*fx.breakForce = 20000;
		fx.breakTorque = 20000;*/

		fx.breakForce = Mathf.Infinity + 1.0f;
		fx.breakTorque = Mathf.Infinity + 1.0f;

		return fx;
	}

	// Break the joint
	void BreakJoint(GameObject new_held)
	{
		// Check if this hand is holding the object the other hand wants to pick up
		if (held_object == new_held)
		{
			// Find the joint
			FixedJoint fx = gameObject.GetComponent<FixedJoint>();

			// Destroy the joint
			Destroy(fx);
		}
	}

	// For dropping the object
	void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{

			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			held_object.GetComponent<Rigidbody>().velocity = (device.velocity.x * gameObject.transform.parent.right + device.velocity.y * gameObject.transform.parent.up + device.velocity.z * gameObject.transform.parent.forward);
			held_object.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
		}
		held_object = null;
	}

	// Set hands to inactive
	void Disable_Hands()
	{
		hand_regular.SetActive(false);
		//hand_point.SetActive(false);
		hand_wrench.SetActive(false);
		hand_screwdriver.SetActive(false);
		hand_pliers.SetActive(false);
		hand_torch.SetActive(false);

		// Remove the outlines
		foreach (GameObject obj in collide_objects)
		{
			// If the object has an outline
			if (obj.gameObject.GetComponent<Outline>()) 
			{
				// remove the outline
				Component.Destroy(obj.gameObject.GetComponent<Outline> ());
			}
		}

		// Clear the collide list
		collide_objects.Clear();
	}

	// Activate a tool in the toolbelt
	void Collect_Tool (string tool_name)
	{
		//Find all the tools in the hierarchy
		GameObject wrench, screwdriver, torch, pliers;

		wrench = GameObject.Find("Wrench");
		screwdriver = GameObject.Find("Screwdriver");
		torch = GameObject.Find("Torch");
		pliers = GameObject.Find("Pliers");

		// Set to lowercase to reduce bug chance
		tool_name = tool_name.ToLower();

		// Check which tool was picked up
		switch (tool_name)
		{
		case "wrench":
			wrench.SetActive(true);
			break;
		case "torch":
			torch.SetActive(true);
			break;
		case "pliers":
			pliers.SetActive(true);
			break;
		case "screwdriver":
			screwdriver.SetActive(true);
			break;
		}
	}
}