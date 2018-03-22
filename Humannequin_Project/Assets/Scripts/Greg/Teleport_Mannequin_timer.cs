// TELEPORT_MANNEQUIN_TIMER.CS
// GREG BALBIRNIE
// Please note this is a modification of Max Mills' "Mannequin_Teleport" script. most of this code was written by him.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Mannequin_timer : MonoBehaviour {

	public Transform[] enemy_positions; 		// Positions to teleport to
	public GameObject position_parent;			// Empty object that holds all the enemy positions

	public Transform player;	// Player position

	public GameObject CameraRigPlayer, FPSController; // Used to determine if the player is in VR or using the FP controller
						
	int index = 1;								// Index variable for the array, starts at 1 so it ignores the first one
	bool enemy_moved;							// Checks if the mannequin can actually move and 
	Vector3 target_postition;					// Players position

	float timer;								// A timer for the mannequin's movement
	public float move_time = 10.0f;				// The amount of time before the mannequin moves
	Vector3 current_position;					// The position the mannequin should be in 

	public bool is_enabled;							// The mannequin is enabled when it can start moving towards the player
	public Light flickering_light;				// Enemy light that will affect the mannequins position
	public GameObject line_puzzle; 				// Used to check if the player has completed the puzzle

	// Use this for initialization
	void Start () 
	{
		// Initialise variables
		enemy_moved = false;

		// Looks through all the positions in the enemy positions object and adds it to the array
		enemy_positions = position_parent.GetComponentsInChildren<Transform> ();

		current_position = this.transform.position;

		// Mannequin is disabled to start
		is_enabled = false;

		//Find both player objects
		CameraRigPlayer = GameObject.Find("[CameraRig]");
		FPSController = GameObject.Find("FPSController");

		player = CameraRigPlayer.transform;

		/*//Switch who the player is for debugging
		if (FPSController.activeInHierarchy == true || CameraRigPlayer == null) 
		{
			print ("yes");
			player = FPSController.transform;

		} else if (CameraRigPlayer.activeInHierarchy == true || FPSController == null ) 
		{
			print ("yes");
			player = CameraRigPlayer.transform;
		}*/
	}

	// Update is called once per frame
	void Update () 
	{
		if (Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING) 
		{		
			// Check if enabled
			if (is_enabled) {
				// Update mannequin
				Mannequin_Update ();
			}
		}
	}

	// Mannequin update is called once per frame when mannequin can move
	void Mannequin_Update () 
	{

		// Run the timer
		timer += Time.deltaTime;

		//print(timer);

		// Check if the timer has exceeded move time
		if (timer >= move_time)
		{
			// If the enemy has not moved already
			if (!enemy_moved) 
			{
				// If the array is still full
				if(index < enemy_positions.Length) 
				{
					// Move the enemy
					Move_enemy ();
				}
				else
				{
					if(!line_puzzle.GetComponent<Wire_Puzzle_VR>().finished)
					{
						// Enemy is at last position, so it should kill you here 
						Game_State_Controller.Lose_Game();

                        print("MannequinKills");
					}


				}
			}
			// Reset timer
			timer = 0.0f;
		}
		else
		{
			// Reset bools if light comes back on
			enemy_moved = false;
		}

		// If mannequin is unseen
		if (!this.GetComponent<Renderer> ().isVisible)
		{

			// Set mannequin position
			this.transform.position = current_position;

		}
	}

	//Used to move the enemy
	void Move_enemy()
	{
		// Enemy has now been moved (or is about to be)
		enemy_moved = true;

		// Move the enemy to the next set location
		current_position = enemy_positions[index].position;

		//Get the look position of the player
		target_postition = new Vector3 (player.position.x, 
			gameObject.transform.position.y, 
			player.position.z);

		// Look at the player position
		transform.LookAt (target_postition);

		// Go to the next position in the array
		index++;

		//play mannequin move sound
		gameObject.GetComponent<AudioSource>().Play();

	}

	// Start the enemy moving at all
	public void Enable_enemy()
	{
		print("Get enabled");
		is_enabled = true;
	}
}
