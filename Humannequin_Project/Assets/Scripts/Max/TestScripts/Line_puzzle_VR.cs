using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_puzzle_VR : MonoBehaviour {


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

	public int MAX_LENGTH = 10;
	public float move_distance = 0.12f;

	//To hold all the empty cubes in the puzzle
	public GameObject[] cubes;

	//Custom colours to pass in
	public Material yellow_colour, magenta_colour, green_colour;

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

	}


	// Update is called once per frame
	void Update () 
	{
		if(red_line.line_complete && blue_line.line_complete && green_line.line_complete && magenta_line.line_complete && yellow_line.line_complete) // add new colour here
		{
			Debug.Log("WIN");
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


	public void Get_start_input(Color color, bool hit, GameObject collider)
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
				SetStart(collider,ref red_line);
			}
			else if(color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the blue one
				current_line = blue_line;
				SetStart(collider,ref blue_line);
			}
			else if(color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = green_line;
				SetStart(collider,ref green_line);
			}
			else if(color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = magenta_line;
				SetStart(collider,ref magenta_line);
			}
			else if(color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = yellow_line;
				SetStart(collider,ref yellow_line);
			}
		}

	}

	public void Get_empty_input(bool hit, GameObject empty_cube)
	{
		Debug.Log("Collided with empty");
		//If there is a current line set
		if(current_line.line_renderer != null)
		{
			//If cube is not already hit AND if the distance between last position and new position is less than the set move distance
			if(hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
				empty_cube.transform.position) < move_distance)
			{
				Debug.Log("hit empty");
				//Set the cube hit variable to true
				empty_cube.GetComponent<VR_puzzle_cube_test>().SetHit(true);

				//Add the hit cube to the array of boxes
				current_line.boxes[current_line.line_renderer.positionCount/* -1*/] = empty_cube;

				//Make a new position for the line renderer and set it to the hix box position
				current_line.line_renderer.positionCount++;
				current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, empty_cube.transform.position);
			}
		}
	}

	public void Check_line(Color color, bool hit, GameObject cube)
	{
		//Debug.Log(current_line.boxes[current_line.line_renderer.positionCount - 1].GetComponent<Renderer>().material.name);

		if(current_line.boxes[0].GetComponent<Renderer>().material.name == current_line.boxes[current_line.line_renderer.positionCount - 1].GetComponent<Renderer>().material.name && 
			current_line.line_renderer.positionCount > 2)
		{
			//If the distance between last position and new position is less than the set move distance
			if(hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1), cube.transform.position) < move_distance)
			{
				//If cube is red
				if(color == Color.red && current_line.line_renderer == red_line.line_renderer)
				{
					SetFinish(cube, ref red_line);
				}
				else if(color == Color.blue && current_line.line_renderer == blue_line.line_renderer)
				{
					SetFinish(cube, ref blue_line);
				}
				else if(color == green_colour.color && current_line.line_renderer == green_line.line_renderer)
				{
					SetFinish(cube, ref green_line);
				}	
				else if(color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer)
				{
					SetFinish(cube, ref magenta_line);
				}	
				else if(color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer)
				{
					SetFinish(cube, ref yellow_line);
				}	
			}
		}
		else
		{	
			Debug.Log("This should reset");
			Reset();
		}

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
						SetFinish(collider.gameObject,ref red_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && current_line.line_renderer == blue_line.line_renderer)
					{
						SetFinish(collider.gameObject,ref blue_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color && current_line.line_renderer == green_line.line_renderer)
					{
						SetFinish(collider.gameObject, ref green_line);
					}	
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer)
					{
						SetFinish(collider.gameObject, ref magenta_line);
					}	
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer)
					{
						SetFinish(collider.gameObject, ref yellow_line);
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
						SetStart(collider.gameObject,ref red_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the blue one
						current_line = blue_line;
						SetStart(collider.gameObject,ref blue_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = green_line;
						SetStart(collider.gameObject,ref green_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = magenta_line;
						SetStart(collider.gameObject,ref magenta_line);
					}
					else if(collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the green one
						current_line = yellow_line;
						SetStart(collider.gameObject,ref yellow_line);
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
				cubes[i].gameObject.GetComponent<Puzzle_cube>().SetHit(false);
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
						collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);

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
	void Reset()
	{
		//Not using line anymore
		using_line = false;

		current_line.line_complete = false;

		//Reset hit variable for all boxes that were hit
		for(int i = 0;i < current_line.line_renderer.positionCount/*-1*/; i++)
		{
			current_line.boxes[i].SendMessage("SetHit", false);

		}

		//Remove current positions for the line
		current_line.line_renderer.positionCount = 1;
	}

	//This will set the first cube in the line and reset line if already completed line
	void SetStart(GameObject hit, ref lines_struct line)
	{
		//If the line is not completed already
		if(!line.line_complete && line.line_renderer.positionCount < 2)
		{
			//Currently using the line
			using_line = true;

			//Set the cube hit variable to true
			hit.GetComponent<VR_puzzle_cube_test>().SetHit(true);

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
			Reset();

			line.line_complete = false;
			//hit.GetComponent<Puzzle_cube>().SetHit(true);

		}
	}
	//This will set the last cube in the line
	void SetFinish(GameObject hit,ref lines_struct line)
	{
		//Finished using the line
		using_line = false;

		//Set the cube hit variable to true
		hit.GetComponent<VR_puzzle_cube_test>().SetHit(true);

		//Add the hit cube to the array of boxes
		line.boxes[line.line_renderer.positionCount -1] = hit;

		//Make a new position for the line renderer and set it to the hix box position
		line.line_renderer.positionCount++;
		line.line_renderer.SetPosition(line.line_renderer.positionCount -1, hit.transform.position);

		//Debug.Log(line.line_renderer.positionCount);

		//Set line complete to true
		line.line_complete = true;
	}
}
