// LIGHT_CONTROLLER.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Controller : MonoBehaviour 
{
	// Variables
	// Materials for light on and light off
	Material on_material;
	Material off_material;

	// Light on the object
	Light this_light;

	public bool broken;
	bool is_off;

	// Use this for initialization
	void Start () 
	{
		on_material = Resources.Load("Materials/on_light", typeof(Material)) as Material;
		off_material = Resources.Load("Materials/off_light", typeof(Material)) as Material;

		// This gets the light which is a child of the gameObject
		this_light = gameObject.GetComponentInChildren<Light> ();
		is_off = false;

		// If the light is declared as "broken" it will not be able to turn on
		if (broken) 
		{
			is_off = true;
			this_light.enabled = false;
			gameObject.GetComponent<Renderer> ().material = off_material;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// This function turns off the light and changes the material to the off_material
	void Light_Off ()
	{
		if (!is_off) 
		{
			is_off = true;
			this_light.enabled = false;
			gameObject.GetComponent<Renderer> ().material = off_material;
		}
	}

	// This function turns on the light and changes the material to the on_material
	void Light_On ()
	{
		if (!broken && is_off) 
		{
			is_off = false;
			this_light.enabled = true;
			gameObject.GetComponent<Renderer> ().material = on_material;
		}
	}
}
