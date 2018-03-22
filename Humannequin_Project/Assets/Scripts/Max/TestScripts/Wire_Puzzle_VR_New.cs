using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Puzzle_VR_New : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*
	public void Get_Start_Input(Color color, bool hit, GameObject start_box)
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

			line.line_complete = false;

			Reset();


			//hit.GetComponent<Puzzle_cube>().Set_Hit(true);

		}
	}*/
}
