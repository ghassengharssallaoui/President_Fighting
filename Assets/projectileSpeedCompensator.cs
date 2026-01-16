using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileSpeedCompensator : MonoBehaviour
{
    public projectileTraj projScript;
    public float xDisplacement;
    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.transform.parent != null && dummy.GetComponent<projectileTraj>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        if (dummy.GetComponent<projectileTraj>() != null)
        {
            projScript = dummy.GetComponent<projectileTraj>();
            transform.position = transform.parent.transform.position + dummy.GetComponent<projectileTraj>().traj * 3.5f;
        }
    }
}
//You need to add a thing to make Scalar happen to hurtboxes as well.
//Then you need to fix the bandaid solution for this script (where we * traj by 3.5f
//Then you need to add the stuff to all projectiles (and don't forget to make obama's hitbox a projectile
//and fix abes hurtboxes you lazy fuck