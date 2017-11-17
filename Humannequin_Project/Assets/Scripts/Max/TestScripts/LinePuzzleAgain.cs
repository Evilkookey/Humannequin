using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePuzzleAgain : MonoBehaviour {

	float raycast_distance = 200.0f;

	[System.Serializable]
	public struct lines_struct
	{
		public LineRenderer line_renderer;
		public bool line_complete;

	};

	//Line which gets set when you press the start cube
	public lines_struct current_line;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Cast ray from camera 
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray.origin, ray.direction, out hit, raycast_distance)) 
		{
			if(Input.GetMouseButton(0))
			{
				//if cube is a starting cube
				if (hit.collider.gameObject.tag == "start") 
				{
					//If cube is red
					if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red)
					{
						//Set the line renderer to the red one
						current_line.line_renderer = GameObject.Find("LineRendererRed").GetComponent<LineRenderer>();

						current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
					}
				}

				//if cube is a starting cube
				if (hit.collider.gameObject.tag == "empty") 
				{
					if(current_line.line_renderer != null)
					{
						//if cube is not already hit
						if(hit.collider.gameObject.GetComponent<Puzzle_cube>().hit == false)
						{
							hit.collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);

							current_line.line_renderer.positionCount++;
							current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
						}
					}
				}

			}
			if(Input.GetMouseButtonUp(0))
			{
				//if cube is a finish cube
				if (hit.collider.gameObject.tag == "Finish") 
				{
					//If cube is red
					if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red)
					{
						current_line.line_renderer.positionCount++;
						current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
					}

				}

				current_line.line_renderer = null;
			}
		}
	}
}
