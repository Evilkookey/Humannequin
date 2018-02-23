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
			// Fade to black
			// Display lose screen
			// Reload Main_Menu
		}
	}
}