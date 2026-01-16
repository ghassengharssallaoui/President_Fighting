using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateByTraj : MonoBehaviour
{
    PlayerInfo infoScript;
    // Start is called before the first frame update
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
        float y = infoScript.traj.y;
        float x = infoScript.traj.x;
        num = Mathf.Atan(Mathf.Abs(y) + 0.001f / (Mathf.Abs(x) + 0.00001f));
        if (infoScript.traj.y >= 0)
        {
            if (infoScript.traj.x >= 0)
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
            if (infoScript.traj.x >= 0)
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, 90 - (num * 57.2f) - 180);
            }
            else
            {
                infoScript.transform.eulerAngles = new Vector3(0, 0, (num * 57.2f) + 90);
            }
        }
    }

    void OnDisable()
    {
        infoScript.transform.eulerAngles = new Vector3(0, 0, 0);

    }
}
