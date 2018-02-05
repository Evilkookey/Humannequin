// PAUSE_MENU_CONTROLLER
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Menu_Controller : MonoBehaviour 
{
	// Variables
	// Get the canvases
	GameObject pause_menu_object;
	GameObject notes_menu_object;
	GameObject note_screen_object;

	// Get the button objects
	GameObject notes_menu_button;
	GameObject note_1_button;
	GameObject note_2_button;
	GameObject note_3_button;
	GameObject note_4_button;

	// Bool to determine whether or not other Update() scripts will run
	public bool is_paused;

	// Use this for initialization
	void Start () 
	{
		// Initialise the is_paused variable
		is_paused = false;

		// Initialise the canvases
		pause_menu_object = GameObject.Find("pause_screen");
		notes_menu_object = GameObject.Find("notes_menu");
		note_screen_object = GameObject.Find("note_screen");

		// Initialise the buttons
		notes_menu_button.GetComponent<Button> ().interactable = false;
		note_1_button.GetComponent<Button> ().interactable = false;
		note_2_button.GetComponent<Button> ().interactable = false;
		note_3_button.GetComponent<Button> ().interactable = false;
		note_4_button.GetComponent<Button> ().interactable = false;

		// Set the canvases to be inactive
		pause_menu_object.SetActive(false);
		notes_menu_object.SetActive(false);
		note_screen_object.SetActive(false);

		//Time.timeScale = 1;
	}


	// Update is called once per frame
	public void Activate () 
	{
		if (pause_menu_object.activeInHierarchy == false) 
		{
			is_paused = true;
			Debug.Log("pause game");
			pause_menu_object.SetActive(true);
			//Time.timeScale = 0;
		}
		else if (pause_menu_object.activeInHierarchy == true 
				|| notes_menu_object.activeInHierarchy == true 
				|| note_screen_object.activeInHierarchy == true)
		{
			is_paused = false;
			Debug.Log("play game");
			pause_menu_object.SetActive(false);
			//Time.timeScale = 1;
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
	}

	// Function called when the back button is pressed on the note screen
	public void Note_Screen_Deactivate ()
	{
		// Activate and Deactivate the appropriate canvases
		notes_menu_object.SetActive (true);
		note_screen_object.SetActive (false);

		// Deactivate all notes
	}

	// Function called when the quit button is pressed
	public void Quit_Game()
	{
		Application.Quit();
	}
}