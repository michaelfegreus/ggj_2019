using UnityEngine;

public class Inventory {


	public Item[] itemsHeld;


	public Inventory(Item[] itemsToAdd){
		itemsHeld = new Item[itemsToAdd.Length];

		for (int i = 0; i < itemsHeld.Length; i++) {
			itemsHeld [i] = itemsToAdd [i];
		}
	}

}
