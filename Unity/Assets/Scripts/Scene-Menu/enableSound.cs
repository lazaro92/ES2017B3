using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enableSound: MonoBehaviour {

	public GameObject SoundButton;

	public Sprite yesSound;
	public Sprite nonSound;

	private Image UIImage;

	// Use this for initialization
	void Start () {
		UIImage = SoundButton.GetComponent<Image> ();

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
