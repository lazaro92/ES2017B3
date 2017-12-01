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
	public Slider chickenSlider;
	public Slider flagsSlider;
	/*
	 * To set number of chickens per Player into globals variables 
	 * Its necessary pass float to make dynamic method
	 */
	public void SetChickenToGamePlay(float value){
		int va = (int)value;
		int temp = (Globals.numChickens == 0) ? 1: Globals.numChickens;
		//this only to show/hide chickens in lobby scene
		if (Globals.numChickens > value) {
			for (int i = Globals.numChickens; i > va; i--) chickens[i-1].SetActive (false);
		} else {
			for (int i = temp; i <= va; i++) chickens[i-1].SetActive(true);//UI chicken image hide
		}
		Globals.numChickens = va;//setting num chickens to gameplay
		numChickenLabel.text = va.ToString(); 

	}

	/*
	 * To set number of flags per Player into globals variables 
	 * Its necessary pass float to make dynamic method
	 */
	public void SetFlagsToGamePlay(float value){
		int va = (int)value;//change to int, because need access idx position in array
		int temp = (Globals.numFlags == 0) ? 1: Globals.numFlags;
		if (Globals.numFlags > value) {
			for (int i = Globals.numFlags; i > va; i--) flags[i-1].SetActive (false);
		} else {
			for (int i = temp; i <= va; i++) flags[i-1].SetActive(true);//UI chicken image hide
		}
		Globals.numFlags = va;//setting num flags to gameplay
		numFlagsLabel.text = va.ToString();
	}

	/*
	 * To set life Chicken into globals variables 
	 */
	public void setHealthToGamePlay(string action){
		int value = Globals.HEALTH;
		int total = (action.Equals ("plus")) ? Globals.HEALTH + value : Globals.HEALTH - value;
		Globals.HEALTH = total;
		healthInputField.text = total.ToString ();
	}

	public void setLimitedAmmo(){
		Globals.limitedAmmo = limitedAmmoToggle.isOn; //setting true or false
	}

	// Use this for initialization
	void Start () {
		healthInputField.text = Globals.HEALTH.ToString ();
		for(int i = Globals.numChickens; i <= 9; i++) chickens[i].SetActive(false);//UI chicken image hide
		for(int i = Globals.numFlags; i <= 9; i++) flags[i].SetActive(false);//UI flags image hide
		chickenSlider.value = Globals.numChickens;
		flagsSlider.value = Globals.numFlags;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
