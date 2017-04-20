using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health = 5;
	public float cooldown_hit = 2;

	float timeStamp;
	bool can_be_hit = true;

	void DealDamage(){
		if (can_be_hit){
			health -= 1;
			can_be_hit = false;
			timeStamp = Time.time + cooldown_hit;
		}
	}

	void Start(){
		timeStamp = Time.time;
	}

	void Update(){
		if (Time.time >= timeStamp){
			can_be_hit = true;
		}
	}
}
