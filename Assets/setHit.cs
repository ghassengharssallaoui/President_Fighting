using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setHit : MonoBehaviour
{
    PlayerInfo infoScript;
    GameObject godObject;
    public int value;
    public int frameDelay;
    public int counter;
    // Start is called before the first frame update
    void OnEnable()
    {
        counter = 0;
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        godObject = dummy;
        if (frameDelay == 0)
        {
            infoScript.hit = value;
        }
        if (value == 100)
        {
            infoScript.traj = new Vector3(0, 0, 0);
        }
    }
    void FixedUpdate()
    {
        counter += 1;
        if(frameDelay == counter)
        {
 
            infoScript.hit = value;
            if (value == 100)
            {
                infoScript.traj = new Vector3(0, 0, 0);
            }
        }
    }
}