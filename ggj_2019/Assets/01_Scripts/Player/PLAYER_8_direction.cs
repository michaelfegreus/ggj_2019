using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_8_direction : MonoBehaviour {

	public Transform targetTransform;
	float targetRotation;

	// This script takes a 0-359 degree euler angle and then rounds it to a 45 degree interval.
	// This basically allows for retro-style 8 directional movement snapping really easily.
	void Update(){
		targetRotation = targetTransform.transform.rotation.eulerAngles.z;
		targetRotation /= 45f;
		targetRotation = Mathf.RoundToInt (targetRotation);
		targetRotation *= 45f;
		if (targetRotation >= 360f) { // This makes sure the eulerAngles are limited to under 360 degrees.
			targetRotation = 0f;
		}
		transform.position = targetTransform.position;
	}

	/*float targetRotation;

	// Update is called once per frame
	void Update () {
		if ((337.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 360f) || (transform.rotation.eulerAngles.z < 22.5f)) { // The || or split is there because of how euler angles loop back around on themselves, and go to 0 degrees not 360
			targetRotation = 0f;
		}
		else if (22.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 67.5f) {
			targetRotation = 45f;
		}
		else if(67.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 112.5f){
			targetRotation = 90f;
		}
		else if(112.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 157.5f){
			targetRotation = 135f;
		}
		else if(157.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 202.5f){
			targetRotation = 180f;
		}
		else if(202.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 247.5f){
			targetRotation = 225f;
		}
		else if(247.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 292.5f){
			targetRotation = 270f;
		}
		else if(292.5f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 337.5f){
			targetRotation = 315f;
		}
	}*/
	void LateUpdate(){
		transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetRotation);
	}
}