using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

	//Handles setting animations as well as hitboxes (based on bools from PlayerMovement)

	public float destanceTime = 2f;
	public GameObject[] hitboxManager;

	private PlayerControls playerControls;
	private PlayerFacing playerFacing;
	private PlayerMovement playerMovement;
	private Animator anim;

	int hitboxLength;

	//0 = standingL
	//1 = standingR
	//2 = stance
	//3 = attack1L
	//4 = attack1R
	//5 = RunningL
	//6 = RunningR
	//7 = Dodge
	//8 = attack2L

	float stance_timeStamp;
	bool stancing = false;
	void Start(){
		playerFacing = GetComponent<PlayerFacing>();
		playerControls = GetComponent<PlayerControls>();
		playerMovement = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator> ();
		hitboxLength = hitboxManager.Length;
		stance_timeStamp = Time.time;
	}

	void Setter(string input_bool){
		bool idleL_set = (input_bool == "idleL") ? true : false;
		bool idleR_set = (input_bool == "idleR") ? true : false;
		bool stanceL_set = (input_bool == "stanceL") ? true : false;
		bool stanceR_set = (input_bool == "stanceR") ? true : false;
		bool attack1L_set = (input_bool == "attack1L") ? true : false;
		bool attack1R_set = (input_bool == "attack1R") ? true : false;
		bool run_set = (input_bool == "run_bool") ? true : false;
		bool runL_set = (input_bool == "runL_bool") ? true : false;
		bool dodgeL_set = (input_bool == "dodgeL") ? true : false;
		bool dodgeR_set = (input_bool == "dodgeR") ? true : false;
		bool attack2L_set = (input_bool == "attack2L") ? true : false;
		bool attack2R_set = (input_bool == "attack2R") ? true : false;
		bool attack3L_set = (input_bool == "attack3L") ? true : false;
		bool attack3R_set = (input_bool == "attack3R") ? true : false;

		anim.SetBool ("idleL", idleL_set);
		anim.SetBool ("idleR", idleR_set);
		anim.SetBool("stanceL",stanceL_set);
		anim.SetBool ("stanceR",stanceR_set);
		anim.SetBool("attack1L",attack1L_set);
		anim.SetBool("attack1R",attack1R_set);
		anim.SetBool("run_bool",run_set);
		anim.SetBool("runL_bool",runL_set);
		anim.SetBool("dodgeL",dodgeL_set);
		anim.SetBool("dodgeR",dodgeR_set);
		anim.SetBool("attack2L",attack2L_set);
		anim.SetBool("attack2R",attack2R_set);
		anim.SetBool("attack3L",attack3L_set);
		anim.SetBool("attack3R",attack3R_set);
	}

	void HitboxSetter(int input){
		for (int i = 0; i<hitboxLength; i++){
			if (i == input){
				hitboxManager[i].SetActive(true);	
			} else {
				hitboxManager[i].SetActive(false);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= stance_timeStamp && stancing == true){
			stancing = false;
		}
		if (stancing && (Input.GetKey(playerControls.moveLeft) || Input.GetKey(playerControls.moveRight))){
			stance_timeStamp = Time.time + destanceTime;
		}
		if (playerFacing.dir){	//right
			if (playerMovement.attack1ing){
				Setter("attack1R");
				HitboxSetter(4);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.attack2ing){
				Setter("attack2R");
				HitboxSetter(9);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.attack3ing){
				Setter("attack3R");
				HitboxSetter(11);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.dodging){Setter("dodgeR");HitboxSetter(7);
			} else if (playerMovement.running){Setter("run_bool");HitboxSetter(6);
			} else if (stancing){Setter("stanceR");HitboxSetter(2);
			} else { Setter("idleR");HitboxSetter(1);}
		} else { //left
			if (playerMovement.attack1ing){
				Setter("attack1L");
				HitboxSetter(3);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.attack2ing){
				Setter("attack2L");
				HitboxSetter(8);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.attack3ing){
				Setter("attack3L");
				HitboxSetter(10);
				stancing = true;
				stance_timeStamp = Time.time + destanceTime;
			} else if (playerMovement.dodging){Setter("dodgeL");HitboxSetter(7);
			} else if (playerMovement.running){Setter("runL_bool");HitboxSetter(5);
			} else if (stancing){Setter("stanceL");HitboxSetter(2);
			} else { Setter("idleL");HitboxSetter(0);}
		}
	}
}
