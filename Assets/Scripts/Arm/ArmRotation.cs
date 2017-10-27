using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{

    //Variables
    public int rotationOffset = 90;

    private bool rotationEnabled = false;


    // Update is called once per frame
    void Update()
    {
        if (rotationEnabled){
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();

            //Fin angle in degrees where arm is pointing.
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }    
    }
    public void setEnabledRotation(bool enabled){
        rotationEnabled = enabled;
    }
}
