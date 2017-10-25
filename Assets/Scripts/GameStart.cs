using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
	// player Prefab from inspector
	public GameObject player;
	// array of chickes
	public GameObject[] chickens;
	// actual chicken to move (index of chickens)
	public int actualChicken;

	//chicken numbers
	private int chickenNum = 5;
	private PlayerController playerController;
	private CameraFollow camFollow;


	// Use this for initialization
	void Start () {
		actualChicken = 0;

		chickens = new GameObject[chickenNum];
		for (var i=0; i < chickens.Length; i++) {
			chickens[i] = Instantiate(player, new Vector3(-7.82f + i, -3.0f, 0), Quaternion.identity);
		}
		playerController = chickens[0].GetComponent<PlayerController>();
		playerController.movement = true;

		camFollow = Camera.main.GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.N)){
			playerController.movement = false;

			actualChicken++;
			if (actualChicken >= chickenNum){
				actualChicken = 0;
			}
			playerController = chickens[actualChicken].GetComponent<PlayerController>();
			playerController.movement = true;
			camFollow.setFollower(actualChicken);
		}
	}
}
