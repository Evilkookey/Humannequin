using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Movement : MonoBehaviour
{
	public Transform rig;			//SteamVr CameraRig
	public float speed;				//Speed for moving to player 

	//Button initialisation
	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad; 	

	//defining controller and tracked object
	private SteamVR_Controller.Device controller;// { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject tracked_obj;

	//define Vector2 to store the touchpad axis
	private Vector2 axis = Vector2.zero;


	void Start()
	{
		//Find the tracked object on the gameobject
		tracked_obj = GetComponent<SteamVR_TrackedObject>();

		//Find the camera rig in the scene
		rig = GameObject.Find ("[CameraRig]").transform;
	}

	void Update()
	{
		//If error checking for controller initialisation
		if (controller == null)
		{
			Debug.Log("Controller not initialized");
			return;
		}

		//get input from the controller object
		controller = SteamVR_Controller.Input((int)tracked_obj.index);

		//get touch input from the controller
		if (controller.GetTouch(touchpad))
		{
			//set touch input to the axis Vector2
			axis = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

			//If rig is set to something
			if (rig != null) 
			{
				//Move rig in position of the axis with delta time and speed variables
				rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime * speed;
				rig.position = new Vector3 (rig.position.x, 0, rig.position.z);

			} else 
			{
				//Error checking
				Debug.Log ("Rig not initialised");
			}
		}

	}
}