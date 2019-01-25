using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_movement_2d : MonoBehaviour {

	Rigidbody2D rb;
	public PLAYER_animation animScript;

	// Analog Stick Input
	float inputX;
	float inputY;
	bool directionInput; // Is the player moving the analog stick?
	public float inputDeadzoneThreshold; // How far you need to move the stick to get this script to register it as movement.

	public bool runInput;
	bool moving; // Is the player currently in motion? This is difference from asking if the player is moving the analog stick.

	// Movespeeds
	public float walkSpeed;
	public float runSpeed;
	float currentMoveSpeed;
	float diagonalMoveModifier = .8f;

	Vector3 moveVec; // tracks rigidbody movement

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	void Update(){

		// Make sure to not run this when paused:
		if(Time.timeScale == 0f) { return; }
		
		PlayerControls ();

		// Calculate where and how to move:
		//if (directionInput) {
			if ((inputX < -inputDeadzoneThreshold || inputX > inputDeadzoneThreshold) && (inputY > inputDeadzoneThreshold || inputY < -inputDeadzoneThreshold)) { // Slow you down a bit for diagonal movement - feels more natural.
				if (runInput) {
					currentMoveSpeed = runSpeed * diagonalMoveModifier;
				} else {
					currentMoveSpeed = walkSpeed * diagonalMoveModifier;
				}
			} else {
				if (runInput) {
					currentMoveSpeed = runSpeed;
				} else {
					currentMoveSpeed = walkSpeed;
				}
			}

			moveVec = transform.up * inputY * currentMoveSpeed // Forward and backward movement
				+ transform.right * inputX * currentMoveSpeed; // Left and right movement
						transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);// The all important background Z-space layer movement experiment.
						//transform.localScale = new Vector3 (transform.position.y, transform.position.y, transform.position.y);
	//	}

		//animScript.SetMovement (directionInput, new Vector2(inputX, inputY), inputDeadzoneThreshold);
	}

	void PlayerControls(){
		// Get the joystick's axes.
		inputX = Input.GetAxis ("Horizontal");
		inputY = Input.GetAxis ("Vertical");
		// Is the player moving the analog stick?
		if (inputX < -inputDeadzoneThreshold || inputX > inputDeadzoneThreshold || inputY > inputDeadzoneThreshold || inputY < -inputDeadzoneThreshold) {
			directionInput = true;
		} else {
			directionInput = false;
		}
		// Check run button.
		if (Input.GetButton ("Joystick1")) {
			runInput = true;
		} else {
			runInput = false;
		}
	}

	// FixedUpdate is called once per *PHYSICS* frame, at a fixed framerate. (Fixed frame is run at it's own framerate, indepedent of the visual framerate and sound framerate)
	void FixedUpdate () {
		// Movement actually executes in Fixed Update, as it is a physics-based operation.
		rb.velocity = moveVec;
	}
}