using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTrajXToZero : MonoBehaviour
{
    PlayerInfo info;
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
        info.traj = new Vector3(0, info.traj.y, 0);

    }
}
