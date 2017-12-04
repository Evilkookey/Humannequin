// PUZZLE_CUBE.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_cube : MonoBehaviour {

	// Changed true when cube has been interacted with
	public bool hit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool Get_Hit()
	{
		//Debug.Log ("GETHIT");

		return hit;
	}
	public void Set_Hit (bool t)
	{
		//Debug.Log ("SETHIT");

		hit = t;
	}
}
