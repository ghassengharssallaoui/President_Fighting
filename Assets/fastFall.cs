using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastFall : MonoBehaviour
{
    public GameObject[] fastFallAnims;
    PlayerInfo info;
    bool canFastFall;
    bool animationSafe;
    float fastFallSpeed;
    ReceiveInputs receiver;
    int upframes;
    void Start()
    {
        info = GetComponent<PlayerInfo>();
        if(fastFallSpeed == 0)
        {
            fastFallSpeed = info.jumpForce * 0.5f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animationSafe = false;
        foreach(GameObject g in fastFallAnims)
        {
            if(g.active == true)
            {
                animationSafe = true;
            }
        }
        if (transform.position.y > 0)
        {
            if (info.traj.y > 0)
            {
                canFastFall = true;
            }
            if (info.traj.y < 0.1f && info.receiver.moveVector.y < -0.7f && canFastFall && info.hit == 0 && animationSafe && upframes > 0)
            {
                canFastFall = false;
                info.traj += new Vector3(0, -fastFallSpeed, 0);
            }
            if(info.receiver.moveVector.y > -0.2)
            {
                upframes = 2;
            }
            else
            {
                upframes += -1;
            }
            
        }
        
    }
}
