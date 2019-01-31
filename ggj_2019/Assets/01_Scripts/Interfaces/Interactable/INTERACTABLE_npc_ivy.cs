using Yarn.Unity; // Can delete this if the following Monobehavior doesn't actually directly call the StartDialogue() function
using UnityEngine;
using Cinemachine;

public class INTERACTABLE_npc_ivy : MonoBehaviour, IInteractable {


	// Below taken from the Yarn Spinner example script, NPC.cs
	// See original script for MIT License for open source code.

	//public string characterName = "";

	//[FormerlySerializedAs("startNode")]
	public string talkToNodeNoConfidence = "";
	public string talkToNodeEndGood = "";
	public string talkToNodeEndBad = "";


	public int confidenceNeeded;

	public int badEndDrunkLevel;

	//public bool onlyTalkOnce = true;

	public AudioSource myDitty;
	public GameObject myCam;
	CinemachineBrain mainCamBrain;


	// Use this for initialization
	void Start () {
		myDitty = GetComponent<AudioSource> ();

		if (Camera.main.gameObject.GetComponent<CinemachineBrain> () != null) {
			mainCamBrain = Camera.main.gameObject.GetComponent<CinemachineBrain> ();
		}
	}

	public void OnInteract(){
		/// Going to want to replace this later with a reference to the Game Manager or a singleton. Searching for objects in runtime is non-ideal.
		//FindObjectOfType<DialogueRunner> ().StartDialogue (talkToNode);
		if (GAME_manager.Instance.globalVariables.confidencePoints >= confidenceNeeded) {

			// Set the new virtual camera for Ivy
			if (myCam != null) {
				mainCamBrain.ActiveVirtualCamera.VirtualCameraGameObject.SetActive (false);
				myCam.SetActive (true);
			}

			if (GAME_manager.Instance.globalVariables.alcoholPoints >= badEndDrunkLevel) {
				GAME_manager.Instance.dialogRunner.StartDialogue (talkToNodeEndBad);
			} else {
				GAME_manager.Instance.dialogRunner.StartDialogue (talkToNodeEndGood);
				if (myDitty != null) {
					myDitty.Play ();
					GAME_manager.Instance.yarnUIManager.SetDittyAudioSource (myDitty);
				}
			}


		} else {
			//FindObjectOfType<Yarn.Unity.DialogueRunner>().AddScript(GAME_manager.Instance.globalVariables.notEnoughConfidenceScript);
			GAME_manager.Instance.dialogRunner.StartDialogue (talkToNodeNoConfidence);
		}
	}

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
