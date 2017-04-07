using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

	[SerializeField] int ymin;
	[SerializeField] HandController left;
	[SerializeField] HandController right;

	private Vector3 startPos;

	Rigidbody body;

	void Start () {
		// save the starting position to reset if you fall
		startPos = transform.position;	

		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		// reset the position if you fall too far
		if (transform.position.y < ymin) {
			resetPosition ();
		}
			
		// handle climbing
		if (!left.isClimbing () && left.canClimb()) {
			left.setClimbing (true);
			body.useGravity = false;
			body.isKinematic = true;
		}
		if (left.isClimbing () && left.canClimb ()) {
			transform.position += (left.getController ().getPrevLocalPosition () - left.getController ().getLocalPosition ());
		}
		if (left.isClimbing () && !left.canClimb ()) {
			body.velocity = (left.getController ().getPrevLocalPosition () - left.getController ().getLocalPosition ()) / Time.deltaTime;
			left.setClimbing (false);

		}
		if (!right.isClimbing () && right.canClimb()) {
			right.setClimbing (true);
			body.useGravity = false;
			body.isKinematic = true;
		}
		if (right.isClimbing () && right.canClimb ()) {
			transform.position += (right.getController ().getPrevLocalPosition () - right.getController ().getLocalPosition ());
		}
		if (right.isClimbing () && !right.canClimb ()) {
			body.velocity = (right.getController ().getPrevLocalPosition () - right.getController ().getLocalPosition ()) / Time.deltaTime;
			right.setClimbing (false);
		}

		if (!left.isClimbing() && !right.isClimbing()) {
			body.useGravity = true;
			body.isKinematic = false;
		}

	}

	// Reset the body's position to it's starting position
	private void resetPosition() {
		transform.position = startPos;
	}
}		
