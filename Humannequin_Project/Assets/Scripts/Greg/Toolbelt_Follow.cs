// TOOLBELT_FOLLOW.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbelt_Follow : MonoBehaviour {

	public GameObject player_head; 
	public GameObject player_body;
	public Ray down_ray;
	public RaycastHit hit;
	public float height_diff = 6.0f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Set toolbelt position
		gameObject.transform.position = (player_head.transform.position - (Vector3.up * height_diff));
		// Set toolbelt rotation
		gameObject.transform.eulerAngles = new Vector3(0.0f, player_body.transform.eulerAngles.y, 0.0f);
	}

	// Calculate the height of the Toolbelt
	void Set_Toolbelt_Height (float hand_height)
	{
		//Find distance between hand and head
		height_diff = player_head.transform.position.y - hand_height;
	}
}
