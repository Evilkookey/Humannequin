// KILL_SELF.CS
// NATALIE BAKER-HALL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS USED FOR OBJECTS WITH ANIMATIONS 
// ON IT THAT WILL BE DESTROYED AT END OF ANIMATION

public class Kill_Self : MonoBehaviour 
{
	void Destroy_This ()
	{
		Destroy (this.GetComponent<Transform> ().parent.gameObject);
	}
}
