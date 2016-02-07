using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour {
	static public FollowCam S; // a FollowCam Singleton
	// fields set in the Unity Inspector pane
	public float easing = 0.05f;
	public Vector2 minXY;
	public bool _____________________________;
	// fields set dynamically
	public GameObject poi; // The point of interest
	public float camZ; // The desired Z pos of the camera
	void Awake() {
		S = this;
		camZ = this.transform.position.z;
	}
	void Update () {
		// if there's only one line following an if, it doesn't need braces
		if (poi == null) return; // return if there is no poi
		// Get the position of the poi

//Commented out line below
		// Vector3 destination = poi.transform.position;

// This is where it said void FixedUpdate () {

		Vector3 destination;
		// If there is no poi, return to P:[0,0,0]
		if (poi == null) {
			destination = Vector3.zero;
		} else {
			// Get the position of the poi
			destination = poi.transform.position;
			// If poi is a Projectile, check to see if it's at rest
			if (poi.tag == "Projectile") {
				// if it is sleeping (that is, not moving)
				if ( poi.rigidbody.IsSleeping() ) {
					// return to default view
					poi = null;
					// in the next update
					return;
				}
			}
		}
//End of the last pasted block of code
		// Limit the X & Y to minimum values
		destination.x = Mathf.Max( minXY.x, destination.x );
		destination.y = Mathf.Max( minXY.y, destination.y );
		// Interpolate from the current Camera position toward destination
		destination = Vector3.Lerp(transform.position, destination, easing);
		// Retain a destination.z of camZ
		destination.z = camZ;
		// Set the camera to the destination
		transform.position = destination;
//Ending
	}
}