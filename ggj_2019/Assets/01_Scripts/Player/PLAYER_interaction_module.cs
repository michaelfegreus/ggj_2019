using UnityEngine;

public class PLAYER_interaction_module : MonoBehaviour {

	// An array that keeps track of all the things the trigger collider is colliding with.
	// This prevents the player from interacting with more than one thing at a time, should interactable objects be close enough.
	public GameObject[] moduleInteractables;
	[System.NonSerialized]
	public int interactableArraySize = 10;

	void Start () {
		// Can change amount of nearby interactable objects if need be, but there should not be too many.
		moduleInteractables = new GameObject[interactableArraySize];
	}


	// Check to see if the player entered the range of interactable objects.
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag.Trim().Equals("Interactable".Trim()) ){
			// Add to array of nearby interactables.
			for (int i = 0; i < moduleInteractables.Length; i++) {
				// If it finds a null slot, then it replaces it with the interactable.
				if (moduleInteractables [i] == null) {
					moduleInteractables [i] = col.gameObject;
					// This breaks the loop early
					return;
				}
			}
		}
	}

	// Check to see if the player exited the range of interactable objects.
	void OnTriggerExit2D(Collider2D col){
		if (col.tag.Trim().Equals("Interactable".Trim()) ){
			// Remove from list of nearby interactables.
			for (int i = 0; i < moduleInteractables.Length; i++) {
				// Remove the interactable from the array.
				if (moduleInteractables [i] == col.gameObject) {
					moduleInteractables [i] = null;
					// This breaks the loop early
					return;
				}
			}
		}
	}
}