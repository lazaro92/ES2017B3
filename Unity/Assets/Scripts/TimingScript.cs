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

	// Use this for initialization
	void Start () {
		varTime = 5;
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
		if (ENDTIME > varTime) {
            varTime = 5;
			GameStart.points[GameStart.currentTeam] += Globals.accPoints;
			if(GameStart.currentTeam == 0)
				bar1.fillAmount = GameStart.points[GameStart.currentTeam] / Globals.MAX_POINTS;
			else
				bar2.fillAmount = GameStart.points[GameStart.currentTeam] / Globals.MAX_POINTS;

			Globals.accPoints = 0;
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
