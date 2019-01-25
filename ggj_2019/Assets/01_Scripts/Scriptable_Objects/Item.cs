using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class Item : ScriptableObject {

	[Header("Item Properties")]
	[Tooltip("Name of the item that appears in menus and other in-game text.")]
	public string itemName;
	[Tooltip("Description of the item that appears in the item menu.")]
	public string itemDescription;
	[Tooltip("Icon of the item that appears in the item HUD.")]
	public Sprite iconSprite;
}
