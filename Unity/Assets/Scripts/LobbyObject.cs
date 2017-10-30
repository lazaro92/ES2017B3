using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyObject : MonoBehaviour {

	public GameObject chicken;
	public GameObject[] chickens;
	public GameObject[] flags;
	public Text numChickenLabel;
	public Text numFlagsLabel;
	public InputField healthInputField;
	public Toggle limitedAmmoToggle;

	/*
	 * To set number of chickens per Player into globals variables 
	 * Its necessary pass float to make dynamic method
	 */
	public void SetChickenToGamePlay(float value){
		int va = (int)value;
		//this only to show/hide chickens in lobby scene
		if (Globals.numChickens >= value) chickens[va].SetActive(false);
		else chickens[va-1].SetActive(true);
		Globals.numChickens = va;//setting num chickens to gameplay
		numChickenLabel.text = va.ToString(); 
	}

	/*
	 * To set number of flags per Player into globals variables 
	 * Its necessary pass float to make dynamic method
	 */
	public void SetFlagsToGamePlay(float value){
		int va = (int)value;//change to int, because need access idx position in array
		//this only to show/hide chickens in lobby scene
		if (Globals.numFlags >= value) flags[va].SetActive(false);
		else flags[va-1].SetActive(true);
		Globals.numFlags = va;//setting num flags to gameplay
		numFlagsLabel.text = va.ToString();
	}

	/*
	 * To set life Chicken into globals variables 
	 */
	public void setHealthToGamePlay(string action){
		int value = Globals.HEALTHVALUE;
		int total = (action.Equals ("plus")) ? Globals.health + value : Globals.health - value;
		Globals.health = total;
		healthInputField.text = total.ToString ();
	}

	public void setLimitedAmmo(){
		Globals.limitedAmmo = limitedAmmoToggle.isOn; //setting true or false
	}

	// Use this for initialization
	void Start () {
		healthInputField.text = Globals.health.ToString ();
		for(int i = 0; i <= 9; i++){
			chickens[i].SetActive(false);//UI chicken image hide
			flags[i].SetActive(false);//UI flags image hide
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
