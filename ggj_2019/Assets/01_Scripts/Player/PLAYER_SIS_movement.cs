using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_SIS_movement : MonoBehaviour {

	Rigidbody2D rbody; // Variable to hold a reference to our rigidbody.

	public float walkSpeed;
	public float runSpeed;
	float targetMoveSpeed;
	float currentMoveSpeed;

	float inputX;
	float inputY;

	float inputRTrigger;
	float inputLTrigger;
	float runTriggerPressure = .5f;

	public bool running;

	Vector3 moveVec; // tracks rigidbody movement

	public Camera mainCamera; // Holds the main camera. This allows the player script to tell it when to move.

	void Start () {

		rbody = GetComponent<Rigidbody2D> ();
		currentMoveSpeed = targetMoveSpeed;
		running = false;
	}
	void Update(){
		// Directional Movement Controls

		inputX = Input.GetAxis ("Horizontal"); // A/D, LeftArrow/RightArrow
		inputY = Input.GetAxis ("Vertical"); // W/S, UpArrow/DownArrow



		if (inputX != 0 && inputY != 0) { // Slow you down a bit for diagonal movement - feels more natural.
			if (running) {
				currentMoveSpeed = runSpeed * .9f;
			} else {
				currentMoveSpeed = walkSpeed * .9f;
			}
		} else {
			if (running) {
				currentMoveSpeed = runSpeed;
			} else {
				currentMoveSpeed = walkSpeed;
			}
		}



		moveVec = transform.up * inputY * currentMoveSpeed // Forward and backward movement
			+ transform.right * inputX * currentMoveSpeed; // Left and right movement

		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);// The all important background Z-space layer movement experiment.

		//transform.localScale = new Vector3 (transform.position.y, transform.position.y, transform.position.y);
	}

	// FixedUpdate is called once per *PHYSICS* frame, at a fixed framerate. (Fixed frame is run at it's own framerate, indepedent of the visual framerate and sound framerate)
	void FixedUpdate () {
		// Movement actually executes in Fixed Update, as it is a physics-based operation.
		rbody.velocity = moveVec;
	}
}
