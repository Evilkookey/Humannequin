// WIN_GAME.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Game : MonoBehaviour 
{	
	GameObject pause_controller;
	public GameObject end_mannequin;

	Animator mannequin_anim;
	public float delay_time; 

	void Start ()
	{
		pause_controller = GameObject.Find("pause_controller");

		mannequin_anim = end_mannequin.GetComponent<Animator> ();
		mannequin_anim.speed = 0;

		delay_time = mannequin_anim.runtimeAnimatorController.animationClips [0].length;
	}

	// Update is called once per frame
	void Update () 
	{
		// Checks if player has won the game
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.WIN) 
		{
			print ("ENDGAME");

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
			mannequin_anim.speed = 1;

			StartCoroutine (Win ());

		}

	}

	IEnumerator Win()
	{

		yield return new WaitForSeconds (delay_time);

		// Change game state to WIN
		Game_State_Controller.Win_Game();

		yield return null;
	}
}