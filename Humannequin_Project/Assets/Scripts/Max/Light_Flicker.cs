using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Flicker : MonoBehaviour {

	Light light_;
	
	/*public float heightScale = 1;
	public float xScale = 5;
	public float yScale = 10;
	public float cap = 1;*/

	public float counter = 0;
	public float intensity_max, intensity_min, freq_max, freq_min, increase_min, increase_max;

	public bool tiny_flicker = false;
	public bool good_flicker = false;

	// Use this for initialization
	void Start () 
	{
		light_ = this.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Random.Range(increase_min,increase_max);
		if(counter > Random.Range(freq_min,freq_max))
		{
			light_.intensity = Random.Range(intensity_max,intensity_min);
			counter = 0;
		}
		else
		{
			//light_.intensity = intensity_max;
			light_.intensity = Mathf.Lerp(light_.intensity,intensity_max,Time.time);
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


		// PERLIN NOISE TEST
		/*	
		float height = heightScale * Mathf.PerlinNoise(xScale*Time.time, yScale*Time.time);

		if (height < cap) 
		{
			light_.intensity = height;
		} 
		else 
		{
			light_.intensity = 0.0f;
		}*/
	}
}
