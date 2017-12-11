using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {

	//Variables
	private float fireRate = 0; //FireRate is for how many bullets go when you press click (0 is for 1 bullet rate)
	public float throwForce = Globals.GRENADE_THROW_FORCE;
	public GameObject grenadePrefab;

	private bool enabledShoot = false;
	Transform firePoint;

	//Ammo
	private bool infiniteAmmo = !Globals.limitedAmmo; //OJO Negado
	private int magazine = Globals.GRENADE_THROW_AMMO;

	// Use this for initialization
	void Awake()
	{
		firePoint = transform.Find("FirePoint"); //Bind GameObject with variable
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
		if (Input.GetButtonDown ("Fire1") && enabledShoot) {
			if (magazine > 0 && Globals.remainingGrenades > 0) {
				ThrowGranade (throwForce);
				magazine--;
				Globals.remainingGrenades--;

				if (Globals.remainingGrenades == 0)
					Globals.skipTurn = true;

				if (infiniteAmmo)
					magazine++;

			} else {
				//Debug.Log ("There are no grenades in magazine or the turn's shots are overrr");
				soundManager.PlaySound ("dry"); 
			}
		} else {
			if (Input.GetButtonDown("Fire2") && enabledShoot)
			{
				if (magazine > 0 && Globals.remainingGrenades > 0) {
					ThrowGranade (0);
					magazine--;
					Globals.remainingGrenades--;

					if (Globals.remainingGrenades == 0)
						Globals.skipTurn = true;

					if (infiniteAmmo) 
						magazine++;

				} else {
					//Debug.Log("There are no grenades in magazine or the turn's shots are overrr");
					soundManager.PlaySound("dry"); 
				}
			}
		}
	}
		
	void ThrowGranade(float throwForce )
	{
		//Debug.Log ("Tiro granada desde pistola");
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		GameObject grenade = Instantiate (grenadePrefab,firePointPosition,transform.rotation);
		//Camera
		//Assign last Gameobject to camera
		CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
		cam.setPreviousObject (cam.getFollower ());
		cam.setFollower (grenade);
		Rigidbody2D rb = grenade.GetComponent<Rigidbody2D> ();
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		rb.velocity = (mousePosition - firePointPosition) * throwForce;
	}

	//EnableShoot for the chicken
	public void setEnabledShoot(bool enable){
		enabledShoot = enable;
	}

	public int getMagazine(){
		return magazine;
	}

	public bool getInfiniteAmmo(){
		return infiniteAmmo;
	}
}
