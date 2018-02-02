﻿// WIRE_PUZZLE_VR.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Puzzle_VR : MonoBehaviour 
{
	// 'Line' referes to a wire 
	// 'Cube' refers to a node to connect the wire to


	//public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	//public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	//
	//define the controller object
	//public SteamVR_TrackedObject tracked_object;
	////public SteamVR_Controller.Device device;

	//Distance for the ray 
	float raycast_distance = 3.0f;

	//If a line renderer is being used
	public bool using_line;

	[System.Serializable]
	public struct lines_struct
	{
		public LineRenderer line_renderer;
		public bool line_complete;
		public GameObject[] boxes;

	};


	//Line which gets set when you press the start cube
	public lines_struct current_line;

	//This could be an array
	public lines_struct red_line, blue_line, green_line, magenta_line, yellow_line;

	public int MAX_LENGTH = 100;
	public float move_distance = 0.12f;

	//To hold all the empty cubes in the puzzle
	public GameObject[] cubes;

	//Custom colours to pass in
	public Material yellow_colour, magenta_colour, green_colour;

	public GameObject[] end_cubes;
	public bool finished;
	bool played_sound;
	public GameObject entrance_door;

	int num; // To add up the number of connected cubes


	public Light light;
	public float heightScale, xScale,cap;

	// Use this for initialization
	void Start () 
	{
		//tracked_object = GetComponent<SteamVR_TrackedObject>();

		//Find line renderers
		red_line.line_renderer = GameObject.Find("LineRendererRed").GetComponent<LineRenderer>();
		blue_line.line_renderer = GameObject.Find("LineRendererBlue").GetComponent<LineRenderer>();
		green_line.line_renderer = GameObject.Find("LineRendererGreen").GetComponent<LineRenderer>();
		magenta_line.line_renderer = GameObject.Find("LineRendererMagenta").GetComponent<LineRenderer>();
		yellow_line.line_renderer = GameObject.Find("LineRendererYellow").GetComponent<LineRenderer>();


		//Set up arrays
		current_line.boxes = new GameObject[MAX_LENGTH];
		red_line.boxes = new GameObject[MAX_LENGTH];
		blue_line.boxes = new GameObject[MAX_LENGTH];
		green_line.boxes = new GameObject[MAX_LENGTH];
		magenta_line.boxes = new GameObject[MAX_LENGTH];
		yellow_line.boxes = new GameObject[MAX_LENGTH];

		//current_line = red_line;
		using_line = false;
		finished = false;
		played_sound = false;

		num = 0;

	}


	// Update is called once per frame
	void Update () 
	{
		float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0F);
		if (height < cap) {
			light.intensity = height;
		} else {
			light.intensity = 0.0f;
		}
		Debug.Log(height);

		if(red_line.line_complete && blue_line.line_complete && green_line.line_complete && magenta_line.line_complete && yellow_line.line_complete) // add new colour here
		{
			Debug.Log("WIN");
		}

		// Scans through the list of endcubes and increases a counter if they are all hit (completed)
		for(int i = 0; i < end_cubes.Length; i++)
		{
			if(end_cubes[i].GetComponent<VR_Puzzle_Cube>().hit == true)
			{
				num++;
			}
		}

		//Debug.Log (num);

		// Puzzle is completed if you complete all the lines
		if (num == end_cubes.Length) 
		{
			finished = true;
		} else 
		{
			// Reset num 
			num = 0;
		}

		if(finished)
		{
			//Debug.Log("ITS DONE");
			// Open door here
			if(!played_sound)
			{
				gameObject.GetComponent<AudioSource>().Play();
				played_sound = true;
			}
			// Activates the door opening function on the door
			entrance_door.SendMessage("Activate");
		}

		/*if (device.GetPressUp(trigger_button))
		{
		
			//If there is a current set line 
			if(current_line.line_renderer != null)
			{
				//If the line is not complete  
				if (current_line.line_renderer == red_line.line_renderer && !red_line.line_complete) 
				{						
					//Reset all line positions
					Reset();
				}
				if (current_line.line_renderer == blue_line.line_renderer && !blue_line.line_complete) 
				{				
					//Reset all line positions
					Reset();
				}
				if (current_line.line_renderer == green_line.line_renderer && !green_line.line_complete ) 
				{
					//Reset all line positions
					Reset();
				}
				if (current_line.line_renderer == magenta_line.line_renderer && !magenta_line.line_complete ) 
				{
					//Reset all line positions
					Reset();
				}
				if (current_line.line_renderer == yellow_line.line_renderer && !yellow_line.line_complete ) 
				{
					//Reset all line positions
					Reset();
				}

			}
			//Unassign current line
			//current_line.line_renderer = null;

		}*/
		//}
	}


	public void Get_Start_Input(Color color, bool hit, GameObject collider)
	{
		Debug.Log("Collided with start");

		//If line is not already being used
		if(!using_line)
		{
			//If cube is red
			if(color == Color.red) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{						
				//Set the line renderer to the red one
				current_line = red_line;
				Set_Start(collider,ref red_line);
			}
			else if(color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the blue one
				current_line = blue_line;
				Set_Start(collider,ref blue_line);
			}
			else if(color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = green_line;
				Set_Start(collider,ref green_line);
			}
			else if(color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = magenta_line;
				Set_Start(collider,ref magenta_line);
			}
			else if(color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = yellow_line;
				Set_Start(collider,ref yellow_line);
			}
		}

	}

	public void Get_Empty_Input(Color color, bool hit, GameObject empty_cube)
	{
		//Debug.Log("Collided with empty");
		//If there is a current line set
		if(current_line.line_renderer != null)
		{
			//If cube is not already hit AND if the distance between last position and new position is less than the set move distance
			if(hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
				empty_cube.transform.position) < move_distance)
			{

				if (empty_cube.GetComponent<VR_Puzzle_Cube> ().type == VR_Puzzle_Cube.cube_type.EMPTY && !current_line.line_complete)
				{
					Debug.Log ("hit empty");
					//Set the cube hit variable to true
					empty_cube.GetComponent<VR_Puzzle_Cube> ().Set_Hit (true);

					//Add the hit cube to the array of boxes
					current_line.boxes [current_line.line_renderer.positionCount/* -1*/] = empty_cube;

					//Make a new position for the line renderer and set it to the hix box position
					current_line.line_renderer.positionCount++;
					current_line.line_renderer.SetPosition (current_line.line_renderer.positionCount - 1, empty_cube.transform.position);
				} else 
				{
					// If you collide with a start cube(to end the line)
					Debug.Log("FINISH");

					//If cube is red
					if (color == Color.red && current_line.line_renderer == red_line.line_renderer) {
						Set_Finish (empty_cube, ref red_line);
					} else if (color == Color.blue && current_line.line_renderer == blue_line.line_renderer) {
						Set_Finish (empty_cube, ref blue_line);
					} else if (color == green_colour.color && current_line.line_renderer == green_line.line_renderer) {
						Set_Finish (empty_cube, ref green_line);
					} else if (color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer) {
						Set_Finish (empty_cube, ref magenta_line);
					} else if (color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer) {
						Set_Finish (empty_cube, ref yellow_line);
					}	

				}

			}
		}
	}

	public void Check_Line(Color color, bool hit, GameObject cube)
	{
		//Debug.Log(current_line.boxes[current_line.line_renderer.positionCount - 1].GetComponent<Renderer>().material.name);

		//Add the hit cube to the array of boxes
		//current_line.boxes[current_line.line_renderer.positionCount -1] = cube;


		if(current_line.boxes[0].GetComponent<Renderer>().material.name == current_line.boxes[current_line.line_renderer.positionCount - 1].GetComponent<Renderer>().material.name && 
			current_line.line_renderer.positionCount > 2)

		{
			//If the distance between last position and new position is less than the set move distance
			if(hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1), cube.transform.position) < move_distance)
			{
				//If cube is red
				if(color == Color.red && current_line.line_renderer == red_line.line_renderer)
				{
					Set_Finish(cube, ref red_line);
				}
				else if(color == Color.blue && current_line.line_renderer == blue_line.line_renderer)
				{
					Set_Finish(cube, ref blue_line);
				}
				else if(color == green_colour.color && current_line.line_renderer == green_line.line_renderer)
				{
					Set_Finish(cube, ref green_line);
				}	
				else if(color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer)
				{
					Set_Finish(cube, ref magenta_line);
				}	
				else if(color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer)
				{
					Set_Finish(cube, ref yellow_line);
				}	
			}
		}
		else
		{	
			Debug.Log("This should reset");
			Reset();
		}

		using_line = false;

	}

	public void Reset_All()
	{
		Debug.Log("RESET ALL");

		//Removes all positions from line renderers
		red_line.line_renderer.positionCount = 1;							// Add new colour here
		blue_line.line_renderer.positionCount = 1;
		green_line.line_renderer.positionCount = 1;
		magenta_line.line_renderer.positionCount = 1;
		yellow_line.line_renderer.positionCount = 1;

		//Resets variables
		using_line = false;
		current_line.line_complete = false;

		//Reset every cube hit bool
		for(int i =0;i<cubes.Length;i++)
		{
			cubes[i].gameObject.GetComponent<VR_Puzzle_Cube>().Set_Hit(false);
		}
		for(int i =0;i<end_cubes.Length;i++)
		{
			end_cubes[i].gameObject.GetComponent<VR_Puzzle_Cube>().Set_Hit(false);
		}
	}

	void Reset()
	{
		//Not using line anymore
		using_line = false;

		current_line.line_complete = false;

		//Reset hit variable for all boxes that were hit
		for(int i = 0;i < current_line.line_renderer.positionCount/*-1*/; i++)
		{
			current_line.boxes[i].SendMessage("Set_Hit", false);

		}

		//Remove current positions for the line
		current_line.line_renderer.positionCount = 1;
	}

	//This will set the first cube in the line and reset line if already completed line
	void Set_Start(GameObject hit, ref lines_struct line)
	{
		//If the line is not completed already
		if(!line.line_complete && line.line_renderer.positionCount < 2)
		{
			//Currently using the line
			using_line = true;

			//Set the cube hit variable to true
			hit.GetComponent<VR_Puzzle_Cube>().Set_Hit(true);

			//Set the line renderer to the red one
			current_line = line;

			Debug.Log("set START");

			//Add the hit cube to the array of boxes
			current_line.boxes[current_line.line_renderer.positionCount -1] = hit;

			//Set a new position for the line renderer to the hix box position
			current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.transform.position);
		}
		//This will reset the line if clicked on the start cube again when the line is completed
		else if(current_line.line_complete)
		{
			//Reset();

			//line.line_complete = false;
			//hit.GetComponent<Puzzle_cube>().Set_Hit(true);

		}
	}
	//This will set the last cube in the line
	void Set_Finish(GameObject hit,ref lines_struct line)
	{
		//Finished using the line
		using_line = false;

		//Set the cube hit variable to true
		hit.GetComponent<VR_Puzzle_Cube>().Set_Hit(true);

		//Add the hit cube to the array of boxes
		line.boxes[line.line_renderer.positionCount -1] = hit;

		//Make a new position for the line renderer and set it to the hix box position
		line.line_renderer.positionCount++;
		line.line_renderer.SetPosition(line.line_renderer.positionCount -1, hit.transform.position);

		//Debug.Log(line.line_renderer.positionCount);

		//Set line complete to true
		line.line_complete = true;
		current_line.line_complete = true;
	}

	/*
	void OnTriggerExit(Collider collider)
	{
		device = SteamVR_Controller.Input((int)tracked_object.index);
		if (device.GetPressUp(trigger_button))
		{
			//If cube is a finish cube
			if (collider.gameObject.tag == "start") 
			{
				//If the distance between last position and new position is less than the set move distance
				if(collider.gameObject.GetComponent<Puzzle_cube>().hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
					collider.gameObject.transform.position) < move_distance)
				{
					//If cube is red
					if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && current_line.line_renderer == red_line.line_renderer)
					{
						Set_Finish(collider.gameObject,ref red_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && current_line.line_renderer == blue_line.line_renderer)
					{
						Set_Finish(collider.gameObject,ref blue_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color && current_line.line_renderer == green_line.line_renderer)
					{
						Set_Finish(collider.gameObject, ref green_line);
					}	
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer)
					{
						Set_Finish(collider.gameObject, ref magenta_line);
					}	
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer)
					{
						Set_Finish(collider.gameObject, ref yellow_line);
					}	
				}

			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		
		//If cube is a starting cube
		if (collider.gameObject.tag == "start") 
		{
			device = SteamVR_Controller.Input((int)tracked_object.index);
			if (device.GetPressDown(trigger_button))
			{

				Debug.Log("Collided with start");

				//If line is not already being used
				if(!using_line)
				{
					//If cube is red
					if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.red) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{						
						//Set the line renderer to the red one
						current_line = red_line;
						Set_Start(collider.gameObject,ref red_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the blue one
						current_line = blue_line;
						Set_Start(collider.gameObject,ref blue_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = green_line;
						Set_Start(collider.gameObject,ref green_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = magenta_line;
						Set_Start(collider.gameObject,ref magenta_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = yellow_line;
						Set_Start(collider.gameObject,ref yellow_line);
					}
				}


			}
		}

		//If clicked on resetting cube
		if (collider.gameObject.tag == "Respawn") 
		{
			//Removes all positions from line renderers
			red_line.line_renderer.positionCount = 1;							// Add new colour here
			blue_line.line_renderer.positionCount = 1;
			green_line.line_renderer.positionCount = 1;
			magenta_line.line_renderer.positionCount = 1;
			yellow_line.line_renderer.positionCount = 1;

			//Resets variables
			using_line = false;
			current_line.line_complete = false;

			//Reset every cube hit bool
			for(int i =0;i<cubes.Length;i++)
			{
				cubes[i].gameObject.GetComponent<Puzzle_cube>().Set_Hit(false);
			}
		}

		//If cube is an empty cube
		if (collider.gameObject.tag == "empty") 
		{
			device = SteamVR_Controller.Input((int)tracked_object.index);
			if (device.GetPressDown(trigger_button))
			{
				Debug.Log("Collided with empty");
				//If there is a current line set
				if(current_line.line_renderer != null)
				{
					//If cube is not already hit AND if the distance between last position and new position is less than the set move distance
					if(collider.gameObject.GetComponent<Puzzle_cube>().hit == false && 
						Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
							collider.gameObject.transform.position) < move_distance)
					{
						Debug.Log("hit empty");
						//Set the cube hit variable to true
						collider.gameObject.GetComponent<Puzzle_cube>().Set_Hit(true);

						//Add the hit cube to the array of boxes
						current_line.boxes[current_line.line_renderer.positionCount -1] = collider.gameObject;

						//Make a new position for the line renderer and set it to the hix box position
						current_line.line_renderer.positionCount++;
						current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, collider.gameObject.transform.position);
					}
				}
			}

		}
	}
*/
}
