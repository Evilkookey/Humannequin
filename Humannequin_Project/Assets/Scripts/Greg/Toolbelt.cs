using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbelt : MonoBehaviour 
{
	struct tool
	{
		public GameObject tool_object;
		public bool is_in_belt;
		public bool is_acquired;
	}

	// The tools
	static tool wrench, torch, screwdriver, pliers;

	// An array of tools
	tool[] tools = new tool[4] {wrench, torch, screwdriver, pliers}; 

	/*// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}*/

	bool Take_Tool (string tool_name)
	{
		int tool_pointer;

		// Make tool name lower case
		tool_name = tool_name.ToLower();

		switch (tool_name)
		{
		case "wrench":
			tool_pointer = 0;
			break;
		case "torch":
			tool_pointer = 1;
			break;
		case "screwdriver":
			tool_pointer = 2;
			break;
		case "pliers":
			tool_pointer = 3;
			break;
		default:
			return false;
		}

		// Check if tool is active and aquired
		if (tools[tool_pointer].is_acquired && tools[tool_pointer].is_in_belt)
		{
			// Deactivate tool
			tools[tool_pointer].tool_object.SetActive(false);

			// Take tool out of belt
			tools[tool_pointer].is_in_belt = false;

			return true;
		}
		else
		{
			return false;
		}
	}

	bool Return_Tool (string tool_name)
	{

	}

	void New_Tool(string tool_name)
	{

	}
}
