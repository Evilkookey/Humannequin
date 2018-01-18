//VR_INPUT2_WITH_JOINT.CS
//GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Input2_with_joint : MonoBehaviour 
{
	// Define the buttons
	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

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

	public enum Interaction_Type
	{
		NONE,
		INTERACT,
		HOLD,
		TOOL
	}
	public Interaction_Type type_identifier;

	// Object to save what object is held, collided with or used
	public GameObject held_object;
	public GameObject collide_object;
	public GameObject interact_object;

	public Transform tool_transform;

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

	// Use this for initialization
	void Start () 
	{
		// Get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();

		// Initialise interact type
		type_identifier = Interaction_Type.NONE;
	}

	// Update is called once per frame
	void Update () 
	{
		// Take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);

		// Check grip for pointing
		if (device.GetPressDown(grip_button))
		{
			// Make sure you arent holding a tool
			if (active_tool == Tool.NONE)
			{
				// Disable all hands
				Disable_Hands();
				// Set current hand to point
				hand_point.SetActive(true);
			}
		}

		if (device.GetPressUp(grip_button))
		{
			if (hand_point.activeInHierarchy)
			{
				// Disable all hands
				Disable_Hands();
				// Set current hand to point
				hand_regular.SetActive(true);
			}
		}

		// Press trigger
		if (device.GetPressDown(trigger_button))
		{
			if (type_identifier != Interaction_Type.NONE)
			{
				// Set the interact object to the collide object
				interact_object = collide_object;
				// Check the identifier
				switch (type_identifier)
				{
				case Interaction_Type.INTERACT:
					Debug.Log("activate was sent");
					interact_object.SendMessage("Activate", active_tool.ToString());
					break;
				case Interaction_Type.HOLD:
					Debug.Log("object picked up");
					held_object = interact_object.gameObject;

					// Connect the object with a fixed joint
					FixedJoint joint = AddFixedJoint(); //FixedJoint
					joint.connectedBody = held_object.GetComponent<Rigidbody>();
				
					break;
				case Interaction_Type.TOOL:
					if (active_tool == Tool.NONE)
					{
						// Remove tool from belt
						Debug.Log("tool selected");
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
					}
					// If the player has a tool and the tool is not in the slot
					else if (active_tool != Tool.NONE && interact_object.activeInHierarchy == false)
					{
						// Put the tool back in the belt
						interact_object.SetActive(true);
						Debug.Log("tool put back");
						// Set active tool to NONE
						active_tool = Tool.NONE;
						// Disable all hands
						Disable_Hands();
						// Set hand back to standard
						hand_regular.SetActive(true);
					}
					break;
				}
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
				Debug.Log("object dropped");
				// Remove the fixed joint
				ReleaseObject();
			}
		}
	}

	void ParentOnTriggerEnter(Collider other)
	{
		// Set the type of object it is
		if (other.tag == "Interact")
		{
			// Set the object to the one collided with
			collide_object = other.gameObject;

			type_identifier = Interaction_Type.INTERACT;
		}
		if (other.tag == "Pick_Up")
		{
			// Set the object to the one collided with
			collide_object = other.gameObject;

			type_identifier = Interaction_Type.HOLD;
		}
		if (other.tag == "ToolSlot")
		{
			// Find the tool in the slot
			collide_object = other.transform.GetChild(0).gameObject;

			// Set identifier to TOOL
			type_identifier = Interaction_Type.TOOL;
		}
	}

	void ParentOnTriggerExit(Collider other)
	{
		// Check if there is an object collided with
		if (collide_object)
		{
			// Set collide object to null
			collide_object = null;
			// Remove identifier
			type_identifier = Interaction_Type.NONE;
		}
	}

	// Creates joint
	FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	// For dropping the object
	void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{

			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			held_object.GetComponent<Rigidbody>().velocity = new Vector3(-device.velocity.z,device.velocity.y,device.velocity.x) ;
			held_object.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
		}
		held_object = null;
	}

	// Set hands to inactive
	void Disable_Hands()
	{
		hand_regular.SetActive(false);
		hand_point.SetActive(false);
		hand_wrench.SetActive(false);
		hand_screwdriver.SetActive(false);
		hand_pliers.SetActive(false);
		hand_torch.SetActive(false);
	}
}