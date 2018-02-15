// TEST_INPUT.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Input : MonoBehaviour 
{
	GameObject screw_0;
	GameObject screw_1;
	GameObject screw_2;
	GameObject screw_3;
	GameObject cover;

	// Use this for initialization
	void Start () 
	{
		screw_0 = GameObject.Find ("screw_0");
		screw_1 = GameObject.Find ("screw_1");
		screw_2 = GameObject.Find ("screw_2");
		screw_3 = GameObject.Find ("screw_3");
		cover = GameObject.Find ("cover");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown("1")) 
		{
			Debug.Log ("screw_0");
			screw_0.SendMessage ("Activate");
		}

		if (Input.GetKeyDown("2")) 
		{
			Debug.Log ("screw_1");
			screw_1.SendMessage ("Activate");
		}

		if (Input.GetKeyDown("3")) 
		{
			Debug.Log ("screw_2");
			screw_2.SendMessage ("Activate");
		}

		if (Input.GetKeyDown("4")) 
		{
			Debug.Log ("screw_3");
			screw_3.SendMessage ("Activate");
		}

		if (Input.GetKeyDown("5")) 
		{
			Debug.Log ("cover");
			cover.SendMessage("Activate");
		}
	}
}
