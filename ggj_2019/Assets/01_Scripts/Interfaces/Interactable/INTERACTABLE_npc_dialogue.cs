using UnityEngine;
using Yarn.Unity; // Can delete this if the following Monobehavior doesn't actually directly call the StartDialogue() function
//using Cinemachine;

public class INTERACTABLE_npc_dialogue : MonoBehaviour, IInteractable {


	// Below taken from the Yarn Spinner example script, NPC.cs
	// See original script for MIT License for open source code.

	//public string characterName = "";

	//[FormerlySerializedAs("startNode")]
	public string talkToNode = "";

	//[Header("Optional")]
	//public TextAsset scriptToLoad;

	public int confidenceNeeded;

	//public bool onlyTalkOnce = true;

	AudioSource myDitty;

	// For getting the camera to look at this characfter while takling.
	/*CinemachineBrain mainCamBrain;
	public bool lockCamOnCharacter = true;
	public Transform lastCamFollowTarget;*/

	// Use this for initialization
	void Start () {
		/*if (scriptToLoad != null) {
			FindObjectOfType<Yarn.Unity.DialogueRunner> ().AddScript (scriptToLoad);
		}*/
		myDitty = GetComponent<AudioSource> ();
		/*
		if (Camera.main.gameObject.GetComponent<CinemachineBrain> () != null) {
			mainCamBrain = Camera.main.gameObject.GetComponent<CinemachineBrain> ();
		}*/
	}

	public void OnInteract(){
		/// Going to want to replace this later with a reference to the Game Manager or a singleton. Searching for objects in runtime is non-ideal.
		//FindObjectOfType<DialogueRunner> ().StartDialogue (talkToNode);
		if (GAME_manager.Instance.globalVariables.confidencePoints >= confidenceNeeded) {
			//FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(scriptToLoad);
			GAME_manager.Instance.dialogRunner.StartDialogue (talkToNode);
			/*if (onlyTalkOnce) {
				// Mark as non-interactive now that the conversation is done.
				gameObject.tag = "Non-Interactive";
			}*/
			if (myDitty != null) {
				myDitty.Play ();
				GAME_manager.Instance.yarnUIManager.SetDittyAudioSource (myDitty);
			}
		//	PLAYER_manager.Instance.playerInteraction
		} else {
			//FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(GAME_manager.Instance.globalVariables.notEnoughConfidenceScript);
			GAME_manager.Instance.dialogRunner.StartDialogue (GAME_manager.Instance.globalVariables.talkToNode);
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "PlayerInteraction") {
			lastCamFollowTarget = mainCamBrain.ActiveVirtualCamera.Follow;
			mainCamBrain.ActiveVirtualCamera.Follow = this.transform;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "PlayerInteraction") {
			mainCamBrain.ActiveVirtualCamera.Follow = lastCamFollowTarget;
		}
	}
	*/

	// Stop interacting with the player.
	[YarnCommand("StopInteracting")]
	public void StopInteracting(){
		this.gameObject.tag = "Non-Interactive";
		Debug.Log ("I ran STOP INTERACTING");
		//onlyTalkOnce = true;
	}

	// Start interacting with the player.
	[YarnCommand("StartInteracting")]
	public void StartInteracting(){
		this.gameObject.tag = "Interactable";
		//onlyTalkOnce = true;
	}
}