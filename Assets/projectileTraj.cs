using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTraj : MonoBehaviour
{
    public Vector3 traj;
    public Vector3 accel;
    public Vector3 constantTraj;
    // Start is called before the first frame update
    
    void Start()
    {
        if (constantTraj != new Vector3(0, 0, 0))
        {
            traj = constantTraj;
        }
        traj = new Vector3(traj.x * (transform.localScale.x/Mathf.Abs(transform.localScale.x)), traj.y, 0);
        accel = new Vector3(accel.x * (transform.localScale.x/Mathf.Abs(transform.localScale.x)), accel.y, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        traj += accel;
        transform.position += traj;    
    }
}
