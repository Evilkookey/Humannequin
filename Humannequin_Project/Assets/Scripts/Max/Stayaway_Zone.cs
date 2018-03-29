using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stayaway_Zone : MonoBehaviour {

	public bool in_zone = true; 
	bool boards_broken;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{

			//if(!boards_broken)
			//{
				in_zone = true;
			//}
			print("Collided");
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{

			in_zone = false;
		}

	}
}
