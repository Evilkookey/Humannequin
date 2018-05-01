// INCRIMENTED_TURN.CS
// GREG BALBIRNIE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incrimented_Turn : MonoBehaviour 
{
	// Define the buttons
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	// Define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	public float turn_incriment;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		// Get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();
		turn_incriment = 30.0f; 
	}

	// Update is called once per frame
	void Update () 
	{
		// Take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);

        // If player presses touch pad
		if (device.GetPressDown(touch_pad))
		{
			Vector2 axis = device.GetAxis(touch_pad);
            // If on right side
			if (axis.x > 0.5f)
			{
                // Turn right
				player.transform.Rotate(Vector3.up * turn_incriment);
			}
            // If on left side
			else if (axis.x < -0.5f)
			{
                // Turn left
				player.transform.Rotate(Vector3.down * turn_incriment);
			}
		}
	}
}
