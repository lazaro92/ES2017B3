using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	private int damage = Globals.GRENADE_DAMAGE; //Damage is for the damage of the player that hit it.
	private float delay = Globals.GRENADE_DELAY; //Seconds when the granade is throw and explode
	private float radius = Globals.GRENADE_RADIUS;
	public float force = 5f;

	public Transform explosionEffect;

	float countdown;
	bool hasExploded = false;
	// Use this for initialization
	void Start ()
	{
		countdown = delay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		countdown -= Time.deltaTime; //Decrease countdown
		if (countdown <= 0f && !hasExploded) {
			Explode ();
			hasExploded = true;
		}
	}

	void Explode()
	{
		//Debug.Log ("Booom!!");
		//Show Effect
		Effect();
		//Get nearby objects in array
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius);

		foreach (Collider2D nearbyCollider2D in colliders) {
			//Debug.Log (nearbyCollider2D.gameObject);
			applyDamage (nearbyCollider2D.gameObject);
		}
		//Remove granade
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0f);
		StartCoroutine(WaitAndDestroy(gameObject,0.7f));


	}

	private IEnumerator WaitAndDestroy(GameObject gameObject, float time){
		yield return new WaitForSeconds(time);
		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
		//Debug.Log (cam.getFollower ().name);
		if (cam.getFollower ().name == "Grenade(Clone)" ) {
			cam.setFollower (cam.getPreviousObject ());
		}
		cam.setPreviousObject (null);
		Destroy(gameObject);
	}

	//Flash effect
	void Effect()
	{
		Transform clone = Instantiate (explosionEffect, transform.position, transform.rotation);
		float size = Random.Range (0.5f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 1f);
	}

	void applyDamage(GameObject nearbyObject)
	{
		switch(nearbyObject.name)
		{
		case "Player(Clone)":
			//Debug.Log ("Player in Granade");
			//Move player by the explosion
//			Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D> ();
//			if (rb != null) {
//				rb.AddForce (Vector2 (2f, 2f));
//			}
			PlayerController player = nearbyObject.GetComponent<PlayerController> ();
			player.decreaseHealth (damage);
			break;
		case "block":
			//Debug.Log ("Block in Granade");
			Explodable _explo = nearbyObject.GetComponent<Explodable> ();
			_explo.explode ();
			break;
		default:
			//Debug.Log ("Default in Granade: " + gameObject.name);
			break;
		}

	}

		
}
