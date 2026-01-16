using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableOnPause : MonoBehaviour
{
    public float delay;
    float startingDelay;
    // Start is called before the first frame update
    public GameObject[] enable;
    public GameObject[] disable;
    public SpriteRenderer ObamaSprite;

    // Update is called once per frame
    void OnEnable()
    {
        if(startingDelay == 0)
        {
            startingDelay = delay;
        }
        delay = startingDelay;
    }
    void Update()
    {
        if (Time.timeScale == 0.00001f && Time.deltaTime < 0.00001f)
        {
            delay += -Time.deltaTime * 100000;
        }
        if(delay <= 0)
        {
            foreach(GameObject g in enable)
            {
                g.active = true;
            }
            foreach (GameObject g in disable)
            {
                g.active = false;
            }
            if(ObamaSprite != null)
            {
                ObamaSprite.enabled = false;

            }

        }
    }
}
