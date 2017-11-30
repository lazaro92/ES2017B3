using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour {

	private const int ENDTIME = 0;
	private float varTime;

	private Text txtTime;
	Image bar1, bar2;
	bool skipTurn;

	// Use this for initialization
	void Start () {
		varTime = Globals.TIME_PER_TURN;
		skipTurn = Globals.skipTurn;
		txtTime = GameObject.Find("txtTime").GetComponent<Text>();
		bar1 = GameObject.Find("team1_bar").GetComponent<Image>();
		bar2 = GameObject.Find("team2_bar").GetComponent<Image>();
		bar1.fillAmount = 0; 
		bar2.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		varTime -= Time.deltaTime;
		txtTime.text = ((int) varTime).ToString();
		//skipTurn = Globals.skipTurn;
		/*if (skipTurn)
		{
			skipTurn = false;
			Globals.skipTurn = false;
			varTime = 2;
		}*/
		if (ENDTIME > varTime) {
            varTime = Globals.TIME_PER_TURN;
			Globals.points[GameStart.currentTeam] += Globals.accPoints;

			bar1.fillAmount = GameStart.pointProportion(1);

			bar2.fillAmount = GameStart.pointProportion(0);

			Globals.accPoints = 0;
			Globals.remainingShots = Globals.SHOTS_PER_TURN;

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
