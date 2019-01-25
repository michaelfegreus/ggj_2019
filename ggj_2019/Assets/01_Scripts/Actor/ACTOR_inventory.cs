using UnityEngine;

public class ACTOR_inventory : MonoBehaviour {

	// A generic item inventory --
	// Thus, ideally useable by multiple actor types, including the Player, NPCs, and treasure chests / backpacks, etc.

	public Item[] itemsHeld;

	// Add an item during run time.
	public void AddItem(Item newItem){
		// If there's an empty slot, add the Item asset to that slot.
		for (int i = 0; i < itemsHeld.Length; i++) {
			if (itemsHeld [i] == null) {
				itemsHeld [i] = newItem;
				// This breaks the loop early
				return;
			}
		}
	}

	// Check to see if the item inventory's slots are all taken.
	public bool CheckFull(){
		bool full = true; // Starts as true, but flips to false as soon as it finds an exception.
		for (int i = 0; i < itemsHeld.Length; i++) {
			if (itemsHeld [i] == null) {
				full = false;
				return full;
			}
		}
		return full;
	}

	// Search through this inventory and see if a particular item is within it.
	public bool CheckInventoryForItem(Item checkingItem){
		bool hasItem = false; // Starts as true, but flips to false as soon as it finds the item.
		for (int i = 0; i < itemsHeld.Length; i++) {
			if (itemsHeld [i] == checkingItem) {
				hasItem = true;
				return hasItem;
			}
		}
		return hasItem;
	}
}
