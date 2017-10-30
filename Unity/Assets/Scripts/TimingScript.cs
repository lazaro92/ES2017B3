using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimingScript : MonoBehaviour {

	public int endTime = 0;
	private float varTime;

	// Use this for initialization
	void Start () {
		varTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		varTime += Time.deltaTime;
		// Debug.Log ("var: " + varTime); // Veure i entendre el temps
		if (endTime < varTime) {
			varTime %= endTime;
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
