// PAUSE_MENU_CONTROLLER.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Menu_Controller : MonoBehaviour 
{
	// VARIABLES // 
	// The canvas objects
	public GameObject pause_menu_object;
	public GameObject win_lose_object;

	// The images
	GameObject win_image;
	GameObject lose_image;

	// Use this for initialization
	void Start () 
	{
		// INITIALISATION //
		// Initialise the canvases
		pause_menu_object = GameObject.Find("pause_screen");
		win_lose_object = GameObject.Find ("win/lose");

		// Initialise the images
		win_image = GameObject.Find ("win_image");
		lose_image = GameObject.Find ("lose_image");

		// Set the images to be inactive
		win_image.SetActive(false);
		lose_image.SetActive(false);

		// Set the canvases to be inactive
		pause_menu_object.SetActive(false);
		win_lose_object.SetActive(false);
	}

	// Update is called once per frame
	public void Activate () 
	{
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING) 
		{
			// Set the current state to PAUSED
			Game_State_Controller.Pause_Game ();

			Debug.Log("pause game");

			// Set the pause menu object to active
			pause_menu_object.SetActive(true);
		}
		else if (Game_State_Controller.current_state == Game_State_Controller.Game_States.PAUSED)
		{
			// Set the current state to PLAY
			Game_State_Controller.Play_Game ();

			Debug.Log("play game");

			// Set all the canvases to inactive
			pause_menu_object.SetActive(false);
		}
	}

	// Lose Game
	public void Lose_Game ()
	{
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.LOSE)
		{
			win_lose_object.SetActive (true);
			lose_image.SetActive (true);
		}
	}

	// Win Game
	public void Win_Game ()
	{
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.WIN)
		{
			win_lose_object.SetActive (true);
			win_image.SetActive (true);
		}
	}
}