// LOSE_GAME.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_Game : MonoBehaviour 
{
	// Variables
	GameObject pause_controller;
    public float blackout_time = 1.5f;

    void Start ()
	{
		// Get the pause controller object
		pause_controller = GameObject.Find("pause_controller");
	}

	// Update is called once per frame
	void Update () 
	{
		// If the current game state is "LOSE" then the player has died
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.LOSE) 
		{
			// Fade to black
			SteamVR_Fade.Start(Color.black, blackout_time, false);

			// Display lose screen
			pause_controller.SendMessage ("Lose_Game");

			// Reload Main_Menu
			StartCoroutine (Lose());
		}
	}

	IEnumerator Lose ()
	{
		yield return new WaitForSeconds (blackout_time + 4.5f);

		// Return to main menu
		Move_Wall_Trigger.Restart();

		yield return null;
	}
}