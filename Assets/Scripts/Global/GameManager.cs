using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
//
// The purpose of the gamemanager script will be to handle the game states of the player. 
// With future features, the gamemanager script will split off its player responsibilities to a playermanager script,
// 		and instead take the responsibilities of the simulational elements for the entire game. 
//
	public static GameManager instance = null;
	[HideInInspector] public BoardManager boardManager;

	private bool doingSetup = false;

	void Awake(){
		if(instance = null)
			instance = this;
		DontDestroyOnLoad (gameObject);
		boardManager = GetComponent<BoardManager>();
		InitGame();
	}

	void InitGame(){
		doingSetup = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
