using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followOffScreenObject : MonoBehaviour
{
    public GameObject followObject;
    bool ready;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ready == false)
        {
            if (followObject != null)
            {
                ready = true;
            }
        }
        else
        {
            if (followObject == null)
            {
                Destroy(gameObject);
            }
            else
            {
                if (followObject.active == false)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Vector3 vect = (followObject.transform.position - transform.position) / 10;
                    transform.position += vect;
                }
            }
        }
    }
}
