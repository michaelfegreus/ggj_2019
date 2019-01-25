using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_movement_rigidbody2d : MonoBehaviour {

	Rigidbody2D rb;
	public PLAYER_animation animScript;

	// Analog stick input
	float inputX = 0f;
	float inputY = 0f;
	float inputDeadzoneThreshold = .01f; // How far you need to move the stick to get this script to register it as movement.
	bool directionInput; // Is the player moving the analog stick?
	// Button input
	public bool runInput;

	// Movespeed values
	public float walkSpeed;
	public float runSpeed;
	float currentMoveSpeed;
	//float targetMoveSpeed;
	// Acceleration rates
	//public float accelerationRate; // Rate at which the player accelerates to meet the target velocity.
	//public float deccelerationRate;

	// Rotation
	Quaternion desiredRotation; // Calculated rotation of where the player is analog sticks are rotating the player towards.
	Vector3 movementVector; // Used to calculate direction analog stick is pointing.
	public float rotationSpeed; // Rate at which the player rotates to meet the target rotation. 
	float angleDifference; // The difference between the angle of the current rotation and the desired rotation. If it's over the rotationToMoveThreshold, rotate to match analog stick before moving. If it's above 180 degrees, skid.
	public float rotationToMoveThreshold; // The player must be have rotated to within this threshold to begin walking.
	// Pivoting (aka Skidding)
	public float pivotThreshold; // The degree of change in angle needed to pass the Pivot Check and skid.
	Quaternion[] lastRotationArray; // Array containing data about your last rotation. This is used to check against your current desired rotation. If pivotThreshold is met, the player skids.
	public int lastRotationFrameCount;  // This number determines how many frames of player rotation are being tracked in the Update loop.
	public float pivotRegainControlSpeed; // What speed does the player have to deccelerate to in order to begin moving again after a skid.
	public float skidDeccelerationRate; // How quickly the player slows to a stop when skidding.
	bool pivoting; // Is the player currently pivoting.
	public float pivotResetTimeLimit; // How long to reset after a pivot
	float pivotResetTimer; // Keeps track of time after a pivot


	bool moving; // Is the player currently in motion? This is difference from asking if the player is moving the analog stick.

	//Debug:
	float pivotColorTimer = 0f;
	SpriteRenderer rend;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rend = GetComponent<SpriteRenderer> ();
		//animScript = GetComponent<PLAYER_animation> ();

		currentMoveSpeed = walkSpeed;
		lastRotationArray = new Quaternion[lastRotationFrameCount];
		pivotResetTimer = pivotResetTimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pivoting) {
			PlayerControls ();
		}

		DirectionCheck ();

		if (directionInput && !pivoting) {
			// If you're already facing the direction, toggle on moving. Otherwise, you'll turn before beginning to walk.
			if (angleDifference < rotationToMoveThreshold) {
				moving = true;
			}
		} else {
			moving = false;
		}

		PivotDebug ();

		PivotCheck ();

		if (directionInput && !pivoting) {
			transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed); // Trying to reach analog stick's rotation angle.
		}

		PlayerAnimation ();
	}
		
	void FixedUpdate (){
		// Move input that pushes the character forward towards the direction faced
		if (moving && !pivoting) {
			// Apply force to begin moving!
			rb.AddForce ((transform.up * -1f) * currentMoveSpeed);
		}
		if (pivoting) {
			Skid ();
		}
	}

	void PlayerControls(){
		// Get the joystick's axes.
		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
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

		if (runInput) {
			currentMoveSpeed = runSpeed;
		} else {
			currentMoveSpeed = walkSpeed;
		}
	}

	// Calculate which way you're facing
	void DirectionCheck(){
		movementVector = new Vector3 (inputX, inputY, 0.0f);
		float angle = Mathf.Atan2 (inputX, inputY) * Mathf.Rad2Deg; // Calculate angle of analog stick input.
		desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, -1f * angle + 180f));

		angleDifference = Quaternion.Angle (transform.rotation, desiredRotation);

		// Keep track of the player's rotation on this frame and increment the array. This is for pivots:
		for (int i = 0; i < lastRotationArray.Length - 1; i++) { // End at Array.Length - 1 so it doesn't go out of range.
			lastRotationArray[i] = lastRotationArray[i + 1]; // Get it from the next slot up.
		}
		lastRotationArray [lastRotationArray.Length - 1] = transform.rotation;

	}

	void PivotCheck(){
		if (pivotResetTimer < pivotResetTimeLimit) { // Just worry about the pivot reset timer if the player recently pivoted.
			pivotResetTimer += Time.deltaTime;
		} else if (moving & !pivoting) {
			// If anywhere in the last few frames your rotatation was different enough from the desired rotation, the character should skid abruptly.
			for (int i = 0; i < lastRotationArray.Length; i++) {
				if (Quaternion.Angle (lastRotationArray [i], desiredRotation) > pivotThreshold) {
					//Debug.Log ("Begin skid");
					pivotColorTimer = .01f;
					rend.color = Color.red;
					pivoting = true;
					Debug.Log("Pivot angle: " + Quaternion.Angle (lastRotationArray [i], desiredRotation));
					break;
				}
			}


			if (angleDifference > pivotThreshold) {
				if (moving) {
					//Debug.Log ("Begin skid");
					pivotColorTimer = .01f;
					rend.color = Color.red;
				}
			}
		}
	}

	void Skid(){
		// Eventually, this will pair with animation.
		if (currentMoveSpeed > pivotRegainControlSpeed) {
			rb.AddForce ((transform.up * -1f) * currentMoveSpeed); // Continue to apply force as you slow down to get a decceleration, not an abrupt stop.
			currentMoveSpeed = Mathf.Lerp (currentMoveSpeed, 0f, Time.deltaTime * skidDeccelerationRate);
			Debug.Log ("Skidding!");
		} else if (currentMoveSpeed < pivotRegainControlSpeed){ // Skid stops when you slow down enough OR when you leave the ground.
			transform.rotation = desiredRotation; // Flip around the character for the skid.
			pivoting = false;
			if (!runInput) {
				currentMoveSpeed = walkSpeed; // Reset movespeed
			} else {
				currentMoveSpeed = runSpeed;
			}
			Debug.Log ("End skid.");
			pivotResetTimer = 0f;

		}
	}

	void PlayerAnimation(){
		//animScript.SetMovement (moving, (transform.up * -1f));
	}

	void PivotDebug(){
		if (pivotColorTimer > .001f) {
			pivotColorTimer += Time.deltaTime;
			if (pivotColorTimer > .05f) {
				pivotColorTimer = 0f;
				rend.color = Color.white;
			}
		}
	}
}