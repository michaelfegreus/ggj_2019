using System;
using UnityEngine;

public class GAME_STATE_Free : IState {

	private GameObject gameManagerObject;
	GAME_inventory_manager inventoryScript;


	// Constructor:
	public GAME_STATE_Free(GameObject gameManager){
		gameManagerObject = gameManager;
		inventoryScript = gameManagerObject.GetComponent<GAME_inventory_manager> ();
	}

	public void Enter(){
		PLAYER_manager.Instance.EnterMenuState ();
	}

	public void Execute(){
		// Control hub for in-game functions that do not involve direct control over the

		// Inventory Menu button check:
		if (Input.GetKeyDown (GAME_input_manager.Instance.itemMenuButton1) || Input.GetKeyDown (GAME_input_manager.Instance.itemMenuButton2)) {


			// I'm just going to handle the Player state toggle directly through the the Inventory Manager instead for now...
			inventoryScript.SetItemMenuInput();

			/*

			// Set menu or close menu and check if the player is in the menu state or not.
			bool _inItemMenu = inventoryScript.SetItemMenuInput ();
			if (_inItemMenu) {
				PLAYER_manager.Instance.EnterMenuState();
			} else {
				PLAYER_manager.Instance.EnterFreeState();
			}*/
		}
	}

	public void Exit(){
		PLAYER_manager.Instance.EnterPreviousState ();
	}
}
