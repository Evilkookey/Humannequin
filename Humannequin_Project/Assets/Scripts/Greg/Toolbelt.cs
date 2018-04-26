using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbelt : MonoBehaviour 
{
	[System.Serializable]
	public struct tool
	{
		public GameObject tool_object;
        public GameObject ghost_tool;
		public bool is_in_belt;
		public bool is_acquired;
	}

	// The tools
	public static tool wrench, screwdriver, torch, pliers;

	// An array of tools
	public tool[] tools = new tool[4] {wrench, screwdriver, torch, pliers}; 

	// Turn all tools on for testing
	public bool is_testing = false;

	// The temporary slot transform
	//public GameObject temp_slot;

	// The object in the temporary slot
	//public GameObject held_object;

	// Use this for initialization
	void Start () 
	{
		if (is_testing)
		{
			for (int i = 0; i < tools.Length; i++)
			{
				// Set all to aquired
				tools[i].is_acquired = true;

				// Set all to in belt
				tools[i].is_in_belt = true;

				// Active all tools
				tools[i].tool_object.SetActive(true);
                tools[i].ghost_tool.SetActive(false);
            }
		}
		else
		{
			for (int i = 0; i < tools.Length; i++)
			{
				// Set all to not aquired
				tools[i].is_acquired = false;

				// Set all to out of belt
				tools[i].is_in_belt = false;

				// Deactive all tools
				tools[i].tool_object.SetActive(false);
                tools[i].ghost_tool.SetActive(false);
            }
		}

        // Find temp slot
      //  temp_slot = transform.Find("Temp_Slot").gameObject;
	}

	int Find_Tool_Pointer (string tool_name)
	{
		// Make tool name lower case
		tool_name = tool_name.ToLower();

		switch (tool_name)
		{
		case "wrench":
			return 0;
		case "screwdriver":
			return 1;
		case "torch":
			return 2;
		case "pliers":
			return 3;
		default:
			print ("tool pointer error");
			return 0;
		}
	}

	public bool Take_Tool (string tool_name)
	{
        print(tool_name);


		// Get the array position of the tool
		int tool_pointer = Find_Tool_Pointer(tool_name);

		// Check if tool is in the belt and aquired
		if (tools[tool_pointer].is_acquired && tools[tool_pointer].is_in_belt)
		{
			// Deactivate tool
			tools[tool_pointer].tool_object.SetActive(false);
            tools[tool_pointer].ghost_tool.SetActive(true);

            // Take tool out of belt
            tools[tool_pointer].is_in_belt = false;

            // Play sound
            AudioSource[] audio = gameObject.GetComponents<AudioSource>();
            int rand_int = Random.Range(0, audio.Length);
            audio[rand_int].Play();

			return true;
		}
		else
		{
			return false;
		}
	}

	public bool Return_Tool (string tool_name)
	{
		// Get the array position of the tool
		int tool_pointer = Find_Tool_Pointer(tool_name);

		// Check if tool is not in the belt and aquired
		if (tools[tool_pointer].is_acquired && !tools[tool_pointer].is_in_belt)
		{
			// Reactivate tool
			tools[tool_pointer].tool_object.SetActive(true);
            tools[tool_pointer].ghost_tool.SetActive(false);

            // Put tool back in belt belt
            tools[tool_pointer].is_in_belt = true;

            // Play sound
            AudioSource[] audio = gameObject.GetComponents<AudioSource>();
            int rand_int = Random.Range(0, audio.Length);
            audio[rand_int].Play();

            return true;
		}
		else
		{
			return false;
		}
	}

	public bool New_Tool(string tool_name)
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

			// Put tool in belt
			tools[tool_pointer].is_in_belt = true;

            // Play sound
            AudioSource[] audio = gameObject.GetComponents<AudioSource>();
            int rand_int = Random.Range(0, audio.Length);
            audio[rand_int].Play();

            return true;
		}
		else
		{
			return false;
		}
	}

  /*  public bool Put_In_Temp(GameObject new_object)
    {
        // If there is no held object
        if (!held_object)
        {
            // Set the object to the one in the belt
            held_object = new_object;

            // set position of the object
            held_object.transform.position = temp_slot.transform.position;

            // Hold with a joint
            FixedJoint fx = temp_slot.AddComponent<FixedJoint>();
            fx.connectedBody = held_object.GetComponent<Rigidbody>();
            fx.breakForce = Mathf.Infinity;
            fx.breakTorque = Mathf.Infinity;

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Take_Out_Temp()
    {
        // If there is no held object
        if (held_object)
        {
            // Set the object to the one in the belt
            held_object = null;

            return true;
        }
        else
        {
            return false;
        }
    }*/
}