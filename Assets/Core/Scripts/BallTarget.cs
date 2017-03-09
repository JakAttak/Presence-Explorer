using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTarget : MonoBehaviour {

	[SerializeField] Color missedCol;
	[SerializeField] Color hitCol;
	[SerializeField] TextMesh scoreText;

	private int score;

	void Start () {
		score = 0;
		updateText ();
		gameObject.GetComponent<Renderer>().material.color = missedCol;

	}

	private void updateText() {
		scoreText.text = "Score: " + score;
	}
	
	// Triggers actions based on if a ball hits it
	private void OnTriggerEnter(Collider coll) {
		if (coll.CompareTag ("Ball")) {
			score += 1;
			updateText ();
			gameObject.GetComponent<Renderer> ().material.color = hitCol;
		}
	}

	private void OnTriggerExit(Collider coll) {
		gameObject.GetComponent<Renderer> ().material.color = missedCol;
	}
}
