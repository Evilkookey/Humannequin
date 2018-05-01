// VR_PUZZLE_CUBE_TEST.CS
// MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Puzzle_Cube : MonoBehaviour 
{
	public bool hit;					// Set when oject is interacted with
	public GameObject puzzle_board;		// Gameobject that contains the puzzle script

	// Types of cubes
	public enum cube_type
	{
		EMPTY,
		START,
		RESET
	};

	// This cubes type
	public cube_type type;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	// Called when cube is interacted with
	void Activate(string tool_type)
	{	
		// If current tool is the pliers 
		if(tool_type == "PLIERS")
		{
			if (type == cube_type.START && hit) 
			{
				puzzle_board.GetComponent<Wire_Puzzle_VR>().Reset_Line(gameObject.GetComponent<Renderer>().material.color,false);
			}

			if (type == cube_type.EMPTY && hit) 
			{
				Debug.Log("Reset to here");
			}

			// If colliding with start cube, call start function in puzzle script
			if(type == cube_type.START && !hit)
			{
				puzzle_board.GetComponent<Wire_Puzzle_VR>().Get_Start_Input(gameObject.GetComponent<Renderer>().material.color,hit,gameObject);
			}

			// If colliding with reset cube, call reset function in puzzle script
			if(type == cube_type.RESET)
			{
				puzzle_board.GetComponent<Wire_Puzzle_VR>().Reset_All();			
			}
		}
	}

	// Called when cube is not interacted with
	void Deactivate()
	{
		Debug.Log(gameObject.name);

		Debug.Log("deactivate");
		if(type == cube_type.EMPTY)
		{
			puzzle_board.GetComponent<Wire_Puzzle_VR>().Reset_Line(gameObject.GetComponent<Renderer>().material.color,true);
		}
		//This will check if the first interacted cube was a start, and will then check if line was completed or not in the puzzle script
		/*if(type == cube_type.START)
		{
			puzzle_board.GetComponent<Wire_Puzzle_VR>().Check_Line(gameObject.GetComponent<Renderer>().material.color, hit, gameObject);

		}*/
	}

	bool Get_Hit()
	{
		Debug.Log ("GETHIT");
		return hit;
	}

	// Used for setting the hit variable
	public void Set_Hit (bool t)
	{
		//Debug.Log ("SETHIT");
		hit = t;
	}
		
	void OnTriggerEnter(Collider other)
	{
		
		// If object in hand is pliers - this is not needed since only the pliers can interact with the cube
		//if(other.GetComponent<Hand_Call>().is_pliers)
		//{			
			// If you collide with an empty cube, call empty cube function in puzzle script
			if(type == cube_type.EMPTY && puzzle_board.GetComponent<Wire_Puzzle_VR>().using_line)
			{
				puzzle_board.GetComponent<Wire_Puzzle_VR>().Get_Empty_Input(gameObject.GetComponent<Renderer>().material.color, hit, gameObject);
			}

			if(type == cube_type.START && !hit && puzzle_board.GetComponent<Wire_Puzzle_VR>().using_line)
			{
				puzzle_board.GetComponent<Wire_Puzzle_VR>().Get_Start_Input(gameObject.GetComponent<Renderer>().material.color,hit,gameObject);
			}

		//}
	}


}
