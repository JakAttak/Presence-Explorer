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

	List<GameObject> cars = new List<GameObject>();
	List<float> timePassed = new List<float>();
	[SerializeField]
	float timeToMove = 10;

	void Start() {
		spawnCar();
		spawnCar(0.5f);
	}

	void spawnCar(float percent = 0.0f) {
		cars.Add(createCar());
		timePassed.Add(percent * timeToMove);
	}

	GameObject createCar() {
		GameObject prefab = prefabs[(int) (Random.value * prefabs.Length)];
		GameObject car = Instantiate(prefab, startPos.position, prefab.transform.rotation);
		car.transform.LookAt(endPos);

		return car;
	}

	void FixedUpdate() {
		if (cars != null) {
			for (int i = 0; i < cars.Count; i++) {
				timePassed[i] += Time.deltaTime;
				cars[i].transform.position = Vector3.Lerp (startPos.position, endPos.position, timePassed[i] / timeToMove);

				if (timePassed[i] >= timeToMove) {
					GameObject.DestroyImmediate(cars[i]);
					cars[i] = createCar();
					timePassed[i] = 0;
				}
			}
		}
	}
}
