// MOVE_WALL_TRIGGER.CS
// MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Wall_Trigger : MonoBehaviour 
{
	// Objects to move 
	GameObject start_wall;
	GameObject light_;

	// Use this for initialization
	void Start () 
	{
		// Find the light at the start
		light_ = GameObject.Find("lightbulb_at_start");

		// Find the start wall
		start_wall = GameObject.Find("start_wall");
		start_wall.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public static void Restart ()
	{
		// Load the Main Menu scene
		SceneManager.LoadScene (1);
	}

	void OnTriggerEnter(Collider other)
	{
		// If player cameraRig or FPScontroller interact
		if (other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController") 
		{
			// If the player is not looking at the door
			if (!light_.GetComponentInChildren<Renderer> ().isVisible) 
			{
				// Unload main menu scene
				SceneManager.UnloadSceneAsync (1);

				// Set the replacement wall active
				start_wall.SetActive (true);
			}
		}
	}
}
