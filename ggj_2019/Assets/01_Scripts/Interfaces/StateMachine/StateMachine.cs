using UnityEngine;

public class StateMachine {
	// *** Might need to make this a Monobehavior depending on whether or not it's even possible to set new instances of general classes

	private IState currentlyRunningState;
	private IState previousState;

	public void ChangeState(IState newState){ // Called by main Monobehavior or manager
		
		if (currentlyRunningState != null) {
			currentlyRunningState.Exit ();
		}
		previousState = currentlyRunningState;

		currentlyRunningState = newState;
		currentlyRunningState.Enter ();
	}

	public void ExecuteStateUpdate(){ // Called by main Monobehavior or manager
		IState runningState = this.currentlyRunningState;
		if (runningState != null) {
			runningState.Execute ();
		}
	}

	public void SwitchToPreviousState(){
		ChangeState (previousState);
	}
	public IState GetCurrentState(){
		return currentlyRunningState;
	}
}