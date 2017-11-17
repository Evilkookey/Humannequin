using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMannequinMove : MonoBehaviour {

	public Transform player;
	float min_dist = 2.0f;
	Vector3 target_position;
	float move_speed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// For interacting with objects ingame
		RaycastHit hit,hitL,hitR;
		Ray rayR = new Ray(new Vector3(transform.position.x + 0.5f,transform.position.y,transform.position.z), player.position);
		Ray rayL = new Ray(new Vector3(transform.position.x - 0.5f,transform.position.y,transform.position.z), player.position);
		//Ray ray = new Ray(transform.position, player.position);

		//markerObject.position = ray.GetPoint(rayDistance);

		Debug.DrawRay (rayL.origin, rayL.direction);


		/*if (!this.GetComponent<Renderer> ().isVisible & Vector3.Distance (transform.position, player.position) >= min_dist) {

			//Debug.Log("WALKING");

					//Debug.Log("HitWall");
					target_position = new Vector3 (player.position.x, 
						this.transform.position.y, 
						player.position.z);
					
					gameObject.transform.LookAt (target_position);

					transform.position += transform.forward * move_speed * Time.deltaTime;

		}*/
		if(this.GetComponent<Renderer> ().isVisible)
		{
			if (Physics.Raycast (rayL.origin, rayL.direction, out hitL, 1000.0f) && Physics.Raycast (rayR.origin, rayR.direction, out hitR, 1000.0f)) 
			{
				if (hitR.collider.name == "Wall" || hitL.collider.name == "Wall") {
					target_position = new Vector3 (player.position.x, 
						this.transform.position.y, 
						player.position.z);

					gameObject.transform.LookAt (target_position);

					transform.position += transform.forward * move_speed * Time.deltaTime;
				}

			}


		}

	}
}
