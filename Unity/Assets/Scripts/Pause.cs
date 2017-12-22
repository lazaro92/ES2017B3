using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour {

	bool active;
	Canvas canvas;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false; // Comença desactivat
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			active = !active;
			canvas.enabled = active; // Activació canvas
			Time.timeScale = (active) ? 0 : 1f; // Bloquegem el joc si és 0 tot bloquejat sino 1 desbloqueig
		}
	}

	public void onExit(){
		Time.timeScale = 1f;
        SceneManager.LoadScene("FinalScene");
    }
}
