using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_Light_Trigger : MonoBehaviour {

	public GameObject light_to_turn_on;
	public bool collided = false;
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
			light_to_turn_on.GetComponentInChildren<Light_Controller>().Light_Flicker_On();
			collided = true;

		}

	}
}
