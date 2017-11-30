using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu_Controller : MonoBehaviour 
{
	// Variables
	Canvas pause_menu_canvas;
	GameObject pause_menu_object;

	// Use this for initialization
	void Start () 
	{
		pause_menu_object = GameObject.Find("pause");
		//pause_menu_canvas = pause_menu_object.GetComponent<Canvas>();
		//pause_menu_canvas.enabled = false;
		pause_menu_object.SetActive(false);
		Time.timeScale = 1;
	}

	// Update is called once per frame
	public void Activate () 
	{
		if (pause_menu_object.activeInHierarchy == false) 
		{
			Debug.Log("pause game");
			pause_menu_object.SetActive(true);
			Time.timeScale = 0;
		}
		else if (pause_menu_object.activeInHierarchy == true) 
		{
			Debug.Log("play dat game");
			pause_menu_object.SetActive(false);
			Time.timeScale = 1;
		}
	}
}