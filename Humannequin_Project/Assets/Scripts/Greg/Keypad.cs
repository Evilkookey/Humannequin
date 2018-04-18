// KEYPAD.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

	public enum game_state	// The state the game is in
	{
		INACTIVE,
		GENERATE,
		INPUT,
		WIN,
		FAIL,
		COMPLETE
	}


	public const int sequence_length = 6;					// The length of the sequence 
	public int[] sequence;				// The sequence to be replicated
	public int number_pointer;					// The number that currently needs to be input
	public int[] player_sequence;		// The sequence the player has input
	public float timer;							// A timer

	public game_state current_state;			// The state the game is in
	public bool is_on;							// If the light is on

	public List<GameObject> flickering_light;				// The light that turns off and moves the mannequin 
	public float light_timer;					// A timer for the light

	public GameObject entrance_door;

	public Text keypad_screen;			// The screen on the keypad

	public GameObject keycard;			// The keycard you can find

	// Use this for initialization
	void Start ()
    {
		// Initialise variables
		sequence = new int[sequence_length];
		number_pointer = 0;
		player_sequence = new int[sequence_length];
		timer = 0.0f;

		current_state = game_state.GENERATE;
		is_on = false;

		// Set the keycard
		//keycard = GameObject.Find("keycard");

		//flickering_light = GameObject.Find("Enemy_Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		// Switch for game states
		switch (current_state)
		{
		case game_state.GENERATE:
			Generate_Random_Numbers();
			break;
		case game_state.INPUT:
			//Take_Player_Input();
			break;
		case game_state.WIN:
			Victory();
			break;
		case game_state.FAIL:
			Debug.Log("FAIL");
			break;
		}
	}

	void Generate_Random_Numbers()
	{
		// Loop through sequence length
		for (int i = 0; i < sequence_length; i++)
		{
			// Set this member of sequence to random colour
			int rando = Random.Range(1, 10);
			sequence[i] = rando;
		}

        // Check if this has a card
        if (keycard)
        {
            // Send code to card
            keycard.SendMessage("Set_Code", sequence);
        }

		// Change to display state;
		current_state = game_state.INPUT;
	}
		
	void Get_Player_Input(int input_number)
	{
        // Check if reset has been pressed
        if (input_number == 0)
        {
            // Reset pointer
            number_pointer = 0;

            // Clear the screen
            keypad_screen.text = "";
        }
        else
        {
            // Add to player sequence
            player_sequence[number_pointer] = input_number;

            // Add input to display
            keypad_screen.text = (keypad_screen.text + input_number.ToString());

            // Move pointer
            number_pointer++;

            // Compare the input with the part of sequence
            if (number_pointer >= sequence_length)
            {
                // Check sequences
                if (Check_Input())
                {
                    // Player wins
                    Debug.Log("correct");
                    current_state = game_state.WIN;
                    Victory();
                }
                else
                {
                    Debug.Log("incorrect");
                    // Reset pointer before allowing another try
                    number_pointer = 0;

                    // Clear the screen
                    keypad_screen.text = "";

                    //turn light off and on again
					foreach (GameObject light in flickering_light)
					{
						light.GetComponentInChildren<Light_Controller>().Light_Off();
					}
					InvokeRepeating("Turn_On_Light", 1.0f, Time.deltaTime);

                    // Make mannequin closer to attacking
                }
            }
        }
	}
		
	void Victory()
	{
		// Open door here
		entrance_door.SendMessage("Activate");

		// Set state to complete
		current_state = game_state.COMPLETE;
	}

	void Turn_On_Light()
	{
		// Time the light
		light_timer += Time.deltaTime;
		// After one second
		if (light_timer >= 1.0f)
		{
			// Turn light back on, rest timer and stop repeating
			foreach (GameObject light in flickering_light)
			{
				light.GetComponentInChildren<Light_Controller>().Light_Flicker_On();
			}
			light_timer = 0.0f;
			CancelInvoke();
		}
	}

	// Compare the sequence with the player input
	bool Check_Input()
	{
		// Loop through sequence
		for (int i = 0; i < 6; i++)
		{
			//Check if these parts of the array are different
			if (player_sequence[i] != sequence[i])
			{
				// If they differ return false
				return false;
			}
		}
		// If none were different return true
		return true;
	}
}