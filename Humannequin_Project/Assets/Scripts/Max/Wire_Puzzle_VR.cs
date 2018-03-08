// WIRE_PUZZLE_VR.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Puzzle_VR : MonoBehaviour 
{
	// 'Line' referes to a wire 
	// 'Cube' refers to a node to connect the wire to

	//Distance for the ray 
	//float raycast_distance = 3.0f;


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
	//public Transform[] cubes;
	//public Transform[] end_cubes;
	//public GameObject cubes_parent;
	//public GameObject end_cubes_parent;

	//Custom colours to pass in
	public Material yellow_colour, magenta_colour, green_colour;
	public bool finished;
	bool played_sound;

	//Test variables
	public float heightScale, xScale,cap;

	[Header("Things to be dragged in:")]
	public GameObject entrance_door;
	public Light light_flicker;


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

		yellow_colour = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
		magenta_colour = Resources.Load("Materials/Magenta", typeof(Material)) as Material;
		green_colour = Resources.Load("Materials/Green", typeof(Material)) as Material;

		//current_line = red_line;
		using_line = false;
		finished = false;
		played_sound = false;

	}


	// Update is called once per frame
	void Update () 
	{
		

		if(red_line.line_complete && blue_line.line_complete && green_line.line_complete && /*magenta_line.line_complete &&*/ yellow_line.line_complete) // add new colour here
		{
			//Debug.Log("WIN");
			finished = true;
		}

		// Scans through the list of endcubes and increases a counter if they are all hit (completed)
		/*for(int i = 0; i < end_cubes.Length; i++)
		{
			if(end_cubes[i].gameObject.GetComponent<VR_Puzzle_Cube>().hit == true)
			{
				num++;
			}
		}*/

		//Debug.Log (num);

		// Puzzle is completed if you complete all the lines
		/*if (num == end_cubes.Length) 
		{
			finished = true;
		} else 
		{
			// Reset num 
			num = 0;
		}*/

		if(finished)
		{
			
			// Open door here
			if(!played_sound)
			{
				Debug.Log("ITS DONE");

				gameObject.GetComponent<AudioSource>().Play();
				played_sound = true;

				// Activates the door opening function on the door
				entrance_door.SendMessage("Activate");
			}


		}


	}

	public void Reset_Line(Color color, bool empty)
	{
		//Debug.Log("REset Line");
		if(!empty)
		{
			//If cube is red
			if(color == Color.red) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{				
				red_line.line_renderer.positionCount = 1;				// Add new colour here
				//Set the line renderer to the red one
				current_line = red_line;
				Reset(ref red_line);
			}
			else if(color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				blue_line.line_renderer.positionCount = 1;
				//Set the line renderer to the blue one
				current_line = blue_line;
				Reset(ref blue_line);			
			}
			else if(color == green_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				green_line.line_renderer.positionCount = 1;
				//Set the line renderer to the green one
				current_line = green_line;
				Reset(ref green_line);						
			}/*
			else if(color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				magenta_line.line_renderer.positionCount = 1;
				//Set the line renderer to the green one
				current_line = magenta_line;
				Reset(ref magenta_line);					
			}*/
			else if(color == yellow_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				yellow_line.line_renderer.positionCount = 1;
				//Set the line renderer to the green one
				current_line = yellow_line;
				Reset(ref yellow_line);					
			}
		}
		else
		{
			if(!current_line.line_complete)
			{
				current_line.line_renderer.positionCount = 1;
				Reset(ref current_line);		
			}
		}
	}

	public void Reset(ref lines_struct line)
	{
		//Not using line anymore
		using_line = false;

		line.line_complete = false;
		//current_line.line_complete = false;

		//Reset hit variable for all boxes that were hit
		for(int i = 0;i < line.boxes.Length +1 /*line.line_renderer.positionCount/*-1*/; i++)
		{

			if(line.boxes[i] != null)
			{	
				line.boxes[i].GetComponent<VR_Puzzle_Cube>().Set_Hit(false);
				line.boxes[i] = null;
				//current_line.boxes[i] = null;
				Debug.Log(i);
			}
		}

		//Remove current positions for the line
		line.line_renderer.positionCount = 1;

		current_line.line_renderer.positionCount = 1;


	}

	public void Get_Start_Input(Color color, bool hit, GameObject collider)
	{
		Debug.Log("Collided with start");

		if(current_line.line_renderer != null && Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
			collider.transform.position) < move_distance && current_line.line_renderer.positionCount >1 )
		{
			Debug.Log("Should complete");

			// If you collide with a start cube(to end the line)														//add new colour here
			//If cube is red
			if (color == Color.red && current_line.line_renderer == red_line.line_renderer) 
			{
				Set_Finish (collider, ref red_line);
			} 
			else if (color == Color.blue && current_line.line_renderer == blue_line.line_renderer) 
			{
				Set_Finish (collider, ref blue_line);
			} 
			else if (color == green_colour.color && current_line.line_renderer == green_line.line_renderer) 
			{
				Set_Finish (collider, ref green_line);
			} /*
			else if (color == magenta_colour.color && current_line.line_renderer == magenta_line.line_renderer) 
			{
				Set_Finish (collider, ref magenta_line);
			} */
			else if (color == yellow_colour.color && current_line.line_renderer == yellow_line.line_renderer) 
			{
				Set_Finish (collider, ref yellow_line);
			}	
		}

		//If line is not already being used																		//add new colour here
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
			}/*
			else if(color == magenta_colour.color) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
			{	
				//Set the line renderer to the green one
				current_line = magenta_line;
				Set_Start(collider,ref magenta_line);
			}*/
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
				}
			}
		}
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
		red_line.line_complete = false;										// Add new colour here
		blue_line.line_complete= false;
		green_line.line_complete = false;
		magenta_line.line_complete = false;
		yellow_line.line_complete = false;

		//Reset every cube hit bool
		/*for(int i =0;i<cubes.Length;i++)
		{
			cubes[i].gameObject.GetComponent<VR_Puzzle_Cube>().Set_Hit(false);
		}
		for(int i =0;i<end_cubes.Length;i++)
		{
			end_cubes[i].gameObject.GetComponent<VR_Puzzle_Cube>().Set_Hit(false);
		}*/
	}



	//This will set the first cube in the line and reset line if already completed line
	void Set_Start(GameObject hit, ref lines_struct line)
	{
		//If the line is not completed already
		if(!line.line_complete && line.line_renderer.positionCount < 2)
		{
			//Currently using the line
			using_line = true;
			//What a big gay
			//Set the cube hit variable to true
			hit.GetComponent<VR_Puzzle_Cube>().Set_Hit(true);

			//Set the line renderer to the red one
			current_line = line;

			Debug.Log("set START");

			//Add the hit cube to the array of boxes
			current_line.boxes[0] = hit;

			//Set a new position for the line renderer to the hix box position
			current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.transform.position);
		}
		/*
		//This will reset the line if clicked on the start cube again when the line is completed
		else if(current_line.line_complete)
		{

			//line.line_complete = false;
			
			//Reset();


			//hit.GetComponent<Puzzle_cube>().Set_Hit(true);

		}*/
	}
	//This will set the last cube in the line
	void Set_Finish(GameObject hit,ref lines_struct line)
	{
		//Finished using the line
		using_line = false;

		//Set the cube hit variable to true
		hit.GetComponent<VR_Puzzle_Cube>().Set_Hit(true);

		//Add the hit cube to the array of boxes
		line.boxes[line.line_renderer.positionCount] = hit;

		//Make a new position for the line renderer and set it to the hix box position
		line.line_renderer.positionCount++;
		line.line_renderer.SetPosition(line.line_renderer.positionCount -1, hit.transform.position);

		//Debug.Log(line.line_renderer.positionCount);

		//Set line complete to true
		line.line_complete = true;
		//current_line.line_complete = true;
	}


	public bool GetFinished()
	{
		return finished;
	}
}