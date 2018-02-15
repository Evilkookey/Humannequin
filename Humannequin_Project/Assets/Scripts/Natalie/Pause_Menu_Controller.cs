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
	GameObject pause_menu_object;
	GameObject notes_menu_object;
	GameObject note_screen_object;

	// The button objects
	GameObject notes_menu_button;
	GameObject note_1_button;
	GameObject note_2_button;
	GameObject note_3_button;
	GameObject note_4_button;

	// The image objects
	GameObject note_1_image;
	GameObject note_2_image;
	GameObject note_3_image;
	GameObject note_4_image;

	// Use this for initialization
	void Start () 
	{
		// INITIALISATION //
		// Initialise the canvases
		pause_menu_object = GameObject.Find("pause_screen");
		notes_menu_object = GameObject.Find("notes_menu");
		note_screen_object = GameObject.Find("note_screen");

		// Initialise the buttons
		notes_menu_button = GameObject.Find("notes_button");
		note_1_button = GameObject.Find("note_1_button");
		note_2_button = GameObject.Find("note_2_button");
		note_3_button = GameObject.Find("note_3_button");
		note_4_button = GameObject.Find("note_4_button");

		// Initialise the images
		note_1_image = GameObject.Find("note_1_image");
		note_2_image = GameObject.Find("note_2_image");
		note_3_image = GameObject.Find("note_3_image");
		note_4_image = GameObject.Find("note_4_image");

		// Set the buttons to be non-interactable
		notes_menu_button.GetComponent<Button> ().interactable = false;
		note_1_button.GetComponent<Button> ().interactable = false;
		note_2_button.GetComponent<Button> ().interactable = false;
		note_3_button.GetComponent<Button> ().interactable = false;
		note_4_button.GetComponent<Button> ().interactable = false;

		// Set the buttons to be inactive
		note_1_image.SetActive(false);
		note_2_image.SetActive(false);
		note_3_image.SetActive(false);
		note_4_image.SetActive(false);

		// Set the canvases to be inactive
		pause_menu_object.SetActive(false);
		notes_menu_object.SetActive(false);
		note_screen_object.SetActive(false);
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
			notes_menu_object.SetActive(false);
			note_screen_object.SetActive(false);
		}
	}

	// Function called when the notes_menu button is pressed
	public void Notes_Menu_Activate ()
	{
		// Activate and Deactivate the appropriate canvases
		pause_menu_object.SetActive (false);
		notes_menu_object.SetActive (true);
	}

	// Function called when the back button is pressed from the notes menu
	public void Notes_Menu_Dectivate ()
	{
		// Activate and Deactivate the appropriate canvases
		pause_menu_object.SetActive (true);
		notes_menu_object.SetActive (false);
	}

	// Function called when a note is selected from the notes menu
	public void Note_Screen_Activate(int note_num)
	{
		Debug.Log (note_num);

		// Activate and Deactivate the appropriate canvases
		notes_menu_object.SetActive (false);
		note_screen_object.SetActive (true);

		// Activate the appropriate note
		if (note_num == 1) 
		{
			note_1_image.SetActive(true);
		} 
		else if (note_num == 2) 
		{
			note_2_image.SetActive(true);
		}
		else if (note_num == 3) 
		{
			note_3_image.SetActive(true);
		}
		else if (note_num == 4) 
		{
			note_4_image.SetActive(true);
		}
	}

	// Function called when the back button is pressed on the note screen
	public void Note_Screen_Deactivate ()
	{
		// Activate and Deactivate the appropriate canvases
		notes_menu_object.SetActive (true);
		note_screen_object.SetActive (false);

		// Deactivate all notes
		note_1_image.SetActive(false);
		note_2_image.SetActive(false);
		note_3_image.SetActive(false);
		note_4_image.SetActive(false);
	}

	// Function called when the quit button is pressed
	public void Quit_Game()
	{
		Application.Quit();
	}
}