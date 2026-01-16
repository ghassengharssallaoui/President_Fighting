using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetWipe : MonoBehaviour
{
    public int timer;
    public float pauseTimer;
    // Start is called before the first frame update
    public Vector3 traj;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (startingPosition == new Vector3(0, 0, 0))
        {
            startingPosition = transform.position;
        }
        transform.position = startingPosition;
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        transform.position += traj;
        if(timer == 60)
        {
            transform.position = new Vector3(0, -43, 0);
            gameObject.active = false;
        }
    }
    void Update()
    {
        pauseTimer += (Time.deltaTime / Time.timeScale);
        if (Time.timeScale!= 1 && pauseTimer > 1f/60f)
        {
            FixedUpdate();
            pauseTimer = 0;
        }
    }
}
