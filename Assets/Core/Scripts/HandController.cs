using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour {

	public GameObject cube;
	public Color change;

	[SerializeField] GameObject hand;

	private GameObject inside;
	private FixedJoint holding;

	private List<Vector3> holding_positions = new List<Vector3>();

	private ViveWandController controller;

	void Start () {
		// Initialize our controller as an instance of the ViveWandController class that is attached to
		controller = GetComponent<ViveWandController> ();
	}

	void FixedUpdate () {
		if (controller.trigger_down) {
			cube.GetComponent<Renderer>().material.color = change;	// change the color of the cube when you pull the trigger
		}

		//  pick up an object if the controller is inside it and the trigger is pulled and it is not already being held
		if (holding == null && inside != null && !inside.GetComponent<FixedJoint>() && controller.trigger_pressed) {
			holding = inside.AddComponent<FixedJoint> ();
			holding.connectedBody = hand.GetComponent<Rigidbody> ();
			print ("picked up object");

			holding_positions.Clear();
		}

		if (holding != null) {
			// fill / update position list
			if (holding_positions.Count < 10) {
				holding_positions.Add (inside.GetComponent<Rigidbody> ().position);
				print ("Logged position: " + holding_positions [holding_positions.Count - 1]);
			} else {
				for (int i = 1; i < holding_positions.Count; i++) {
					holding_positions [i - 1] = holding_positions [i];
				}
				holding_positions [holding_positions.Count - 1] = inside.GetComponent<Rigidbody> ().position;
			}

			// throw the object when trigger is released
			if (controller.trigger_up) {
				Object.DestroyImmediate (holding); // destroy the joint holding the object to the hand
				if (holding_positions.Count > 0) {
					inside.GetComponent<Rigidbody> ().velocity =  (holding_positions [holding_positions.Count - 1] - holding_positions [0]) * 10; // set the objects velocity to the average direction it traveled over the tracked frames, so that it will move with a velocity that matches what the player applied to it
				}

				print (inside.GetComponent<Rigidbody> ().velocity);
				print (hand.GetComponent<Rigidbody> ().velocity);
				holding = null; // set our variables to null because we are no longer holding anything
				inside = null;
			}
		}
			
	}

	// Set which object the controller is currently inside of
	private void OnTriggerEnter(Collider coll) {
		inside = coll.gameObject;
	}

	private void OnTriggerExit(Collider coll) {
		inside = null;
	}
}
