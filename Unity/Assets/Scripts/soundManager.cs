using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

	public static AudioClip damageSound, shootSound, jumpSound, digSound, drySound;
	
	static AudioSource audioSrc;
	private static bool enabledSound;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Inicialitzat el so");
		audioSrc = GetComponent<AudioSource>();

		damageSound = Resources.Load<AudioClip>("Sounds/damage");
		shootSound = Resources.Load<AudioClip>("Sounds/shoot");
		jumpSound = Resources.Load<AudioClip>("Sounds/jump");
		digSound = Resources.Load<AudioClip>("Sounds/dig");
		drySound = Resources.Load<AudioClip>("Sounds/dry");
		enabledSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound(string clip)
	{
		if (!enabledSound)
			return;

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
		enabledSound ^= true;
		Debug.Log ("Canvi de so: " + enabledSound);
	}
}
