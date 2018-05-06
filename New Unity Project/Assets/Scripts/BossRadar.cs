using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRadar : MonoBehaviour {

	// Use this for initialization

	private BossIA script;
	void Start () {

		//pega script do componente pai, radar é filho do boss
		script = (BossIA)GetComponentInParent (typeof(BossIA));
		
	}
	//quando jogador colide com o radar
	void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Player"){
				//script.GoToPlayer();
				script.lostPlayer = false;
				script.canFly = true;
			}
		}
	//quando jogador sai do radar
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player") {
			script.BackToHome ();
			script.lostPlayer = true;
			script.canFly = true;
		}
	}






	
	// Update is called once per frame
	void Update () {
		
	}
}
