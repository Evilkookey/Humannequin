// SCREWDRIVER_INTERACTION.CS
// NATALIE BAKER-HALL AND MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver_Interaction : MonoBehaviour 
{
	// Variables
	// An int to hold the number of screws that have been activated
	int screw_counter;

	// An int to hold the number of screws to be unscrewed before the cover can be interacted with
	public int screws;

	// This hold the activated object's animator
	Animator object_animator;

	// Cover object
	public GameObject cover;

	//public List<GameObject> screws_list = new List<GameObject>();
	public List<HingeJoint> HingeJoints = new List<HingeJoint>();

	// The mannequin's script
	public Teleport_Mannequin_timer mannequin;

	// The line puzzle
	public GameObject line_puzzle;

	public bool Enable_mannequin = false;
	public bool Enable_line_puzzle = false;


	void Start()
	{
		// Initialises screw counter to 0
		screw_counter = 0;

		HingeJoints.AddRange(cover.GetComponents<HingeJoint> ());

		if(Enable_mannequin)
		{
			mannequin = GameObject.Find("mannequin_teleport_new").GetComponentInChildren<Teleport_Mannequin_timer>();
		}
		else
		{
			mannequin = null;
		}

		if (!Enable_line_puzzle)
		{
			line_puzzle = null;
		}
		else
		{
			if (line_puzzle)
			{
				line_puzzle.SetActive(false);
			}
			else
			{
				print("You need to pass in a line puzzle to activate it");
			}
		}
	}

	public void Interact(GameObject activated_object, Activate_Screwable_Object.Object_Type object_type)
	{
		// Finds all the required components of the activated object
		object_animator = activated_object.GetComponent<Animator>();

		// If the object is not a cover, it must be a screw
		if (object_type != Activate_Screwable_Object.Object_Type.COVER) 
		{
			print("1");

			// Play animation
			object_animator.SetBool ("play", true);

			// Add one to the screw counter
			screw_counter = screw_counter + 1;

			print("2");

			// Check screw count
			if (screw_counter >= screws)
			{
				// Set the tag of the cover
				cover.tag = "Interact";
			}

			print("3");

			foreach (HingeJoint hj in HingeJoints) 
			{
				print("occurence");
				if (activated_object.name == hj.connectedBody.gameObject.name) 
				{
					HingeJoints.Remove(hj);
					Destroy (hj);
					break;
				}
			}

			print("4");

			if (screw_counter == screws - 1) 
			{				
				print ("SWING");
				// TODO Activate puzzle here	
				cover.GetComponent<Rigidbody>().isKinematic = false;


				if (Enable_line_puzzle)
				{
					// Activate the line puzzle
					line_puzzle.SetActive(true);
				}
				print("send enable");

				if(Enable_mannequin)
				{
					// Enable the mannequin
					mannequin.Enable_enemy();
				}
			}


			Debug.Log("Counter" + screw_counter);

		}
		// If the object is a cover 
		if (object_type == Activate_Screwable_Object.Object_Type.COVER) 
		{
			// If screw counter is 4 then all screws have been interacted with
			if (cover.tag == "Interact") 
			{
                // Play animation
                print("interact with cover now pls");
                //object_animator.SetBool ("play", true);
                cover.GetComponent<Rigidbody>().isKinematic = false;

			}
		}
	}
}