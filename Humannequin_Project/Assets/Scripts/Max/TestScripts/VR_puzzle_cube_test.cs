using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_puzzle_cube_test : MonoBehaviour {

	public bool hit;
	public GameObject puzzle_board;

	public enum cube_type
	{
		EMPTY,
		START,
		RESET
	};
	public cube_type type;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//called when cube is interacted with
	void Activate(string tool_type)
	{	
		//Debug.Log(tool_type);

	
		if(tool_type == "PLIERS")
		{
			if(type == cube_type.START)
			{
				puzzle_board.GetComponent<Line_puzzle_VR>().Get_start_input(gameObject.GetComponent<Renderer>().material.color,hit,gameObject);
			}
			if(type == cube_type.RESET)
			{
				puzzle_board.GetComponent<Line_puzzle_VR>().Reset_all();
			
			}
//			if(type == cube_type.EMPTY)
//			{
//				puzzle_board.GetComponent<Line_puzzle_VR>().Get_empty_input(hit, gameObject);
//			}
		}

	}
	//Called when cube is not interacted with
	void Deactivate()
	{
		Debug.Log(gameObject.name);
		if(type == cube_type.START)
		{
			puzzle_board.GetComponent<Line_puzzle_VR>().Check_line(gameObject.GetComponent<Renderer>().material.color, hit, gameObject);

		}
		else if(type == cube_type.EMPTY)
		{
			//puzzle_board.GetComponent<Line_puzzle_VR>().Get_reset_input(); 					//this wont work
		}
	}

	bool GetHit()
	{
		Debug.Log ("GETHIT");
		return hit;
	}
	public void SetHit (bool t)
	{
		//Debug.Log ("SETHIT");
		hit = t;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Hand_Call>().is_pliers)
		{			
			if(type == cube_type.EMPTY)
			{
				puzzle_board.GetComponent<Line_puzzle_VR>().Get_empty_input(hit, gameObject);
			}
			else if(type == cube_type.START)
			{
				Debug.Log("SET FINISH");
				//puzzle_board.GetComponent<Line_puzzle_VR>().Check_line(gameObject.GetComponent<Renderer>().material.color, hit, gameObject);
				puzzle_board.GetComponent<Line_puzzle_VR>().Get_empty_input(hit, gameObject);
			}
		}
	}
}
