using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionOnGround : MonoBehaviour
{
    //the whole point of this script is to not complicate the options script for adding grounding while hit
    public GameObject ground;
    void FixedUpdate()
    {
        if(transform.position.y == 0)
        {
            ground.active = true;
            gameObject.active = false;
        }
    }
}
