using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_wrench_for_test : MonoBehaviour {

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
			rb.MovePosition(rb.transform.position + (Vector3.forward * speed));
		}
		if (Input.GetKey(KeyCode.S))
		{			
			rb.MovePosition(rb.transform.position + (Vector3.back * speed));
		}
		if (Input.GetKey(KeyCode.D))
		{			
			rb.MovePosition(rb.transform.position + (Vector3.right * speed));
		}
		if (Input.GetKey(KeyCode.A))
		{			
			rb.MovePosition(rb.transform.position + (Vector3.left * speed));
		}
		if (Input.GetKey(KeyCode.Q))
		{			
			rb.MovePosition(rb.transform.position + (Vector3.down * speed));
		}
		if (Input.GetKey(KeyCode.E))
		{			
			rb.MovePosition(rb.transform.position + (Vector3.up * speed));
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			speed = 0.1f;
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			speed = 0.01f;
		}
	}
}
