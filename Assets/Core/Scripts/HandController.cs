using UnityEngine;
using System.Collections;

[RequireComponent ViveWandController];

public class HandController : MonoBehaviour {

	public GameObject cube;
	public Color change;

	private GameObject inside;
	private FixedJoint holding;

	private ViveWandController controller;

	void Start () {
		controller = GetComponent<ViveWandController> ();
	}

	void FixedUpdate () {
		if (controller.trigger_down) {
			cube.GetComponent<Renderer>().material.color = change;
		}

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

	private void OnTriggerEnter(Collider coll) {
		inside = coll.gameObject;
	}

	private void OnTriggerExit(Collider coll) {
		inside = null;
	}
}
