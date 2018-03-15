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
				head.GetComponent<Rigidbody> ().AddForce (Vector3.right * force,ForceMode.Impulse);
				stop = true;
			}
		}
	}
}
