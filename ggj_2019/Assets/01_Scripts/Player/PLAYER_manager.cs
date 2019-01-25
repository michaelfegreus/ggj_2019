using UnityEngine;

[RequireComponent(typeof(PLAYER_movement_directional_2d))]
[RequireComponent(typeof(PLAYER_interaction))]
[RequireComponent(typeof(PLAYER_animation))]
public class PLAYER_manager : Singleton<PLAYER_manager> {
	
	private StateMachine playerStateMachine = new StateMachine();

	[System.NonSerialized]
	public PLAYER_movement_directional_2d playerMovement;
	[System.NonSerialized]
	public PLAYER_interaction playerInteraction;
	[System.NonSerialized]
	public PLAYER_animation playerAnimation;

	void Start(){
		EnterFreeState ();

		playerMovement = GetComponent<PLAYER_movement_directional_2d> ();
		playerInteraction = GetComponent<PLAYER_interaction> ();
		playerAnimation = GetComponent<PLAYER_animation> ();
	}

	void Update(){
		this.playerStateMachine.ExecuteStateUpdate ();
	}

	public void EnterFreeState(){
		this.playerStateMachine.ChangeState(new PLAYER_STATE_FreeControl(gameObject));
	}
	public void EnterMenuState(){
		this.playerStateMachine.ChangeState(new PLAYER_STATE_Menu(gameObject));
	}
	public void EnterPreviousState(){
		this.playerStateMachine.SwitchToPreviousState ();
	}
}