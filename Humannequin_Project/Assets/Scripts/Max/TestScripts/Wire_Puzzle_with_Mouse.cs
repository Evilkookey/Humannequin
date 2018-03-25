using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Puzzle_with_Mouse : MonoBehaviour {
	public Camera main_camera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	
		//Cast ray from camera 
		RaycastHit hit;
		Ray ray = main_camera.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray.origin, ray.direction, out hit, 200.0f)) 
		{
			if(Input.GetMouseButtonDown(0))
			{
				/*if (gameObject.GetComponent<BoxCollider> ().enabled == false) {
					gameObject.GetComponent<BoxCollider> ().enabled = true;
				} else {
					gameObject.GetComponent<BoxCollider> ().enabled = false;
				}*/
				gameObject.GetComponent<BoxCollider> ().enabled = true;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				hit.collider.SendMessage("Deactivate");
				gameObject.GetComponent<BoxCollider> ().enabled = false;
			}

		}
	}

	void OnTriggerEnter(Collider other)
	{
		//other.gameObject.SendMessage("Activate", "PLIERS");
	}
}
