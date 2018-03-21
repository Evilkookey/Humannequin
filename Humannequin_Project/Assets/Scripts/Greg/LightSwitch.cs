// LIGHTSWITCH.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public GameObject room_light;	// The light in the room

	// Activate is called when the player uses the light switch
	void Activate()
	{
        print("bello");
        // Turn the light on
        room_light.SendMessage("Light_On");
    }
}
