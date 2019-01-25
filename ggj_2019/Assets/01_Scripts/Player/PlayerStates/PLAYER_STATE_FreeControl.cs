using System;
using UnityEngine;
using System.Collections.Generic;

public class PLAYER_STATE_FreeControl : IState {

	private GameObject playerObject;

	PLAYER_movement_directional_2d movementScript;
	PLAYER_interaction interactionScript;

	// Constructor:
	public PLAYER_STATE_FreeControl(GameObject player){
		playerObject = player;
		movementScript = playerObject.GetComponent<PLAYER_movement_directional_2d>();
		interactionScript = playerObject.GetComponent<PLAYER_interaction>();
	}

	public void Enter(){
		Debug.Log ("Player entered free move state");
		// Now go ahead and enable the scripts that the player should have when they are free to move around.
		movementScript.enabled = true;
		interactionScript.enabled = true;
	}

	public void Execute(){
		// This serves as the current hub for controlling the player character in the game's action.

		// Interaction button check:
		if (Input.GetKeyDown (GAME_input_manager.Instance.actionButton1) || Input.GetKeyDown (GAME_input_manager.Instance.actionButton2)) {
			interactionScript.InteractInput ();
		}
		// Item Use button check:
		if (Input.GetKeyDown (GAME_input_manager.Instance.itemUseButton1) || Input.GetKeyDown (GAME_input_manager.Instance.itemUseButton2)) {
			interactionScript.UseItemInput (GAME_inventory_manager.Instance.equippedItem);
		}

		// Set player movement:
		movementScript.SetMovementInput(Input.GetAxis ("HorizontalAnalog"), Input.GetAxis ("VerticalAnalog"), Input.GetAxisRaw ("HorizontalDigital"), Input.GetAxisRaw ("VerticalDigital"));

		// Run button check:
		// Might want to create some sort of dashing sub-state later to unify this. This will get animation, game mechanics, etc, on the same page.
		// (Sub-states might also be used for things like controlling the character while she's tired, walking with umbrella, etc.)
		if (Input.GetKey(GAME_input_manager.Instance.dashButton1) || Input.GetKey(GAME_input_manager.Instance.dashButton2)) {
			movementScript.SetDashInput (true);
		} else {
			movementScript.SetDashInput (false);
		}

	}

	public void Exit(){		
		// Reset analog stick input when exiting FreeControl state.
		movementScript.SetMovementInput(0f, 0f, 0f, 0f);
	}

}