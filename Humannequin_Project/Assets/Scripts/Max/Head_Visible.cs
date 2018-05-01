// Head_Visible.CS
// MAX MILLS

// THis is used to determine if the mannequin head object is visible
// The mannequi attached to the head will continuously check this to allow it to move/stalk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Visible : MonoBehaviour {

	bool not_visible;
	public bool head_turn = false;
	public bool use_fps_controller = false;
	public Transform player_position;
	Vector3 target_dir,target_postition;
	public float tilt = 0.3f;
	bool found = false;

	// Use this for initialization
	void Start () {
		if(use_fps_controller)
		{
			player_position = GameObject.Find("FPSController").transform;
		}
		/*else
		{
			player_position = GameObject.Find("[CameraRig]").transform;
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If using camera rig, continuously search for one until found
		if(!use_fps_controller && !found)
		{
			player_position = GameObject.Find("[CameraRig]").transform;
			found = true;
		}


		if(!head_turn)
		{
			// If not rendereed, update visibilty toggle
			if(!this.GetComponent<Renderer> ().isVisible)
			{
				not_visible = true;
			}
			else
			{
				not_visible = false;
			}
		}
		else
		{
			// This is used for seperate mannequin heads to follow the player
			if(!this.GetComponent<Renderer> ().isVisible)
			{
				//this.transform.LookAt(player_position);
				Head_Turn();
			}


		}
	}

	public bool Get_Not_Visible()
	{
		return not_visible;
	}

	// For rotating the mannequins head towards the player
	void Head_Turn()
	{
		// Set target position to player position but using head y position + tilt 
		target_postition = new Vector3 (player_position.position.x, 
			//this.transform.position.y, 
			this.transform.position.y - tilt,
			player_position.position.z);


		// Calculate vector
		target_dir = target_postition - transform.position;

		// Calculate difference in head rotation angle
		float difference = Vector3.Angle (target_dir, transform.parent.forward);

		// Look at target if head position is less than 90 degrees
		if (difference < 270.0f && difference < 90.0f)
		{			
			this.transform.LookAt (target_postition);
		}

	}
}
