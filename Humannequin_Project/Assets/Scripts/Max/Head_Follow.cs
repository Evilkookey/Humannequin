// Head_Follow.CS
// MAX MILLS

// This was used for mannequin heads to track the players location and look at it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Follow : MonoBehaviour {

	public GameObject player_;
	Transform player_pos;
	Vector3 target_pos;

	// Use this for initialization
	void Start () {
		
		player_pos = GameObject.Find("FPSController").transform;
	}
	
	// Update is called once per frame
	void Update () {

		// Player position in X and Z axis
		target_pos = new Vector3(player_pos.transform.position.x ,0.0f ,player_pos.transform.position.z); 

		// When not rendered (visible)
		if (!this.gameObject.GetComponent<Renderer> ().isVisible) {

			// Look at player
			this.transform.LookAt (player_pos);
		}
		
	}
}
