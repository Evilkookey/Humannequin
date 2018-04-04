// VR_MOVEMENT_WITH_RAY.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Movement_with_ray : MonoBehaviour
{

	public Transform rig;				//SteamVr CameraRig
	public float speed;					//Speed for moving to player 
    public float counter;
    public int no_of_presses = 0;
    public bool double_tap = false; 

    //Button initialisation
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	// Defining controller and tracked object
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	// Vector2 to store the touchpad axis
	private Vector2 axis = Vector2.zero;
    
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
		if(Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING || Game_State_Controller.current_state == Game_State_Controller.Game_States.WIN)
		{
			
			//Debug.Log(camera_object.transform.position);

			// Error checking for controller initialisation
			if (controller == null)
			{
				Debug.Log("Controller not initialized");
				return;
			}

            // Get touch input from the controller
            if (controller.GetPressDown(touchpad))
            {
                no_of_presses ++;

               
            }
            if (controller.GetPressUp(touchpad) && double_tap)
            {
                double_tap = false;
                no_of_presses = 0;
                counter = 0.0f;
            }

            if (no_of_presses > 0)
            {
                counter += 1.0f/60.0f;
            }
            
            // Get input from the controller object
            var device = SteamVR_Controller.Input((int)trackedObj.index);

			// Get touch input from the controller
			if (controller.GetPress(touchpad))
			{
                // Set touch input to the axis Vector2
                axis = device.GetAxis(touchpad);

				// If there is a rig
				if (rig != null)
				{

                    //no_of_presses++;
                    if (no_of_presses == 2)
                    {
                        if (counter < 0.3f)
                        {

                            double_tap = true;
                        }
                        else
                        {
                            no_of_presses = 0;
                            double_tap = false;
                        }

                        no_of_presses = 0;
                        counter = 0.0f;

                    }
                    if (!double_tap)
                    {
                        // Move rig in position of the axis with delta time and speed variables
                        rig.Translate(((controller_object.transform.right * axis.x) + (controller_object.transform.forward * axis.y)) * Time.deltaTime * speed/*, Space.Self*/);
                    }
                    else
                    {
                        // Move rig in position of the axis with delta time and double speed variables - Sprint
                        rig.Translate(((controller_object.transform.right * axis.x) + (controller_object.transform.forward * axis.y)) * Time.deltaTime * speed * 2.0f/*, Space.Self*/);

                    }
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

        RaycastHit hit;

        // Check for raycast collisions
        //if (Physics.Raycast (check_ray.origin, check_ray.direction, Vector3.Distance(check_ray.origin, camera_object.transform.position) + 0.175f)) 
        if (Physics.Raycast(check_ray, out hit, Vector3.Distance(check_ray.origin, camera_object.transform.position) + 0.175f))
        {
           // print(hit.collider.name);

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