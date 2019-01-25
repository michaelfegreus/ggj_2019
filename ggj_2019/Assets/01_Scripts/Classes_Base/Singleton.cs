using UnityEngine;

// <T> makes it "of type" so it can be a generic class. For example, Singleton<AudioManager>
// Abstract ensures that there won't be any instances of these scripts themselves getting attached to stuff. Wouldn't want to be able to write "new Singleton" and have it work
public abstract class Singleton<T> :  MonoBehaviour where T : Component {

	// Local storage of script:
	private static T instance;

	// Public access of script ("get" and "set"):

	// Remember, this would be "set" from another class like so:
	// GAME_manager.Instance = *insert new instance here* ;

	// And another class could "get" like so:
	// GAME_manager.Instance.UsePublicPauseFunction(); 

	public static T Instance{
		get{
			if (instance == null) {
				// If something tries to get a Singleton of the type, and can't find it - this will go in and create an object in the scene with the Singleton trying to be accessed.
				instance = FindObjectOfType<T>(); // This searches again through the scene just to be sure there isn't one already.
				if (instance == null) {
					GameObject InstantiatedSingletonObject = new GameObject ();
					InstantiatedSingletonObject.hideFlags = HideFlags.DontSave;
					instance = InstantiatedSingletonObject.AddComponent<T> ();
				}
			}
			return instance;
		}
		set{
			instance = value;
		}
	}
}
