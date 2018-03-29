// MANNEQUIN_STALK.CS
// MAX MILLS

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Mannequin_Stalk : MonoBehaviour {

	//[SerializeField]
	public enum Enemy_Type
	{
		//TELEPORT,
		FOLLOW,
		TORCH,
		TEST
	};
	public Enemy_Type enemy;

	public bool USE_FPSCONTROLLER, do_not_kill;
	public Transform player;			// Location of the Player
	public float min_dist;				// Distance which the enemy stops at from the player
	//public float move_speed;			// Speed at which the enemy walks at
	public GameObject enemyBody;		// The enemy gameobject to move (in case this script isnt already on the object)
	//public bool follow_enemy_type;		// Used to select which type of enemy to use
	public Transform head;				// Head object used to rotate

	public float rotate_speed = 5.0f;

	NavMeshAgent agent;					// Nav mesh agent stored on the enemy
	Vector3 target_postition;			// Position to turn towards
	Vector3 target_dir;

	//float distance;
	float rotationY;					// Y rotation variable
	public float tilt = 0.3f;			// Used offset the enemy's target y position to tilt the enemy's head  

	bool head_not_visible = false;
	bool can_move = false;
	public bool in_stayaway_zone = false;
	public GameObject stayaway_zone;

	[Header("Drag In")]
	// Used to determine if the player is in VR or using the FP controller
	public GameObject CameraRigPlayer;
	public GameObject FPSController; 
	public List<GameObject> room_lights = new List<GameObject>();
	public GameObject main_room_light;

    AudioSource death_sound;


    // Use this for initialization
    void Start () 
	{
		// Init variables
		agent = GetComponent<NavMeshAgent> ();

		//Find both player objects
		//CameraRigPlayer = GameObject.Find("[CameraRig]");
		//FPSController = GameObject.Find("FPSController");

		head = gameObject.transform.Find("head").gameObject.transform;

        death_sound = gameObject.GetComponent<AudioSource>();


    }

	// Update is called once per frame
	void Update () 
	{
		if (Game_State_Controller.current_state != Game_State_Controller.Game_States.PAUSED) {

			//Switch who the player is for debugging
			if (USE_FPSCONTROLLER) {
				player = FPSController.transform;

			} else {
				player = CameraRigPlayer.transform;
			}

			in_stayaway_zone = stayaway_zone.GetComponent<Stayaway_Zone> ().in_zone;


			// For checking whether the head object is visisble to the player
			head_not_visible = head.GetComponent<Head_Visible> ().Get_Not_Visible ();


			// If using the follow enemy
			if (enemy == Enemy_Type.FOLLOW) 
			{
				// If the gameobject is not visible to the renderer and the distance between the player and enemy is less than min_dist
				if (!this.GetComponent<Renderer> ().isVisible && head_not_visible) // || light.intensity == 0.0f) 
				{
					
					//gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

					// Enable the NavMesh Agent
					agent.enabled = true;

					// Turn head towards player
					Head_Turn ();

					// If player is not in the mannequin stay away zone
					if(!in_stayaway_zone)
					{						
						// Checks distance between mannequin and player
						if (Vector3.Distance (transform.position, player.position) >= min_dist) 
						{
							// Agent will advance to players position 
							agent.SetDestination (player.position);

							// Used to speed up the rotation of the enemy
							Vector3 lookrotation = target_postition - transform.position;
							transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (lookrotation), Time.deltaTime * rotate_speed);
					
							// Set animation speed to 1
							//GetComponent<Animator> ().speed = 1;
						} 
						else if (!do_not_kill) 
						{
							// Kills the player if it gets close enough to the player
							Kill_Player ();

						}
					}

				} 
				else 
				{
					// Stop enemy from moving	
					transform.position = transform.position;
					transform.rotation = transform.rotation;

					// Disables the navmesh agent when you see the mannequin
					agent.enabled = false;


					//gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

					// Freeze animations 
					//GetComponent<Animator> ().speed = 0;
				}

			} 
			else if (enemy == Enemy_Type.TEST) 
			{
				// Testing code - enemy will just follow the player
				if (Vector3.Distance (transform.position, player.position) >= min_dist) 
				{
					//transform.position += transform.forward * move_speed * Time.deltaTime;

					target_postition = new Vector3 (player.position.x, 
					//this.transform.position.y, 
						head.position.y,
						player.position.z);
				
					agent.enabled = true;

					agent.SetDestination (player.position);

					head.LookAt (target_postition);
					//GetComponent<Animator> ().speed = 1;

				} else 
				{
					//agent.SetDestination (transform.position);			

					transform.position = transform.position;
					transform.rotation = transform.rotation;
					//GetComponent<Animator> ().speed = 0;
				}
			} 
			else if (enemy == Enemy_Type.TORCH) 
			{
				// If player is outwith the minimum distance from the mannequin, and the main light is off
				if (can_move && Vector3.Distance (transform.position, player.position) >= min_dist && main_room_light.GetComponent<Light_Controller>().is_off == true) 
				{
					// Enable the NavMesh Agent
					agent.enabled = true;

					// Turn head towards player
					Head_Turn ();

			
					// Agent will advance to players position 
					agent.SetDestination (player.position);

					// Used to speed up the rotation of the enemy
					Vector3 lookrotation = target_postition - transform.position;
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (lookrotation), Time.deltaTime * rotate_speed);

					//transform.position += transform.forward * move_speed * Time.deltaTime;
				} 
				else 
				{
					// Disables the navmesh agent when you see the mannequin
					agent.enabled = false;
				}
			}
		}
	}

	// For rotating the mannequins head towards the player
	void Head_Turn()
	{
		// Set target position to player position but using head y position + tilt 
		target_postition = new Vector3 (player.position.x, 
			//this.transform.position.y, 
			head.position.y - tilt,
			player.position.z);


		// Calculate vector
		target_dir = target_postition - transform.position;

		// Calculate difference in head rotation angle
		float difference = Vector3.Angle (target_dir, transform.forward);

		// Look at target if head position is less than 90 degrees
		if (difference < 270.0f && difference < 90.0f)
		{
			//print("tilt_me_with_your_rhythm_stick");
			head.LookAt (target_postition);
		}

	}

	// For ending the game
	void Kill_Player()
	{
		//Disables nav mesh agent
		agent.enabled = false;

		Debug.Log("Player killed");

		// Turn off the lights in the room
		foreach(GameObject g in room_lights)
		{
			g.GetComponent<Light_Controller> ().is_off = true;
		}

		// Start jumpscare event
		StartCoroutine (Jumpscare ());
       
		// Dont kill the enemy again
        do_not_kill = true;

        // Change game state to LOSE
        Game_State_Controller.Lose_Game();
	}

	IEnumerator Jumpscare()
	{
		// Waits until lights have completly gone off before moving mannequin
		yield return new WaitForSeconds (0.5f);

		// Moves mannequin in front of the player
		this.gameObject.transform.position = /*new Vector3(player.position.x, player.position.y, player.position.z + 1.0f)*/ player.position + (player.transform.forward * 0.75f);

		tilt = 0.1f;

		// Turn head towards player
		target_postition = new Vector3 (player.position.x, 
			this.transform.position.y,
			player.position.z);

		// Turns manneuqin body
		gameObject.transform.LookAt(target_postition);

		// Set target position to player position but using head y position + tilt 
		target_postition = new Vector3 (player.position.x, 
			//this.transform.position.y, 
			head.position.y - tilt,
			player.position.z);

		head.LookAt (target_postition);

		// Delay
		yield return new WaitForSeconds (2.0f);

		// Lights flicker back on
		foreach(GameObject g in room_lights)
		{
			g.GetComponent<Light_Controller> ().Light_Flicker_On ();
		
		}

		// Play jumpscare sound
		death_sound.Play();

	}

	void OnCollisionEnter(Collision other)
	{
		// Test code
		// If two enemies have collided
		if (other.gameObject.tag == "Enemy") 
		{
			//transform.position = transform.position;
			Debug.Log ("Collided with other enemy");
		}

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Light") 
		{
			can_move = false;
			//Debug.Log ("HIT");
		}

	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Light") 
		{
			can_move = true;
		}
	}

}

