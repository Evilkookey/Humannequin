// MOVETESTHAND.CS
// GREG BABIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTestHand : MonoBehaviour 
{
	public Rigidbody rb;
	float speed;
	// Use this for initialization
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody>();
		speed = 0.01f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.W))
		{			
            // Move forward
			rb.MovePosition(rb.transform.position + (Vector3.forward * speed));
		}
		if (Input.GetKey(KeyCode.S))
		{
            // Move back
            rb.MovePosition(rb.transform.position + (Vector3.back * speed));
		}
		if (Input.GetKey(KeyCode.D))
		{
            // Move right
            rb.MovePosition(rb.transform.position + (Vector3.right * speed));
		}
		if (Input.GetKey(KeyCode.A))
		{
            // Move left
            rb.MovePosition(rb.transform.position + (Vector3.left * speed));
		}
		if (Input.GetKey(KeyCode.Q))
		{
            // Move down
            rb.MovePosition(rb.transform.position + (Vector3.down * speed));
		}
		if (Input.GetKey(KeyCode.E))
		{
            // Move up
            rb.MovePosition(rb.transform.position + (Vector3.up * speed));
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
            // Speed up
			speed = 0.1f;
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
            // Slow down
			speed = 0.01f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Set the type of object it is
		if (other.tag == "ToolSlot")
		{
			// Set the object to the one to be interacted with
			Debug.Log("hello");
		}
	}
}