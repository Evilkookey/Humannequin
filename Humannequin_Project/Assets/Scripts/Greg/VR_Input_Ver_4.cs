﻿//VR_INPUT2_WITH_JOINT.CS
//GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class VR_Input_Ver_4 : MonoBehaviour 
{
	// Define the buttons
	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	public Valve.VR.EVRButtonId pause_button = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;	// NAT

	// Define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	// Define hand models
	public GameObject hand_regular;
	public GameObject hand_point;
	public GameObject hand_wrench;
	public GameObject hand_screwdriver;
	public GameObject hand_pliers;
	public GameObject hand_torch;

	// Object to save what object is held, collided with or used
	public GameObject held_object;

	// The objects the player is colliding with and will interact with
	public List<GameObject> collide_objects;
	public GameObject interact_object;

	// The toolbelt game object
	public Toolbelt toolbelt;

    // If the player is touching the temp slot on the belt
    public bool is_in_temp = false;

	// Using tools
	public enum Tool
	{
		NONE,
		WRENCH,
		SCREWDRIVER,
		PLIERS,
		TORCH
	}
	public Tool active_tool;

	// Pause menu controller
	GameObject pause_menu_controller;	// NAT
	// How long the pause button is held
	public float pause_timer;

	// The other hand
	GameObject other_hand;

	// Animator for the hand and other variables for hand animations
	public Animator hand_animator;
	float trigger_axis;
	Vector3 default_hand_size, default_hand_center;
	BoxCollider hand_box_collider, point_hand_box_collider, pliers_box_collider;
	public bool do_not_anim = false;
	public bool is_grabbing = false;
	public bool pointing = false;

	// Use this for initialization
	void Start () 
	{
		// Get the tracked object
		tracked_object = GetComponent<SteamVR_TrackedObject>();

		// Get the pause menu controller
		pause_menu_controller = GameObject.Find("pause_controller");

		// If this is the left hand
		if (gameObject.name == "Controller (left)")
		{
			// Set the other hand to the right hand
			other_hand = GameObject.Find("Controller (right)");
		}
		// If this is the right hand
		else if (gameObject.name == "Controller (right)")
		{
			// Set the other hand to the left hand
			other_hand = GameObject.Find("Controller (left)");
		}

		// Find the toolbelt
		toolbelt = GameObject.Find("Toolbelt").GetComponent<Toolbelt>();

		// Initialise the pause timer
		pause_timer = 0.0f;

		// Initialise the animator and other variables
		hand_animator = hand_regular.GetComponent<Animator>();
		hand_box_collider = hand_regular.GetComponent<BoxCollider>();
		point_hand_box_collider = hand_point.GetComponent<BoxCollider>();
		pliers_box_collider = hand_pliers.GetComponent<BoxCollider>();

		default_hand_center = hand_box_collider.center;
		default_hand_size = hand_box_collider.size;


	}

	// Update is called once per frame
	void Update () 
	{
        // Only call if state is playing
        if (Game_State_Controller.current_state == Game_State_Controller.Game_States.PLAYING)
        {
            // Take input ID from controller
            device = SteamVR_Controller.Input((int)tracked_object.index);

            // Take trigger axis value for test
            //print (device.GetAxis (trigger_button).x);

            // Sets the grab value in the hand animator 
            trigger_axis = device.GetAxis(trigger_button).x;

			if (trigger_axis <= 0.3f && active_tool == Tool.NONE)
            {
                hand_animator.SetBool("Is_grabbing", false);
            }

            if (active_tool == Tool.NONE && !do_not_anim)
            {
                //hand_animator.SetFloat("Grab", trigger_axis);
            }

			if (active_tool == Tool.PLIERS)
			{
				hand_box_collider.center = pliers_box_collider.center;
				hand_box_collider.size = pliers_box_collider.size;

				// Plays the pliers animation when trigger is pressed a small amount
				if (trigger_axis > 0.2f)
				{
					hand_pliers.GetComponent<Animator>().SetBool("Holding_Pliers", true);
				}
				else if (trigger_axis < 0.2f)
				{
					hand_pliers.GetComponent<Animator>().SetBool("Holding_Pliers", false);
				}
			}
			else if(!pointing)
			{				
				hand_box_collider.center = default_hand_center;
				hand_box_collider.size = default_hand_size;
			}

            // TODO
            // You gotta reset this value when you change back to the regular hand model
            /*if(trigger_axis >= 0.5f)
            {
                hand_box_collider.center = new Vector3(default_hand_center.x,default_hand_center.y,0.01045922f);
                hand_box_collider.size = new Vector3(default_hand_size.x,default_hand_size.y,0.2508576f);
            }
            else
            {
                hand_box_collider.center = default_hand_center;
                hand_box_collider.size = default_hand_size;
            }*/
               

            // Only point if there is no object being held
            if (!held_object)
            {
                // Check grip for pointing
                if (device.GetPressDown(grip_button))
                {
                    // Make sure you arent holding a tool
                    if (active_tool == Tool.NONE)
                    {
                        // Plays the pointing hand animation in the animator
                        hand_animator.SetBool("Pointing", true);

                        hand_box_collider.center = point_hand_box_collider.center;
                        hand_box_collider.size = point_hand_box_collider.size;

						pointing = true;

                        //TODO
                        // We could enable the box collider when animation is over (somehow)

                        // Disable all hands
                        //Disable_Hands();

                        //if(hand_point.GetComponent<Animator>().
                        // Set current hand to point
                        //hand_point.SetActive(true);
                    }

                    // If player presses the grip whilst they are holding the torch
                    if (active_tool == Tool.TORCH)
                    {
                        // Turn off/on the light on the hand torch
                        hand_torch.GetComponentInChildren<Light>().enabled = !hand_torch.GetComponentInChildren<Light>().enabled;

						// Plays torch click sound
						hand_torch.GetComponent<AudioSource>().Play();
                    }

                }

                // Check release of grip
                if (device.GetPressUp(grip_button))
                {
                    // Stops to hand pointing animation;
                    hand_animator.SetBool("Pointing", false);
					pointing = false;

                    //Reset the hand box collider
                    hand_box_collider.center = default_hand_center;
                    hand_box_collider.size = default_hand_size;

                    if (hand_point.activeInHierarchy)
                    {
                        // Disable all hands
                        //Disable_Hands();
                        // Set current hand to point
                        //hand_regular.SetActive(true);
                    }


                }
            }

            // Press trigger
            if (device.GetPressDown(trigger_button))
            {
                hand_animator.SetBool("Is_grabbing", true);

                // Loop through all objects colided with
                foreach (GameObject collide in collide_objects)
                {
                    // Check the identifier
                    switch (collide.tag)
                    {
                        case "Interact":
                            // Set the interact object to the collide object
                            interact_object = collide;

                            // Send activate with held tool
                            interact_object.SendMessage("Activate", active_tool.ToString());
                            break;
                        case "Pick_Up":
                            // Check if the player has a regular hand, not a tool and there is no current held object and player is not pointing
                            if (hand_regular.activeInHierarchy && !held_object && !hand_animator.GetBool("Pointing"))
                            {
                                // Set the held object to the collide object
                                held_object = collide;

                                // Take held object out of other hand
                                other_hand.SendMessage("BreakJoint", held_object);

                                // Check if this object has a joint to any other object
                              /*  if (held_object.GetComponent<FixedJoint>())
                                {
                                    // Get the joint
                                    FixedJoint current_joint = held_object.GetComponent<FixedJoint>();

                                    // Break the joint
                                    Destroy(current_joint);
                                }*/

                                // Check if this object is in the temp slot
                             /*   if (held_object == toolbelt.held_object)
                                {
                                    toolbelt.Take_Out_Temp();
                                }*/

                                // Connect the object with a fixed joint
                                FixedJoint joint = AddFixedJoint(); //FixedJoint
                                joint.connectedBody = held_object.GetComponent<Rigidbody>();
                            }
                            break;
                        case "ToolSlot":
                            // Only change to a tool if holding nothing
                            if (!held_object)
                            {
                                // Set the interact object to the collide object
                                interact_object = collide.transform.GetChild(0).gameObject;

                                // If the player has no tool 
                                if (active_tool == Tool.NONE)
                                {
                                    // Remove tool from belt
                                    if (toolbelt.Take_Tool(interact_object.name))
                                    {
                                        // Check the name of the tool and apply to hand 
                                        switch (interact_object.name.ToLower())
                                        {
                                            case "wrench":
                                                // Set the active tool to WRENCH
                                                active_tool = Tool.WRENCH;
                                                // Disable all hands
                                                Disable_Hands();
                                                // Set correct hands to active
                                                hand_wrench.SetActive(true);
                                                break;
                                            case "screwdriver":
                                                // Set the active tool to SCREWDRIVER
                                                active_tool = Tool.SCREWDRIVER;

                                                hand_animator.SetBool("Holding_Tool", true);
                                                hand_animator.SetFloat("Grab", 1.0f);

                                                // Disable all hands
                                                Disable_Hands();
                                                // Set correct hands to active
                                                hand_screwdriver.SetActive(true);


                                                break;
                                            case "torch":
                                                // Set the active tool to TORCH
                                                active_tool = Tool.TORCH;
                                                // Disable all hands
                                                Disable_Hands();
                                                // Set correct hands to active
                                                hand_torch.SetActive(true);
                                                break;
                                            case "pliers":
                                                // Set the active tool to PLIERS
                                                active_tool = Tool.PLIERS;
                                                // Disable all hands
                                                Disable_Hands();
                                                // Set correct hands to active
                                                hand_pliers.SetActive(true);
                                                break;
                                        }
                                    }



                                }
                                // If the player has a tool and the tool is not in the slot
                                else if (active_tool != Tool.NONE)
                                {
                                    //TODO try romoving this and maybe add it to toolbelt
                                    // Check if the tool in the slot is the active tool
                                    if (interact_object.name.ToLower() == active_tool.ToString().ToLower())
                                    {
                                        // Put the tool back in the belt
                                        if (toolbelt.Return_Tool(interact_object.name))
                                        {
                                            // Set active tool to NONE
                                            active_tool = Tool.NONE;
                                            // Disable all hands
                                            Disable_Hands();
                                            // Set hand back to standard
                                            hand_regular.SetActive(true);
                                            hand_regular.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

                                            //do_not_anim = true;
                                            //hand_animator.SetFloat("Grab", 1.0f);
                                            hand_animator.SetBool("Holding_Tool", false);



                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                // TODO
                // This might need changed
                /*
                if(active_tool == Tool.PLIERS)
                {
                    hand_pliers.GetComponent<Animator>().SetBool("Holding_Pliers",true);
                }*/
            }

            // Release trigger
            if (device.GetPressUp(trigger_button))
            {
                // If there is an interact object
                if (interact_object)
                {
                    // Send a deactivate call the the interact object
                    interact_object.SendMessage("Deactivate");
                    // Set interact object to null
                    interact_object = null;
                }
                // If the player is holding an object drop it
                if (held_object)
                {
                    // Remove the fixed joint
                    ReleaseObject();

                    //trigger_axis = 1;
                    //hand_animator.SetFloat("Grab", 1);
                }

                if (active_tool == Tool.PLIERS)
                {
                    hand_pliers.GetComponent<Animator>().SetBool("Holding_Pliers", false);
                }
            }
        }

		//If the player holds pause
		if (device.GetPress (pause_button)) 
		{
			// Start the pause timer
			pause_timer += Time.deltaTime;

			// If the button is held for longer than a second
			if (pause_timer >= 1.0f)
			{
				//Find the toolbelt and set it to hand height
				GameObject.Find("Toolbelt").SendMessage("Set_Toolbelt_Height", gameObject.transform.position.y);
			}
		} 

		//If the player releases pause
		if (device.GetPressUp (pause_button)) 
		{
			//Check the pause timer is below 1 sec
			if (pause_timer < 1.0f)
			{
				// Enables the pause menu
				pause_menu_controller.SendMessage ("Activate");
			}

			// Reset pause timer
			pause_timer = 0.0f;
		} 
	}

	void OnTriggerEnter(Collider other)
	{
        // If the object can be used in some way
        if (other.tag =="Interact" || other.tag == "Pick_Up" || other.tag == "ToolSlot")
		{
			
			// Find all renderers on object & children
			Renderer[] all_renderers = other.GetComponentsInChildren<Renderer>();

			// Add the outline to everything with a renderer
			foreach (Renderer renderer in all_renderers) 
			{
                // If it has no outline
                if (!renderer.gameObject.GetComponent<Outline>())
                {
                    renderer.gameObject.AddComponent<Outline>();
                }
			}

			// Set the type of object it is
			if (other.tag == "Interact")
			{
				// Set the object to the one collided with
				collide_objects.Add(other.gameObject);
			}
			if (other.tag == "Pick_Up")
			{
				// Set the object to the one collided with
				collide_objects.Add(other.gameObject);
			}
			if (other.tag == "ToolSlot")
			{
				// Add the slot to the list
				collide_objects.Add(other.gameObject);
			}
            if (other.tag == "Temp_Slot")
            {
                // show the player is touching the temp slot
                is_in_temp = true;
            }
        }
	}

	void OnTriggerExit(Collider other)
	{
		// Find all renderers on object & children
		Renderer[] all_renderers = other.GetComponentsInChildren<Renderer>();

		// Add the outline to everything with a renderer
		foreach (Renderer renderer in all_renderers) 
		{
            if (renderer.gameObject.GetComponent<Outline>() ) 
			{
				// remove the outline
				Component.Destroy(renderer.gameObject.GetComponent<Outline> ());
			}
		}

		// If the object has an outline
		/*if (other.gameObject.GetComponent<Outline>()) 
		{
			// remove the outline
			Component.Destroy(other.gameObject.GetComponent<Outline> ());
		}*/

		// Check if there is an object collided with
		if (collide_objects.Contains(other.gameObject))
		{
			// Remove object
			collide_objects.Remove(other.gameObject);
		}

        if (other.tag == "Temp_Slot")
        {
            // show the player is not touching the temp slot
            is_in_temp = false;
        }
    }

	// Creates joint
	FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();

        fx.breakForce = Mathf.Infinity;
		fx.breakTorque = Mathf.Infinity;

		return fx;
	}

	// Break the joint
	void BreakJoint(GameObject new_held)
	{
		// Check if this hand is holding the object the other hand wants to pick up
		if (held_object == new_held)
		{
			// Find the joint
			FixedJoint fx = gameObject.GetComponent<FixedJoint>();

			// Destroy the joint
			Destroy(fx);
		}
	}

	// For dropping the object
	void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{

			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

            held_object.GetComponent<Rigidbody>().velocity = (device.velocity.x * gameObject.transform.parent.right + device.velocity.y * gameObject.transform.parent.up + device.velocity.z * gameObject.transform.parent.forward);
            held_object.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
        }
		held_object = null;
	}

	// Set hands to inactive
	void Disable_Hands()
	{
		//TODO
		// SO setting this inactive messes up the blend tree
		// BUT not rendering it fucks up collisions again
		// TODO
		hand_regular.SetActive(false);
		//hand_regular.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		//hand_point.SetActive(false);
		hand_wrench.SetActive(false);
		hand_screwdriver.SetActive(false);
		hand_pliers.SetActive(false);
		hand_torch.SetActive(false);

		// Remove the outlines
		foreach (GameObject obj in collide_objects)
		{
			// If the object has an outline
			if (obj.gameObject.GetComponent<Outline>()) 
			{
				// remove the outline
				Component.Destroy(obj.gameObject.GetComponent<Outline> ());
			}
		}

		// Clear the collide list
		collide_objects.Clear();
	}
}