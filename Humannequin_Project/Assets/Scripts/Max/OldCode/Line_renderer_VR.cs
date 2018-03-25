using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_renderer_VR : MonoBehaviour {

	public GameObject[] empty_boxes;
	public GameObject[] start_boxes;

	public Valve.VR.EVRButtonId trigger_button = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public Valve.VR.EVRButtonId grip_button = Valve.VR.EVRButtonId.k_EButton_Grip;
	public Valve.VR.EVRButtonId touch_pad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

	//define the controller object
	public SteamVR_TrackedObject tracked_object;
	public SteamVR_Controller.Device device;

	[System.Serializable]
	public struct lines_struct
	{
		public LineRenderer line_renderer;
		public bool line_complete;

	};

	int counter;
	int line_index;
//	bool stop = false;

	//public LineRenderer[] line_renderer;
	public lines_struct[] lines;
	//float raycast_distance = 200.0f;
	//bool line_complete = false;
//
	//LineRenderer line_renderer2;



	// Use this for initialization
	void Start () 
	{
		counter = 0;
		line_index = 0;
		tracked_object = GetComponent<SteamVR_TrackedObject>();
		//line_renderer = new LineRenderer[4]{new LineRenderer(),new LineRenderer() ,new LineRenderer(),new LineRenderer()};
	}
	
	// Update is called once per frame
	void Update () 
	{
		device = SteamVR_Controller.Input((int)tracked_object.index);
		//Debug.Log (line_index);

		// For interacting with objects ingame
		//RaycastHit hit;
		//Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));
		//Debug.DrawRay (ray.origin, ray.direction);

		//if (Physics.Raycast (ray.origin, ray.direction, out hit, raycast_distance)) 
		//{

//			if (Input.GetMouseButtonDown (0))
//			{
//				if (hit.collider.gameObject.tag == "start" ) 
//				{
//
//					if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red) {
//						line_index = 0;
//
//						lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
//						//line_renderer.SetPosition (counter+1, box3.transform.position);
//
//						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//					} else {
//						Debug.Log ("BLUEEE!");
//						line_index = 1;
//
//						lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
//						//line_renderer.SetPosition (counter+1, box3.transform.position);
//
//						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//					}
//				}
//			}
//
//			if(Input.GetMouseButton(0))
//			{
//				if (hit.collider.gameObject.tag == "empty")
//				{
//					if (hit.collider.GetComponent<Puzzle_cube> ().hit == false) {
//	
//						lines[line_index].line_renderer.positionCount++;
//
//						lines[line_index].line_renderer.SetPosition(lines[line_index].line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
//						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//					} 
//				}
//				if (hit.collider.gameObject.tag == "start")
//				{
//					if (hit.collider.GetComponent<Puzzle_cube> ().hit == false) {
//						lines[line_index].line_renderer.positionCount++;
//						lines[line_index].line_renderer.SetPosition (lines[line_index].line_renderer.positionCount -1, hit.collider.gameObject.transform.position);
//						hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//						if (lines[line_index].line_complete == false) {
//							lines[line_index].line_complete = true;
//						}
//
//					}
//				}
//			}
//			if (Input.GetMouseButtonUp (0))
//			{
//				if (lines[line_index].line_complete) 
//				{
//
//					Debug.Log ("DONE");
////					line_index++;
////					//line_renderer[line_index] = Instantiate (line_renderer,line_renderer[0].transform) as LineRenderer;
////					lines[line_index].line_renderer.positionCount = 1;
////					lines[line_index].line_renderer.SetPosition (0, new Vector3 (0, 0, 0));
////					lines[line_index].line_complete = false;
//
//				}
//				else
//				{
//					ResetCubes ();
//					lines[line_index].line_renderer.positionCount = 1;
//					counter = 0;
//				
//				}
//			}
		//}

		device = SteamVR_Controller.Input((int)tracked_object.index);
		if (device.GetPressUp(trigger_button))
		{
			if (lines[line_index].line_complete) 
			{

				Debug.Log ("DONE");
				//					line_index++;
				//					//line_renderer[line_index] = Instantiate (line_renderer,line_renderer[0].transform) as LineRenderer;
				//					lines[line_index].line_renderer.positionCount = 1;
				//					lines[line_index].line_renderer.SetPosition (0, new Vector3 (0, 0, 0));
				//					lines[line_index].line_complete = false;

			}
			else
			{
				Debug.Log("incorrect");
				ResetCubes ();
				lines[line_index].line_renderer.positionCount = 1;
				counter = 0;

			}
		}
	}



	void OnMouseOver()
	{
		//box1.GetComponent<Renderer>().material.color -= new Color(0.1f, 0, 0) * Time.deltaTime;

	}
	void ResetCubes()
	{
		for (int i = 0; i < empty_boxes.Length; i++) 
		{
			empty_boxes[i].gameObject.SendMessage ("Set_Hit", false);
			//start_boxes[i].gameObject.SendMessage ("Set_Hit", false);

		}
		for (int i = 0; i < start_boxes.Length; i++) 
		{
			start_boxes[i].gameObject.SendMessage ("Set_Hit", false);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "start")
		{
			device = SteamVR_Controller.Input((int)tracked_object.index);
			if (device.GetPressDown(trigger_button))
			{
				if (other.gameObject.GetComponent<Renderer> ().material.color == Color.red) 
				{
					Debug.Log ("RED!");
					line_index = 0;

					lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, other.gameObject.transform.position);
					//line_renderer.SetPosition (counter+1, box3.transform.position);

					other.GetComponent<Puzzle_Cube> ().Set_Hit (true);
				} 
				else if(other.gameObject.GetComponent<Renderer> ().material.color == Color.blue)
				{
					Debug.Log ("BLUEEE!");
					line_index = 1;

					lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, other.gameObject.transform.position);
					//line_renderer.SetPosition (counter+1, box3.transform.position);

					other.GetComponent<Puzzle_Cube> ().Set_Hit (true);
				}
			}
			if (device.GetPress (trigger_button))
			{
				if (other.GetComponent<Puzzle_Cube> ().hit == false) 
				{
					lines [line_index].line_renderer.positionCount++;
					lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, other.gameObject.transform.position);
					other.GetComponent<Puzzle_Cube> ().Set_Hit (true);

					if (lines [line_index].line_complete == false) 
					{
						lines [line_index].line_complete = true;
					}
				}
			}
		}
		if (other.gameObject.tag == "empty")
		{
			if (device.GetPress(trigger_button))
			{
				if (other.GetComponent<Puzzle_Cube> ().hit == false) {
					Debug.Log ("EMPTY");

					lines[line_index].line_renderer.positionCount++;

					lines[line_index].line_renderer.SetPosition(lines[line_index].line_renderer.positionCount - 1, other.gameObject.transform.position);
					other.GetComponent<Puzzle_Cube> ().Set_Hit (true);
				} 
			}
		}

	}

//	void DoTheThing(string type)
//	{
//		if (type == "start") 
//		{
//			if (hit.collider.gameObject.GetComponent<Renderer> ().material.color == Color.red) {
//				line_index = 0;
//
//				lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
//				//line_renderer.SetPosition (counter+1, box3.transform.position);
//
//				hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//			} else {
//				Debug.Log ("BLUEEE!");
//				line_index = 1;
//
//				lines [line_index].line_renderer.SetPosition (lines [line_index].line_renderer.positionCount - 1, hit.collider.gameObject.transform.position);
//				//line_renderer.SetPosition (counter+1, box3.transform.position);
//
//				hit.collider.GetComponent<Puzzle_cube> ().Set_Hit (true);
//			}
//
//		}
//	}
//			



	
}
