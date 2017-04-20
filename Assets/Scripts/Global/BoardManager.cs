using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
	//
	// The purpose of the boardmanager script will be to handle actions on the current board
	//
	public List<GameObject> list_of_cities = new List<GameObject>();
	[HideInInspector] public GameManager gameManager;

/*===================================================
	DEFAULT ACTIONS
===================================================*/

	// Use this for initialization
	void Awake () {
		gameManager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
