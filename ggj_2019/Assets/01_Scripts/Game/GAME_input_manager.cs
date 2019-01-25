using UnityEngine;

public class GAME_input_manager : SingletonPersistant<GAME_input_manager> {

	// Action button options:
	public KeyCode actionButton1;
	public KeyCode actionButton2;

	// Run button options:
	public KeyCode dashButton1;
	public KeyCode dashButton2;

	// Menu Confirm button options:
	public KeyCode selectionButton1;
	public KeyCode selectionButton2;

	// Item menu buttons:
	public KeyCode itemMenuButton1;
	public KeyCode itemMenuButton2;

	// Item menu buttons:
	public KeyCode itemUseButton1;
	public KeyCode itemUseButton2;

	protected override void Awake(){
		base.Awake ();
		// Can add more awake stuff below.
	}

}