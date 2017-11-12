using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Comprovem la colisio contra el terra
public class CheckGround : MonoBehaviour {

	// Per saber si estem ground o no
	private PlayerController player;
	// Controla la velocidad
	//private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		// Per buscar dins del pare. Busquem script
		player = GetComponentInParent<PlayerController>();
	}

	//void OnCollisionEnter2D(Collision2D collision){
		//if (collision.gameObject.tag == "Platform")
		//{
			//rb2d.velocity = new Vector3 (0f, 0f, 0f);
			//player.transform.parent = collision.transform;
			//player.grounded = true;
		//}
	//}

	// Si choquem contra algu
	void OnCollisionStay2D(Collision2D collision)
	{
		// Si te aquesta etiqueta doncs es terra
		if(collision.gameObject.tag == "Ground")
		{
			player.grounded = true;
		}

		if (collision.gameObject.tag == "Platform")
		{
			player.transform.parent = collision.transform;
			player.grounded = true;
		}

	}

	// Si no choquem contra algu. Per sortir
	void OnCollisionExit2D(Collision2D collision)
	{
		// Sino choquem contra l'etiqueta no es ground
		if (collision.gameObject.tag == "Ground")
		{
			player.grounded = false;
		}

		if (collision.gameObject.tag == "Platform")
		{
			player.transform.parent = null;
			player.grounded = false;
		}
	}
}

