// PAUSE_MENU_CONTROLLER.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls all canvasses including the win/lose canvas //

public class Pause_Menu_Controller : MonoBehaviour 
{
	// VARIABLES // 
	// The canvas objects
	static GameObject pause_menu_object;
	static GameObject notes_menu_object;
	static GameObject note_screen_object;
	static GameObject win_lose_object;

	// The button objects
	static GameObject notes_menu_button;
	static GameObject note_1_button;
	static GameObject note_2_button;
	static GameObject note_3_button;
	static GameObject note_4_button;

	// The image objects
	static GameObject note_1_image;
	static GameObject note_2_image;
	static GameObject note_3_image;
	static GameObject note_4_image;
	static GameObject win_image;
	static GameObject lose_image;

	bool notes_button_active;

	// Use this for initialization
	void Start () 
	{
		// INITIALISATION //
		// Initialise the canvases
		pause_menu_object = GameObject.Find("pause_screen");
		notes_menu_object = GameObject.Find("notes_menu");
		note_screen_object = GameObject.Find("note_screen");
		win_lose_object = GameObject.Find ("win/lose");

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
		win_image = GameObject.Find ("win_image");
		lose_image = GameObject.Find ("lose_image");

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
		win_image.SetActive(false);
		lose_image.SetActive(false);

		// Set the canvases to be inactive
		pause_menu_object.SetActive(false);
		notes_menu_object.SetActive(false);
		note_screen_object.SetActive(false);
		win_lose_object.SetActive(false);

		notes_button_active = false;
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
	public static void Notes_Menu_Activate ()
	{
		// Activate and Deactivate the appropriate canvases
		pause_menu_object.SetActive (false);
		notes_menu_object.SetActive (true);
	}

	// Function called when the back button is pressed from the notes menu
	public static void Notes_Menu_Dectivate ()
	{
		// Activate and Deactivate the appropriate canvases
		pause_menu_object.SetActive (true);
		notes_menu_object.SetActive (false);
	}

	// Function called when a note is selected from the notes menu
	public static void Note_Screen_Activate(int note_num)
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
	public static void Note_Screen_Deactivate ()
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

	// Function called when a note is picked up by the player
	public void Note_Collected (int note_num)
	{
		// If notes menu button is inactive then make it active
		if (notes_button_active == false) 
		{
			notes_menu_button.GetComponent<Button> ().interactable = true;
		}

		// Activate the appropriate note
		if (note_num == 1) 
		{
			note_1_button.GetComponent<Button> ().interactable = true;
		} 
		else if (note_num == 2) 
		{
			note_2_button.GetComponent<Button> ().interactable = true;
		}
		else if (note_num == 3) 
		{
			note_3_button.GetComponent<Button> ().interactable = true;		
		}
		else if (note_num == 4) 
		{
			note_4_button.GetComponent<Button> ().interactable = true;		
		}
	}

	// Function called when the quit button is pressed
	public void Quit_Game ()
	{
		Application.Quit();
	}

	// Lose Game
	public static void Lose_Game ()
	{
		win_lose_object.SetActive (true);
		lose_image.SetActive (true);
	}

	// Win Game
	public static void Win_Game ()
	{
		win_lose_object.SetActive (true);
		win_image.SetActive (true);
	}
}