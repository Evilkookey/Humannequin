using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Grab : MonoBehaviour {

	public Animator anim;
	float grab_speed;
	float trigger_axis;
	float value;

	// Defining controller and tracked object
	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}
	// Set up tracked object
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		trigger_axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
		value = 1-trigger_axis;

		//value +=0.01f;

		// If trigger is down
		/*if (device.GetHairTriggerDown())
		{
			
		}*/
	}

	void OnGUI()
	{
		value = anim.GetFloat("Grab");

		value = GUI.HorizontalSlider(new Rect(10,10,100,20),value, 0,1);

		anim.SetFloat("Grab",value);
	}
}
