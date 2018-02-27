// LOSE_GAME.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose_Game : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		// If the current game state is "LOSE" then the player has died
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.LOSE) 
		{
			// TODO
			// Die
			print("die");
			// Fade to black
			SteamVR_Fade.Start(Color.black, 5.0f, false);

			print("lose");
			// Display lose screen
			Pause_Menu_Controller.Lose_Game();

			print("restart");
			// Reload Main_Menu
			//Move_Wall_Trigger.Restart();
		}
	}
}