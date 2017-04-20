using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public int player_health = 3;
	public float hit_cooldown = 0.7f;

	float hit_timestamp = 0f;

	void OnCollisionEnter2D(Collision2D coll) {
		if (hit_timestamp < Time.time){
			if (coll.gameObject.tag == "Enemies"){
				player_health -= 1;
				hit_timestamp = Time.time + hit_cooldown;
				DeathCheck();
			}
		}
	}

	void DeathCheck(){
		if (player_health <= 0){
			Debug.Log("Player died.");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
