using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Line_renderer_puzzle : MonoBehaviour {

	public GameObject[] empty_boxes;
	public GameObject[] start_boxes;

	[System.Serializable]
	public struct lines_struct
	{
		public LineRenderer line_renderer;
		public bool line_complete;

	};

	int counter;
	int line_index;


	public lines_struct[] lines;
	public lines_struct red_Line;
	public lines_struct blue_Line;
	lines_struct current_line;

	float raycast_distance = 200.0f;

//
	//LineRenderer line_renderer2;
	public bool already_using = false;


	// Use this for initialization
	void Start () 
	{
		counter = 0;
		line_index = 0;
		//line_renderer = new LineRenderer[4]{new LineRenderer(),new LineRenderer() ,new LineRenderer(),new LineRenderer()};
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (line_index);
		if(Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log ("Reset");
			ResetCubes ();
		}

		// For interacting with objects ingame
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
		Debug.DrawRay (ray.origin, ray.direction);

		if (Physics.Raycast (ray.origin, ray.direction, out hit, raycast_distance)) 
		{
			if (Input.GetMouseButtonDown (0))
			{
				if (hit.collider.gameObject.tag == "start" ) 
				{
					if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && !red_Line.line_complete && !already_using) 
					{
						line_index = 0;
						Debug.Log ("REDD");

						already_using = true;
						current_line = red_Line;

						red_Line.line_renderer.SetPosition (red_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);

						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

					} 
					else if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && !blue_Line.line_complete && !already_using) 
					{ 
						Debug.Log ("BLUEEE!");
						line_index = 1;

						already_using = true;

						current_line = blue_Line;


						blue_Line.line_renderer.SetPosition (blue_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);


						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
					}

				}
			}

			if(Input.GetMouseButton(0))
			{
				if (hit.collider.gameObject.tag == "empty")
				{
					if (hit.collider.GetComponent<Puzzle_cube> ().hit == false && !current_line.line_complete) 
					{
						current_line.line_renderer.positionCount++;
					
						current_line.line_renderer.SetPosition (current_line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
						
						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

					} 
				}
				if (hit.collider.gameObject.tag == "start" && already_using)
				{
					/*if (hit.collider.GetComponent<Puzzle_cube> ().hit == false ) 
					{
						if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && !red_Line.line_complete && red_Line.line_renderer.positionCount > 1) 
						{
							already_using = true;

							red_Line.line_renderer.positionCount++;
							red_Line.line_renderer.SetPosition (red_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);

							hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

							if (red_Line.line_complete == false) {
								red_Line.line_complete = true;
							}
						}
						else if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && !blue_Line.line_complete && blue_Line.line_renderer.positionCount > 1) 
						{
							already_using = true;

							blue_Line.line_renderer.positionCount++;
							blue_Line.line_renderer.SetPosition (blue_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);

							hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

							if (blue_Line.line_complete == false) {
								blue_Line.line_complete = true;
							}
						}
					}*/
				}
			}
			if (Input.GetMouseButtonUp (0))
			{
				if (hit.collider.gameObject.tag == "Finish" ) 
				{
					if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red && !red_Line.line_complete && already_using) 
					{
						
						Debug.Log ("DONE REd");
						already_using = false;



						red_Line.line_renderer.positionCount++;
						red_Line.line_renderer.SetPosition (red_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);

						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

						if (red_Line.line_complete == false) {
							red_Line.line_complete = true;
						}

					} 
					if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.blue && !blue_Line.line_complete && already_using) 
					{ 
						Debug.Log ("Done BLUEEE!");
						already_using = false;

						blue_Line.line_renderer.positionCount++;
						blue_Line.line_renderer.SetPosition (blue_Line.line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);

						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);

						if (blue_Line.line_complete == false) {
							blue_Line.line_complete = true;
						}
					}

				}


				if (red_Line.line_complete) 
				{
					Debug.Log ("DONE");

				}
				else 
				{
					ResetCubes ();
					red_Line.line_renderer.positionCount = 1;
					already_using = false;

					counter = 0;
				
				}
//				if (blue_Line.line_complete) 
//				{
//
//					Debug.Log ("DONE");
//
//
////					line_index++;
////					//line_renderer[line_index] = Instantiate (line_renderer,line_renderer[0].transform) as LineRenderer;
////					blue_Line.line_renderer.positionCount = 1;
////					blue_Line.line_renderer.SetPosition (0, new Vector3 (0, 0, 0));
////					blue_Line.line_complete = false;
//
//				}
//				else if(already_using)
//				{
//					ResetCubes ();
//					blue_Line.line_renderer.positionCount = 1;
//					already_using = false;
//
//					counter = 0;
//
//				}
			}
		}
	}



	void OnMouseOver()
	{
		//box1.GetComponent<Renderer>().material.color -= new Color(0.1f, 0, 0) * Time.deltaTime;

	}
	void ResetCubes()
	{
		for (int i = 0; i < empty_boxes.Length; i++) {
			
			empty_boxes[i].gameObject.SendMessage ("Set_Hit", false);

			//start_boxes[i].gameObject.SendMessage ("Set_Hit", false);


		}
		for (int i = 0; i < start_boxes.Length; i++) {
			start_boxes[i].gameObject.SendMessage ("Set_Hit", false);

		}
	}
}
