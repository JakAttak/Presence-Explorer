using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	public GameObject cube;
	public Color change;

	private GameObject inside;
	private FixedJoint holding;

	private ViveWandController controller;

	void Start () {
		// Initialize our controller as an instance of the ViveWandController class that is attached to
		controller = GetComponent<ViveWandController> ();
	}

	void FixedUpdate () {
		if (controller.trigger_down) {
			cube.GetComponent<Renderer>().material.color = change;	// change the color of the cube when you pull the trigger
		}

		//  pick up an object if the controller is inside it and the trigger is pulled
		if (inside != null && controller.trigger_down) {
			holding = inside.AddComponent<FixedJoint> ();
			holding.connectedBody = GetComponent<Rigidbody> ();
			print ("picked up object");
		}

		if (holding != null && controller.trigger_down) {
			//Object.DestroyImmediate (holding);
			//holding = null;
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
