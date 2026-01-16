using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    public projectileTraj ptraj;
    int timer;
    public int rate;
    public Vector3 wobbleAngle;
    // Start is called before the first frame update
    void Start()
    {
        ptraj = GetComponent<projectileTraj>();
        ptraj.traj += new Vector3(wobbleAngle.x * 0.5f, wobbleAngle.y * 0.5f, 0); ;
    }
    void Wobble()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer % rate == 0 && timer!= 0)
        {
            wobbleAngle = new Vector3(wobbleAngle.x * -1, wobbleAngle.y * -1, 0); 
            ptraj.traj += wobbleAngle;
        }
        timer += 1;
    }
}
