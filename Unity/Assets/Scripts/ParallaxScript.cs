using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

	public Transform[] backgrounds; //list of all elements background
	private float[] parallaxScales; //proportion of camera movements to move the background
	public float smoothing = 1.3f; //smooth of parallax effect

	private Transform cam; //main camera
	private Vector3 previousCamPos; // pos of camera in the previous frame

	//called before start()
	void Awake(){
		cam = Camera.main.transform;//get camera
	}

	// Use this for initialization
	void Start () {

		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++)
			parallaxScales [i] = backgrounds [i].position.z * -1; //assign corresponding parallax scales

	}

	// Update is called once per frame
	void Update () {

		for(int i = 0; i < backgrounds.Length; i++){
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			float backTargetPosX = backgrounds [i].position.x + parallax;

			//setting a target position of the background
			Vector3 backTargetPos = new Vector3 (backTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			//change the position of background with the target and his current position
			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backTargetPos, smoothing * Time.deltaTime);

		}

		previousCamPos = cam.position;
	}
}
