// LINE_PUZZLE_RAY.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_Puzzle_Ray : MonoBehaviour 
{
	// 'Line' referes to a wire 
	// 'Cube' refers to a node to connect the wire to

	// Distance for the ray 
	float raycast_distance = 3.0f;

	// If a line renderer is being used
	public bool using_line;

	// Struct to hold variable for for every line
	[System.Serializable]
	public struct lines_struct
	{
		public LineRenderer line_renderer;
		public bool line_complete;
		public GameObject[] boxes;

	};

	// Line which gets set when you press the start cube
	public lines_struct current_line;

	// This could be an array
	public lines_struct red_line, blue_line, green_line, magenta_line, yellow_line;

	public int MAX_LENGTH = 10;
	public float move_distance = 0.12f;

	// To hold all the empty cubes in the puzzle
	public GameObject[] cubes;

	// Custom colours to pass in
	public Material yellow_colour, magenta_colour, green_colour;

	// Use this for initialization
	void Start () 
	{
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
		// If all lines are completed, puzzle is complete
		if(red_line.line_complete && blue_line.line_complete && green_line.line_complete && magenta_line.line_complete && yellow_line.line_complete) // add new colour here
		{
			Debug.Log("WIN");
		}
		
		// Cast ray from centre of the camera view
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray.origin, ray.direction, out hit, raycast_distance)) 
		{
			// If left mouse button pressed
			if(Input.GetMouseButton(0))
			{
				// If cube is a starting cube
				if (hit.collider.gameObject.tag == "start") 
				{
					// If line is not already being used
					if(!using_line)
					{
						// If cube is red etc
						if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
						{						
							// Set the line renderer to the red one
							current_line = red_line;
							Set_Start(hit.collider.gameObject,ref red_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
						{	
							// Set the line renderer to the blue one
							current_line = blue_line;
							Set_Start(hit.collider.gameObject,ref blue_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
						{	
							// Set the line renderer to the green one
							current_line = green_line;
							Set_Start(hit.collider.gameObject,ref green_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
						{	
							// Set the line renderer to the green one
							current_line = magenta_line;
							Set_Start(hit.collider.gameObject,ref magenta_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
						{	
							// Set the line renderer to the green one
							current_line = yellow_line;
							Set_Start(hit.collider.gameObject,ref yellow_line);
						}
					}


				}

				// If clicked on resetting cube
				if (hit.collider.gameObject.tag == "Respawn") 
				{
					// Removes all positions from line renderers
					red_line.line_renderer.positionCount = 1;							// Add new colour here
					blue_line.line_renderer.positionCount = 1;
					green_line.line_renderer.positionCount = 1;
					magenta_line.line_renderer.positionCount = 1;
					yellow_line.line_renderer.positionCount = 1;

					// Resets variables
					using_line = false;
					current_line.line_complete = false;

					// Reset every cube hit bool
					for(int i =0;i<cubes.Length;i++)
					{
						cubes[i].gameObject.GetComponent<Puzzle_Cube>().Set_Hit(false);
					}
				}

				// If cube is an empty cube
				if (hit.collider.gameObject.tag == "empty") 
				{
					// If there is a current line set
					if(current_line.line_renderer != null)
					{
						// If cube is not already hit AND if the distance between last position and new position is less than the set move distance
						if(hit.collider.gameObject.GetComponent<Puzzle_Cube>().hit == false && 
							Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
								hit.collider.gameObject.transform.position) < move_distance)
						{
							// Set the cube hit variable to true
							hit.collider.gameObject.GetComponent<Puzzle_Cube>().Set_Hit(true);

							// Add the hit cube to the array of boxes
							current_line.boxes[current_line.line_renderer.positionCount -1] = hit.collider.gameObject;

							// Make a new position for the line renderer and set it to the hix box position
							current_line.line_renderer.positionCount++;
							current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
						}
					}
				}

			}
			// If left mouse button is released
			if(Input.GetMouseButtonUp(0))
			{
				// If cube is a finish cube
				if (hit.collider.gameObject.tag == "start") 
				{
					// If the distance between last position and new position is less than the set move distance
					if(hit.collider.gameObject.GetComponent<Puzzle_Cube>().hit == false && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
						hit.collider.gameObject.transform.position) < move_distance)
					{
						// If cube is red etc
						if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && current_line.line_renderer == red_line.line_renderer)
						{
							Set_Finish(hit.collider.gameObject,ref red_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && current_line.line_renderer == blue_line.line_renderer)
						{
							Set_Finish(hit.collider.gameObject,ref blue_line);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == green_colour.color && current_line.line_renderer == green_line.line_renderer)
						{
							Set_Finish(hit.collider.gameObject, ref green_line);
						}	
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer)
						{
							Set_Finish(hit.collider.gameObject, ref magenta_line);
						}	
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer)
						{
							Set_Finish(hit.collider.gameObject, ref yellow_line);
						}	
					}

				}
				// If there is a current set line 
				if(current_line.line_renderer != null)
				{
					// If the line is not complete  
					if (current_line.line_renderer == red_line.line_renderer && !red_line.line_complete) 
					{						
						// Reset all line positions
						Reset();
					}
					if (current_line.line_renderer == blue_line.line_renderer && !blue_line.line_complete) 
					{				
						// Reset all line positions
						Reset();
					}
					if (current_line.line_renderer == green_line.line_renderer && !green_line.line_complete ) 
					{
						// Reset all line positions
						Reset();
					}
					if (current_line.line_renderer == magenta_line.line_renderer && !magenta_line.line_complete ) 
					{
						// Reset all line positions
						Reset();
					}
					if (current_line.line_renderer == yellow_line.line_renderer && !yellow_line.line_complete ) 
					{
						// Reset all line positions
						Reset();
					}

				}
				//Unassign current line
				//current_line.line_renderer = null;

			}
		}
	}

	void Reset()
	{
		//Not using line anymore
		using_line = false;

		current_line.line_complete = false;

		//Reset hit variable for all boxes that were hit
		for(int i = 0;i<current_line.line_renderer.positionCount - 1 ;i++)
		{
			current_line.boxes[i].SendMessage("Set_Hit", false);

		}

		//Remove current positions for the line
		current_line.line_renderer.positionCount = 1;
	}

	// This will set the first cube in the line and reset line if already completed line
	void Set_Start(GameObject hit, ref lines_struct line)
	{
		// If the line is not completed already
		if(!line.line_complete && line.line_renderer.positionCount < 2)
		{
			// Currently using the line
			using_line = true;

			// Set the cube hit variable to true
			hit.GetComponent<Puzzle_Cube>().Set_Hit(true);

			// Set the line renderer to the red one
			current_line = line;

			// Add the hit cube to the array of boxes
			current_line.boxes[current_line.line_renderer.positionCount -1] = hit;

			// Set a new position for the line renderer to the hix box position
			current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.transform.position);
		}
		// This will reset the line if clicked on the start cube again when the line is completed
		else if(current_line.line_complete)
		{
			Reset();

			line.line_complete = false;
			//hit.GetComponent<Puzzle_cube>().Set_Hit(true);

		}
	}

	// This will set the last cube in the line
	void Set_Finish(GameObject hit,ref lines_struct line)
	{
		// Finished using the line
		using_line = false;

		// Set the cube hit variable to true
		hit.GetComponent<Puzzle_Cube>().Set_Hit(true);

		// Add the hit cube to the array of boxes
		line.boxes[line.line_renderer.positionCount -1] = hit;

		// Make a new position for the line renderer and set it to the hix box position
		line.line_renderer.positionCount++;
		line.line_renderer.SetPosition(line.line_renderer.positionCount -1, hit.transform.position);

		//Debug.Log(line.line_renderer.positionCount);

		// Set line complete to true
		line.line_complete = true;
	}
}
