using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableAfterWait : MonoBehaviour
{
    // Start is called before the first frame update
    public int waitFrames;
    int timer;
    public GameObject[] enable;
    public GameObject[] disable;
    public GameObject[] instant;
    public float updateTimer;
    // Start is called before the first frame update
    void OnEnable()
    {

        timer = 0;
    }
    void FixedUpdate()
    {
        if(waitFrames <= timer)
        {
            foreach (GameObject g in enable)
            {
                g.active = true;
            }
            foreach (GameObject g in disable)
            {
                g.active = false;
            }
            foreach (GameObject g in instant)
            {
                Instantiate(g);
            }
        }
        timer += 1;
        
    }
    void Update()
    {
        if (Time.timeScale == 0.00001f && Time.deltaTime < 0.0001f)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.00001f / 60f)
            {
                updateTimer += -0.00001f / 60f;
                FixedUpdate();
            }
        }
    }
}
