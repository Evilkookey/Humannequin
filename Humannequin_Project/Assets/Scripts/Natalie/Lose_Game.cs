// LOSE_GAME.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_Game : MonoBehaviour 
{
	GameObject pause_controller;
    public float blackout_time = 1.5f;


    void Start ()
	{
		pause_controller = GameObject.Find("pause_controller");
	}

	// Update is called once per frame
	void Update () 
	{
        //print("trying");

		// If the current game state is "LOSE" then the player has died
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.LOSE) 
		{
			// TODO
			// Die
			//print("die");
			// Fade to black
			SteamVR_Fade.Start(Color.black, blackout_time, false);

			//print("lose");
			// Display lose screen
			pause_controller.SendMessage ("Lose_Game");

			//print("restart");
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