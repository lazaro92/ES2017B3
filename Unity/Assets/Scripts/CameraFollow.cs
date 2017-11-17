using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	GameStart gameStartReference;
	// Objecte que volem seguir en aquest cas el jugador
	public GameObject follow;
	// Per tallar la camara a la posició que volem
	public Vector2 minCamPos, maxCamPos;
	// Moviment suavitzat de la camera
	public float smoothTime;
	// Gestió de la velocitat
	private Vector2 velocity;

	// Use this for initialization
	void Start () {
        follow = GameStart.squads[0].First.Value;
	}

	// Update is called once per frame (Update es un bucle)
	void FixedUpdate () {
		float posX = Mathf.SmoothDamp(transform.position.x,
			follow.transform.position.x, ref velocity.x, smoothTime);
		float posY = Mathf.SmoothDamp(transform.position.y,
			follow.transform.position.y, ref velocity.y, smoothTime);
		// Canviem la posicio de la camera (objecte actual "retall de camera")
		transform.position = new Vector3 (
			Mathf.Clamp(posX,minCamPos.x, maxCamPos.x), 
			Mathf.Clamp(posY,minCamPos.y, maxCamPos.x), 
			transform.position.z);

	}

	
	public void setFollower(GameObject c){
        follow = c;
	}
}
