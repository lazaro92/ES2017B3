using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{

    //Variables
    private float fireRate = 0; //FireRate is for how many bullets go when you press click (0 is for 1 bullet rate)
	private int damage = Globals.PISTOL_DAMAGE; //Damage is for the damage of the player that hit it.
    public LayerMask wantToHit; //Is the layers that we want to hit

    private bool enabledShoot = false;
    float timeToFire = 0;
    Transform firePoint;

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
        //If there is a sigle shot
        if (fireRate == 0)
        {
            //Check if fireButton is pressed
            if (Input.GetButtonDown("Fire1") && enabledShoot)
            {
                Shoot();
            }
        }
        //If firerate is diferent than 0 we do a burst shoot
        else
        {
            if (Input.GetButtonDown("Fire1") & Time.time > timeToFire && enabledShoot)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
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
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
			Debug.Log(hit.collider.name + " has been shot with damage of " + getDamageEqualDistance(hit)); //getDamageEqualDistance(hit)
			if (hit.collider.name == "Player(Clone)") {
				GameObject hitPlayer = hit.transform.gameObject;
				PlayerController player = hitPlayer.GetComponent<PlayerController> ();
				player.decreaseHealth (getDamageEqualDistance(hit));
			}
        }
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

    // setter for chicken
    public void setEnabledShoot(bool enable){
        enabledShoot = enable;
    }
}
