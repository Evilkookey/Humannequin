// GAME_STATE_CONTROLLER.CS
// NATALIE BAKER_HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_State_Controller : MonoBehaviour 
{
	// Variables
	// An enum for the different game states
	public enum Game_States
	{
		PLAYING,
		PAUSED,
		WIN,
		LOSE
	}

	// A variable to store the current game state
	public static Game_States current_state;

	// Use this for initialization
	void Start () 
	{
		// Initialise the current state to be PLAYING
		current_state = Game_States.PLAYING;
	}
	void Update()
	{
		//Debug.Log(current_state);
	}
	// Function called to set the game state to PAUSED
	public static void Pause_Game ()
	{
		current_state = Game_States.PAUSED;
	}	

	// Function called to set the game state to PLAYING
	public static void Play_Game ()
	{
		current_state = Game_States.PLAYING;
	}

	// Function called to set the game state to LOSE
	public static void Lose_Game ()
	{
		current_state = Game_States.LOSE;
	}

	// Function called to set the game state to WIN
	public static void Win_Game ()
	{
		current_state = Game_States.WIN;
	}
}
