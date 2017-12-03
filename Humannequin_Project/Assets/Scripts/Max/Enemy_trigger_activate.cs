using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_trigger_activate : MonoBehaviour {
	
	public GameObject enemy;
	public Light main_light;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	void OnTriggerEnter()
	{
		//turn on the enemy follow script
		enemy.gameObject.GetComponentInChildren<Mannequin_move> ().enabled = true;

		// turn on light
		main_light.gameObject.SetActive (true);

		// Play sound for light turning on
		gameObject.GetComponent<AudioSource>().Play();
	}
}
