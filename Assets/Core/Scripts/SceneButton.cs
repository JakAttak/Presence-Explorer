using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour {

	[SerializeField] string sceneName;

	private Color col;

	void Start() {
		col = GetComponent<Renderer> ().material.color;
	}

	// Do stuff when pressed
	private void OnTriggerEnter(Collider coll) {
		GetComponent<Renderer> ().material.color = Color.white;
	}

	private void OnTriggerExit(Collider coll) {
		GetComponent<Renderer> ().material.color = col;
		switchScene ();
	}

	// switch to the set scene
	private void switchScene() {
		SceneManager.LoadScene (sceneName);
	}
}
