using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour {

	public enum game_state	//the state the game is in
	{
		INACTIVE,
		GENERATE,
		INPUT,
		WIN,
		FAIL,
		COMPLETE
	}


	public const int sequence_length = 6;					//the length of the sequence 
	public int[] sequence;				//the sequence to be replicated
	public int number_pointer;					//the number that currently needs to be input
	public int player_input;			//the colour the player input
	public int[] player_sequence;		// The sequence the player has input
	public float timer;							//a timer

	public game_state current_state;			//the state the game is in
	public bool is_on;							//if the light is on

	public Light flickering_light;				//the light that turns off and moves the mannequin 
	public float light_timer;					//a timer for the light

	public GameObject entrance_door;

	public Text keypad_screen;			// The screen on the keypad

	// Use this for initialization
	void Start ()
    {
		//initialise variables
		sequence = new int[sequence_length];
		number_pointer = 0;
		player_input = 0;
		player_sequence = new int[sequence_length];
		timer = 0.0f;

		current_state = game_state.INACTIVE;
		is_on = false;

		flickering_light = GameObject.Find("Enemy_Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//switch for game states
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
		//loop through sequence length
		for (int i = 0; i < sequence_length; i++)
		{
			//set this member of sequence to random colour
			int rando = Random.Range(0, 10);
			sequence[i] = rando;
		}
		//change to display state;
		current_state = game_state.INPUT;
	}
		
	void Get_Player_Input(int input_number)
	{
		//add to player sequence
		player_sequence[number_pointer] = player_input;
		//TODO
		keypad_screen.text.Insert(4, player_input.ToString());
		// Move pointer
		number_pointer++;

		//compare the input with the part of sequence
		if (number_pointer > sequence_length)
		{
			//check sequences
			if (player_sequence == sequence)
			{
				//player wins
				Debug.Log("correct");
				current_state = game_state.WIN;
			}
			else
			{
				Debug.Log("incorrect");
				//reset pointer before allowing another try
				number_pointer = 0;

				//turn light off and on again
				flickering_light.enabled = false;
				InvokeRepeating("Turn_On_Light", 1.0f, Time.deltaTime);

				//MAKE MANNEQUIN ANGRY HERE//
			}	
		}
	}
		
	void Victory()
	{
		//open door here
		entrance_door.SendMessage("Activate");

		//set state to complete
		current_state = game_state.COMPLETE;
	}
}