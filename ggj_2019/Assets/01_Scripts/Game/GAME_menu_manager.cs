using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAME_menu_manager : Singleton<GAME_menu_manager>  {

	// Menu box from the UI canvas
	public Image menuBox;

	// Is the menu currently in use?
	bool inMenu;

	// Has something been selected? Run this delegate function (instead of setting a boolean flag for other scripts to look for)
	public delegate void OnReturnSelection(int selectedIndex);
	OnReturnSelection currentOnReturnSelection;

	// Should the menu close on selecting an item?
	bool _closeMenuOnSelect;

	//[System.NonSerialized]
	//public bool menuItemSelected = false;
	// Text Fields for options to appear in

	public Text[] optionSlots;

	int optionsCount; // How many options there are for the current menu

	// UI cursor to select things in the menu
	public Image cursorSprite;
	int cursorIndex;// Slot that the cursor is hovering over
	bool cursorMove = false; // Has the cursor just moved?
	// How many units should the menu box vertical size increase by
	public float verticalIncreaseUnits = 20f;

	public string[] testOptions;

	// GGJ2019

	public int drunkLevel01;
	public int drunkLevel02;

	[Tooltip("Lower the value, the more likely to scramble. i.e. -- drunkPoints / scrambleRation -- (Don't set to zero, you can't divide by that!")]
	public int scrambleRatio = 10;
	[Tooltip("Lower the value, the more likely to give a garbage char. i.e. -- drunkPoints / scrambleRation -- (Don't set to zero, you can't divide by that!")]
	public int garbageCharRatio = 10;

	GAME_global_variables globalVariables;
	void Start(){
		// Should just be attached the Game Manager so grab this.
		globalVariables = GetComponent<GAME_global_variables> ();
	}


	// This function gets called from outside scripts and GameObjects to set up a menu.
	public void SetMenu(string[] optionArray, bool closeMenuOnSelect, OnReturnSelection newOnReturnSelection){
		_closeMenuOnSelect = closeMenuOnSelect;
		currentOnReturnSelection = newOnReturnSelection;

		// Reset values
		float verticalSizeUnits = 80f; // Start base size at 60f
		cursorIndex = 0;
		// Reset all Text fields.
		for (int i = 0; i < optionSlots.Length; i++) {
			optionSlots [i].gameObject.SetActive(false);
		}

		// Turn on menu Game Object if it's not already active
		menuBox.gameObject.SetActive (true);
		inMenu = true;
		// Turn on cursor
		cursorSprite.gameObject.SetActive (true);

		optionsCount = optionArray.Length;
		for(int i = 0; i < optionsCount; i++){ 
			verticalSizeUnits += verticalIncreaseUnits; // Increase size of the menu box depending on how many items are on the list
			optionSlots[i].gameObject.SetActive(true); // Activate Text fields for each option
            // TODO: Scramble here!
            string scrambledOption = ScrambleString(optionArray[i]);
			optionSlots[i].text = scrambledOption; // Put strings in the Text fields
		}
		verticalSizeUnits = Mathf.Clamp(verticalSizeUnits, 100f, 480f); // Clamp to ensure that the text box doesn't get too big for the screen.
		menuBox.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, verticalSizeUnits);
	}

	public void CloseMenu(){
		menuBox.gameObject.SetActive (false);
		cursorSprite.gameObject.SetActive (false);
	}

	void Update (){

		if (inMenu) {
			UpdateCursorMoveIndex (); // Place the cursor.

			CheckCursorInput();

			CheckPlayerSelectionInput ();

		}
	}







	// Functions for controlling the menu when it has been opened

	void CheckCursorInput(){
		float inputThreshold = .2f; // Deadzone threshold for pushing the analog stick. Used in the next block.

		float inputY = Input.GetAxis("VerticalDigital");


		// Override with analog stick movement if the analog stick is pushed on the same frame.
		if(Mathf.Abs(Input.GetAxis("VerticalAnalog")) > inputThreshold){
			inputY = Input.GetAxis("VerticalAnalog");
		}

		// If you haven't just moved the cursor.
		if (!cursorMove) {
			// If input down
			if (inputY < -inputThreshold) {
				// If the next cursor slot is past the end of the array...
				if (cursorIndex + 1 == optionsCount) {
					cursorIndex = 0;
				} else {
					cursorIndex++;
				}
				cursorMove = true;
				UpdateCursorMoveIndex ();
				//UpdateItemDescription();

			}
			// If input up
			if (inputY > inputThreshold) {
				// If the next cursor slot is below 0...
				if (cursorIndex - 1 < 0) {
					cursorIndex = optionsCount - 1;
				} else {
					cursorIndex--;
				}
				cursorMove = true;
				UpdateCursorMoveIndex ();
				//UpdateItemDescription();

			}
		} else if (inputY < inputThreshold && inputY > -inputThreshold) {
			cursorMove = false;
		}

		//Debug.Log ("Cursor index: " + cursorIndex);

	//	Debug.Log ("Input Y: " + inputY);
	}

	// Updates the current index of the cursor.
	void UpdateCursorMoveIndex(){
		// Sets the Y position of the cursor sprite equal to the y position of the current UI spot of the index.
		cursorSprite.transform.position = new Vector3(cursorSprite.transform.position.x, optionSlots [cursorIndex].transform.position.y, cursorSprite.transform.position.z);
		// Update the item description box to reflect the newly highlighted item.

	}


	void CheckPlayerSelectionInput(){
		if (Input.GetKeyDown(GAME_input_manager.Instance.selectionButton1) || Input.GetKeyDown(GAME_input_manager.Instance.selectionButton2)) {
			currentOnReturnSelection (cursorIndex); // Delegate function run from the class that originally opened the menu.
			if (_closeMenuOnSelect) { // Close and de-activate the menu if it's not needed.
				CloseMenu ();
				inMenu = false;
			}
		}
	}

    string ScrambleString(string phraseToScramble) {
		
		char[] phraseCharArray = phraseToScramble.ToCharArray();
		string newPhrase = "";

		List<char> letterList = new List<char> ();

		// DRUNK LEVEL 01 SCRAMBLE:

		if (globalVariables.alcoholPoints >= drunkLevel01) {
			for (int i = 0; i < phraseToScramble.Length; i++) {
				// Truncate at spaces, periods, question marks, and exclamation points.
				if (phraseCharArray [i].Equals (' ') || phraseCharArray [i].Equals ('.') || phraseCharArray [i].Equals ('!') || phraseCharArray [i].Equals ('?')) {
					// Scramble the  word just completed

					// RNG if it scrambles or not!
					if (globalVariables.alcoholPoints > Random.Range(0, scrambleRatio)) {
						for (int a = 1; a < letterList.Count; a++) {
							char temp = letterList [a];
							int randomIndex = Random.Range (a, letterList.Count);
							letterList [a] = letterList [randomIndex];
							letterList [randomIndex] = temp;
						}
					}
					// Add the scrambled word to the phrase.
					for (int a = 0; a < letterList.Count; a++) {
						newPhrase += letterList [a];
					}
					// Add the punctuation as seen in the containing if statement.
					newPhrase += phraseCharArray [i];
					// Start a new word, reset the list.
					letterList.Clear ();
				} else {
					letterList.Add (phraseCharArray [i]);
				}
			}

			// Nested if -- DRUNK LEVEL 02 SCRAMBLE:

			if (globalVariables.alcoholPoints >= drunkLevel02) {
				phraseCharArray = newPhrase.ToCharArray();
				// Reset newPhrase
				newPhrase = "";
				for(int i = 0; i < phraseCharArray.Length; i++){
					if (globalVariables.alcoholPoints > Random.Range (0, garbageCharRatio)) {
						switch (Random.Range (0, 10)) {
						case 0:
							newPhrase += "?";
							break;
						case 1:
							newPhrase += "?";
							break;
						case 2:
							newPhrase += "?";
							break;
						case 3:
							newPhrase += "$";
							break;
						case 4:
							newPhrase += "%";
							break;
						case 5:
							newPhrase += "&";
							break;
						case 6:
							newPhrase += "@";
							break;
						case 7:
							newPhrase += "+";
							break;
						case 8:
							newPhrase += "=";
							break;
						case 9:
							newPhrase += "-";
							break;
						case 10:
							newPhrase += "#";
							break;
						}
					} else {
						newPhrase += phraseCharArray [i];
					}
				}
			}
			return newPhrase;
		} else {
			return phraseToScramble;
		}
    }

    /*    Using a delegate function to call from outside this class instead. Originally was setting a flag and waiting others to check it.
	// Function for outside scripts to call when the player makes a selection. The outside script should know what to do with this slot ID.
	public int CheckPlayerSelectionIndex(){
		return cursorIndex;
	}*/
}