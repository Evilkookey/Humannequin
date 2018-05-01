// CAMERAPAN.CS
// MAX MILLS

// This was used to record the trailer, featuring a panning camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour {
	public bool started = false;
	public bool rotated = false;
	public float speed_;

	float spinner = 0.0f;
	public float rotate = 0.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(started)
		{
			gameObject.transform.Translate((-speed_/100) * Vector3.right,Space.World);
		}
		if(rotated)
		{
			gameObject.transform.eulerAngles = new Vector3 (0.0f,gameObject.transform.eulerAngles.y + rotate,0.0f);

		}
	}
}
