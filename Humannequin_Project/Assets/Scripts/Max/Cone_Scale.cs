using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone_Scale : MonoBehaviour {

	public GameObject torch;
	Vector3 cone_scale, cone_pos;

	// Use this for initialization
	void Start () {
		
	
	}

	// Update is called once per frame
	void Update () 
	{
		// This currently works with a spotlight angle of 70 and range of 10

		//this.transform.position = cone_pos; 
		float angle = (torch.GetComponent<Light> ().spotAngle); // 38
		float temp = 1 - (22.5f/angle);



		temp *= 2;

		temp += 1;

		cone_scale = new Vector3(temp, this.transform.localScale.y, temp);

		cone_pos = new Vector3(this.transform.position.x,this.transform.position.y, torch.GetComponent<Light> ().range / 2);

		this.transform.localScale = cone_scale;

		temp = 0;
	}
		
}
