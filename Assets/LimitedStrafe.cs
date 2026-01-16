using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedStrafe : MonoBehaviour
{
    public float speed;
    public float accel;
    PlayerInfo infoScript;
    ReceiveInputs inputs;
    // Update is called once per frame
    void OnEnable()
    {
        if (infoScript == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            infoScript = dummy.GetComponent<PlayerInfo>();
            inputs = infoScript.receiver;
        }
    }
    void FixedUpdate()
    {
        if (inputs.holding[3] > 1)
        {
            if (infoScript.traj.x + accel < speed)
            {
                infoScript.traj += new Vector3(accel, 0, 0);
            }
        }
        else if (inputs.holding[2] > 1)
        {
            if (infoScript.traj.x - accel > -speed)
            {
                infoScript.traj += new Vector3(-accel, 0, 0);
            }
        }
    }
}
