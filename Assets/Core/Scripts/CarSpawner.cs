using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

	[SerializeField]
	Transform startPos;

	[SerializeField]
	Transform endPos;

	[SerializeField]
	GameObject prefab;

	GameObject car;
	float timePassed = 0;
	[SerializeField]
	float timeToMove = 10;

	void Start() {
		spawnCar();
	}

	void spawnCar() {
		car = Instantiate(prefab, startPos.position, prefab.transform.rotation);
	}

	void FixedUpdate() {
		if (car != null) {
			timePassed += Time.deltaTime;
			car.transform.position = Vector3.Lerp (startPos.position, endPos.position, timePassed / timeToMove);

			if (timePassed >= timeToMove) {
				timePassed = 0;
			}
		}
	}
}
