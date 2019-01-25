using UnityEngine;
using Cinemachine;

public class CAMERA_vcam_trigger : MonoBehaviour {

	public GameObject myCam;
	public CinemachineBrain mainCamBrain;

	GameObject lastCam;

	[Tooltip("Enable if the script should return to the previous camera on exiting the trigger collider.")]
	public bool returnToLastCam;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == ("Player")) {
			if (mainCamBrain.ActiveVirtualCamera.VirtualCameraGameObject != myCam) {
				SwapCameras ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.tag == ("Player")) {
			if (returnToLastCam) {
				SwapToLastCam ();
			}
		}
	}

	void SwapCameras(){
		if (returnToLastCam) {
			lastCam = mainCamBrain.ActiveVirtualCamera.VirtualCameraGameObject;
		}

		mainCamBrain.ActiveVirtualCamera.VirtualCameraGameObject.SetActive (false);
		myCam.SetActive (true);
	}

	void SwapToLastCam(){
		myCam.SetActive (false);
		lastCam.SetActive (true);
	}
}
