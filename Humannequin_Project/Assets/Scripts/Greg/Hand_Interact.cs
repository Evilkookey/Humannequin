using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Interact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider interact_object)
	{
		if (interact_object.tag == "Interact") 
		{
			interact_object.SendMessage ("Activate");
		}
	}
}
