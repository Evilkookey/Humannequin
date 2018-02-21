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
	public float max, min, freq_max, freq_min, increase_min, increase_max;

	public bool tiny_flicker = false;

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
			light_.intensity = Random.Range(max,min);
			counter = 0;
		}
		else
		{
			//light_.intensity = max;
			light_.intensity = Mathf.Lerp(light_.intensity,max,Time.time);
		}

		if(tiny_flicker)
		{
			max = 1.0f;
			min = 1.06f;
			freq_max = 1.0f;
			freq_min = 1.0f;

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
