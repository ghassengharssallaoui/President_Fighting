using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirIdleAnimationLogic : MonoBehaviour
{
    PlayerInfo infoScript;
    GameObject godObject;
    public GameObject[] frames;
    public float maxSpeed;
    public float[] speedNums;
    int activeInt;
    public GameObject[] invalidFrames; //this is a list that it auto shutsoff for.
    // Start is called before the first frame update
    void OnEnable()
    {

        speedNums = new float[frames.Length];
        if (infoScript == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            infoScript = dummy.GetComponent<PlayerInfo>();
            godObject = dummy;
            //
        }
        if(maxSpeed == 0)
        {
            maxSpeed = infoScript.fallSpeed * 5;
        }
        for(int i = 0; i < speedNums.Length; i++)
        {
            float number = 2 * maxSpeed / speedNums.Length;
            if (i == 0)
            {
                speedNums[0] = maxSpeed - number;
            }
            else
            {
                speedNums[i] = speedNums[i - 1] - number;
            }
        }
        foreach (GameObject g in frames)
        {
            g.active = false;
        }
        FallingAnimate();
    }
    bool Deactivate()
    {
        foreach (GameObject g in frames)
        {
            if (g.active)
            {
                return false;
            }
        }
        return true;
    }
    void FallingAnimate()
    {
        if (godObject.transform.position.y > 0)
        {
            for (int i = 0; i < speedNums.Length; i++)
            {
                if (i == speedNums.Length - 1)
                {
                    if (frames[i].active == false)
                    {
                        foreach (GameObject g in frames)
                        {
                            g.active = false;
                        }
                        frames[i].active = true;
                    }
                }
                else if (infoScript.traj.y > speedNums[i])
                {
                    if (frames[i].active == false)
                    {
                        foreach (GameObject g in frames)
                        {
                            g.active = false;
                        }
                        frames[i].active = true;
                    }
                    break;
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject g in invalidFrames)
        {
            if (g.active)
            {
                gameObject.active = false;
            }
        }
        if (Deactivate())
        {
            gameObject.active = false;
        }
        else
        {
            FallingAnimate();
        }
    }
}
