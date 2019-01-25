using UnityEngine;

// For Singletons that should not be destroyed on scene loads.

// <T> makes it "of type" so it can be a generic class. For example, Singleton<GAME_manager>
// Abstract ensures that there won't be any instances of these scripts themselves getting attached to stuff
public abstract class SingletonPersistant<T> : MonoBehaviour where T : Component {

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

	// To ensure the associated GameObject doesn't get destroyed on scene change:
	protected virtual void Awake(){ // protected virtual allows the class using this SingletonPersistant interface to ensure that the object doesn't get destroyed on load
		DontDestroyOnLoad (this);
		// If it notices that there are no others existing doing this job, then this instance does it.
		if (instance == null) {
			instance = this as T; // We say "this as T" here so that it doesn't save the instance as this "SingletonPersistant" class I'm typing in,
								   //but instead as the Singleton type we're working with, such as a GAME_manager
		} else {
			Destroy (gameObject); // If the associated Singleton type is already in play, then destroy this instance.
		}
	}
}
