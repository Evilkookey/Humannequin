//LOCKER_ROOM_TRIGGER.CS
//MAX MILLS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker_Room_Trigger : MonoBehaviour 
{
	// Objects to trigger if player hit the collider
	public GameObject enemy; 		
	public List<GameObject> lights = new List<GameObject>();

	public bool testing = false;

	// Use this for initialization
	void Start () 
	{
		if(!testing)
		{
			if(enemy.gameObject.GetComponentInChildren<Mannequin_Stalk> ().enabled == true)
			{
				enemy.gameObject.GetComponentInChildren<Mannequin_Stalk> ().enabled = false;

			}

			foreach( GameObject g in lights)
			{
				g.SetActive (false);
			}
		}


	}

	// Update is called once per frame
	void Update () {


	}
	void OnTriggerEnter(Collider collider)
	{
		if(collider.name == "[CameraRig]")
		{
			// Turn on the enemy follow script
			enemy.gameObject.GetComponentInChildren<Mannequin_Stalk> ().enabled = true;

			// Turn on light
			foreach(GameObject g in lights)
			{
				g.SetActive (true);
			}

			// Play sound for light turning on
			gameObject.GetComponent<AudioSource>().Play();
		}
	}
}
