using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{

	public float speed = 0.001f;
	public string name;
	public int id;
	float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighbourDistance = 3.0f;

	bool turning = false;

	// Use this for initialization
	void Start ()
	{
		if (id != 4)
			speed = Random.Range (0.5f, 1);	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (id != 4) {
			if (Vector3.Distance (transform.position, Vector3.zero) >= globalFlock.tankSize) {
				turning = true;
			} else
				turning = false;			

			if (turning) {
				Vector3 direction = Vector3.zero - transform.position;
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);

				speed = Random.Range (0.5f, 1);
			} else {

				if (Random.Range (0, 5) < 1)
					ApplyRules ();
			}
			transform.Translate (0, 0, Time.deltaTime * speed);
		}
	}

	void ApplyRules ()
	{
		GameObject[] fishes;
		fishes = globalFlock.allFish;

		Vector3 goalPos = Vector3.zero;
		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.2f;
			
		if (id != 4) {
			goalPos = globalFlock.goalPrefabPositions [id].transform.position;
		}

		float dist;

		int groupSize = 0;
		foreach (GameObject fish in fishes) {
			if (fish != this.gameObject) {
				dist = Vector3.Distance (fish.transform.position, this.transform.position);
				if (id == fish.GetComponent<flock> ().id) {
					if (dist <= neighbourDistance) {
						vcentre += fish.transform.position;
						groupSize++;

						if (dist < 1.0f) {
							vavoid = vavoid + (this.transform.position - fish.transform.position);
						}

						flock anotherFlock = fish.GetComponent<flock> ();
						gSpeed = gSpeed + anotherFlock.speed;

					}
				} else {
					if ((fish.GetComponent<flock> ().id == 4 && dist < 7.0f) || (dist < 2.0f)) {
						vavoid = vavoid + (this.transform.position - fish.transform.position);
					}
				}
			}
		}

		if (groupSize > 0) {
			vcentre = vcentre / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vcentre + vavoid) - transform.position;
			if (direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					rotationSpeed * Time.deltaTime);
		}
	}

}
