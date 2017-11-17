// SCENE_CONTROLLER.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour 
{
	// Variables
	public int scene_to_load;

	void Start()
	{
		// Load next scene in the background of the current scene
		SceneManager.LoadSceneAsync (scene_to_load,LoadSceneMode.Additive);
		// Unload last scene in background of the current scene
		SceneManager.UnloadSceneAsync (0);
	}

	public void Change_Scene(string scene)
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
	}

	void OnTriggerEnter(Collider other)
	{
		// Checks if colliding with player, if so then move player object to next scene
		if(other.gameObject.name == "[CameraRig] (1)")
		{
			SceneManager.MoveGameObjectToScene(GameObject.Find("[CameraRig] (1)").gameObject,SceneManager.GetSceneByBuildIndex(scene_to_load));
			Debug.Log ("COLLIDE");
			// Unload main menu scene
			SceneManager.UnloadSceneAsync (1);
		}
	}

}
