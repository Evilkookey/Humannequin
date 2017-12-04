// MOVE_WALL_TRIGGER.CS
// MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_wall_trigger : MonoBehaviour {

	// Objects to move 
	public GameObject wall;
	public GameObject door;

	// Positions to move to 
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
		// If player cameraRig or FPScontroller interact
		if (other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController") 
		{
			// Move objects to new positions
			wall.transform.position = new Vector3 (wall.transform.position.x, wall_pos, wall.transform.position.z);
			door.transform.position = new Vector3 (door.transform.position.x, door_pos, door.transform.position.z);

		}
	}
}
