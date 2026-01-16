using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnSuperFloat : MonoBehaviour
{
    PlayerInfo info;
    public int totalTime;
    public float minHeight;
    Vector3 startingVector;
    float step;
    int counter;
    // Start is called before the first frame update
    void OnEnable()
    {
        info = GetComponent<Options>().infoScript;
        if (info.transform.position.y > 0.01f)
        {
            startingVector = info.traj;
        }
        else
        {
            startingVector = new Vector3(0, 0, 0);
        }
        step = minHeight / totalTime;
        counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        info.traj = info.traj * 0.925f;
        if (info.transform.position.y + info.traj.y <= step * counter)
        {
            startingVector = new Vector3(startingVector.x, 0, 0);
            info.traj = new Vector3(info.traj.x, 0, 0);
            startingVector = new Vector3(startingVector.x, 0, 0);
            info.transform.position = new Vector3(info.transform.position.x, step * counter, 0);
        }
        if (counter < totalTime)
        {
            counter += 1;
        }
    }
    void OnDisable()
    {
        info.traj = startingVector;
    }
}
