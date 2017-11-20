using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{

    //Variables
    public int rotationOffset = 90;

    private bool rotationEnabled = false;

	// Canviar color del braç
	private SpriteRenderer spr;

	//Start
	void Start(){
		spr = GetComponent<SpriteRenderer> ();
	}

    // Update is called once per frame
    void Update()
    {
        if (rotationEnabled){
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();

            //Find angle in degrees where arm is pointing.
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }    
    }
    public void setEnabledRotation(bool enabled){
        rotationEnabled = enabled;
    }

	public void flip(float h){
		// Vamos a la derecha
		if (h > 0.1f) {
			// Assignem nou vector
			transform.localScale = new Vector3 (1f, 1f, 1f);
		}

		// Vamos a la izq. i girem el personatge mirem la izq.
		if (h < -0.1f) {
			transform.localScale = new Vector3 (-1f, -1f, 1f);
		}
	}

	public void colorDamage(){
		Color color = new Color (236/255f, 137/255f, 137/255f);
		spr.color = color;
	}

	public void resetColor(){
		spr.color = Color.white;
	}

}
