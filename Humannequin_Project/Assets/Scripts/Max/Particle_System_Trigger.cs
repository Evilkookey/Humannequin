using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_System_Trigger : MonoBehaviour {

    public ParticleSystem particle_system;
    
    public bool played = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if colliding with player, if so then move player object to next scene
        if (other.gameObject.name == "[CameraRig]" || other.gameObject.name == "FPSController")
        {
            if (!played)
            {
                particle_system.Play();
                //gameObject.GetComponent<AudioSource>().Play();

                played = true;
            }
        }
    }

    
}
