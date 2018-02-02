using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone_Scale : MonoBehaviour {

	public GameObject torch;
	Vector3 cone_scale, cone_pos;

	// Use this for initialization
	void Start () {
		
		//this.transform.position = cone_pos; 
	}

	// Update is called once per frame
	void Update () {
		float angle = (torch.GetComponent<Light> ().spotAngle); // 38
		float temp = 1 - (22.5f/angle);

		temp += 1;

		temp *= 2;

		cone_scale = new Vector3(temp, this.transform.localScale.y, temp);

		cone_pos = new Vector3(this.transform.position.x,this.transform.position.y, torch.GetComponent<Light> ().range / 2);

		this.transform.localScale = cone_scale;
	}
}
