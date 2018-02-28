// WIN_GAME.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Game : MonoBehaviour 
{	
	GameObject pause_controller;

	void Start ()
	{
		pause_controller = GameObject.Find("pause_controller");
	}

	// Update is called once per frame
	void Update () 
	{
		// Checks if player has won the game
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.WIN) 
		{
			// Fade to black
			SteamVR_Fade.Start(Color.black, 5.0f, false);

			// Display win screen
			pause_controller.SendMessage ("Win_Game");

			// Reload Main_Menu
			Scene_Controller.Change_Scene("restart");
		}
	}

	// Detect a collision from the player
	void OnTriggerEnter (Collider other)
	{
		// Checks if colliding with player, if so then move player object to next scene
		if(other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController")
		{
			// Change game state to WIN
			Game_State_Controller.Win_Game();
		}
	}
}