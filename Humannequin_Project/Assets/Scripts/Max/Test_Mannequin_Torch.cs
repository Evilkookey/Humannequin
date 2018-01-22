using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mannequin_Torch : MonoBehaviour {

	public Transform player;
	float min_dist = 2.0f;
	Vector3 target_position;
	float move_speed = 2.0f;
	bool move = true;

	public GameObject torch;
	Vector3 cone_scale, cone_pos;

	// Use this for initialization
	void Start () {
	

		float angle = (torch.GetComponent<Light> ().spotAngle); // 38
		float temp = 1 - (33/angle);

		temp += 1;

		cone_scale = new Vector3(temp, this.transform.localScale.y, temp);

		cone_pos = new Vector3(this.transform.position.x,this.transform.position.y, torch.GetComponent<Light> ().range /2);

		this.transform.localScale = cone_scale;
		this.transform.position =cone_pos; 

	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (move) 
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
