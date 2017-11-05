using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour {

	private const int ENDTIME = 0;
	private float varTime;

	private Text txtTime;

	// Use this for initialization
	void Start () {
		varTime = 60;
		txtTime = GameObject.Find("txtTime").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		varTime -= Time.deltaTime;
		txtTime.text = ((int) varTime).ToString();
		// Debug.Log ("var: " + varTime); // Veure i entendre el temps
		if (ENDTIME > varTime) {
			// varTime %= endTime;
			//Debug.Log ("Prova"); // Quan fa la crida
			SceneManager.LoadScene ("FinalScene");
		}
	}

	void Pause () {
		Time.timeScale = 0;
	}

	void Resume () {
		Time.timeScale = 1;
	}
}
