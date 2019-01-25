using UnityEngine;

public class LEVEL_warp : MonoBehaviour {

	[Header("Set either an object to warp to, or direct coordinates.")]
	[Tooltip("Set an object for the Player to warp to. This script will prefer warping to the object over coordinates.")]
	public Transform destinationObject;
	[Tooltip("Set direct coordinates for the Player to warp to. Coordinates will get overwritten if an object is set above.")]
	public Vector2 destinationCoordinates;

	void Start(){
		// Overwrite destinationCoordinates if there is a destinationObject in the editor.
		if(destinationObject != null){
			destinationCoordinates = new Vector2 (destinationObject.position.x, destinationObject.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == ("Player")) {
			if (destinationObject != null) {
				// Warps player to a destination empty object transform
				col.transform.position = destinationObject.transform.position;
			} else if (destinationCoordinates != null) {
				col.transform.position = new Vector3 (destinationCoordinates.x, destinationCoordinates.y, 0f);;
			}

			/* OLD
			thePlayer.GetComponent<scr_player_manager> ().Warp (destinationWarp.position);
			myCam.enabled = false;
			newCam.enabled = true; */
		}
	}

}
