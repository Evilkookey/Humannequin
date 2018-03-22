using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Visible : MonoBehaviour {

	bool not_visible;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!this.GetComponent<Renderer> ().isVisible)
		{
			not_visible = true;
		}
		else{
			not_visible = false;
		}
	}

	public bool Get_Not_Visible()
	{
		return not_visible;
	}
}
