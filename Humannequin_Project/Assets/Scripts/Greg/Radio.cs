// RADIO.CS
// GREG BALBIRNIE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour {

   public AudioSource radio_audio;    // The sound coming from the radio

	// Use this for initialization
	void Start ()
    {
        // Find the audiosource
       // radio_audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Activate is called when the player uses the radio
    void Activate (string tool)
    {
        // Check if there is no active tool
        if (tool.ToLower() == "none")
        {
            //Turn off the radio
            radio_audio.Stop();
        }
    }
}
