using UnityEngine;

public class ITEMTARGET_take_item : MonoBehaviour, IItemTarget {

	public void OnItemUse (Item itemUsed) {
		Debug.Log ("Used item via interface.");
		GAME_inventory_manager.Instance.RemoveItem (itemUsed);
	}
}
