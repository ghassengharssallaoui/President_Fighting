using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantTraj : MonoBehaviour
{
    PlayerInfo info;
    public Vector3 vector;
    public bool notAdditive;
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
    }
    void FixedUpdate()
    {
        Vector3 dummyVect = new Vector3(vector.x * info.facing, vector.y, 0);
        if (notAdditive)
        {
            info.traj = dummyVect;
        }
        else
        {
            info.traj += dummyVect;
        }
    }
}
