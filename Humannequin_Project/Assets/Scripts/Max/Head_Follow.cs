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

		target_pos = new Vector3(player_pos.transform.position.x ,0.0f ,player_pos.transform.position.z); 

		if (!this.gameObject.GetComponent<Renderer> ().isVisible) {

			this.transform.LookAt (player_pos);
		}
		
	}
}
