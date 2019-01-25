using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_animation_module : MonoBehaviour {

	// This component allows the Sprite to follow along with the player's movement module positional transforms, while maintaining 0 rotational values.

	public Transform targetTransform;
	Vector3 startPositionOffset;

	// This preserves the sprite's offset, as if it were a child of the MovementModule.
	void Awake(){
		startPositionOffset = transform.localPosition;
	}

	void Update(){
		transform.position = targetTransform.position + startPositionOffset;
	}
}
