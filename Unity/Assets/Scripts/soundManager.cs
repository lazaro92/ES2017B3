using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

	public static AudioClip damageSound, shootSound, jumpSound, digSound;
	
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () 
	{
		audioSrc = GetComponent<AudioSource>();

		damageSound = Resources.Load<AudioClip>("Sounds/damage");
		shootSound = Resources.Load<AudioClip>("Sounds/shoot");
		jumpSound = Resources.Load<AudioClip>("Sounds/jump");
		digSound = Resources.Load<AudioClip>("Sounds/dig");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound(string clip)
	{
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
		}
	}
}
