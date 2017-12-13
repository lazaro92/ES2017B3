using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundSetting : MonoBehaviour {

	private Image UIImage;
	public Sprite nonSound;
	public Sprite yesSound;

	// Use this for initialization
	void Start () {
		// SoundButton
		UIImage = GameObject.Find("SoundButton").GetComponent<Image>();
		showSprite ();
	}
	

	public void switchSound ()
	{
		Globals.enabledSound ^= true;
		showSprite ();
	}

	private void showSprite ()
	{
		if (Globals.enabledSound)
			UIImage.sprite = yesSound;
		else
			UIImage.sprite = nonSound;
	}
}
