using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour {

	public static AudioClip damageSound, shootSound, jumpSound, digSound, drySound;
	
	static AudioSource audioSrc;

	private Image UIImage;
	public Sprite nonSound;
	public Sprite yesSound;

	// Use this for initialization
	void Start () 
	{
		audioSrc = GetComponent<AudioSource>();

		damageSound = Resources.Load<AudioClip>("Sounds/damage");
		shootSound = Resources.Load<AudioClip>("Sounds/shoot");
		jumpSound = Resources.Load<AudioClip>("Sounds/jump");
		digSound = Resources.Load<AudioClip>("Sounds/dig");
		drySound = Resources.Load<AudioClip>("Sounds/dry");

		UIImage = GameObject.Find("sound").GetComponent<Image>();
		showSprite ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("m"))
			switchSound ();
	}

	public static void PlaySound(string clip)
	{
		if (!Globals.enabledSound) return;
		
		switch(clip)
		{
			case "damage":
				audioSrc.PlayOneShot(damageSound);
				break;
			case "shoot":
				audioSrc.PlayOneShot(shootSound);
				break;
			case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
			case "dig":
                audioSrc.PlayOneShot(digSound);
                break;
			case "dry":
				audioSrc.PlayOneShot(drySound);
				break;
		}
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
