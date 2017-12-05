// LIGHTSWITCH.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public Light room_light;	// The light in the room

	// Activate is called when the player uses the light switch
	void Activate()
	{
		// Turn the light on/off
		room_light.enabled = !room_light.enabled;
	}
}
