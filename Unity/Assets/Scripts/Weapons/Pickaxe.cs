using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour{

	//Variables
	private int damage = Globals.PICKAXE_DAMAGE; //Damage is for the damage of the player that hit it.
	public LayerMask wantToHit; //Is the layers that we want to hit
	private float digRange = 0.8f; //Range that player can dig and damage

	public Transform DirtDugPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	private bool enabledAction = false;
	Transform firePoint;

	// Use this for initialization
	void Awake()
	{
		firePoint = transform.Find("FirePointPickaxe"); //Bind GameObject with variable
		//If the object is not defined, launch an error
		if (firePoint == null)
		{
			Debug.LogError("No FirePoint");
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Check if fireButton is pressed
		if (Input.GetButtonDown("Fire1") && enabledAction)
		{
			Action ();
		}
	}

	//Function Shoot
	void Action()
	{
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, wantToHit);
		Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.blue);
		//Debug.Log ("Here is the distance: " + hit.distance);
		//Debug.Log ("Here is the DigRange: " + digRange);
		if (hit.collider != null && hit.distance <= digRange)
		{
			if (Time.time >= timeToSpawnEffect){
				Debug.DrawLine(firePointPosition, hit.point, Color.red);
				Debug.Log(hit.collider.name +"with TAG: "+ hit.collider.tag+ " has been shot with damage of " + damage);
				if (hit.collider.name == "Player(Clone)") {
					GameObject hitPlayer = hit.transform.gameObject;
					PlayerController player = hitPlayer.GetComponent<PlayerController> ();
					player.decreaseHealth (damage);
				} else {
					timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
					GameObject sceneObject = hit.transform.gameObject;
					if (sceneObject.name == "block") {
						Explodable _explo = sceneObject.GetComponent<Explodable> ();
						_explo.explode ();
					} else if (sceneObject.transform.Find ("block") != null) {
						GameObject impactObject = sceneObject.transform.Find ("block").gameObject;
						Explodable _explo = impactObject.GetComponent<Explodable> ();
						_explo.explode ();
					}
					soundManager.PlaySound("dig");
				}	
			}
		}
	}

	//Dirt Excavation Effect
	void Effect(){
		Transform clone = Instantiate (DirtDugPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
	}
	//EnableShoot for the chicken
	public void setEnabledShoot(bool enable){
		enabledAction = enable;
	}
}
