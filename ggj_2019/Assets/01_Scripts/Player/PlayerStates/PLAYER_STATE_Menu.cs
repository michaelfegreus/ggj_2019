using System;
using UnityEngine;

public class PLAYER_STATE_Menu : IState {

	private GameObject playerObject;

	// Constructor:
	public PLAYER_STATE_Menu(GameObject player){
		playerObject = player;
	}

	public void Enter(){
		// Disable the scripts that the player should have when they are free to move around.
		//playerObject.GetComponent<PLAYER_movement_directional_2d> ().enabled = false; --- Keep movement script running so it doesn't awkwardly jerk the character to a stop in disabling the script.
		playerObject.GetComponent<PLAYER_interaction> ().enabled = false;
	}

	public void Execute(){
	}

	public void Exit(){

	}

}
