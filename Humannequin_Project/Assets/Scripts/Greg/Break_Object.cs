using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Object : MonoBehaviour {

	public GameObject unbroken_object;	
	public GameObject broken_object;
	public Rigidbody[] piece_of_object;
	public float force;
	public float radius;
	public float break_speed;

	//for find speed
	public GameObject wrench;
	public float wrench_speed;
	public Vector3 last_wrench_pos;

	public bool is_broken;

	// Use this for initialization
	void Start () 
	{
		//find all children of object
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();

		//find the unbroken and broken
		for (int i = 0; i < children.Length; i++) 
		{
			if (children[i].name == "Unbroken")
			{
				unbroken_object = children[i].gameObject;
			}
			else if (children[i].name == "Broken")
			{
				broken_object = children[i].gameObject;
				broken_object.SetActive(false);
			}
		}

		//find the pieces' rigidbodies
		piece_of_object = broken_object.GetComponentsInChildren<Rigidbody>();

		//initialise floats
		force = 1.0f;
   		radius = 0.5f;
    	break_speed = 3.0f;

		//initialise speeds
		wrench_speed = 0.0f;
		last_wrench_pos = Vector3.zero;

		//init audio 
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Find_Speed ();
	}

	void shatter(Vector3 hit_pos, float hit_speed)
	{
		//Debug.Log(hit_speed);

		//switch out fixed glass for broken
		unbroken_object.SetActive(false);
		broken_object.SetActive (true);

		//add explosion force to all shards
		for (int i = 0; i < piece_of_object.Length; i++) 
		{
			piece_of_object[i].AddExplosionForce(force * hit_speed, hit_pos, radius, 0.0f, ForceMode.Impulse);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wrench")
		{
		//	Debug.Log ("yes");
		//	Rigidbody temp = other.gameObject.GetComponent<Rigidbody>();
			if (wrench_speed >= break_speed)
			{
				shatter(other.transform.position, wrench_speed);

				if(!is_broken)
				{
					// play break
					gameObject.GetComponents<AudioSource>()[0].Play();
					is_broken = true;
				}
			}
			else
			{
				if(!is_broken)
				{
					//Player hit
					gameObject.GetComponents<AudioSource>()[1].Play();
				}

			}
		}
	}

	void Find_Speed()
	{
		wrench_speed = (((wrench.transform.position - last_wrench_pos).magnitude) / Time.deltaTime);
		last_wrench_pos = wrench.transform.position;

		//Debug.Log (wrench_speed);
	}
}

// + (Vector3.back * 0.1f)