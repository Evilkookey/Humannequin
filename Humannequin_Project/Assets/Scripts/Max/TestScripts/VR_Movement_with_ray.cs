using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Movement_with_ray : MonoBehaviour
{

	public Transform rig;			//SteamVr CameraRig
	public float speed;				//Speed for moving to player 

	//Button initialisation
	private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	//defining controller and tracked object
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	//private SteamVR_TrackedObject trackedObj2;

	//define Vector2 to store the touchpad axis
	private Vector2 axis = Vector2.zero;

	public float raycast_distance;
	public GameObject camera;
	public GameObject controller_object;
	public float difference;

	void Start()
	{
		//Find the tracked object on the gameobject
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		//Find the camera rig in the scene
		rig = GameObject.Find ("[CameraRig]").transform;

		difference = /*rig.position - */camera.transform.position.y - rig.position.y;
	}

	void Update()
	{
		//Debug.Log(camera.transform.position);

		//If error checking for controller initialisation
		if (controller == null)
		{
			Debug.Log("Controller not initialized");
			return;
		}

		//get input from the controller object
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		//get touch input from the controller
		if (controller.GetTouch(touchpad))
		{			//set touch input to the axis Vector2
			axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

			if (rig != null)
			{
				
				///Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
				///Debug.DrawRay (ray.origin, ray.direction);


				RaycastHit hit;
				// TEST FIX
				Vector3 test_fix = (controller_object.transform.right * axis.x + controller_object.transform.forward * axis.y);
				test_fix.y = 0;
				Ray collision_ray = new Ray (rig.position + (Vector3.up * difference), test_fix); // This should lock the y position

				Debug.DrawRay (collision_ray.origin,collision_ray.direction,Color.red);
			

				if (Physics.Raycast (collision_ray.origin, collision_ray.direction, out hit, raycast_distance)) 
				{
				//	Debug.Log (hit.collider.name);
					//gameObject.GetComponent<AudioSource>().Stop();
				} else 
				{
					//Move rig in position of the axis with delta time and speed variables
					rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime * speed;
					rig.position = new Vector3(rig.position.x, 0, rig.position.z);


				}

			}
		}

		Blackout_Check();
	}

	void Blackout_Check()
	{
		//RaycastHit hit;
		Vector3 ray_origin = rig.position + (Vector3.up * difference);
		Ray check_ray = new Ray (ray_origin, (camera.transform.position - ray_origin)); // This should lock the y position

		//Debug.Log(ray_origin);
		Debug.DrawRay (check_ray.origin, check_ray.direction,Color.blue);

		//check the raycast
		if (Physics.Raycast (check_ray.origin, check_ray.direction, Vector3.Distance(check_ray.origin, camera.transform.position) + 0.175f)) 
		{
			SteamVR_Fade.Start(Color.black, 0.1f, false);
		}
		else
		{
			SteamVR_Fade.Start(Color.clear, 0.1f, false);
		}
	}
}