using UnityEngine;

public class PLAYER_animation : MonoBehaviour {

	public Animator anim;
	Vector2 lastMove;

	bool moving;

	void Update(){

		if (!moving) {
			anim.Play ("IdleStanding");
		} else {
			anim.Play ("Walk");
		}

		// Use this to fix the error of not being able to idle diagonally when using the D-pad or keys.
		//DiagonalDigitalCheck();

	}

	public void SetMovement(bool playerMoving, Vector2 playerMovement, float movementThreshold){

		moving = playerMoving;

		// lastMove is used for getting information about the last direction the player faced for idle and standing animations.
		if (playerMoving) {
			lastMove = new Vector2 (0f, 0f);
			if (playerMovement.x > movementThreshold || playerMovement.x < movementThreshold * -1f) {
				//lastMove = new Vector2 (playerMovement.x, 0f);
				//if (playerMovement.x != 0) {
					lastMove.x = playerMovement.x;
				//}

			}
			if (playerMovement.y > movementThreshold || playerMovement.y < movementThreshold * -1f) {
				//lastMove = new Vector2 (0f, playerMovement.y);
				//if (playerMovement.y != 0) {
					lastMove.y = playerMovement.y;
			//	}
			}
		}

		anim.SetFloat ("DirectionX", lastMove.x);
		anim.SetFloat ("DirectionY", lastMove.y);
	}

	//int diagonalCheckFrames = 12;
	//int currentDiagonalCheckFrame;

	/*void DiagonalDigitalCheck(){
		// Use this to fix the error of not being able to idle diagonally when using the D-pad or keys.
		if (Input.GetAxis ("HorizontalDigital") != 0f && Input.GetAxis ("VerticalDigital") != 0f) {
			currentDiagonalCheckFrame = 0;
		}
	}*/
}