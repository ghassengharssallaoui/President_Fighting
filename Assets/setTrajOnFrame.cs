using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTrajOnFrame : MonoBehaviour
{
    PlayerInfo info;
    public Vector3 vector;
    public bool additive;
    public bool constant;
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
        Vector3 dummyVect = new Vector3(vector.x * info.facing, vector.y, 0);
        if (additive)
        {
            info.traj += dummyVect;
        }
        else
        {
            info.traj = dummyVect;
        }
    }
    void Update()
    {
        if (constant)
        {
            OnEnable();
        }
    }

}
