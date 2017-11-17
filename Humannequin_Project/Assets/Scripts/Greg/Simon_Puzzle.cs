using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simon_Puzzle : MonoBehaviour {

	public enum colour_name						//the different colours that could be shown
	{
		NULL,
		RED,
		GREEN,
		BLUE,
		YELLOW,
		PURPLE,
		WHITE
	}

	public enum game_state	//the state the game is in
	{
		INACTIVE,
		GENERATE,
		DISPLAY,
		INPUT,
		WIN,
		FAIL,
		COMPLETE
	}

	public int round_number;					//the round the player is on
	public int final_round;						//the last round before victory
	public int sequence_length;					//the length of the sequence this round 
	public colour_name[] sequence;				//the sequence to be replicated
	public int colour_pointer;					//the colour that currently needs to be input
	public colour_name player_input;			//the colour the player input
	public float timer;							//a timer

	public game_state current_state;			//the state the game is in
	public bool is_on;							//if the light is on

	public int fail_count;						//the amount of times the player has failed
	public int lose_value;						//the amount of fails that loses the game

	public Light flickering_light;				//the light that turns off and moves the mannequin 
	public float light_timer;					//a timer for the light

	public Light[] coloured_lights;				//the coloured lights that indicate the sequence

	public GameObject entrance_door;

	// Use this for initialization
	void Start ()
    {
		//initialise variables
		round_number = 0;
		sequence = new colour_name[10];
		colour_pointer = 0;
		player_input = colour_name.NULL;
		timer = 0.0f;

		current_state = game_state.INACTIVE;
		is_on = false;

		flickering_light = GameObject.Find("Enemy_Light").GetComponent<Light>();

		coloured_lights = new Light[6];
		coloured_lights = gameObject.GetComponentsInChildren<Light>();

		//turn off all lights
		Turn_Lights_Off();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//switch for game states
		switch (current_state)
		{
		case game_state.GENERATE:
			Generate_Random_Colours();
			break;
		case game_state.DISPLAY:
			Display_sequence();
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

//	void Turn_On_Machine()
//	{
//		//generate the sequence;
//		current_state = game_state.GENERATE;
//	}

	void Generate_Random_Colours()
	{
		//choose sequence length based on round number
		switch (round_number)
		{
		case 0:
			sequence_length = 3;
			break;
		case 1:
			sequence_length = 5;
			break;
		case 2:
			sequence_length = 7;
			break;
		}
		//loop through sequence length
		for (int i = 0; i < sequence_length; i++)
		{
			//set this member of sequence to random colour
			int rando = Random.Range(0, 6);
			switch (rando)
			{
			case 0:
				sequence[i] = colour_name.RED;
				break;
			case 1:
				sequence[i] = colour_name.GREEN;
				break;
			case 2:
				sequence[i] = colour_name.BLUE;
				break;
			case 3:
				sequence[i] = colour_name.YELLOW;
				break;
			case 4:
				sequence[i] = colour_name.PURPLE;
				break;
			case 5:
				sequence[i] = colour_name.WHITE;
				break;
			}
		}
		//change to display state;
		current_state = game_state.DISPLAY;
	}

	void Display_sequence()
	{
		//update timer
		timer += Time.deltaTime;
		//if half a second has passed 
		if (timer > 0.5f)
		{
			//reset timer
			timer = 0.0f;
			//change true/false
			is_on = !is_on;
			//if it went off

			//if there is more in sequence
			if (colour_pointer < sequence_length)
			{
				if (!is_on)
				{
					//iterate through to next in sequence
					Lights_Change(sequence[colour_pointer], is_on);
					colour_pointer++;
				}
				//if it went on
				else if (is_on)
				{
					//show colour;
					//Debug.Log(sequence[colour_pointer]);
					Lights_Change(sequence[colour_pointer], is_on);
				}
			}
			else if (colour_pointer >= sequence_length)
			{
				//reset colour pointer and is_on and change state
				colour_pointer = 0;
				is_on = false;
				current_state = game_state.INPUT;
			}
		}
	}

	void Get_Player_Input(string input_text)
	{
		//if the game is in the input stage
		if (current_state == game_state.INPUT) 
		{
			//take player input
			switch (input_text) 
			{
			case "red":
				player_input = colour_name.RED;
				break;
			case "green":
				player_input = colour_name.GREEN;
				break;
			case "blue":
				player_input = colour_name.BLUE;
				break;
			case "yellow":
				player_input = colour_name.YELLOW;
				break;
			case "purple":
				player_input = colour_name.PURPLE;
				break;
			case "white":
				player_input = colour_name.WHITE;
				break;
			}

			//light up the input
			Turn_Lights_Off();
			Lights_Change(player_input, true);

			//compare the input with the part of sequence
			if (player_input == sequence[colour_pointer])
			{
				//move to next member
				colour_pointer++;
				Debug.Log("correct");
				//if it is complete
				if (colour_pointer >= sequence_length)
				{
					//move to next round
					Turn_Lights_Off();
					Debug.Log("You win round " + round_number);
					round_number++;
					//if last round is done, win game
					if (round_number >= final_round)
					{
						current_state = game_state.WIN;
					}
					else
					{
						//reset pointer 
						colour_pointer = 0;
						current_state = game_state.GENERATE;
					}
				}
			}
			else
			{
				//turn lights off
				Turn_Lights_Off();
				//display again and add to fail
				Debug.Log("incorrect");
				//reset pointer before reshowing
				colour_pointer = 0;
				fail_count++;

				//turn light off and on again
				flickering_light.enabled = false;
				InvokeRepeating("Turn_On_Light", 1.0f, Time.deltaTime);
				Debug.Log(1/Time.deltaTime);

				//if failed enough to lose
				if (fail_count >= lose_value)
				{
					current_state = game_state.FAIL;
				}
				else
				{
					current_state = game_state.DISPLAY;
				}
			}
		}
		else if (current_state == game_state.INACTIVE)
		{
			if (input_text == "ON")
			{
				current_state = game_state.GENERATE;
			}
		}
	}

	void Turn_On_Light()
	{
		//time the light
		light_timer += Time.deltaTime;
		//after one second
		if (light_timer >= 1.0f)
		{
			Debug.Log("NOPE");
			//turn light back on, rest timer and stop repeating
			flickering_light.enabled = true;
			light_timer = 0.0f;
			CancelInvoke();
		}
	}

	void Lights_Change(colour_name light_colour, bool colour_on)
	{
		int light_num = 0;

		//pick light
		switch (light_colour)
		{
		case colour_name.RED :
			light_num = 0;
			break;
		case colour_name.GREEN :
			light_num = 1;
			break;
		case colour_name.BLUE :
			light_num = 2;
			break;
		case colour_name.YELLOW :
			light_num = 3;
			break;
		case colour_name.PURPLE :
			light_num = 4;
			break;
		case colour_name.WHITE :
			light_num = 5;
			break;
		}

		//turn on or off
		if (colour_on) 
		{
			coloured_lights[light_num].enabled = true;
		}
		else
		{
			coloured_lights[light_num].enabled = false;
		}
	}

	void Turn_Lights_Off()
	{
		//turn off all lights
		for (int i = 0; i < coloured_lights.Length; i++)
		{
			coloured_lights[i].enabled = false;
		}
	}
	void Victory()
	{
		//Debug.Log ("hello");
		//open door here//////////////////////////////
		entrance_door.SendMessage("Activate");

		//set state to complete
		current_state = game_state.COMPLETE;
	}
}

//put input stuff in one function. 
//You no longer need newInput as the function is only called when player inputs.