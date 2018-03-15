using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin_Animated : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * 2.0f / 1000);
	}
}
