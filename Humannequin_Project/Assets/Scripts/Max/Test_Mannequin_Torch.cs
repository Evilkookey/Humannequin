using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mannequin_Torch : MonoBehaviour {

	public Transform player;
	float min_dist = 2.0f;
	Vector3 target_position;
	float move_speed = 2.0f;
	bool move = false;

	public Light room_light;

	// Use this for initialization
	void Start () 
	{
		//room_light = GameObject.Find("");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (move && Vector3.Distance (transform.position, player.position) >= 0.2f && room_light.enabled == false) 
		{
			// Move and rotate towards player 
			target_position = new Vector3 (player.position.x, 
				this.transform.position.y, 
				player.position.z);

			gameObject.transform.LookAt (target_position);

			transform.position += transform.forward * move_speed * Time.deltaTime;
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Light") 
		{
			move = false;
			//Debug.Log ("HIT");
		}

	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Light") 
		{
			move = true;
		}
	}
}
