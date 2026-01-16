using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionOverTime : MonoBehaviour
{
    public int totalTimeInFrames;
    public GameObject next;
    int timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        if(timer == totalTimeInFrames)
        {
            next.active = true;
            gameObject.active = false; 
        }
    }
}
