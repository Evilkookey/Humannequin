// LIGHT_CONTROLLER.CS
// NATALIE BAKER-HALL & MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Controller : MonoBehaviour 
{
	// Variables
	// Light flicker Variables
	// Light light_;

	public float counter = 0;
	public float intensity_max, intensity_min, freq_max, freq_min, increase_min, increase_max;

	public bool tiny_flicker = false;
	public bool good_flicker = false;

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
		// Load the materials from resources
		on_material = Resources.Load("Materials/on_light3", typeof(Material)) as Material;
		off_material = Resources.Load("Materials/off_light", typeof(Material)) as Material;

		// This gets the light which is a child of the gameObject
		this_light = gameObject.GetComponentInChildren<Light> ();
		is_off = false;


	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the light is declared as "broken" it will not be able to turn on
		if (broken) {
			is_off = true;
			this_light.enabled = false;
			gameObject.GetComponent<Renderer> ().material = off_material;
		} 
		else 
		{
			is_off = false;
			this_light.enabled = true;
			gameObject.GetComponent<Renderer> ().material = on_material;
		}

		counter += Random.Range(increase_min,increase_max);
		if(counter > Random.Range(freq_min,freq_max))
		{
			this_light.intensity = Random.Range(intensity_max,intensity_min);
			counter = 0;
		}
		else
		{
			//light_.intensity = max;
			this_light.intensity = Mathf.Lerp(this_light.intensity,intensity_max,Time.time);
		}

		if(tiny_flicker)
		{
			intensity_max = 1.0f;
			intensity_min = 1.06f;
			freq_max = 1.0f;
			freq_min = 1.0f;
			increase_max = 1.0f;
			increase_min = 1.0f;
		}
		if(good_flicker)
		{
			intensity_max = 0.95f;
			intensity_min = 0.82f;
			freq_max = 1735.0f;
			freq_min = 12.0f;
			increase_max = 3.9f;
			increase_min = 1.48f;
		}
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
