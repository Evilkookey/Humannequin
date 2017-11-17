using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_wall_trigger : MonoBehaviour {

	public GameObject wall;
	public GameObject door;

	public float wall_pos = 1.746f;
	public float door_pos = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController") 
		{
			wall.transform.position = new Vector3 (wall.transform.position.x, wall_pos, wall.transform.position.z);
			door.transform.position = new Vector3 (door.transform.position.x, door_pos, door.transform.position.z);

		}
	}
}
