using UnityEngine;
using Yarn.Unity;

public class GAME_titlescreen_manager : MonoBehaviour {

	//[FormerlySerializedAs("startNode")]
	public string talkToNode = "";

	public TextAsset titleScreenText;

	// Use this for initialization
	void Start () {
		if (titleScreenText != null) {
			FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(titleScreenText);

			GAME_manager.Instance.dialogRunner.StartDialogue (talkToNode);
		}

	}
}
