using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectTest : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject; 
	private GameObject objectInHand;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update () 
	{
		//If trigger is down
		if (Controller.GetHairTriggerDown())
		{
			//If you are colliding with an object
			if (collidingObject)
			{
				//Make a joint to grab the object
				GrabObject();
			}
		}
		//If trigger is up
		if (Controller.GetHairTriggerUp())
		{
			//If you are holding an object
			if (objectInHand)
			{
				//Destroy the joint, letting go of the object
				ReleaseObject();
			}
		}
	}
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}
		
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}
	//For setting the collision object 
	void SetCollidingObject(Collider col)
	{		
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}

		collidingObject = col.gameObject;
	}

	//For grabbing to object
	void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint(); //FixedJoint
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	//Creates joint
	FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	//For dropping the object
	void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
		
			objectInHand.GetComponent<Rigidbody>().velocity = new Vector3(-Controller.velocity.x,Controller.velocity.y,-Controller.velocity.z) ;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		objectInHand = null;
	}

	

}
