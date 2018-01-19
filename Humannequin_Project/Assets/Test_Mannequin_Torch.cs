using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mannequin_Torch : MonoBehaviour {

	public Transform player;
	float min_dist = 2.0f;
	Vector3 target_position;
	float move_speed = 2.0f;
	bool move = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (move) {
			// Move and rotate towards player 
			target_position = new Vector3 (player.position.x, 
				this.transform.position.y, 
				player.position.z);

			gameObject.transform.LookAt (target_position);

			transform.position += transform.forward * move_speed * Time.deltaTime;
		}
		move = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Light") {
			move = false;
		} else {
			move = true;
		}

	}
}
