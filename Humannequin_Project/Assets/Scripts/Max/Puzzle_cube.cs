using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_cube : MonoBehaviour {


	public bool hit = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
