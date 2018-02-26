using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Type : MonoBehaviour {

	public enum type_
	{
		FAX,
		POSTIT,
		JOURNAL
	};

	public type_ Note;
	public bool picked_up;

	// Use this for initialization
	void Start () 
	{
		picked_up = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Activate(string tool)
	{
		if(tool == "NONE")
		{
			picked_up = true;

			// Send message to pause menu contrller here

		}
	}
}
