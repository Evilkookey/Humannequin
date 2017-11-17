using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public Light room_light;	//the light in the room

	//activate is called when the player uses the light switch
	void Activate()
	{
		//turn the light on/off
		room_light.enabled = !room_light.enabled;
	}
}
