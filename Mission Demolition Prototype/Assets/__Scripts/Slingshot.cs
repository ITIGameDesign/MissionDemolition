using UnityEngine;
using System.Collections;
public class Slingshot : MonoBehaviour {
	static public Slingshot S;

	// fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _____________________________;
	// fields set dynamically
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;


// void Awake May need to be entered before Void Update
	void Awake() {
		// Set the Slingshot singleton S
		S = this;

		Transform launchPointTrans = transform.Find("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive( false );
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter() {
		// print("Slingshot:OnMouseEnter()");
		launchPoint.SetActive( true );
	}

	void OnMouseExit() {
				// print("Slingshot:OnMouseExit()");
				launchPoint.SetActive (false);
		}
	void OnMouseDown() {
			// The player has pressed the mouse button while over Slingshot
			aimingMode = true;
			// Instantiate a Projectile
			projectile = Instantiate( prefabProjectile ) as GameObject;
			// Start it at the launchPoint
			projectile.transform.position = launchPos;
			// Set it to isKinematic for now
			projectile.rigidbody.isKinematic = true;
		}


	void Update() {
		// If Slingshot is not in aimingMode, don't run this code
		if (!aimingMode) return;
		// Get the current mouse position in 2D screen coordinates
		Vector3 mousePos2D = Input.mousePosition;
		// Convert the mouse position to 3D world coordinates
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
		// Find the delta from the launchPos to the mousePos3D
		Vector3 mouseDelta = mousePos3D-launchPos;
		// Limit mouseDelta to the radius of the Slingshot SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}
		// Move the projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;
		if ( Input.GetMouseButtonUp(0) ) {
			// The mouse has been released
			aimingMode = false;
			projectile.rigidbody.isKinematic = false;
			projectile.rigidbody.velocity = -mouseDelta * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
			MissionDemolition.ShotFired();
			
		}
	}



}