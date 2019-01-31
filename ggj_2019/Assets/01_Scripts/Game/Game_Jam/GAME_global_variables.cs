using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class GAME_global_variables : MonoBehaviour {

	public int alcoholPoints;
	public int confidencePoints;

	//[Tooltip("The default dialogue when the player doesn't have enough confidence points to talk to someone.")]
	// Moved this into dialogue runner
	//public TextAsset notEnoughConfidenceScript;

	public string talkToNode = "";

	public AudioSource confidenceUp;
	public AudioSource confidenceDown;

	void Start () {
		
	}

	// Yarn commands ahead:

	// Add a certain amount of alcohol points based on a value specified in Yarn
	[YarnCommand("AddAlcoholPoints")]
	public void AddAlcoholPoints(string pointsValueString){
		int addedPoints = int.Parse (pointsValueString);
		alcoholPoints += addedPoints;
	}

	// Add a certain amount of confidence points based on a value specified in Yarn
	[YarnCommand("AddConfidencePoints")]
	public void AddConfidencePoints(string pointsValueString){
		int addedPoints = int.Parse (pointsValueString);
		confidencePoints += addedPoints;
		if (confidenceUp != null && confidenceDown != null) {
			if (addedPoints > 0) {
				confidenceUp.Play ();
			} else if (addedPoints < 0) {
				confidenceDown.Play ();
			}
		}
	}

	// Load main game scene
	[YarnCommand("LoadScene")]
	public void YarnLoadScene(string sceneIndex){
		SceneManager.LoadScene (int.Parse (sceneIndex));
	}
}