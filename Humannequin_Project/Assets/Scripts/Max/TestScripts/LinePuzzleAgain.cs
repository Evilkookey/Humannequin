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
		public GameObject[] boxes;
	
	};

	//Line which gets set when you press the start cube
	public lines_struct current_line;

	public LineRenderer red_line, blue_line, green_line;

	public int MAX_LENGTH = 10;
	public float move_distance = 0.12f;

	// Use this for initialization
	void Start () 
	{
		red_line = GameObject.Find("LineRendererRed").GetComponent<LineRenderer>();
		blue_line = GameObject.Find("LineRendererBlue").GetComponent<LineRenderer>();
		green_line = GameObject.Find("LineRendererGreen").GetComponent<LineRenderer>();

		current_line.boxes = new GameObject[MAX_LENGTH];

//		red_line.boxes = new GameObject[MAX_LENGTH];
//		blue_line.boxes = new GameObject[MAX_LENGTH];
//		green_line.boxes = new GameObject[MAX_LENGTH];
//
//		current_line = red_line;
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
					if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{
						//Set the line renderer to the red one
						current_line.line_renderer = red_line;
						SetStart(hit.collider.gameObject);
					}
					else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the red one
						current_line.line_renderer = blue_line;
						SetStart(hit.collider.gameObject);
					}
					else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.green) //THIS CAN BE DONE BY GAMEOBJECT NAME INSTEAD OF COLOUR IF WE DONT WANT COLOUR TO BE VISIBLE
					{	
						//Set the line renderer to the red one
						current_line.line_renderer = green_line;
						SetStart(hit.collider.gameObject);
					}
					
//						//If the line is not completed already
//						if(!current_line.line_complete)
//						{
//							//Set the cube hit variable to true
//							hit.collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);
//
//							//Set the line renderer to the red one
//							current_line.line_renderer = red_line;
//
//							//Add the hit cube to the array of boxes
//							current_line.boxes[current_line.line_renderer.positionCount -1] = hit.collider.gameObject;
//
//							//Set a new position for the line renderer to the hix box position
//							current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
//						}
//						//This will reset the line if clicked on the start cube again when the line is completed
//						else
//						{
//							current_line.line_renderer = red_line;
//							current_line.line_complete = false;
//							hit.collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);
//							Reset();
//						}
					//}

				}

				//if cube is a starting cube
				if (hit.collider.gameObject.tag == "Respawn") 
				{
//					current_line.line_renderer = red_line;
//					current_line.line_complete = false;
//					Reset();
//					current_line.line_renderer = blue_line;
//					current_line.line_complete = false;
//					Reset();
//					current_line.line_renderer = green_line;
//					current_line.line_complete = false;
//					Reset();
				}

				//If cube is an empty cube
				if (hit.collider.gameObject.tag == "empty") 
				{
					//If there is a current line set
					if(current_line.line_renderer != null)
					{
						//If cube is not already hit AND if the distance between last position and new position is less than the set move distance
						if(hit.collider.gameObject.GetComponent<Puzzle_cube>().hit == false && 
							Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
							hit.collider.gameObject.transform.position) < move_distance)
						{
							//Set the cube hit variable to true
							hit.collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);

							//Add the hit cube to the array of boxes
							current_line.boxes[current_line.line_renderer.positionCount -1] = hit.collider.gameObject;

							//Make a new position for the line renderer and set it to the hix box position
							current_line.line_renderer.positionCount++;
							current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
						}
					}
				}

			}
			if(Input.GetMouseButtonUp(0))
			{
				//If cube is a finish cube
				if (hit.collider.gameObject.tag == "Finish") 
				{
					//If the distance between last position and new position is less than the set move distance
					if(Vector3.Distance(current_line.line_renderer.GetPosition(current_line.line_renderer.positionCount - 1),
						hit.collider.gameObject.transform.position) < move_distance)
					{
						//If cube is red
						if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && current_line.line_renderer == red_line)
						{
							SetFinish(hit.collider.gameObject);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && current_line.line_renderer == blue_line)
						{
							SetFinish(hit.collider.gameObject);
						}
						else if(hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.green && current_line.line_renderer == green_line)
						{
							SetFinish(hit.collider.gameObject);
						}
//							//Set the cube hit variable to true
//							hit.collider.gameObject.GetComponent<Puzzle_cube>().SetHit(true);
//
//							//Add the hit cube to the array of boxes
//							current_line.boxes[current_line.line_renderer.positionCount -1] = hit.collider.gameObject;
//
//							//Make a new position for the line renderer and set it to the hix box position
//							current_line.line_renderer.positionCount++;
//							current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
//
//							//Set line complete to true
//							current_line.line_complete = true;
						//}
					}

				}
				//If the line is not complete and there is a line set
				if (!current_line.line_complete && current_line.line_renderer != null) 
				{
					//Reset all line positions
					Reset();
				}

				//Unassign current line
				current_line.line_renderer = null;
				
			}
		}
	}

	void Reset()
	{

		//Reset hit variable for all boxes that were hit
		for(int i = 0;i<current_line.line_renderer.positionCount - 1 ;i++)
		{
			current_line.boxes[i].SendMessage("SetHit", false);

		}

		//remove current positions for the line
		current_line.line_renderer.positionCount = 1;
	}

	void SetStart(GameObject hit)
	{
		//If the line is not completed already
		if(!current_line.line_complete && current_line.line_renderer.positionCount < 2)
		{
			//Set the cube hit variable to true
			hit.GetComponent<Puzzle_cube>().SetHit(true);

			//Set the line renderer to the red one
			//current_line.line_renderer = line;

			//Add the hit cube to the array of boxes
			current_line.boxes[current_line.line_renderer.positionCount -1] = hit;

			//Set a new position for the line renderer to the hix box position
			current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.transform.position);
		}
		//This will reset the line if clicked on the start cube again when the line is completed
		else
		{
			//current_line.line_renderer = line;
			current_line.line_complete = false;
			hit.GetComponent<Puzzle_cube>().SetHit(true);
			Reset();
		}
	}

	void SetFinish(GameObject hit)
	{
		//Set the cube hit variable to true
		hit.GetComponent<Puzzle_cube>().SetHit(true);

		//Add the hit cube to the array of boxes
		current_line.boxes[current_line.line_renderer.positionCount -1] = hit;

		//Make a new position for the line renderer and set it to the hix box position
		current_line.line_renderer.positionCount++;
		current_line.line_renderer.SetPosition(current_line.line_renderer.positionCount -1, hit.transform.position);

		//Set line complete to true
		current_line.line_complete = true;
	}
}
