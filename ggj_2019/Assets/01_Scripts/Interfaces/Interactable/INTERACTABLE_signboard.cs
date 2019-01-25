using UnityEngine;

public class INTERACTABLE_signboard : MonoBehaviour, IInteractable {

	public TextAsset myText;

	public void OnInteract(){
		GAME_textbox_manager.Instance.InitiateTextBox (myText);
	}
}
