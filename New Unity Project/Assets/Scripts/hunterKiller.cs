using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunterKiller : MonoBehaviour
{

	public float hungerTimer = 10;
	float timer;
	float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	Vector3 goalPos;
	public float speed = 3;

	public GameObject prey;
	public GameObject home;

	public AudioSource source;
	public AudioClip biteSFX;

	bool hunting = false;

	// Use this for initialization
	void Start ()
	{
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (globalFlock.allFish.Length > 1) {
			if (timer >= hungerTimer) {
				if (hunting == false) {
					prey = findPrey ();
					hunting = true;
					speed = 5.0f;
				}
				goalPos = prey.transform.position;
			} else {
				speed = 2.0f;
				goalPos = home.transform.position;
			}
		}

		doMovement ();
	}

	void doMovement ()
	{
		float dist;
		Vector3 vcentre = Vector3.zero;

		dist = Vector3.Distance (goalPos, this.transform.position);
		vcentre += goalPos;

		if (dist <= 0.5) {
			doBite ();
		}
		
		Vector3 direction = vcentre - transform.position;
		if (direction != Vector3.zero)
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction),
				rotationSpeed * Time.deltaTime);

		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void doBite ()
	{
		if (hunting) {
			hunting = false;
			timer = 0;
			globalFlock.eatFish (prey);
			source.pitch = Random.Range (0.8f, 1.2f);
			source.PlayOneShot (biteSFX);
		} else {
			speed = 0;
		}
	}

	GameObject findPrey ()
	{
		prey = globalFlock.allFish [Random.Range (0, globalFlock.allFish.Length)];

		while (prey == this.gameObject) {
			prey = globalFlock.allFish [Random.Range (0, globalFlock.allFish.Length)];
		}

		return prey;

	}
}
