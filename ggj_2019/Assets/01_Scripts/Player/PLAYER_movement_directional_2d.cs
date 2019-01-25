using UnityEngine;

public class PLAYER_movement_directional_2d : MonoBehaviour {

	// The child object of the player this is actually moving around. This allows for certain this to go off and transform, but the Player manager to remain at the root.
		/* So -- do I really need the Player object to sit at the root? The way I'm building the systems now, it seems like it would be okay to move the main object with the manager.
		The reason it's built this way is because movement is driven by rotation to a point, then acceleration. So it works like I originally built the 3D character.
		It causes issues to rotate the object containing the Sprite and the Interaction collider, however.
		I think this is still a good character controller, but it would be nice if everything was a bit less spread out. */

	public GameObject playerMovementModule;

	Rigidbody2D rb;

	// Analog stick input
	[System.NonSerialized]
	public float inputX = 0f;
	[System.NonSerialized]
	public float inputY = 0f;
	public float inputDeadzoneThreshold = .01f; // How far you need to move the stick to get this script to register it as movement.
	public bool directionInput; // Is the player moving the analog stick?
	// Button input
	bool dashInput;

	// Movespeed values
	public float walkSpeed;
	public float dashSpeed;
	float currentMoveSpeed;
	//float targetMoveSpeed;
	// Acceleration rates
	//public float accelerationRate; // Rate at which the player accelerates to meet the target velocity.
	//public float deccelerationRate;

	// Rotation
	Quaternion desiredRotation; // Calculated r otation of where the player is analog sticks are rotating the player towards.
	//Vector3 movementVector; // Used to calculate direction analog stick is pointing.

	// Use this to modify vertical movement speed of the player on the screen. This is like a movement for-shortening technique to make the character move in perspective more or less properly.
	// Should prioritize game-feel and what looks natural -- should be subtle.
	public float yMovementForshortenModifier;

	void Start () {
		rb = playerMovementModule.GetComponent<Rigidbody2D> ();

		currentMoveSpeed = walkSpeed;
	}
	
	void Update () {

		PlayerControls ();

		DirectionInputCheck ();

		if (directionInput) {
			playerMovementModule.transform.rotation = desiredRotation;
		}

		// Send information about movement to the animator every frame.
		PLAYER_manager.Instance.playerAnimation.SetMovement(directionInput, new Vector2(inputX, inputY), inputDeadzoneThreshold);
	}

	void FixedUpdate (){
		// Move input that pushes the character forward towards the direction faced
		if (directionInput) {
			// Apply force to begin moving!
			rb.AddForce ((playerMovementModule.transform.up * -1f) * (currentMoveSpeed /*substract based on yMovementForshortenMod, if player is drawn from a camera angle needing compensation for moving in perspective*/ - Mathf.Abs(yMovementForshortenModifier * inputY * currentMoveSpeed)));
		}
	}

	void PlayerControls(){

		// Derive player controls from PLAYER_STATE_FreeControl.
		// This method interprets what to do with those controls once input.

		// Is the player moving the analog stick?
		if (inputX < -inputDeadzoneThreshold || inputX > inputDeadzoneThreshold || inputY > inputDeadzoneThreshold || inputY < -inputDeadzoneThreshold) {
			directionInput = true;
		} else {
			directionInput = false;
		}

		if (dashInput) {
			currentMoveSpeed = dashSpeed;
		} else {
			currentMoveSpeed = walkSpeed;
		}
	}

	// Calculate which way you're facing
	void DirectionInputCheck(){
		//movementVector = new Vector3 (inputX, inputY, 0.0f);
		float angle = Mathf.Atan2 (inputX, inputY) * Mathf.Rad2Deg; // Calculate angle of analog stick input.
		desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, -1f * angle + 180f));

		// For lerped rotation: 
		//angleDifference = Quaternion.Angle (transform.rotation, desiredRotation);


	}

	public void SetDashInput(bool input){
		dashInput = input;
	}

	// The below could probably be...a lot slimmer in terms of lines of code.
	// But leave it unless it causes an issue, or you plan on adding more depth to input, because it works for now.

	// For use in fixing digital (keys or dpad) diagonal movement.
	bool digitalDiagonalMovement = false; // Is the player moving diagonally
	int digitalDiagonalCheckFrames = 4;
	int digitalDiagonalCheckCurrentFrame;
	float digitalDiagonalStoredInputX;
	float digitalDiagonalStoredInputY;

	public void SetMovementInput(float anX, float anY, float digX, float digY){

		float analogX = anX;
		float analogY = anY;

		// Set player movement with digital input:
		float digitalX = digX;
		float digitalY = digY;

		// Set player movement with digital input:
		SetMovementAxes (digitalX, digitalY);

		// Priorizite with analog stick input, if there is movement on the stick:
		if (analogX != 0f || analogY != 0f) {

			// Set the player movement with analog input:
			SetMovementAxes (analogX, analogY);
		}
		else {
			// The below fixes the digital diagonal movement error where the player character cannot standle in idle
			// at a diagonal angle. The reason for this is because when releasing diagonals on the keyboard,
			// the player will generally release one key a frame or two apart from the other. Using a frame timer,
			// the diagonal input can be stored and used.

			if (digitalX != 0f && digitalY != 0f) {
				digitalDiagonalMovement = true;
				digitalDiagonalCheckCurrentFrame = 0;
				digitalDiagonalStoredInputX = digitalX;
				digitalDiagonalStoredInputY = digitalY;
			}

			if (digitalDiagonalMovement) {
				digitalDiagonalCheckCurrentFrame++;
			}
			if ((digitalX == 0f || digitalY == 0f) && (digitalDiagonalCheckCurrentFrame >= digitalDiagonalCheckFrames)) {
				digitalDiagonalMovement = false;  
			}
			if (digitalDiagonalMovement && digitalDiagonalCheckCurrentFrame < digitalDiagonalCheckFrames) {
				SetMovementAxes (digitalDiagonalStoredInputX, digitalDiagonalStoredInputY);
			}

		}

	}

	// Convert all movement controls to digital input, and send those to the player movement script.
	void SetMovementAxes(float horzInput, float vertInput){

		inputX = 0f;
		inputY = 0f;

		if (horzInput > 0) {
			inputX = 1f;
		} else if (horzInput < 0) {
			inputX = -1f;
		}

		if (vertInput > 0) {
			inputY = 1f;
		} else if (vertInput < 0) {
			inputY = -1f;
		}

		// Tweak >diagonal< input so she moves along the perspective grid.
		if (inputX != 0f && inputY != 0f) {
			float diagonalVerticalAdjust = .5f; // Tweak this value to get diagonal movement to match the perspective grid.
			if (inputY < 0) {
				inputY = inputY * diagonalVerticalAdjust;
			}
			if (inputY > 0) {
				inputY = inputY * diagonalVerticalAdjust;
			}
		}
	}
}
