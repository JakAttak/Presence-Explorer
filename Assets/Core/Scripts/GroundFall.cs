using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour {

	[SerializeField] GameObject groundParent;	// will hold the game object set to parent everything that is part of the ground and must sink

	[SerializeField] float fallRate; // how fast it should fall

	private bool falling = false; 

	void Update () {
		if (falling) {
			groundParent.transform.position = new Vector3(groundParent.transform.position.x, groundParent.transform.position.y - fallRate * Time.deltaTime, groundParent.transform.position.z); // shift the ground down fallRate units
		}

		// for testing purposes:
		if (Input.GetButtonUp("Fire1")) { // Fire1 = left ctrl or mouse click
			toggleFall();
		}
	}

	void startFall() {
		falling = true;
	}

	void endFall() {
		falling = false;
	}

	void toggleFall() {
		falling = !falling;
	}
}
