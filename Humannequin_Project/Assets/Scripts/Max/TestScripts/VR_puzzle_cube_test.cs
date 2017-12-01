using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_puzzle_cube_test : MonoBehaviour {

	public bool hit;
	public GameObject puzzle_board;

	public enum cube_type
	{
		EMPTY,
		START
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
//			if(type == cube_type.EMPTY)
//			{
//				puzzle_board.GetComponent<Line_puzzle_VR>().Get_empty_input(hit, gameObject);
//			}
		}

	}
	//Called when cube is not interacted with
	void Deactivate()
	{
		if(type == cube_type.START)
		{
			puzzle_board.GetComponent<Line_puzzle_VR>().Get_finish_input(gameObject.GetComponent<Renderer>().material.color, hit, gameObject);
		}
		else if(type == cube_type.EMPTY)
		{
			//puzzle_board.GetComponent<Line_puzzle_VR>().Get_reset_input();
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
		if(type == cube_type.EMPTY)
		{
			puzzle_board.GetComponent<Line_puzzle_VR>().Get_empty_input(hit, gameObject);
		}

	}
}
