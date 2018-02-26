// SCENE_CONTROLLER.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour 
{
	GameObject hallway_1_lights;
	GameObject menu_door;
	GameObject start_wall;

	void Start()
	{
		// Load next scene in the background of the current scene
		SceneManager.LoadSceneAsync (2, LoadSceneMode.Additive);

		// Unload last scene in background of the current scene
		//SceneManager.UnloadSceneAsync (0);

		// Set the hallway lights of the Final Prototype scene to be off
		hallway_1_lights = GameObject.Find("hallway_1_lights");
		hallway_1_lights.SetActive (false);

		// Find the menu door
		menu_door = GameObject.Find("start_door");

		// Find the start wall
		start_wall = GameObject.Find("start_wall");
		start_wall.SetActive (false);
	}

	public static void Change_Scene(string scene)
	{
		// If door type is play
		if (scene == "play")
		{
			// Next scene loads
			Debug.Log ("Play Game");
		}

		// If door type is end
		if (scene == "end") 
		{
			Debug.Log ("Quit");
			// Exit Game
			Application.Quit();
		}

		if (scene == "restart")
		{
			// Load the Main Menu scene
			SceneManager.LoadScene (1);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "[CameraRig]")
		{
			// Move the player to the next scene
			SceneManager.MoveGameObjectToScene (GameObject.Find ("[CameraRig]").gameObject, SceneManager.GetSceneByBuildIndex (2));
			Debug.Log ("COLLIDE");

			// If the player is not looking at the door
			if (!menu_door.GetComponent<Renderer> ().isVisible) 
			{
				// Unload main menu scene
				SceneManager.UnloadSceneAsync (1);

				// Set the replacement wall active
				start_wall.SetActive (true);
			}

			// Turn hallway lights on
			hallway_1_lights.SetActive (false);

		}

		if(other.gameObject.name == "FPSController")
		{
			// Move the player to the next scene
			SceneManager.MoveGameObjectToScene(GameObject.Find("FPSController").gameObject, SceneManager.GetSceneByBuildIndex(2));
			Debug.Log ("COLLIDE");

			// If the player is not looking at the door
			if (!menu_door.GetComponent<Renderer> ().isVisible) 
			{
				// Unload main menu scene
				SceneManager.UnloadSceneAsync (1);

				// Set the replacement wall active
				start_wall.SetActive (true);
			}

			// Turn hallway lights on
			hallway_1_lights.SetActive (false);
		}
	}

}
