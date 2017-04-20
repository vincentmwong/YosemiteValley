using UnityEngine;
using System.Collections;

public class PlayerFacing : MonoBehaviour {

	private PlayerMovement playerMovement;
	private PlayerControls controls;
	[HideInInspector] public bool dir = true; 			//Boolean for facing direction. True = Right, False = Left

	void Start(){
		controls = GetComponent<PlayerControls> ();
		playerMovement = GetComponent<PlayerMovement>();
	}

	// Update is called once per frame
	void Update () {
		if (!playerMovement.attack1ing && !playerMovement.attack2ing && !playerMovement.attack3ing){
			if (Input.GetKey (controls.moveLeft)) {
				dir = false;
			} else if (Input.GetKey (controls.moveRight)) {
				dir = true;
			}
		}
	}
}
