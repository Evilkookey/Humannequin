﻿// SCENE_CONTROLLER.CS
// NATALIE BAKER-HALL && MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour 
{

	public AudioSource radio_audio,menu_audio;
	bool found = false; 

	void Start()
	{
		// Load next scene in the background of the current scene
		//if(!SceneManager.GetSceneAt(2).isLoaded)
		//{
			SceneManager.LoadSceneAsync (2, LoadSceneMode.Additive);
		//}
		// Unload last scene in background of the current scene
		//SceneManager.UnloadSceneAsync (0);


	}

	void Update()
	{
		if(!found) 
		{
			radio_audio = GameObject.Find("radio").GetComponent<AudioSource>();
			found = true;
		}

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
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "[CameraRig]")
		{
			Debug.Log ("COLLIDE");

			// Move the player and the scene manager to the next scene
			SceneManager.MoveGameObjectToScene (GameObject.Find ("[CameraRig]").gameObject, SceneManager.GetSceneByBuildIndex (2));
			SceneManager.MoveGameObjectToScene (this.gameObject, SceneManager.GetSceneByBuildIndex (2));

			StartCoroutine(Audio_Effects.AudioFadeOut(menu_audio,1.5f));
			StartCoroutine(Audio_Effects.AudioFadeIn(radio_audio,1.5f));

		}

		if(other.gameObject.name == "FPSController")
		{
			Debug.Log ("COLLIDE");

			// Move the player and the scene manager to the next scene
			SceneManager.MoveGameObjectToScene(GameObject.Find("FPSController").gameObject, SceneManager.GetSceneByBuildIndex(2));
			SceneManager.MoveGameObjectToScene (this.gameObject, SceneManager.GetSceneByBuildIndex (2));

			StartCoroutine(Audio_Effects.AudioFadeOut(menu_audio,1.5f));	
			StartCoroutine(Audio_Effects.AudioFadeIn(radio_audio,1.5f));

		}
	}


}