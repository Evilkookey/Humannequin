using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Mannequin_move : MonoBehaviour {

	public Transform player;		//Location of the Player
	public float min_dist;			//Distance which the enemy stops at from the player
	public float move_speed;		//Speed at which the enemy walks at
	public GameObject enemyBody;
	public bool WeepingAngelEnemyType;

	NavMeshAgent agent;
	Vector3 target_postition;
	public Transform head;
	public Transform test;

	Plane plane;
	float distance;
	float rotationY;
	Vector3 test_rotation;
	public GameObject CameraRigPlayer,FPSController;

	//[SerializeField]
	public enum Enemy_Type
	{
		TELEPORT,
		FOLLOW,
		TEST
	};

	// Use this for initialization
	void Start () 
	{
		plane = new Plane(Vector3.up, Vector3.up*5);
		agent = GetComponent<NavMeshAgent> ();

		//CameraRigPlayer = GameObject.Find ("[CameraRig]");
		//FPSController = GameObject.Find ("FPSController");


			
	}

	// Update is called once per frame
	void Update () 
	{
		//Switch who the player is for debugging
		if (FPSController.activeInHierarchy == true) 
		{
			player = FPSController.transform;

		} else if (CameraRigPlayer.activeInHierarchy == true) 
		{
			player = CameraRigPlayer.transform;
		//	Debug.Log ("AACTIVE");
		}

		// For interacting with objects ingame
		//RaycastHit hit;
//		Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
//
//		plane.Raycast(ray, out distance);
//		Debug.Log (distance);
//
//		//markerObject.position = ray.GetPoint(rayDistance);
//
//		Debug.DrawRay (ray.origin, ray.direction);

		//if (Physics.Raycast (ray.origin, ray.direction, out hit, 1000.0f)) 
		//{
		//	Debug.Log("HELP");
		//}



		if (WeepingAngelEnemyType) {
			
			if (!this.GetComponent<Renderer> ().isVisible & Vector3.Distance (transform.position, player.position) >= min_dist) 
			{
				//transform.position += transform.forward * move_speed * Time.deltaTime;

				target_postition = new Vector3 (player.position.x, 
					//this.transform.position.y, 
					head.position.y - 0.4f,
					player.position.z);
				
				agent.SetDestination (player.position);

				float difference = Vector3.Angle ( transform.forward, target_postition);
				
				if (difference < 90.0f) 
				{
					head.LookAt (target_postition);
				} else 
				{
					Debug.Log ("BreakNeck");
					head.rotation = Quaternion.Euler(new Vector3(0.0f,90.0f,0.0f));
				}
				//Debug.Log (difference);
//				test.LookAt (player);
//				rotationY = test.rotation.eulerAngles.y;
//
//				rotationY = head.rotation.y;
//				rotationY = Mathf.Clamp (rotationY, -90f, 90f);
//				head.rotation = Quaternion.Euler(0.0f, 0.0f, rotationY);
//


				//GetComponent<Animator> ().speed = 1;

			} else 
			{
				//agent.SetDestination (null);
				agent.SetDestination(transform.position);
				transform.position = transform.position;
				transform.rotation = transform.rotation;
				//GetComponent<Animator> ().speed = 0;
			}
		} else 
		{
			if (Vector3.Distance (transform.position, player.position) >= min_dist) 
			{
				//transform.position += transform.forward * move_speed * Time.deltaTime;

				target_postition = new Vector3 (player.position.x, 
					//this.transform.position.y, 
					head.position.y,
					player.position.z);
				
				agent.SetDestination (player.position);

				head.LookAt (target_postition);
				//GetComponent<Animator> ().speed = 1;

			} else 
			{
				//agent.SetDestination (transform.position);
				agent.SetDestination(transform.position);

				transform.position = transform.position;
				transform.rotation = transform.rotation;
				//GetComponent<Animator> ().speed = 0;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			//transform.position = transform.position;
			Debug.Log ("sdkjfn");
		}

	}

}

