using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalText : MonoBehaviour {

	//We'll need the text fields 

	Text TextPoints1, TextPoints2, TextDeath1, TextDeath2, TextFlags1, TextFlags2;
	

	// Use this for initialization
	void Start () {
		TextPoints1 = GameObject.Find("TextPoints1").GetComponent<Text>();
		TextPoints2 = GameObject.Find("TextPoints2").GetComponent<Text>();
		TextDeath1 = GameObject.Find("TextDeath1").GetComponent<Text>();
		TextDeath2 = GameObject.Find("TextDeath2").GetComponent<Text>();
		TextFlags1 = GameObject.Find("TextFlags1").GetComponent<Text>();
		TextFlags2 = GameObject.Find("TextFlags2").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
