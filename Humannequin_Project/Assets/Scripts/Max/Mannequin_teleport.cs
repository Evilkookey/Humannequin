// MANNEQUIN_TELEPORT.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_teleport : MonoBehaviour {

	public Transform[] enemy_positions; 		// Positions to teleport to
	public GameObject position_parent;			// Empty object that holds all the enemy positions

	public Light flickering_light;				// Enemy light that will affect the mannequins position
	public Transform player;					// Player position
	public GameObject mannequin;				// Actual mannequin object to move

	int index = 1;								// Index variable for the array, starts at 1 so it ignores the first one
	bool can_move, enemy_moved;					// Checks if the mannequin can actually move and 
	Vector3 target_postition;					// Players position

	// Use this for initialization
	void Start () 
	{
		// Initialise variables
		can_move = false;
		enemy_moved = false;

		// Looks through all the positions in the enemy positions object and adds it to the array
		enemy_positions = position_parent.GetComponentsInChildren<Transform> ();

	}
	// Update is called once per frame
	void Update () 
	{
		// If the light is not on
		if (!flickering_light.enabled) 
		{
			// Enemy can now move
			can_move = true;

			// If the enemy has not moved already
			if (!enemy_moved) 
			{
				// If the array is still full
				if(index < enemy_positions.Length) 
				{
					//Debug.Log (index);

					// Move the enemy
					Move_enemy ();
				}
				else
				{
					// Enemy is at last position, so it should kill you here //////////////
					//Debug.Log("KILLL");
				}
			}
		}
		else
		{
			//reset bools if light comes back on
			can_move = false;
			enemy_moved = false;
		}

		// Light is changed by the simon puzzle

//		// Debug code for turning on and off the light
//		if(Input.GetKeyDown(KeyCode.L))
//		{
//			flickering_light.enabled = !flickering_light.enabled;
//		}

	}

	//Used to move the enemy
	void Move_enemy()
	{
		// Enemy has now been moved (or is about to be)
		enemy_moved = true;

		// Move the enemy to the next set location
		mannequin.transform.position = enemy_positions[index].position;

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
