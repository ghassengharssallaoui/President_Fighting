using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public Vector3 traj;
    PlayerInfo infoScript;
    void Start()
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
    void Update()
    {
        float num;
        float y = traj.y;
        float x = traj.x;
        num = Mathf.Atan(Mathf.Abs(y) + 0.001f / (Mathf.Abs(x) + 0.00001f));
        float facingRotate;
        if (infoScript.traj.x > 0)
        {
            facingRotate = 1;
        }
        else
        {
            facingRotate = 0;
        }
        if (traj.y >= 0)
        {
            if (traj.x >= 0)
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, (num * 57.2f) - 90);
            }
            else
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, 90 - (num * 57.2f));
            }
        }
        else
        {
            if (traj.x >= 0)
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, 90 - (num * 57.2f) -180);
            }
            else
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, (num * 57.2f) + 90);
            }
        }
    }
}
