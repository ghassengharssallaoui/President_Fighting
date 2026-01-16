using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnSuperRock : MonoBehaviour
{
    Vector3 traj;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        float size = Random.Range(0.5f, 3);
        transform.localScale = new Vector3(size, size, size);
        traj = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(1f, 2.5f), 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += traj;
        traj += new Vector3(0, -0.1f, 0);
    }
}
