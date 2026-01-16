using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateOnDisable : MonoBehaviour
{
    public GameObject[] activate;
    public GameObject[] deactivate;
    public GameObject[] instantiate;
    public bool dontDoWeirdScaleThing;//i have the scale in place for something else. idk why for
    void OnDisable()
    {
        foreach(GameObject g in activate)
        {
            g.active = true;
        }
        foreach(GameObject g in deactivate)
        {
            g.active = false;
        }
        foreach (GameObject g in instantiate)
        {
            GameObject dummy;
            dummy = Instantiate(g, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
            if (dontDoWeirdScaleThing == false)
            {
                dummy.transform.localScale = transform.localScale;
            }
        }
    }


}
