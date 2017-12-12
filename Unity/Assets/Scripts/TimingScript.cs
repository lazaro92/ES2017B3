using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour {

	private const int ENDTIME = 0;
	private float varTime;

	private Text txtTime;
	bool skipTurn;

	// Use this for initialization
	void Start () {
		varTime = Globals.TIME_PER_TURN;
		skipTurn = Globals.skipTurn;
		txtTime = GameObject.Find("txtTime").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		varTime -= Time.deltaTime;
		txtTime.text = ((int) varTime).ToString();
		if (ENDTIME > varTime) {
            varTime = Globals.TIME_PER_TURN;

			Globals.remainingShots = Globals.SHOTS_PER_TURN;
			Globals.remainingGrenades = Globals.GRENADE_THROW_PER_TURN;

			Globals.changeTurn = true;
		}
	}

	void Pause () {
		Time.timeScale = 0;
	}

	void Resume () {
		Time.timeScale = 1;
	}
}
