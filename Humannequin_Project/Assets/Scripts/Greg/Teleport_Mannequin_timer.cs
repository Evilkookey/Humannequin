﻿// TELEPORT_MANNEQUIN_TIMER.CS
// GREG BALBIRNIE
// Please note this is a modification of Max Mills' "Mannequin_Teleport" script. most of this code was written by him.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Mannequin_timer : MonoBehaviour {

	public Transform[] enemy_positions; 		// Positions to teleport to
	public GameObject position_parent;			// Empty object that holds all the enemy positions

	public Light flickering_light;				// Enemy light that will affect the mannequins position
	public Transform player;					// Player position
	public GameObject mannequin;				// Actual mannequin object to move

	int index = 1;								// Index variable for the array, starts at 1 so it ignores the first one
	bool enemy_moved;							// Checks if the mannequin can actually move and 
	Vector3 target_postition;					// Players position

	float timer;								// A timer for the mannequin's movement
	public float move_time = 10.0f;				// The amount of time before the mannequin moves
	Vector3 current_position;					// The position the mannequin should be in 

	// Use this for initialization
	void Start () 
	{
		// Initialise variables
		enemy_moved = false;

		// Looks through all the positions in the enemy positions object and adds it to the array
		enemy_positions = position_parent.GetComponentsInChildren<Transform> ();

		current_position = mannequin.transform.position;

	}

	// Update is called once per frame
	void Update () 
	{
		// Run the timer
		timer += Time.deltaTime;

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
					// Enemy is at last position, so it should kill you here 
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
			mannequin.transform.position = current_position;

			//play mannequin move sound
			gameObject.GetComponent<AudioSource>().Play();
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
			this.transform.position.y, 
			player.position.z);

		// Look at the player position
		transform.LookAt (target_postition);

		// Go to the next position in the array
		index++;

	}
}
