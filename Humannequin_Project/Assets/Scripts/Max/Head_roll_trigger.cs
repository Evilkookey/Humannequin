// Head_roll_trigger.CS
// MAX MILLS

// THis is used to roll a head from around a corner once the player hits the trigger
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_roll_trigger : MonoBehaviour {

	public bool stop = false;
	public GameObject head;
	public float force;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "[CameraRig]" || col.gameObject.name == "FPSController") 
		{
			if (!stop)
			{
				// Apply a force to the head object
				head.GetComponent<Rigidbody> ().AddForce (Vector3.forward * force,ForceMode.Impulse);
				stop = true;
			}
		}
	}
}
