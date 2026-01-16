using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpSquat : MonoBehaviour
{
    public Vector3 vector;
    public bool useBetterJumpSquat;
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (infoScript == null) {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }

            infoScript = dummy.GetComponent<PlayerInfo>();
        }
        if (vector.y == 0)
        {
            if (Mathf.Abs(infoScript.traj.x) < infoScript.airMax)
            {
                vector = new Vector3(infoScript.traj.x, infoScript.jumpForce, vector.z);
            }
            else
            {
                vector = new Vector3(infoScript.traj.x / Mathf.Abs(infoScript.traj.x) * infoScript.airMax, infoScript.jumpForce, vector.z);

            }
        }
    }
    void OnDisable()
    {
        if (useBetterJumpSquat == false)
        {
            infoScript.traj = vector;
        }
        vector = new Vector3(0, 0, 0);
    }
}
