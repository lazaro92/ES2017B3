using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void switchSound ()
	{
		Globals.enabledSound ^= true;
		Debug.Log ("Canvi de so: " + Globals.enabledSound);
	}
}
