using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facingEqualsTraj : MonoBehaviour
{
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
    }
        // Update is called once per frame
    void FixedUpdate()
    {
        if(infoScript.traj.x != 0)
        {
            if (infoScript.traj.x > 0)
            {
                infoScript.facing = -1;
            }

            else
            {
                infoScript.facing = -1;
            }
        }
    }
}
