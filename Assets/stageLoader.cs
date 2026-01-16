using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageLoader : MonoBehaviour
{
    public GameObject[] stages;
    public int needsToReset; //this is the most annoyin
    public GameObject menu;
    // Start is called before the first frame update
    void OnEnable()
    {
        foreach(GameObject g in stages)
        {
            g.active = false;
        }
        stages[Random.Range(0, stages.Length)].active = true;
        needsToReset = 2;
    }
    void OnDisable()
    {
        foreach (GameObject g in stages)
        {
            g.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ///NOTE TO FUTURE XANDER, this is the most convoluted way to do this ever. If you EVER need to change this just give up and re do it.
        if (menu.active == false)
        {
            needsToReset = 0;
        }
        else
        {
            if(needsToReset == 0)
            {
                gameObject.active = false;
            }
            needsToReset += 1;
        }
    }
}
