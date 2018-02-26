using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone_Scale : MonoBehaviour {

	public GameObject torch;
	public Vector3 cone_scale, cone_pos;

	public float angle; 
	public float range; 
	public float temp_scale;
	public	float temp_y;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{

		angle = (torch.GetComponent<Light> ().spotAngle); 
		range = (torch.GetComponent<Light> ().range);
	

		Debug.Log(range*Mathf.Tan(Mathf.Deg2Rad * (angle/2.0f)));

		float scale = (range * Mathf.Tan (Mathf.Deg2Rad * (angle / 2.0f))) / 3.0f;


		cone_scale.x = scale;
		cone_scale.y = scale;
		cone_scale.z = range / 10.0f;

		this.transform.parent.localScale = cone_scale;

	}
		
}
