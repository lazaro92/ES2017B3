using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
	// player Prefab from inspector
	public GameObject player;
	// array of chickes
	public List<GameObject> chickens;
	// actual chicken to move (index of chickens)
	public int actualChicken;

	//chicken numbers
	private int chickenNum = Globals.numChickens;//num of chickens seleted in lobby scene
	private PlayerController playerController;
	private CameraFollow camFollow;


	// Use this for initialization
	void Start () {
		actualChicken = 0;

		chickens = new List<GameObject>();
		for (var i = 0; i < chickenNum; i++) {
			chickens.Add (Instantiate (player, new Vector3 (-7.82f + i, -3.0f, 0), Quaternion.identity));
		}
		playerController = chickens[0].GetComponent<PlayerController>();
		//playerController.setMovement(true);

		camFollow = Camera.main.GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		playerController.setMovement(true);
		if (Input.GetKeyDown(KeyCode.N)){
			playerController.setMovement(false);

			actualChicken++;
			if (actualChicken >= chickenNum){
				actualChicken = 0;
			}
			playerController = chickens[actualChicken].GetComponent<PlayerController>();
			playerController.setMovement(true);
			camFollow.setFollower(actualChicken);
		}
	}
}
