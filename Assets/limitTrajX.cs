using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitTrajX : MonoBehaviour
{
    PlayerInfo info;
    public float limit;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (info == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
        }
        if(info.traj.x > limit)
        {
            info.traj = new Vector3(limit, 0, 0);
        }
        if(info.traj.x < -limit)
        {
            info.traj = new Vector3(-limit, 0, 0);
        }
    }
}
