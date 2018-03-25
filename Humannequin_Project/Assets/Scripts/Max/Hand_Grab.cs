using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Grab : MonoBehaviour {

	public Animator anim, point_anim;
	float grab_speed;
	float trigger_axis = 0.0f;
	float value;
	public bool pliers_grip;


	// Defining controller and tracked object
	public SteamVR_TrackedObject trackedObj;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;

	public SteamVR_Controller.Device device;
	//{
	///	get { return SteamVR_Controller.Input((int)trackedObj.index); }
	//}/
	// Set up tracked object
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		//anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//TEST IN VR
		//trigger_axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

		//value = 1-trigger_axis;

		//value +=0.01f;
		//Debug.Log(trigger_axis);
		
		anim.SetFloat("Grab",trigger_axis);
		// TODO
		// You gotta reset this value when you change back to the regular hand model

		// Check grip for pointing
		//value = device.GetAxis(grip_button).x;

		/*if (device.GetPressDown(grip_button))
		{
			anim.SetBool("Pointing",true);
			Debug.Log("PoINT");
		}
		else if (device.GetPressUp(grip_button))
		{
			anim.SetBool("Pointing",false);
		}*/

		anim.SetBool("Holding_Pliers",pliers_grip);
	}

	void OnGUI()
	{
		value = anim.GetFloat("Grab");

		value = GUI.HorizontalSlider(new Rect(10,10,100,20),value, 0,0.5f);

		anim.SetFloat("Grab",value);


	
	}
}
