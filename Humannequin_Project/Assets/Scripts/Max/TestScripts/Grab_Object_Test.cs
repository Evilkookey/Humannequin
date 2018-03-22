// GRAB_OBJECT_TEST.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Object_Test : MonoBehaviour {

	// Defining controller and tracked object
	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	GameObject colliding_object;		// Object you collide with
	GameObject object_in_hand;			// Object you pick up

	// Set up tracked object
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update () 
	{
		// If trigger is down
		if (Controller.GetHairTriggerDown())
		{
			//I f you are colliding with an object
			if (colliding_object)
			{
				// Make a joint to grab the object
				Grab_Object();
			}
		}
		// If trigger is up
		if (Controller.GetHairTriggerUp())
		{
			// If you are holding an object
			if (object_in_hand)
			{
				// Destroy the joint, letting go of the object
				Release_Object();
			}
		}
	}

	// If collided with object, call set
	public void OnTriggerEnter(Collider other)
	{
		Set_Colliding_Object(other);
	}

	// If collided with object, call set
	public void OnTriggerStay(Collider other)
	{
		Set_Colliding_Object(other);
	}

	// If not colliding with object
	public void OnTriggerExit(Collider other)
	{
		// Remove collided object if there is one set
		if (!colliding_object)
		{
			return;
		}

		colliding_object = null;
	}

	// For setting which object you collided with
	void Set_Colliding_Object(Collider col)
	{		
		// If there is already a colliding object or if the collider has no rigidbody
		if (colliding_object || !col.GetComponent<Rigidbody>())
		{
			// Do not set colliding object
			return;
		}

		// Set colliding object
		colliding_object = col.gameObject;
	}

	// For grabbing to object
	void Grab_Object()
	{
		// Set up object 
		object_in_hand = colliding_object;
		colliding_object = null;

		// Set up joint 
		FixedJoint joint = Add_Fixed_Joint();

		//Create joint to the held object
		joint.connectedBody = object_in_hand.GetComponent<Rigidbody>();
	}

	// Creates joint
	FixedJoint Add_Fixed_Joint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	// For dropping the object
	void Release_Object()
	{
		if (GetComponent<FixedJoint>())
		{
			// Destroying joint 
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
		
			// Adding velocity to the object in the direction of the controller
			object_in_hand.GetComponent<Rigidbody>().velocity = new Vector3(-Controller.velocity.x,Controller.velocity.y,-Controller.velocity.z) ;
			object_in_hand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		object_in_hand = null;
	}

	

}
