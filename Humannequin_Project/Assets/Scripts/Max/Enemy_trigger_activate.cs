// ENEMY_TRIGGER_ACTIVATE.CS
// MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_trigger_activate : MonoBehaviour 
{
	// Objects to trigger if player hit the collider
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
		// Turn on the enemy follow script
		enemy.gameObject.GetComponentInChildren<Mannequin_move> ().enabled = true;

		// Turn on light
		main_light.gameObject.SetActive (true);

		// Play sound for light turning on
		gameObject.GetComponent<AudioSource>().Play();
	}
}
