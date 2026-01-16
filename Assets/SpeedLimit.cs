using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public PlayerInfo infoScript;
    public float limit;
    public GameObject lastAnim;
    public BasicGroundMovement bgm;
    public SpeedLimit partner;
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        Vector3 traj = infoScript.traj;
        if(traj.x > limit)
        {
            infoScript.traj = new Vector3(limit, traj.y, 0);
        }
        if(traj.x < -limit)
        {
            infoScript.traj = new Vector3(-limit, traj.y, 0);

        }
        if(traj.y > limit)
        {
            infoScript.traj = new Vector3(traj.x, limit, 0);
        }
        if (traj.y < -limit)
        {
            infoScript.traj = new Vector3(traj.x, -limit, 0);
        }
    }
    void OnDisable()
    {
        if (lastAnim != null)
        {
            if (lastAnim.name == "Run" || lastAnim.name == "Dash")
            {
                bgm.gameObject.active = true;
                bgm.dashing = true;
                infoScript.traj = new Vector3(0.4f * infoScript.facing, 0, 0);
            }
        }
    }
}
