using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

	[SerializeField]
	Transform startPos;

	[SerializeField]
	Transform endPos;

	[SerializeField]
	GameObject[] prefabs;

	// Lists to hold the information about our cars
	List<GameObject> cars = new List<GameObject>();
	List<float> timePassed = new List<float>();
	[SerializeField]
	float timeToMove = 10;

	void Start() {
		// for testing - spawns two cars, one at the 0 position and one halfway through the path
		spawnCar();
		spawnCar(0.5f);
	}

	// Spawns a car a percentage of the way through the path
	void spawnCar(float percent = 0.0f) {
		cars.Add(createCar());
		timePassed.Add(percent * timeToMove);
	}

	// Creates a new car instance randomly chosen from the list of choices, points it in the right direction, and returns it
	GameObject createCar() {
		GameObject prefab = prefabs[(int) (Random.value * prefabs.Length)];
		GameObject car = Instantiate(prefab, startPos.position, prefab.transform.rotation);
		car.transform.LookAt(endPos);

		return car;
	}

	void FixedUpdate() {
		if (cars != null) {
			for (int i = 0; i < cars.Count; i++) {
				// Move the car
				timePassed[i] += Time.deltaTime;
				cars[i].transform.position = Vector3.Lerp (startPos.position, endPos.position, timePassed[i] / timeToMove);

				// if the car has reached the end, it is destroyed and a new one is created at the start to replace it
				if (timePassed[i] >= timeToMove) {
					GameObject.DestroyImmediate(cars[i]);
					cars[i] = createCar();
					timePassed[i] = 0;
				}
			}
		}
	}
}
