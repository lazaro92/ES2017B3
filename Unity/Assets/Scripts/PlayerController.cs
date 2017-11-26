using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	// La força en el personatge horitzontalment perque es mogui
	public float speed = 2f;
	// La maxim velocitat del personatge
	public float maxSpeed = 5f;
	// Bool per saber si toquem terra
	public bool grounded;
	// Bool per saber si és mort
	public bool dead;
	// Bool per saber equip
	public bool teamRed;
	// Bool per saber equip
	public bool teamBlue;
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
	private bool movement = false;
	// Health
	public float health = Globals.HEALTHVALUE;
	// Canviar el color del personatge
	private SpriteRenderer spr;

	//Arm
	private GameObject arm;//Persistent
	private ArmRotation rotation;

	//TODO Albert
	private GameObject goPistol;
	private Pistol pistol;

	private GameObject goPickaxe;
	private Pickaxe pickaxe;

	// Canvas HUD i text
	private Canvas HUD_player;
	private Text txtMagazine;

	[Header("Unity Stuff")]
	// Asignar la barra de vida al player
	public Image healthBar;

	// Use this for initialization
	void Start () {
		// Obtenim el component (del player)
		rb2d = GetComponent<Rigidbody2D>();
		// Obtenim el component animador
		anim = GetComponent<Animator>();
		spr = GetComponent<SpriteRenderer>();
		//Arm 
		arm = transform.Find("Arm").gameObject;
		rotation = arm.GetComponent<ArmRotation>();

		goPistol = transform.Find ("Arm/Pistol").gameObject;
		pistol = goPistol.GetComponent<Pistol>();

		goPickaxe = transform.Find ("Arm/Pickaxe").gameObject; //TODO: Albert
		pickaxe = goPickaxe.GetComponent<Pickaxe>();

		goPickaxe.active = false;

		if (gameObject.tag == "team1") {
			teamRed = true;
		} else {
			teamBlue = true;
		}

	}

	// Problemes amb fisiques
	void Update () {
		// Munició per cada pollo
		if (pistol.getInfiniteAmmo () != true) {
			// Mirem la munició
			this.GetComponentInChildren<Canvas> ().GetComponentInChildren<Text> ().text = "Bullets: " + pistol.getMagazine ().ToString ();
			//this.GetComponentInChildren<Canvas> ().transform.Find("txtMagazine").GetComponent<Text>().text = "Bullets: " + pistol.getMagazine ().ToString ();
		} else {
			this.GetComponentInChildren<Canvas> ().GetComponentInChildren<Text> ().text = "Bullets: ∞";
		}
		// HUD_player
		HUD_player = this.GetComponentInChildren<Canvas>();
		// Assignem la velocitat del personatge. Buscant el valor positiu
		anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
		// Assignem si toquem el terra
		anim.SetBool("Grounded", grounded);
		// Asigenm si està mort
		anim.SetBool("Dead", dead);
		// Asigenm si és Red
		anim.SetBool("TeamRed", teamRed);
		// Asigenm si és Red
		anim.SetBool("TeamBlue", teamBlue);

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
		if (movement) {
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				goPistol.active = true;
				goPickaxe.active = false;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				goPickaxe.active =true;
				goPistol.active =false;
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

		if (!movement) {
			h = 0;
		}
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
			// Canviem la posició del HUD per veures bé
			HUD_player.transform.localScale = new Vector3 (0.03f, 0.03f, 0.03f);
			//Arm 
			rotation.flip (h);
			deactivateArm();


		}
		// Vamos a la izq. i girem el personatge mirem la izq.
		if (h < -0.1f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
			// Canviem la posició del HUD per veures bé
			HUD_player.transform.localScale = new Vector3 (-0.03f, 0.03f, 0.03f);
			//Arm 
			rotation.flip (h);
			deactivateArm();
		}
		if(0.1f > h && h > -0.1f && !dead){
			//Debug.Log ("Arm is active!!!");
			if (movement) {
				activateArm ();
			}
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
	/*
	void OnBecameInvisible()
	{
		transform.position = new Vector3(-1, 0, 0);
	}
	*/

	public void setMovement(bool mov){
		//Debug.Log (mov);
		this.movement = mov;
        //Enable rotation to player
        if (this.rotation == null || this.pistol == null) // ALERTA (más de lo mismo)
        {
            arm = transform.Find("Arm").gameObject;
            rotation = arm.GetComponent<ArmRotation>();

            goPistol = transform.Find("Arm/Pistol").gameObject;
            pistol = goPistol.GetComponent<Pistol>();

			goPickaxe = transform.Find ("Arm/Pickaxe").gameObject; //TODO: Albert
			pickaxe = goPickaxe.GetComponent<Pickaxe>();
        }
		this.rotation.setEnabledRotation(movement);
		this.pistol.setEnabledShoot (movement);//TODO Albert
		this.pickaxe.setEnabledShoot (movement);
	}

	/*
 +	* Decrease health of the player
 +	*/
	public void decreaseHealth(int health){
		if (this.health > health) {
			this.health -= health;
			Color color = new Color (236/255f, 137/255f, 137/255f);
			spr.color = color;
			rotation.colorDamage();
			StartCoroutine("waitSecondsHealth");
			healthBar.fillAmount = this.health / Globals.HEALTHVALUE; // Restem la barra de vida 
		} else {
			this.health = 0;
			healthBar.fillAmount = this.health / Globals.HEALTHVALUE; // Restema la barra de vida
			dead = true;
			deactivateArm();
			Destroy (this.goPistol); //TODO: Albert
			Destroy (this.goPickaxe);
			Destroy (this.arm);
            GameStart.deleteChicken(this.gameObject);
			StartCoroutine("waitSecondsDead");
		}
	}

	public void selectWeapon(KeyCode key) {
		
	}

	// Espera 2 segons abans d'eliminar el pollastre
	IEnumerator waitSecondsDead(){
		yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
	}

	// Espera 1 segons 
	IEnumerator waitSecondsHealth(){
		yield return new WaitForSeconds(0.4f);
		spr.color = Color.white;
		if (rotation != null) {
			rotation.resetColor();
		}
	}

	/**
	 * Deactivate arm
	 */
	private void deactivateArm(){
		rotation.setEnabledRotation (false);
		//TODO Albert
		pistol.setEnabledShoot (false);
		pickaxe.setEnabledShoot (false);
		arm.SetActive (false);
	}
	/**
	 * Activate arm
	 */
	private void activateArm(){
		rotation.setEnabledRotation (true);
		pistol.setEnabledShoot(true);
		pistol.setEnabledShoot (true);
		arm.SetActive (true);
	}
		
}

