using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagController : MonoBehaviour {

	private int heightToDead;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        heightToDead = -12;
	}
	
	// Update is called once per frame
	void Update () {
		if (rb2d.position.y < heightToDead) {
			Destroy(this.gameObject);
		}
	}
}
