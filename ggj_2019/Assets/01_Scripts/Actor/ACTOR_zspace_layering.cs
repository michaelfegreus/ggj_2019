using UnityEngine;

public class ACTOR_zspace_layering : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
	}
}