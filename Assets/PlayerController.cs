using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	// La força en el personatge horitzontalment perque es mogui
	public float speed = 2f;
	// La maxim velocitat del personatge
	public float maxSpeed = 5f;
	// Bool per saber si toquem terra
	public bool grounded;
	// La força de salt
	public float jumpPower = 6.5f;

	// Rigibody 2D del personatge
	private Rigidbody2D rb2d;
	// Animator del personatge per utilitzar les relacions creades a l'animador
	private Animator anim;
	// Bool salta
	private bool jump;
	// Doble salto
	private bool doubleJump;
	// Control moviment
	public bool movement = false;




	// Use this for initialization
	void Start () {
		// Obtenim el component (del player)
		rb2d = GetComponent<Rigidbody2D>();
		// Obtenim el component animador
		anim = GetComponent<Animator>();
	}

	// Problemes amb fisiques
	void Update () {
		// Asignem la velocitat del personatge. Buscan el valor positiu
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
		// Asigenm si toquem el terra
		anim.SetBool("Grounded", grounded);

		// Comprovem si estem al terra (salt de precaució)
		if (grounded) {
			doubleJump = true;
		}

		// Per detectar la tecla per saltar
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w")) && movement)
		{
			// Si tocamos el suelo
			if (grounded) {
				jump = true;
				doubleJump = true;
			} else if (doubleJump) { // Si tenim el doble salt
				jump = true;
				doubleJump = false;
			}
		}

	}

	// Evitem problemes amb fisiques (funciona per frames)
	private void FixedUpdate()
	{
		// Rectificamos la velocidad con la fricción
		Vector3 fixedVelocity = rb2d.velocity;
		// Reducimos la velocidad
		fixedVelocity.x *= 0.75f;

		if (grounded)
		{
			rb2d.velocity = fixedVelocity;
		}

		// Detectem quan apretem l'eix horizontal -1 izq, 1 derecha (direcció)
		float h = Input.GetAxis("Horizontal");

		if (!movement)
			h = 0;

		// Apliquem força fisica al rigidbody al personatge
		rb2d.AddForce(Vector2.right * speed * h);


		// Si superem la velocitat maxima x a la dreta sino a la esquerra (control maxima velocitat)
		float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed); ;
		rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

		// Vamos a la derecha
		if (h > 0.1f)
		{
			// Assignem nou vector
			transform.localScale = new Vector3(1f, 1f, 1f);
		}

		// Vamos a la izq. i girem el personatge mirem la izq.
		if (h < -0.1f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
			
		if (jump)
		{
			//Para que cancele la velocidad vertical (controlamos el impulso)
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			jump = false;
		}
			
	}

	// Torna a sortir el personatge al mateix lloc si sortim de l'escena.
	void OnBecameInvisible()
	{
		transform.position = new Vector3(-1, 0, 0);
	}
		
}

