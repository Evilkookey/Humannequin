// TEST_MANNEQUIN_MOVE.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mannequin_Move : MonoBehaviour 
{

	// This was used a test if the mannequin could move towards the player if the players view of the enemy was blocked by a wall
	// It worked but the shadow of the enemy was seen moving 

	public Transform player;
	float min_dist = 2.0f;
	Vector3 target_position;
	float move_speed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Create ray from the centre, left and right of the enemy
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

		// If visible to the camera 
		if(this.GetComponent<Renderer> ().isVisible)
		{
			if (Physics.Raycast (rayL.origin, rayL.direction, out hitL, 1000.0f) && Physics.Raycast (rayR.origin, rayR.direction, out hitR, 1000.0f)) 
			{
				// if colliders hit an object called wall
				if (hitR.collider.name == "Wall" || hitL.collider.name == "Wall") 
				{
					// Move and rotate towards player 
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
