﻿// VR_MOVEMENT_WITH_RAY.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Movement_with_ray : MonoBehaviour
{

	public Transform rig;				//SteamVr CameraRig
	public float speed;					//Speed for moving to player 

	//Button initialisation
	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	// Defining controller and tracked object
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	// Vector2 to store the touchpad axis
	private Vector2 axis = Vector2.zero;

	public float raycast_distance; 		 // Distance for the ray to travel
	public GameObject camera_object; 	 // VR headset object -- Renamed from camera 
	public GameObject controller_object; // VR controller to use

	void Start()
	{
		// Avoid certain collisions
		Physics.IgnoreLayerCollision(8/*Player*/, 9/*Ignore_Player*/, true);

		// Find the tracked object on the gameobject
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		// Find the camera rig in the scene
		rig = GameObject.Find ("[CameraRig]").transform;
	}

	void Update()
	{
		if(Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING)
		{
			
			//Debug.Log(camera_object.transform.position);

			// Error checking for controller initialisation
			if (controller == null)
			{
				Debug.Log("Controller not initialized");
				return;
			}

			// Get input from the controller object
			var device = SteamVR_Controller.Input((int)trackedObj.index);

			// Get touch input from the controller
			if (controller.GetTouch(touchpad))
			{			
				// Set touch input to the axis Vector2
				axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

				// If there is a rig
				if (rig != null)
				{
					// Move rig in position of the axis with delta time and speed variables
					rig.Translate(((Vector3.right * axis.x) + (Vector3.forward * axis.y)) * Time.deltaTime * speed/*, Space.Self*/);
					rig.position = new Vector3(rig.position.x, rig.position.y, rig.position.z);
				}
			}
		}


		// Fade out screen if head is placed in an object
		Blackout_Check();
	}

	void Blackout_Check()
	{
		// Create ray from calclauted vr headset position in the direction of the headset movement
		Vector3 ray_origin = new Vector3(rig.position.x, camera_object.transform.position.y, rig.position.z);
		Ray check_ray = new Ray (ray_origin, (camera_object.transform.position - ray_origin)); 

		//Debug.Log(ray_origin);

		// Draw the ray in the editor
		Debug.DrawRay (check_ray.origin, check_ray.direction,Color.blue);

		// Check for raycast collisions
		if (Physics.Raycast (check_ray.origin, check_ray.direction, Vector3.Distance(check_ray.origin, camera_object.transform.position) + 0.175f)) 
		{
			// Fade to black
			SteamVR_Fade.Start(Color.black, 0.1f, false);
		}
		else if (Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING || Game_State_Controller.current_state == Game_State_Controller.Game_States.PAUSED)
		{
			// Fade back to clear
			SteamVR_Fade.Start(Color.clear, 0.1f, false);
		}
	}
}