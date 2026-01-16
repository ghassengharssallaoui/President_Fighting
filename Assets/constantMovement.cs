using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constantMovement : MonoBehaviour
{
    public Vector3 traj;
    Vector3 startingPosition;
    public int timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (startingPosition == new Vector3(0, 0, 0))
        {
            startingPosition = transform.position;
        }
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        transform.position += traj;
    }
}
