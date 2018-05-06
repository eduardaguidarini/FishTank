using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{

	//Criar um prefab pro objeto
	public GameObject goalPrefab;
	public int nAlfredos;
	public GameObject alfredoPrefab;
	public int nFilipes;
	public GameObject filipePrefab;
	public int nJorges;
	public GameObject jorgePrefab;
	public int nRicardos;
	public GameObject ricardoPrefab;

	public GameObject brutus;

	public static int tankSize = 5;
	public static GameObject[] allFish;

	public static Vector3 goalPos = Vector3.zero;
	public static GameObject[] goalPrefabPositions = new GameObject[4];

	// Use this for initialization
	void Start ()
	{
		int numFish = nAlfredos + nFilipes + nJorges + nRicardos + 1;
		allFish = new GameObject[numFish];

		GameObject goalPositions = new GameObject ("Goal Positions");
		GameObject flockA = new GameObject ("Flock Alfredo");
		GameObject flockB = new GameObject ("Flock Filipe"); 
		GameObject flockC = new GameObject ("Flock Jorge"); 
		GameObject flockD = new GameObject ("Flock Ricardo");

		for (int i = 0; i < 4; i++) {
			goalPrefabPositions [i] = (GameObject)Instantiate (goalPrefab, newRandomPosition (), Quaternion.identity, goalPositions.transform);
		}

		for (int i = 0; i < nAlfredos; i++) {
			//tem que trocara qui depois pra vector2
			Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize),
				              Random.Range (-tankSize, tankSize),
				              0);
			allFish [i] = (GameObject)Instantiate (alfredoPrefab, pos, Quaternion.identity, flockA.transform);
		}

		for (int i = 0; i < nFilipes; i++) {
			Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize),
				              Random.Range (-tankSize, tankSize),
				              0);
			allFish [i + nAlfredos] = (GameObject)Instantiate (filipePrefab, pos, Quaternion.identity, flockB.transform);
		}

		for (int i = 0; i < nJorges; i++) {
			Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize),
				              Random.Range (-tankSize, tankSize),
				              0);
			allFish [i + nAlfredos + nFilipes] = (GameObject)Instantiate (jorgePrefab, pos, Quaternion.identity, flockC.transform);
		}

		for (int i = 0; i < nRicardos; i++) {
			Vector3 pos = new Vector3 (Random.Range (-tankSize, tankSize),
				              Random.Range (-tankSize, tankSize),
				              0);
			allFish [i + nAlfredos + nFilipes + nJorges] = (GameObject)Instantiate (ricardoPrefab, pos, Quaternion.identity, flockD.transform);
		}

		allFish [nAlfredos + nFilipes + nJorges + nRicardos] = brutus;
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < 4; i++) {
			if (Random.Range (0, 10000) < 50) {
				goalPrefabPositions [i].transform.position = newRandomPosition ();
			}
		}
	}

	Vector3 newRandomPosition ()
	{
		return new Vector3 (Random.Range (-tankSize, tankSize), Random.Range (-tankSize, tankSize), 0);
	}

	public static void eatFish (GameObject prey)
	{
		for (int i = 0; i < allFish.Length; i++) {
			if (allFish [i].Equals (prey)) {
				System.Collections.Generic.List<GameObject> fishes = new System.Collections.Generic.List<GameObject> (allFish);
				fishes.Remove (prey);
				GameObject.Destroy (prey);
				allFish = fishes.ToArray ();
				break;
			}
		}
	}
}
