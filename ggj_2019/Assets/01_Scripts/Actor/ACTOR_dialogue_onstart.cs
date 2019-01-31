using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACTOR_dialogue_onstart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<INTERACTABLE_npc_dialogue> ().OnInteract ();
	}
}
