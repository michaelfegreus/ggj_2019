using UnityEngine;

public class ACTOR_conversation_cap : MonoBehaviour {

	int drunkStatCap = 6;

	void Update(){
		if (GAME_manager.Instance.globalVariables.alcoholPoints >= drunkStatCap) {
			this.gameObject.tag = "Non-Interactive";
		}
	}
}
