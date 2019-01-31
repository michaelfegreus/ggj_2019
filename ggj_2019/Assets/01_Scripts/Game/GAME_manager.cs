using UnityEngine;

[RequireComponent(typeof(GAME_input_manager))]
//[RequireComponent(typeof(GAME_inventory_manager))]
[RequireComponent(typeof(GAME_menu_manager))]
[RequireComponent(typeof(Yarn.Unity.DialogueRunner))] // Yarn component
[RequireComponent(typeof(ExampleVariableStorage))] // Yarn component
[RequireComponent(typeof(YARN_ui_manager))] // Yarn component
[RequireComponent(typeof(GAME_global_variables))] // GGJ2019 game state variabes

public class GAME_manager : Singleton<GAME_manager> {

	// Instantiate personal state machine
	private StateMachine gameStateMachine = new StateMachine();

	// Use this "protected override void awake" to ensure that this and the SingletonPersistant interface both prevent destruction on load.
	// Without this, the engine might just run GAME_manager's Awake, and not the SingletonPersistant's awake that keeps it from being destroyed

	// Delete this function if you're not using the SingletonPersistant interface though.
	/*protected override void Awake(){
		base.Awake ();
		// Can add more awake stuff below.
	}*/




	// Set up components
	[System.NonSerialized]
	public GAME_input_manager inputManager;
	//[System.NonSerialized]
	//public GAME_inventory_manager inventoryManager;
	[System.NonSerialized]
	public GAME_menu_manager menuManager;
	[System.NonSerialized]
	public Yarn.Unity.DialogueRunner dialogRunner;
	[System.NonSerialized]
	public ExampleVariableStorage variableStorageManager;
	[System.NonSerialized]
	public YARN_ui_manager yarnUIManager;
	[System.NonSerialized]
	public GAME_global_variables globalVariables;

	void Start(){
		EnterFreeState ();

		inputManager = GetComponent<GAME_input_manager> ();
		//inventoryManager = GetComponent<GAME_inventory_manager> ();
		menuManager = GetComponent<GAME_menu_manager> ();
		dialogRunner = GetComponent<Yarn.Unity.DialogueRunner> ();
		variableStorageManager = GetComponent<ExampleVariableStorage> ();
		yarnUIManager = GetComponent<YARN_ui_manager> ();
		globalVariables = GetComponent<GAME_global_variables> ();
	}





	void Update(){
		this.gameStateMachine.ExecuteStateUpdate ();
	}
		
	public void EnterFreeState(){
		this.gameStateMachine.ChangeState(new GAME_STATE_Free(gameObject));
	}










	// Pausing functionality. Not sure if I'll use it in this way.
	/*
	public static bool gamePauseEnabled;
		
	public void PauseToggle(bool turnOn){
		if (turnOn) {
			this.gameStateMachine.ChangeState (new GAME_STATE_Paused (gameObject, EnterFreeState));
			gamePauseEnabled = true;
		} else {
			this.gameStateMachine.SwitchToPreviousState();
			gamePauseEnabled = false;
		}
	}*/
}