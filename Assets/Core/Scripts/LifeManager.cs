using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

	[SerializeField] int ymin;

	private int climbing_holds;

	private Vector3 startPos;

	void Start () {
		// save the starting position to reset if you fall
		startPos = transform.position;	
	}

	void FixedUpdate () {
		// reset the position if you fall too far
		if (transform.position.y < ymin) {
			resetPosition ();
		}
	}

	// Reset the body's position to it's starting position
	private void resetPosition() {
		transform.position = startPos;
	}

	// Handles toggling gravity when climbing
	public void updateHolds(int holds) {
		climbing_holds += holds;

		if (climbing_holds > 0) {
			GetComponent<Rigidbody> ().useGravity = false;
			GetComponent<Rigidbody> ().isKinematic = true;
		} else {
			GetComponent<Rigidbody> ().useGravity = true;
			GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}
