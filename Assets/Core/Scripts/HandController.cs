using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour {
	public Color change;

	[SerializeField] GameObject hand;
	[SerializeField] GameObject body;
	[SerializeField] GameObject ball;

	private LaserPointerModified laser_pointer;

	private GameObject inside;
	private FixedJoint holding;
	private bool climbing = false;

	private List<Vector3> holding_positions = new List<Vector3>();

	private ViveWandController controller;

	void Start () {
		// Initialize our controller as an instance of the ViveWandController class that is attached to
		controller = GetComponent<ViveWandController> ();

		// Initialize our laser pointer
		laser_pointer = GetComponent<LaserPointerModified>();
		laser_pointer.color = change;

		// Color our hand properly
		hand.GetComponent<Renderer> ().material.color = change;
	}

	void FixedUpdate () {
		//  pick up an object if the controller is inside it and it is tagged as grabbable and the trigger is pulled and it is not already being held
		if (holding == null && inside != null && inside.tag.Contains("Grabbable") && !inside.GetComponent<FixedJoint>() && controller.getTriggerPressed()) {
			holding = inside.AddComponent<FixedJoint> ();
			holding.connectedBody = hand.GetComponent<Rigidbody> ();

			holding_positions.Clear();
		}

		if (holding != null) {
			// fill / update position list
			if (holding_positions.Count < 5) {
				holding_positions.Add (inside.GetComponent<Rigidbody> ().position);
			} else {
				for (int i = 1; i < holding_positions.Count; i++) {
					holding_positions [i - 1] = holding_positions [i];
				}
				holding_positions [holding_positions.Count - 1] = inside.GetComponent<Rigidbody> ().position;
			}

			// throw the object when trigger is released
			if (controller.getTriggerUp() || !controller.getTriggerPressed()) {
				Object.DestroyImmediate (holding); // destroy the joint holding the object to the hand
				if (holding_positions.Count > 0) {
					inside.GetComponent<Rigidbody> ().velocity = (holding_positions [holding_positions.Count - 1] - holding_positions [0]) * 30; // set the objects velocity to the average direction it traveled over the tracked frames, so that it will move with a velocity that matches what the player applied to it
				}

				holding = null; // set our variables to null because we are no longer holding anything
				inside = null;
			}
		}

		// spawn a ball if the grip button is pressed
		if (!holding && controller.getGripUp()) { 
			GameObject newBall = Instantiate(ball, hand.transform.position, Quaternion.identity);
			newBall.GetComponent<Renderer> ().material.color = change;
		}

		// toggle the laser pointer on and off
		if (controller.getTrackpadDown()) {
			laser_pointer.Activate ();
		}
		if (controller.getTrackpadUp()) {
			laser_pointer.DeActivate ();
		}
	}

	// Set which object the controller is currently inside of
	private void OnTriggerEnter(Collider coll) {
		inside = coll.gameObject;
	}

	private void OnTriggerExit(Collider coll) {
		inside = null;
	}

	public ViveWandController getController() {
		return controller;
	}

	public bool isClimbing() {
		return climbing;
	}

	public void setClimbing(bool n) {
		climbing = n;
	}

	public bool canClimb() {
		return (controller.getTriggerPressed () && (isClimbing() || (inside != null && inside.tag.Contains ("Handhold"))));
	}
}
