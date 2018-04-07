// MOVE_WALL_TRIGGER.CS
// MAX MILLS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Wall_Trigger : MonoBehaviour 
{
	// Objects to move 
	public GameObject start_wall;
	GameObject light_;
	public GameObject start_door;
	public bool is_testing;


	// Use this for initialization
	void Start () 
	{
		// Find the light at the start
		light_ = GameObject.Find("lightbulb_at_start");

		// Find the start wall
		//start_wall = GameObject.Find("start_wall");

		if(!is_testing)
		{
			// TODO 
			// Readd this code
			start_wall.SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public static void Restart ()
	{
		// Load the Main Menu scene
		SceneManager.LoadScene (1);
	}

	void OnTriggerEnter(Collider other)
	{
		// If player cameraRig or FPScontroller interact
		if (other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController") 
		{			
			
			// Play door shut animation
			GameObject.Find("hinge").GetComponent<Animator>().speed = 0.5f;
			// Play door shut animation
			GameObject.Find("hinge").GetComponent<Animator>().SetBool("opening",false);



			// Unload main menu scene after a few seconds
			StartCoroutine(UnloadScene());

		}
	}

	IEnumerator UnloadScene()
	{
		
		// Wait until door has shut
		yield return new WaitForSeconds(4.0f);

		// Unload main menu scene
		SceneManager.UnloadSceneAsync (1);

		// Set the replacement wall active
		start_wall.SetActive (true);

		yield return null;
	}
}
