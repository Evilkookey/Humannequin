using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incrimented_Turn : MonoBehaviour 
{
	//define the buttons
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	//define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	public float turn_incriment;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		//get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();
		turn_incriment = 30.0f; 
	}

	// Update is called once per frame
	void Update () 
	{
		//take input ID from controller
		device = SteamVR_Controller.Input((int)tracked_object.index);

		if (device.GetPressDown(touch_pad))
		{
			Vector2 axis = device.GetAxis(touch_pad);
			if (axis.x > 0.5f)
			{
				Debug.Log("right");
				player.transform.Rotate(Vector3.up * turn_incriment);
			}
			else if (axis.x < -0.5f)
			{
				Debug.Log("left");
				player.transform.Rotate(Vector3.down * turn_incriment);
			}
		}
	}
}
