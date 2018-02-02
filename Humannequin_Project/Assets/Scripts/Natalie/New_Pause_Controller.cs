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
		pause_menu_object.SetActive(false);
		Time.timeScale = 1;
	}

	// Update is called once per frame
	public void Activate () 
	{
		StartCoroutine (PauseCoroutine ());
	}

	IEnumerator PauseCoroutine()
	{
		while (true)
		{
			if (pause_menu_object.activeInHierarchy == false) 
			{
				Debug.Log("pause game");
				pause_menu_object.SetActive(true);
				Time.timeScale = 0;
			}
			else if (pause_menu_object.activeInHierarchy == true) 
			{
				Debug.Log("play game");
				pause_menu_object.SetActive(false);
				Time.timeScale = 1;
			}
			yield return null;
		}    
	}

}