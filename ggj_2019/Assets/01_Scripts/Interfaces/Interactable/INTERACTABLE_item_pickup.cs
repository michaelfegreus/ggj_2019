using UnityEngine;

public class INTERACTABLE_item_pickup : MonoBehaviour, IInteractable {

	public Item myItem;

	public void OnInteract(){
		if (GAME_inventory_manager.Instance.AddItem (myItem) == true) {
			// If the item could be added to the player inventory, then destroy this Item Pickup object.
			Destroy (this.gameObject);
		} else {
			Debug.Log ("Inventory full!");
		}
	}
}
