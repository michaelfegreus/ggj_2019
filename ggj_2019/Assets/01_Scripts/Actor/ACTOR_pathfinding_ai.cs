using UnityEngine;
using System.Collections;
using Pathfinding;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class ACTOR_pathfinding_ai : MonoBehaviour {

	// Thanks to Brackeys of YouTube for the following :

	// What to move towards?
	public Transform target;

	// How many times a second each second the path will update.
	public float updateRate = 2f;

	Seeker seeker;
	Rigidbody2D rb;

	// The calculated path
	public Path myPath;

	// The AI's speed per minute
	public float moveSpeed = 300f;
	public ForceMode2D fMode; // A way to control the way in which force is applied to the rigidbody. Gives a way to control its movement.

	public bool pathIsEnded = false;

	// Max distance from the AI to a waypoint for it to continue to the next waypoint.
	public float nextWaypointDistanceLimit = 3;

	// The index of the waypoint currently being moved towards.
	int currentWaypoint = 0; 

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			Debug.Log ("No target found");
			return;
		}

		// Start a new path to the Target's position, and return the result to the OnPathComplete method.
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		// Using a Couroutine here because drawing a new path every frame is going to make an unnecessary amount of calls.
		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath (){
		if (target == null) {
			// TODO: Search for a target.
			yield return false; // This is a modification of the original script since return false; wasn't working, hope this goes okay.
		}

		Debug.Log ("Playing UpdatePath couroutine");

		// Start a new path to the Target's position, and return the result to the OnPathComplete method.
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);

		// Loop the Coroutine by calling it again. Should disable this if the NPC isn't supposed to be moving currently.
		StartCoroutine (UpdatePath ());

	}

	public void OnPathComplete (Path newPath){
		Debug.Log ("Got a path. Did it have an error? " + newPath.error);
		if (!newPath.error) {
			myPath = newPath;
			currentWaypoint = 0;
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			currentWaypoint++;
		}
		/*if (target != null && myPath != null) {
			float waypointDistance = Vector3.Distance (transform.position, myPath.vectorPath [currentWaypoint]);
			if (waypointDistance < nextWaypointDistanceLimit) {
				currentWaypoint++;
			}
		}*/
	}

	void FixedUpdate(){
		if (target == null) {
			return;
		}

		// TODO: Show animation of the character looking at the target.

		if (myPath == null) {
			return;
		}

		// If it's reached its final waypoint (aka if the current waypoint index it's on is greater than the amount of waypoints there are), mark that the path has ended.
		if (currentWaypoint >= myPath.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		} 
		pathIsEnded = false;


		// Now find the direction to the next waypoint.
		Vector3 dir = (myPath.vectorPath[currentWaypoint] - transform.position).normalized; // When working with these Vector3's, subtracting like so gets the direction to the target.

		dir *= moveSpeed * Time.fixedDeltaTime; // fixedDeltaTime because it's in fixed update here.

		// Move the AI
		rb.AddForce(dir, fMode); // Move in the direction of the next waypoint using the selected Force Mode.

		float waypointDistance = Vector3.Distance (transform.position, myPath.vectorPath [currentWaypoint]);
		if (waypointDistance < nextWaypointDistanceLimit) {
			currentWaypoint++;
			return;
		}

	}
}
