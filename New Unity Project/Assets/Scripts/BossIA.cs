using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour {

	public Transform bossHome;
	private Transform player;
	private Vector3 positionPlayerLost;
	private Vector3 positionPlayerFind;
	private Transform boss;

	public float speed;
	private float startTime;
	private float journeyLenght;

	public bool lostPlayer = true;
	public bool canFly = false;

	// Use this for initialization
	void Start () {

		boss = GetComponent<Transform> ();
		bossHome = boss.transform.parent;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		positionPlayerLost = bossHome.position;
		BackToHome ();

		
	}
	
	// Update is called once per frame
	void Update () {

		if (canFly)
		if (lostPlayer) {
			float dist = (Time.time - startTime) * speed;
			float journey = dist / journeyLenght;
		
			if (boss.position == bossHome.position)
				canFly = false;

			boss.position = Vector3.Lerp (positionPlayerLost, bossHome.position, journey);
		} else { //Ainda esta perseguindo jogador
			boss.position = Vector3.Lerp (boss.position, player.position, 0.05f);
		}
	}

	public void BackToHome()
	{
		startTime = Time.time;
		positionPlayerLost = boss.position;
		journeyLenght = Vector3.Distance (positionPlayerLost, bossHome.position);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			lostPlayer = false;

			Debug.Log ("Player perdeu vida");
		}

			

		
	}
}
