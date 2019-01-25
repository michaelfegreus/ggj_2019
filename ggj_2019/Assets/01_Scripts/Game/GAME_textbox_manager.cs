using UnityEngine.UI;
using UnityEngine;

public class GAME_textbox_manager : Singleton<GAME_textbox_manager> {

	// State of textbox
	public bool textBoxActive = false;

	public GameObject textBoxUI;

	public Text onscreenText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public void InitiateTextBox(TextAsset newTextFile){
		textFile = newTextFile;
		if (textFile != null) {
			textBoxActive = true;
			currentLine = 0;

			textLines = new string[1];

			if (textFile != null) {
				textLines = (textFile.text.Split ('\n'));
			}

			endAtLine = textLines.Length - 1;

			onscreenText.text = textLines [currentLine];

			textBoxUI.SetActive (true);
		}
		//GAME_manager.Instance.PauseToggle (true);
	}

	void Update () {
		if (textBoxActive) {
			onscreenText.text = textLines [currentLine];
			if (Input.GetKeyDown(GAME_input_manager.Instance.selectionButton1) || Input.GetKeyDown(GAME_input_manager.Instance.selectionButton2)) {

				currentLine += 1;

			}
			// If you get to the end of the text, close the textbox and deactivate it.
			if (currentLine > endAtLine) {
				textBoxUI.SetActive (false);
				textBoxActive = false;
				//GAME_manager.Instance.PauseToggle (false);
				Debug.Log ("Text box should deactivate");
			}
		}
	}
}
