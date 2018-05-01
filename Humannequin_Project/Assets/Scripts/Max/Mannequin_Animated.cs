//Mannequin_Animated.CS
// MAX MILLS

// This is a simple move script for the final transform mannequin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_Animated : MonoBehaviour {


	public float speed = 4.0f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed / 1000);
	}
}
