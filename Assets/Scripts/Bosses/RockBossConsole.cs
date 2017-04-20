using UnityEngine;
using System.Collections;

public class RockBossConsole : MonoBehaviour {

	public bool attacking;
	public GameObject leftarm;
	public GameObject rightarm;

	// Use this for initialization
	void Start () {
		if (attacking){
			leftarm.active = true;
			rightarm.active = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (attacking){
			leftarm.active = true;
			rightarm.active = true;
		} else {
			leftarm.active = false;
			rightarm.active = false;
		}
	}
}
