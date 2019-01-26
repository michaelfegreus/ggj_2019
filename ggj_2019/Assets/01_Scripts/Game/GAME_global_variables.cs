using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class GAME_global_variables : MonoBehaviour {

	public int alcoholPoints;
	public int confidencePoints;

	void Start(){
		AddConfidencePoints ("-10");
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
	}

	// Load main game scene
	[YarnCommand("StartGame")]
	public void StartMainGameScene(){
		//SceneManager.LoadScene()
		Debug.Log("This should take you to the main game scene!");
		// TODO: Setup scene management.
	}
}