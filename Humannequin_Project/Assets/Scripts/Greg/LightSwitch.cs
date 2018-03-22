// LIGHTSWITCH.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public List<GameObject> room_lights = new List<GameObject>();	// The light in the room

	// Activate is called when the player uses the light switch
	void Activate()
	{
        print("bello");

        foreach (GameObject g in room_lights)
        {
            // Turn the lights on
            g.SendMessage("Light_On");
        }
    }
}
