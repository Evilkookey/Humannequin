using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stayaway_Zone : MonoBehaviour {

	public bool in_zone = false; 
	bool boards_broken = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.name == "[CameraRig]" || col.name == "FPSController") 
		{

			//if(!boards_broken)
			//{
				in_zone = true;
			//}
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.name == "[CameraRig]" || col.name == "FPSController") 
		{

			in_zone = false;
		}

	}
}
