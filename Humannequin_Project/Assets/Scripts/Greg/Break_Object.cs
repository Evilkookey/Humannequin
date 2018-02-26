// BREAK_OBJECT.CS
// GREG BALBIRNIE & MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Object : MonoBehaviour {

	public GameObject unbroken_object;	
	public GameObject broken_object;
	public Rigidbody[] piece_of_object;
	public float force;
	public float radius;
	public float break_speed, lock_break_speed;

	// For find speed
	public GameObject[] hand;
	public float wrench_speed;
	public Vector3 last_wrench_pos;

	public bool is_broken,is_lock_broken;

	public enum breakable_objects
	{
		wood,
		lock_
	};

	public breakable_objects current_object;

	// Use this for initialization
	void Start () 
	{
		// Find all children of object
		Transform[] children = gameObject.GetComponentsInChildren<Transform>();

		// Find the unbroken and broken
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

		// Create hands
		hand = new GameObject[2];



		// Find the pieces' rigidbodies
		piece_of_object = broken_object.GetComponentsInChildren<Rigidbody>();

		// Initialise floats
		force = 1.0f;
   		radius = 0.5f;
    	break_speed = 3.0f;
		lock_break_speed = 2.0f;

		// Initialise speeds
		wrench_speed = 0.0f;
		last_wrench_pos = Vector3.zero;		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!hand[0])
		{
			// Find the left hand
			hand[0] = GameObject.Find("Controller (left)");
		}
		if (!hand[1])
		{
			// Find the left hand
			hand[1] = GameObject.Find("Controller (right)");
		}
		if (hand[0] && hand[1])
		{
			// Find speed only if script knows both hands
			Find_Speed ();
		}
	}

	void shatter(Vector3 hit_pos, float hit_speed)
	{
		// Switch out fixed glass for broken
		unbroken_object.SetActive(false);
		broken_object.SetActive (true);

		// Add explosion force to all shards
		for (int i = 0; i < piece_of_object.Length; i++) 
		{
			piece_of_object[i].AddExplosionForce(force * hit_speed, hit_pos, radius, 0.0f, ForceMode.Impulse);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wrench")
		{

			print(wrench_speed);

			switch (current_object)
			{
				case breakable_objects.wood:
				{
					if (wrench_speed >= break_speed)
					{
						shatter (other.bounds.center, wrench_speed);

						if (!is_broken) 
						{
							// Play break
							gameObject.GetComponents<AudioSource> () [0].Play ();
							is_broken = true;
						}
					} 
					else 
					{
						if (!is_broken) 
						{
							// Play hit
							gameObject.GetComponents<AudioSource> () [1].Play ();
						}

					}
					break;

				}
				case breakable_objects.lock_:
				{
					if (wrench_speed >= lock_break_speed)
					{
						//break the lock
						Rigidbody rb = gameObject.AddComponent<Rigidbody>();

						if (!is_lock_broken) 
						{
							// Play break
							//gameObject.GetComponent<AudioSource>().clip = metal_break_sound
							//gameObject.GetComponents<AudioSource> () [0].Play ();
							is_lock_broken = true;
						}
					}
					else
					{
						if (!is_lock_broken) 
						{
							// Play hit
							//gameObject.GetComponent<AudioSource>().clip = metal_hit_sound;
							//gameObject.GetComponents<AudioSource> () [1].Play ();
						}
					}
					break;
				}

				default:
					break;
			}

		}
	}

	void Find_Speed()
	{
		for (int i = 0; i < 2; i++)
		{
			// Check if the wrench is in the left hand
			if (hand[i].GetComponent<VR_Input_Ver_4>().active_tool == VR_Input_Ver_4.Tool.WRENCH)
			{
				// Find wrench speed from last position and current
				wrench_speed = (((hand[i].transform.position - last_wrench_pos).magnitude) / Time.deltaTime);
				last_wrench_pos = hand[i].transform.position;
			}
		}
	}
}

// + (Vector3.back * 0.1f)