using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Input : MonoBehaviour 
{
	//define the buttons
	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	public Valve.VR.EVRButtonId pause_button = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;	// NAT

	//define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	//object to save what object is held
	GameObject held_object;

	// Pause menu controller
	GameObject pause_menu_controller;	// NAT

	// Use this for initialization
	void Start () 
	{
		//get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();

		// Get the pause menu controller
		pause_menu_controller = GameObject.Find("pause_controller");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);
		//check input
		if (device.GetPressDown(trigger_button))
		{
		//	Debug.Log("Trigger was pressed");
		}
		if (device.GetPress(grip_button))
		{
		//	Debug.Log("Grip was pressed");
		}
		if (device.GetTouch(touch_pad))
		{
		//	Debug.Log(device.GetAxis(touch_pad));
		}
		if (device.GetPress (pause_button)) 
		{
			Debug.Log("pause button pressed");
			// enables the pause menu
			pause_menu_controller.SendMessage ("Activate");
		}
		if (device.GetPressUp(trigger_button))
		{
			if (held_object)
			{
				Debug.Log("object dropped");
				Rigidbody rb = held_object.GetComponent<Rigidbody> ();
				rb.isKinematic = false;
				held_object.transform.parent = null;
				held_object = null;
			}
		}
	}


	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Interact")
		{
			device = SteamVR_Controller.Input((int)tracked_object.index);
			if (device.GetPressDown(trigger_button))
			{
			//	Debug.Log("activate was sent");
				other.SendMessage("Activate");
			}
		}
		if (other.tag == "Pick_Up")
		{
			device = SteamVR_Controller.Input((int)tracked_object.index);
			if (device.GetPressDown(trigger_button))
			{
				Debug.Log("object picked up");
				held_object = other.gameObject;
				held_object.transform.parent = gameObject.transform;
				Rigidbody rb = held_object.GetComponent<Rigidbody> ();
				rb.isKinematic = true;
			}
		}
	}
}
