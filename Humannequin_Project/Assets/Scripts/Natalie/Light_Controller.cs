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

	public static int custom2_smoothness = 10;

	// Array of random values for the intensity.
	private float[] smoothing = new float[custom2_smoothness];


	public enum flicker_types{
		tiny,
		medium,
		simple,
		custom,
		custom2
	}

	public flicker_types flicker;

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

		for(int i = 0 ; i < smoothing.Length; i++)
		{
			smoothing[i] = 0.0f;
		}


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

		switch(flicker)
		{

		case flicker_types.simple:		

			counter += 10.0f;

			//this_light.enabled = true;
			broken = false;

			this_light.intensity = Random.Range(intensity_min, intensity_max);

			if (counter > Random.Range (freq_min, freq_max)) 
			{
				//this_light.enabled = false;
				broken = true;

			}

			counter = 0;
		

			break;

		case flicker_types.custom2:			
			

			float sum = 0.0f;

			// Shift values in the table so that the new one is at the
			// end and the older one is deleted.
			for(int i = 1 ; i < smoothing.Length ; i++)
			{
				smoothing[i-1] = smoothing[i];
				sum+= smoothing[i-1];
			}

			// Add the new value at the end of the array.
			smoothing[smoothing.Length -1] = Random.value;
			sum+= smoothing[smoothing.Length -1];

			// Compute the average of the array and assign it to the
			// light intensity.

			this_light.intensity = sum / smoothing.Length;

		

			break;

		case flicker_types.custom:

			//counter += Random.Range (increase_min, increase_max);
			counter += 1;
			if (counter > Random.Range (freq_min, freq_max)) 
			{
				this_light.intensity = Random.Range (intensity_max, intensity_min);
				counter = 0;
			} 
			else 
			{
				this_light.intensity = intensity_max;
				//this_light.intensity = Mathf.Lerp (this_light.intensity, intensity_max, Time.time);
			}

			break;

		case flicker_types.tiny:

			intensity_max = 1.0f;
			intensity_min = 1.06f;
			freq_max = 1.0f;
			freq_min = 1.0f;
			increase_max = 1.0f;
			increase_min = 1.0f;

			break;

		case flicker_types.medium:
			
			intensity_max = 0.95f;
			intensity_min = 0.82f;
			freq_max = 1735.0f;
			freq_min = 12.0f;
			increase_max = 3.9f;
			increase_min = 1.48f;

			break;

		}
		/*
		if(this_light.intensity < 1.0f)
		{
			broken = true;
		}
		else{
			broken = false;
		}*/
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
