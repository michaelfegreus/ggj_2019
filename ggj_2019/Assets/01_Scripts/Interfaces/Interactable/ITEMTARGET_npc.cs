using UnityEngine;

public class ITEMTARGET_npc : MonoBehaviour, IItemTarget {

	public void OnItemUse (Item itemUsed) {
		// This is a temporary solution. In the future, this will most likely interact with managers
		// or other objects to assign dialogue and send to the right node.

		//GetComponent<INTERACTABLE_npc_dialogue> ().ItemDialogue (itemUsed.itemName);
		//Debug.Log ("Used the " + itemUsed.itemName + " on the " + gameObject.name);
	}
}
