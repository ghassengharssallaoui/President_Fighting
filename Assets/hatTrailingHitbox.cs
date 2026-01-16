using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatTrailingHitbox : MonoBehaviour
{
    public projectileTraj traj;
    public int counter;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter += 1;
        if (counter > 1)
        {
            float xdir = traj.transform.localScale.x;
            float speed = traj.traj.x;
            transform.localScale = new Vector3(speed, transform.localScale.y, 1);
            transform.position = new Vector3(traj.transform.position.x + Mathf.Abs(transform.localScale.x) * -xdir * 0.5f, traj.transform.position.y, 0);
        }
    }
}
