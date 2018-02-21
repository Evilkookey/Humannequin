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

	// Use this for initialization
	void Start () 
	{
		// Find the tools
		wrench.tool_object = transform.Find("Wrench").gameObject;
		torch.tool_object = transform.Find("Torch").gameObject;
		screwdriver.tool_object = transform.Find("Screwdriver").gameObject;
		pliers.tool_object = transform.Find("Pliers").gameObject;

		for (int i = 0; i < tools.Length; i++)
		{
			// Set all to not aquired
			tools[i].is_acquired = false;

			// Set all to out of belt
			tools[i].is_in_belt = false;

			// Deactive all tools
			tools[i].tool_object.SetActive(false);
		}
	}
	
	/*// Update is called once per frame
	void Update () 
	{
		
	}*/

	int Find_Tool_Pointer (string tool_name)
	{
		// Make tool name lower case
		tool_name = tool_name.ToLower();

		switch (tool_name)
		{
		case "wrench":
			return 0;
		case "torch":
			return 1;
		case "screwdriver":
			return 2;
		case "pliers":
			return 3;
		default:
			print ("tool pointer error");
			return 0;
		}
	}

	bool Take_Tool (string tool_name)
	{
		// Get the array position of the tool
		int tool_pointer = Find_Tool_Pointer(tool_name);

		// Check if tool is in the belt and aquired
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
		// Get the array position of the tool
		int tool_pointer = Find_Tool_Pointer(tool_name);

		// Check if tool is not in the belt and aquired
		if (tools[tool_pointer].is_acquired && !tools[tool_pointer].is_in_belt)
		{
			// Reactivate tool
			tools[tool_pointer].tool_object.SetActive(true);

			// Put tool back in belt belt
			tools[tool_pointer].is_in_belt = true;

			return true;
		}
		else
		{
			return false;
		}
	}

	bool New_Tool(string tool_name)
	{
		// Get the array position of the tool
		int tool_pointer = Find_Tool_Pointer(tool_name);

		// Check if tool is not active yet
		if (!tools[tool_pointer].is_acquired)
		{
			// Set tool to acquired
			tools[tool_pointer].is_acquired = true;

			// Activate tool
			tools[tool_pointer].tool_object.SetActive(true);

			// Put tool in belt belt
			tools[tool_pointer].is_in_belt = true;

			return true;
		}
		else
		{
			return false;
		}
	}
}