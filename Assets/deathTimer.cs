using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTimer : MonoBehaviour
{
    public float frameCounter;
    public float lifespan;
    public int frameLifespan;
    public GameObject spawnOnDelete;
    bool replaced;

    // Start is called before the first frame update
    void Start()
    {
        if(lifespan == 0)
        {
            lifespan = 10;
        }
        frameCounter = 0;
        if(frameLifespan == 0)
        {
            frameLifespan = 1000;
        }
        frameLifespan += 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameLifespan == 1000)
        {
            frameCounter += 0.02f;
            if (frameCounter >= lifespan)
            {
                if (spawnOnDelete != null)
                {
                    if (replaced == true)
                    {
                        Destroy(gameObject);

                    }
                    else
                    {
                        GameObject dummy;
                        dummy = Instantiate(spawnOnDelete, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
                        dummy.transform.localScale = transform.localScale;
                        replaced = true;
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (frameLifespan <= 2)
            {
                if (spawnOnDelete != null)
                {
                    if (replaced == true)
                    {
                        Destroy(gameObject);

                    }
                    else
                    {
                        GameObject dummy;
                        dummy = Instantiate(spawnOnDelete, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
                        dummy.transform.localScale = transform.localScale;
                        replaced = true;
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            frameLifespan += -1;

        }
    }
}
