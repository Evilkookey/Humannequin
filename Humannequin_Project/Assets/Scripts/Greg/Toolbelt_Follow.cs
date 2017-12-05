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
	public float total_height;

	// Use this for initialization
	void Start () 
	{
		down_ray = new Ray(player_head.transform.position, Vector3.down);
		if (Physics.Raycast(down_ray.origin, down_ray.direction, out hit))
		{
			total_height = hit.distance;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.position = (player_head.transform.position - (Vector3.up * (total_height / 2)));
		gameObject.transform.eulerAngles = new Vector3(0.0f, player_body.transform.eulerAngles.y, 0.0f);
	}
}
