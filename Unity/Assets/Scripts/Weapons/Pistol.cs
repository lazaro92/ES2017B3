using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour{

    //Variables
    private float fireRate = 0; //FireRate is for how many bullets go when you press click (0 is for 1 bullet rate)
	private int damage = Globals.PISTOL_DAMAGE; //Damage is for the damage of the player that hit it.
    public LayerMask wantToHit; //Is the layers that we want to hit

	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

    private bool enabledShoot = false;
    float timeToFire = 0;
    Transform firePoint;

	//Ammo
	private bool infiniteAmmo = !Globals.limitedAmmo; //OJO Negado
	private int magazine = Globals.PISTOL_AMMO;

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
		if (Input.GetButtonDown("Fire1") && enabledShoot)
		{
			if (magazine > 0 && Globals.remainingShots > 0) {
				Shoot ();

				magazine--;
				Globals.remainingShots--;

				if (Globals.remainingShots == 0)
					Globals.skipTurn = true;

				if (infiniteAmmo) 
					magazine++;

			} else {
				Debug.Log("There are no bullets in magazine or the turn's shots are overrr");
				soundManager.PlaySound("dry"); 
			}
		}
    }

    //Function Shoot
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, wantToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.blue);
        if (hit.collider != null)
        {
			if (Time.time >= timeToSpawnEffect){
				Effect();
				timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
	            Debug.DrawLine(firePointPosition, hit.point, Color.red);
				Debug.Log(hit.collider.name + " has been shot with damage of " + getDamageEqualDistance(hit)); //getDamageEqualDistance(hit)
				if (hit.collider.name == "Player(Clone)") {
					GameObject hitPlayer = hit.transform.gameObject;
					PlayerController player = hitPlayer.GetComponent<PlayerController> ();
					player.decreaseHealth (getDamageEqualDistance(hit));
				}
			}
        }
        soundManager.PlaySound("shoot");
    }

    //Function to define less damage when distance is longer.
    int getDamageEqualDistance(RaycastHit2D hit)
    {
        float finalDamage;

        float euclidDistance = Mathf.Sqrt(Mathf.Pow(firePoint.position.x - hit.point.x, 2.0f) + Mathf.Pow(firePoint.position.y - hit.point.y, 2.0f));
        //Debug.Log(euclidDistance);
		finalDamage = damage - 0.07f * euclidDistance;

		return (int) finalDamage;
    }

	//Flash effect
	void Effect(){
		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
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
